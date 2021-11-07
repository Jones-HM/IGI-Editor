using Microsoft.VisualBasic;
using Newtonsoft.Json;
using QLibc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using UXLib.UX;
using static IGIEditor.QUtils;

namespace IGIEditor
{
    public partial class IGIEditorUI : Form
    {
        bool compileStatus = true;
        List<QUtils.QTask> qtaskList = new List<QUtils.QTask>();
        int buildingsCount = 0, rigidObjCount = 0;
        //Variables section.
        static bool gameFound = false, isBuildingDD = false, isObjectDD = true;
        static string model3d, blenderModel;
        private static int randPosOffset = 0, posOffset = 3000;
        string inputQvmPath, inputQscPath;
        private static int gameLevel = 1;

        static internal IGIEditorUI editorRef;

        //Main-Start - Ctr.
        public IGIEditorUI()
        {
            var editorPosTimer = new System.Windows.Forms.Timer();
            var fileCheckerTimer = new System.Windows.Forms.Timer();

            InitializeComponent();
            UXWorker formMover = new UXWorker();
            formMover.CustomFormMover(formMoverPnl, this);
            editorRef = this;

            //Start Position timer.
            editorPosTimer.Tick += new EventHandler(UpdatePositionTimer);
            editorPosTimer.Interval = 500;
            editorPosTimer.Start();

            //Start File Integrity timer.
            fileCheckerTimer.Tick += new EventHandler(FileIntegrityCheckerTimer);
            fileCheckerTimer.Interval = 120000;
            fileCheckerTimer.Start();

            //Disabling Errors and Warnings.
            GT.GT_SuppressErrors(true);
            GT.GT_SuppressWarnings(true);

            gameLevel = Convert.ToInt32(levelStartTxt.Text.ToString());

            InitApp();
            InitPaths(gameLevel);

            //FileInegrity.GenerateDirHashes(new List<string> { QUtils.cfgAiPath, QUtils.cfgQFilesPath, QUtils.cfgVoidPath });
            //QUtils.ShowInfo("Hashes generated");

            if (QMemory.FindGame())
            {
                gameFound = true;
                gameLevel = QUtils.gGameLevel = QMemory.GetCurrentLevel();
                if (gameLevel < 0 || gameLevel > GAME_MAX_LEVEL) gameLevel = 1;
                QUtils.InjectDllOnStart();
            }
            else
            {
                QMemory.StartLevel(gameLevel, true);
                gameFound = true;
                Thread.Sleep(3000);
                QUtils.RestoreLevel(gameLevel);
                QUtils.ResetFile(gameLevel);
                GenerateAIScriptId(gameLevel);
                QUtils.aiScriptFiles.Clear();
                Thread.Sleep(5000);
                var qscData = QMisc.RemoveCutscene(QUtils.cfgQscPath + gameLevel + "\\" + QUtils.objectsQsc, gameLevel);
                if (!String.IsNullOrEmpty(qscData))
                    QCompiler.CompileEx(qscData);
                QUtils.InjectDllOnStart();
            }

            var inputQscPath = QUtils.cfgQscPath + gameLevel + "\\" + QUtils.objectsQsc;
            QUtils.graphsPath = QUtils.cfgGamePath + gameLevel + "\\" + "graphs";

            if (!File.Exists(QUtils.objectsQsc))
            {
                File.Copy(inputQscPath, QUtils.objectsQsc);

                //Decrypt data onLoad.
                QCryptor.Decrypt(QUtils.objectsQsc, null);
            }

            if (gameReset) QUtils.ResetFile(gameLevel);

            if (appLogs)
            {
                QUtils.EnableLogs();
                GT.GT_EnableLogs();
            }

            //Get current level selected.
            if (gameFound)
            {
                gameLevel = QMemory.GetCurrentLevel();
                if (gameLevel < 0 || gameLevel > GAME_MAX_LEVEL) gameLevel = 1;
                levelStartTxt.Text = gameLevel.ToString();
                SetStatusText("Game running...");
                LoadLevelDetails(gameLevel);
                //QMemory.UpdateHumanHealth(false);
            }
            else
                SetStatusText("Game not running...");

            //Init Dropdown,List UI components.
            Thread.Sleep(3000);
            InitUIComponents(gameLevel);
        }


        private void FileIntegrityCheckerTimer(object sender, EventArgs e)
        {
            FileInegrity.RunFileInegrityCheck(null, new List<string> { QUtils.cfgAiPath, QUtils.cfgQFilesPath, QUtils.cfgVoidPath });
        }

        private void UpdatePositionTimer(object sender, EventArgs e)
        {
            if (gameFound)
            {
                if (posMetersCb.Checked)
                {
                    var meterPos = QHuman.GetPositionInMeter(false);
                    xPosLbl.Text = meterPos.x.ToString();
                    yPosLbl.Text = meterPos.y.ToString();
                    zPosLbl.Text = meterPos.z.ToString();
                }
                else if (posCoordCb.Checked)
                {
                    var realPos = QHuman.GetPositionCoord(false);
                    xPosLbl.Text = realPos.alpha.ToString("0.0000");
                    yPosLbl.Text = realPos.beta.ToString("0.0000");
                    zPosLbl.Text = realPos.gamma.ToString("0.0000");
                }
            }
        }

        private void InitUIComponents(int level, bool initialInit = true)
        {
            //Init Weapons list.
            weaponSelectDD.Items.Clear();
            weaponAiDD.Items.Clear();
            QUtils.weaponList = QHuman.GetWeaponsList();
            foreach (var weapon in QUtils.weaponList)
            {
                var weaponName = weapon.Keys.ElementAt(0);
                weaponSelectDD.Items.Add(weaponName);
                weaponAiDD.Items.Add(weaponName);
            }

            //Init AI model list.
            QUtils.aiModelsListStr.Clear();
            var aiModelNamesList = QAI.GetAiModelNamesList(level);
            foreach (var aiModelName in aiModelNamesList)
            {
                aiModelsListStr.Add(aiModelName);
                if (initialInit)
                    aiModelSelectDD.Items.Add(aiModelName);
            }

            //Init AI types list.
            aiTypeDD.Items.Clear();
            var aiTypesList = QAI.GetAiTypes();
            foreach (var aiType in aiTypesList)
            {
                aiTypeDD.Items.Add(aiType.Replace("AITYPE_", String.Empty).Replace("_AK", String.Empty).Replace("_UZI", String.Empty));
            }

            //Init AI Graph list.
            QUtils.aiGraphIdStr.Clear();
            var graphIdList = QGraphs.GetGraphIds(level);
            foreach (var graphId in graphIdList)
            {
                var nodesList = QGraphs.GetAllNodes4mGraph(graphId);
                //Remove Cutscenes Graph.
                if (nodesList != null)
                {
                    if (nodesList.Count > 1)
                    {
                        aiGraphIdStr.Add(graphId);
                        if (initialInit) aiGraphIdDD.Items.Add(graphId);
                    }
                }
            }

            //Init Buildings list.
            QUtils.buildingListStr.Clear();
            QUtils.buildingList = QObjects.GetObjectList(level, QTYPES.BUILDING, true, true);
            foreach (var building in QUtils.buildingList)
            {
                QUtils.buildingListStr.Add(building.Keys.ElementAt(0));
                if (initialInit)
                    buildingSelectDD.Items.Add(building.Keys.ElementAt(0));
            }

            //Init 3D-Rigid Objects list.
            QUtils.objectRigidListStr.Clear();
            QUtils.objectRigidList = QObjects.GetObjectList(level, QTYPES.RIGID_OBJ, true, true);
            foreach (var rigidObj in QUtils.objectRigidList)
            {
                QUtils.objectRigidListStr.Add(rigidObj.Keys.ElementAt(0));
                if (initialInit)
                {
                    objectSelectDD.Items.Add(rigidObj.Keys.ElementAt(0));
                }
            }

            buildingSelectDD.SelectedIndex = objectSelectDD.SelectedIndex = 0;
        }

        private void InitApp()
        {
            //Read paths from config.
            QUtils.ParseConfig();

            //Initialize app data for QEditor.
            QUtils.InitAppData();

            //Initialize QCompiler setting and modules.
            QUtils.InitQCompiler();

            string userName = QUtils.GetCurrentUserName();
            string keyFileAbsPath = QUtils.igiEditorQEdPath + Path.DirectorySeparatorChar + QUtils.projAppName + "Key.txt";
            string deviceKey = QUtils.GetMachineDeviceId();
            string inputKey = null, welcomeMsg = "Welcome " + userName + " to IGI 1 Editor";

            //Setting Options.
            appLogsCb.Checked = appLogs;
            autoResetCb.Checked = gameReset;
            connectionCb.Checked = isEditorOnline;

            bool initUser;
            //Check if user key exist.
            keyFileExist = File.Exists(keyFileAbsPath);

            //Initialize user info - Offline.
            if (!isEditorOnline) initUser = true;

            //Initialize user info - Online.
            else initUser = QUtils.InitUserInfo();

            if (initUser)
            {
                headerLbl.Text += " " + userName;
                SetStatusText(welcomeMsg);
                AddLog(welcomeMsg);
            }

            else
            {
                QUtils.ShowSystemFatalError("Error occurred while initializing user data. (Error: SERVER_ERR)");
            }

            QUtils.AddLog("Device Id Exist : " + QUtils.keyExist);

            //Check if Machine key is already saved.
            if (QUtils.keyFileExist)
            {
                inputKey = File.ReadAllText(keyFileAbsPath);
            }

            if (!isEditorOnline) QUtils.keyExist = (inputKey == deviceKey);

            if (QUtils.keyFileExist && !QUtils.keyExist)
            {
                QUtils.AddLog("KeyFileExist : " + QUtils.keyFileExist + "\nkeyExist : " + QUtils.keyExist);
                QUtils.ShowSystemFatalError("No such user found in our database, ERROR_NO_USER_FOUND (0x525).");
            }
            else
            {
                if (String.IsNullOrEmpty(inputKey))
                {
                    inputKey = Interaction.InputBox(welcomeMsg, "IGI 1 Editor Key", "Enter your personal IGI 1 Editor Key here", 50, 40);

                    if (deviceKey != inputKey)
                    {
                        QUtils.ShowSystemFatalError("Wrong key encountered! ERROR_INVALID_KEY_USAGE (Error : 0x0000000C)");
                    }
                    else
                    {
                        var dlgResult = QUtils.ShowDialog("Do you want to remember this key for your machine ?");
                        if (dlgResult == DialogResult.Yes)
                        {
                            QUtils.SaveFile(keyFileAbsPath, deviceKey);
                        }
                    }
                }
            }

            //Genrate random scriptId according to Level A.I.
            GenerateAIScriptId(QUtils.gGameLevel);

            string game_path_tmp = QUtils.cfgGamePath;
            var game_abs_path = game_path_tmp.Slice(0, game_path_tmp.IndexOf("\\", game_path_tmp.IndexOf("\\") + 1));

            //Move Humanplayer config file for Weapon ammo limitations.
            var humanplayer_abs_path = game_abs_path + Path.DirectorySeparatorChar + QUtils.humanplayerPath + Path.DirectorySeparatorChar + QUtils.humanplayer;
            if (File.Exists(QUtils.humanplayer) && !File.Exists(humanplayer_abs_path))
                File.Move(Path.GetFullPath(QUtils.humanplayer), humanplayer_abs_path);

            //Creates shortcut of game in current directory.
            QUtils.CreateGameShortcut();

        }

        internal void SetStatusText(string text)
        {
            statusLbl.Text = null;
            statusLbl.Text = text;
        }

        private static void ParseConfigProperty(string configPath, ref bool propertyName, string keyword)
        {
            if (configPath.Trim().Contains("true")) propertyName = true;
            else if (configPath.Trim().Contains("false")) propertyName = false;
            else QUtils.ShowConfigError(keyword);
        }

        private static HumanAi ReadHumanAiConfig(string fileName)
        {
            HumanAi humanAi = new HumanAi();
            try
            {
                humanAi = JsonConvert.DeserializeObject<HumanAi>(File.ReadAllText(fileName));
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Additional text encountered"))
                    QUtils.ShowError("Error occurred while reading HumanAI data from JSON file");
                else
                    QUtils.ShowError(ex.Message);
            }
            return humanAi;
        }


        private void InitPaths(int gameLevel)
        {
            QUtils.gamePath = QUtils.cfgGamePath + gameLevel;
            inputQvmPath = QUtils.cfgQvmPath + gameLevel + "\\" + QUtils.objectsQvm;
            inputQscPath = QUtils.cfgQscPath + gameLevel + "\\" + QUtils.objectsQsc;
            QUtils.graphsPath = QUtils.cfgGamePath + gameLevel + "\\" + "graphs";
            QUtils.gGameLevel = gameLevel;
        }

        public void LoadLevelDetails(int level)
        {
            //load level Description.
            levelNameLbl.Text = QMission.GetMissionInfo(level);
            var imgPath = "mission_" + level + QUtils.jpgExt;
            var imgTmpPath = QUtils.cachePathAppImages + "\\" + imgPath;

            //Load level image from Cache.
            if (File.Exists(imgTmpPath))
            {
                using (var bmpTemp = new Bitmap(imgTmpPath))
                {
                    levelImgBox.Image = new Bitmap(bmpTemp);
                }
            }

            //Load level image from Web.
            else
            {
                var imgUrl = baseImgUrl + levelImgUrl[level - 1];
                QUtils.WebDownload(imgUrl, imgPath);
                LoadImgBoxWeb(imgUrl, levelImgBox);
            }
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void addWeaponBtn_Click(object sender, EventArgs e)
        {
            var weaponModel = QUtils.weaponList[weaponSelectDD.SelectedIndex].Values.ElementAt(0);
            var weaponName = QUtils.weaponList[weaponSelectDD.SelectedIndex].Keys.ElementAt(0);
            string qscData = null;

            int weaponAmmo = 999;
            if (!String.IsNullOrEmpty(weaponAmmoTxt.Text))
                weaponAmmo = Convert.ToInt32(weaponAmmoTxt.Text);

            qscData = QHuman.AddWeapon(weaponModel, weaponAmmo, true);
            if (!String.IsNullOrEmpty(qscData))
                compileStatus = QCompiler.Compile(qscData, QUtils.gamePath, false, false);

            if (compileStatus)
                SetStatusText("Weapon " + weaponName + " added successfully");
        }

        private void addBuildingBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var buildingName = QUtils.buildingList[buildingSelectDD.SelectedIndex].Keys.ElementAt(0);
                var buildingModel = QUtils.buildingList[buildingSelectDD.SelectedIndex].Values.ElementAt(0);
                var buildingPos = QHuman.GetPositionInMeter();
                bool hasOrientation = String.IsNullOrEmpty(alphaTxt.Text) && String.IsNullOrEmpty(betaTxt.Text) && String.IsNullOrEmpty(gammaTxt.Text);
                AddObject(buildingModel, false, buildingPos, !hasOrientation);

                if (compileStatus)
                    SetStatusText("Buildiing " + buildingName + " added successfully");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Index was out of range"))
                {
                    QUtils.ShowError("No Building or Object have been selected to perfom this operation");
                }
                else
                    QUtils.ShowError(ex.Message ?? ex.StackTrace);
            }
        }

        private void removeBuildingBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var buildingModel = QUtils.buildingList[buildingSelectDD.SelectedIndex].Values.ElementAt(0);
                var buildingName = QUtils.buildingList[buildingSelectDD.SelectedIndex].Keys.ElementAt(0);

                if (String.IsNullOrEmpty(buildingModel)) return;
                var qscData = QUtils.LoadFile();
                qscData = QObjects.RemoveObject(qscData, buildingModel, true, false);
                compileStatus = QCompiler.CompileEx(qscData);
                if (compileStatus)
                    SetStatusText("Buildiing " + buildingName + " removed successfully");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Index was out of range"))
                {
                    QUtils.ShowError("No Building or Object have been selected to perfom this operation");
                }
                else
                    QUtils.ShowError(ex.Message ?? ex.StackTrace);
            }
        }


        private void addObjectBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var objectRigidModel = QUtils.objectRigidList[objectSelectDD.SelectedIndex].Values.ElementAt(0);
                var objectRigidName = QUtils.objectRigidList[objectSelectDD.SelectedIndex].Keys.ElementAt(0);
                var objectPos = QHuman.GetPositionInMeter();
                bool hasOrientation = String.IsNullOrEmpty(alphaTxt.Text) && String.IsNullOrEmpty(betaTxt.Text) && String.IsNullOrEmpty(gammaTxt.Text);
                AddObject(objectRigidModel, true, objectPos, !hasOrientation);

                if (compileStatus)
                    SetStatusText("Object " + objectRigidName + " added successfully");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Index was out of range"))
                {
                    QUtils.ShowError("No Building or Object have been selected to perfom this operation");
                }
                else
                    QUtils.ShowError(ex.Message ?? ex.StackTrace);
            }
        }

        private void removeObjectBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var objectRigidModel = QUtils.objectRigidList[objectSelectDD.SelectedIndex].Values.ElementAt(0);
                var objectRigidName = QUtils.objectRigidList[objectSelectDD.SelectedIndex].Keys.ElementAt(0);

                if (String.IsNullOrEmpty(objectRigidModel)) return;
                var qscData = QUtils.LoadFile();
                qscData = QObjects.RemoveObject(qscData, objectRigidModel, true, false);
                compileStatus = QCompiler.CompileEx(qscData);
                if (compileStatus)
                    SetStatusText("Object " + objectRigidName + " removed successfully");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Index was out of range"))
                {
                    QUtils.ShowError("No Building or Object have been selected to perfom this operation");
                }
                else
                    QUtils.ShowError(ex.Message ?? ex.StackTrace);
            }
        }

        private void minimizeBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void closeBtn_MouseMove(object sender, MouseEventArgs e)
        {
            closeBtn.BackColor = Color.Tomato;
            closeBtn.ForeColor = Color.Transparent;
        }

        private void closeBtn_MouseLeave(object sender, EventArgs e)
        {
            closeBtn.BackColor = Color.Transparent;
            closeBtn.ForeColor = Color.Tomato;
        }

        private void resetCurrentLevelBtn_Click(object sender, EventArgs e)
        {
            int level = QMemory.GetCurrentLevel();
            if (level < 0 || level > 0xE) level = 1;
            QUtils.RestoreLevel(level);
            QUtils.ResetFile(level);
            QMemory.RestartLevel(true);
            GenerateAIScriptId(level);
        }

        private void resetAllLevelsBtn_Click(object sender, EventArgs e)
        {
            for (int level = 1; level <= 14; ++level)
            {
                inputQvmPath = QUtils.cfgQvmPath + level + "\\" + QUtils.objectsQvm;
                QUtils.RestoreLevel(level);
            }
            QMemory.RestartLevel(true);
        }

        private void xPosLbl_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(xPosLbl.Text);
            SetStatusText("X-Position Copied successfully");
        }

        private void yPosLbl_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(yPosLbl.Text);
            SetStatusText("Y-Position Copied successfully");
        }

        private void zPosLbl_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(zPosLbl.Text);
            SetStatusText("Z-Position Copied successfully");
        }

        private void posCoordCb_CheckedChanged(object sender, EventArgs e)
        {
            if (posCoordCb.Checked) posMetersCb.Checked = false;
        }

        private void posMetersCb_CheckedChanged(object sender, EventArgs e)
        {
            if (posMetersCb.Checked) posCoordCb.Checked = false;
        }

        private void posOffCb_CheckedChanged(object sender, EventArgs e)
        {
            if (posOffCb.Checked) posMeterCb.Checked = posCurrentCb.Checked = false;
        }

        private void posMeterCb_CheckedChanged(object sender, EventArgs e)
        {
            if (posMeterCb.Checked) posOffCb.Checked = posCurrentCb.Checked = false;
        }

        private void posCurrentCb_CheckedChanged(object sender, EventArgs e)
        {
            if (posCurrentCb.Checked) posOffCb.Checked = posMeterCb.Checked = false;
        }

        private void refreshGame_Click(object sender, EventArgs e)
        {
            gameFound = QMemory.FindGame();
            if (!gameFound)
            {
                QMemory.StartGame();
                SetStatusText("Game not found! starting new game");
                Thread.Sleep(10000);

                QUtils.gGameLevel = gameLevel = QMemory.GetCurrentLevel();
                levelStartTxt.Text = gameLevel.ToString();
                InitPaths(QUtils.gGameLevel);
                gameFound = true;
            }
            else
                SetStatusText("Game found success");
            if (gameFound)
            {
                Thread.Sleep(1000);
                QUtils.gGameLevel = gameLevel = QMemory.GetCurrentLevel();
                LoadLevelDetails(gameLevel);
                RefreshUIComponents(gameLevel);
                InitPaths(gameLevel);
                QUtils.ResetFile(gameLevel);
                QUtils.InjectDllOnStart();
            }
        }

        private void updateObjPosition_Click(object sender, EventArgs e)
        {
            try
            {
                var buildingModel = QUtils.buildingList[buildingPosDD.SelectedIndex].Values.ElementAt(0);
                var buildingName = QUtils.buildingList[buildingPosDD.SelectedIndex].Keys.ElementAt(0);

                var objectRigidModel = QUtils.objectRigidList[objectPosDD.SelectedIndex].Values.ElementAt(0);
                var objectRigidName = QUtils.objectRigidList[objectPosDD.SelectedIndex].Keys.ElementAt(0);

                string model = isBuildingDD ? buildingModel : objectRigidModel;

                Double xpos, ypos, zpos;
                string qscData = null;

                if (posCurrentCb.Checked)
                {
                    var meterPos = QHuman.GetPositionInMeter();

                    xPosTxt_O.Text = meterPos.x.ToString();
                    yPosTxt_O.Text = meterPos.y.ToString();
                    zPosTxt_O.Text = meterPos.z.ToString();
                }

                xpos = Convert.ToDouble(xPosTxt_O.Text);
                ypos = Convert.ToDouble(yPosTxt_O.Text);
                zpos = Convert.ToDouble(zPosTxt_O.Text);

                var objectPos = new Real64(xpos, ypos, zpos);
                if (posOffCb.Checked)
                    qscData = QObjects.UpdatePositionOffset(model, ref objectPos);
                else if (posMeterCb.Checked)
                    qscData = QObjects.UpdatePositionMeter(model, ref objectPos);
                else if (posCurrentCb.Checked)
                    qscData = QObjects.UpdatePositionMeter(model, ref objectPos);

                if (!String.IsNullOrEmpty(qscData))
                    compileStatus = QCompiler.Compile(qscData, QUtils.gamePath, false, true, true);

                if (compileStatus)
                {
                    string statusText = (isBuildingDD ? (buildingName) : (objectRigidName)) + " updated successfully";
                    SetStatusText(statusText);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Index was out of range"))
                    QUtils.ShowError("No building or object have been selected to perfom this operation");
                else if (ex.Message.Contains("not in a correct format"))
                    QUtils.ShowError("Position parameters are empty or invalid");
                else
                    QUtils.ShowError(ex.Message ?? ex.StackTrace);
            }
        }

        private void updateObjOrientation_Click(object sender, EventArgs e)
        {
            try
            {
                var buildingModel = QUtils.buildingList[buildingPosDD.SelectedIndex].Values.ElementAt(0);
                var buildingName = QUtils.buildingList[buildingPosDD.SelectedIndex].Keys.ElementAt(0);

                var objectRigidModel = QUtils.objectRigidList[objectPosDD.SelectedIndex].Values.ElementAt(0);
                var objectRigidName = QUtils.objectRigidList[objectPosDD.SelectedIndex].Keys.ElementAt(0);

                string model = isBuildingDD ? buildingModel : objectRigidModel;

                float alpha, beta, gamma;
                alpha = float.Parse(alphaTxt.Text);
                beta = float.Parse(betaTxt.Text);
                gamma = float.Parse(gammaTxt.Text);
                string qscData = null;

                var orientation = new Real32(alpha, beta, gamma);
                qscData = QObjects.UpdateOrientation(model, ref orientation);

                if (!String.IsNullOrEmpty(qscData))
                    compileStatus = QCompiler.Compile(qscData, QUtils.gamePath, false, true, true);

                if (compileStatus)
                {
                    string statusText = (isBuildingDD ? (buildingName) : (objectRigidName)) + " orientation updated successfully";
                    SetStatusText(statusText);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Index was out of range"))
                    QUtils.ShowError("No Building or Object have been selected to perfom this operation");
                else if (ex.Message.Contains("not in a correct format"))
                    QUtils.ShowError("Position parameters are empty or invalid");
                else
                    QUtils.ShowError(ex.Message ?? ex.StackTrace);
            }
        }

        private void humanPosOffCb_CheckedChanged(object sender, EventArgs e)
        {
            if (humanPosOffCb.Checked) humanPosMeterCb.Checked = false;
        }

        private void humanPosMeterCb_CheckedChanged(object sender, EventArgs e)
        {
            if (humanPosMeterCb.Checked) humanPosOffCb.Checked = false;
        }

        private void updateHumaPosition_Click(object sender, EventArgs e)
        {
            try
            {
                Double xpos, ypos, zpos;
                string qscData = null;

                xpos = Convert.ToDouble(xPosTxt_H.Text);
                ypos = Convert.ToDouble(yPosTxt_H.Text);
                zpos = Convert.ToDouble(zPosTxt_H.Text);

                var humanPos = new Real64(xpos, ypos, zpos);
                if (humanPosOffCb.Checked)
                    qscData = QHuman.UpdatePositionOffset(humanPos);

                else if (humanPosMeterCb.Checked)
                {
                    qscData = QHuman.UpdatePositionInMeter(humanPos);
                }

                if (!string.IsNullOrEmpty(qscData))
                    compileStatus = QCompiler.Compile(qscData, QUtils.gamePath, false, true, false);

                if (compileStatus)
                    SetStatusText("Human position updated successfully");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("not in a correct format"))
                    QUtils.ShowError("Position parameters are empty or invalid");
                else
                    QUtils.ShowError(ex.Message ?? ex.StackTrace);
            }
        }

        private void weaponSelectDD_SelectedValueChanged(object sender, EventArgs e)
        {
            PopulateWeaponDD(weaponSelectDD.SelectedIndex, weaponImgBox);
        }

        private void PopulateWeaponDD(int index, PictureBox imgBox)
        {
            string weaponModel = null, weaponName = null, imgUrl = null, imgPath = null;
            try
            {
                weaponModel = QUtils.weaponList[index].Values.ElementAt(0);
                weaponName = QUtils.weaponList[index].Keys.ElementAt(0);

                //Weapon image paths.
                imgUrl = baseImgUrl + weaponsImgUrl[index];
                imgPath = weaponName + QUtils.jpgExt;
                var imgTmpPath = QUtils.cachePathAppImages + "\\" + imgPath;

                //Load image from Cache.
                if (File.Exists(imgTmpPath))
                {
                    using (var bmpTemp = new Bitmap(imgTmpPath))
                    {
                        imgBox.Image = new Bitmap(bmpTemp);
                    }
                }

                //Load image from Web.
                else
                {
                    LoadImgBoxWeb(imgUrl, imgBox);
                    QUtils.WebDownload(imgUrl, imgPath);
                }
            }
            catch (Exception ex)
            {
                if (weaponName == "MIL" || weaponName == "SENTRY" ||
                    weaponName == "T80" || weaponName == "APC")
                {
                    if (weaponName == "MIL")
                        imgUrl = baseImgBBUrl + "LQFRL8Q/MIL.jpg";
                    else if (weaponName == "APC")
                        imgUrl = baseImgBBUrl + "gyvkYTg/APC.png";
                    else if (weaponName == "T80")
                        imgUrl = baseImgBBUrl + "WxGx9Dn/T80.png";
                    else if (weaponName == "SENTRY")
                        imgUrl = baseImgBBUrl + "Bz81WvR/SENTRY.jpg";

                    LoadImgBoxWeb(imgUrl, imgBox);
                    QUtils.WebDownload(imgUrl, imgPath);
                }
                else
                    imgBox.Image = null;
            }
        }

        private void removeWeaponBtn_Click(object sender, EventArgs e)
        {
            var weaponModel = QUtils.weaponList[weaponSelectDD.SelectedIndex].Values.ElementAt(0);
            var weaponName = QUtils.weaponList[weaponSelectDD.SelectedIndex].Keys.ElementAt(0);
            string qscData = null;

            qscData = QHuman.RemoveWeapon(weaponModel, true);
            if (!String.IsNullOrEmpty(qscData))
                compileStatus = QCompiler.Compile(qscData, QUtils.gamePath, false, true, true);

            if (compileStatus)
                SetStatusText("Weapon " + weaponName + " removed successfully");
        }

        private void exportObjectsBtn_Click(object sender, EventArgs e)
        {
            var qtaskList = QUtils.GetQTaskList(false, true);
            if (csvCb.Checked)
                QUtils.ExportCSV(QUtils.objects + QUtils.csvExt, qtaskList);
            else if (xmlCb.Checked)
                QUtils.ExportXML(QUtils.objects + QUtils.xmlExt);
            else if (jsonCb.Checked)
                QUtils.ExportJson(QUtils.objects + QUtils.jsonExt);
            if (!csvCb.Checked && !xmlCb.Checked && !jsonCb.Checked)
                QUtils.ShowError("Atleast one option should be selected for exporting data");
            else
                SetStatusText("Data exported success");
        }

        private void csvCb_CheckedChanged(object sender, EventArgs e)
        {
            if (csvCb.Checked) xmlCb.Checked = jsonCb.Checked = false;
        }

        private void jsonCb_CheckedChanged(object sender, EventArgs e)
        {
            if (jsonCb.Checked) xmlCb.Checked = csvCb.Checked = false;
        }

        private void xmlCb_CheckedChanged(object sender, EventArgs e)
        {
            if (xmlCb.Checked) jsonCb.Checked = csvCb.Checked = false;
        }

        private void objectSelectDD_Click(object sender, EventArgs e)
        {
            isObjectDD = true;
            isBuildingDD = false;
        }

        private void clearCacheBtn_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(QUtils.cachePath))
            {
                QUtils.DeleteWholeDir(QUtils.cachePath);
                SetStatusText("Application cache cleared");
            }
        }

        private void appLogsCb_CheckedChanged(object sender, EventArgs e)
        {
            if (appLogsCb.Checked)
            {
                QUtils.appLogs = true;
                QUtils.EnableLogs();
                SetStatusText("Application Logs enabled");
            }
            else
            {
                QUtils.appLogs = false;
                QUtils.DisableLogs();
                SetStatusText("Application Logs disabled");
            }
        }

        private void autoResetCb_CheckedChanged(object sender, EventArgs e)
        {
            if (autoResetCb.Checked)
            {
                QUtils.gameReset = true;
                QUtils.ResetFile(gameLevel);
                SetStatusText("Auto reset level enabled");
            }
            else
            {
                QUtils.gameReset = false;
                SetStatusText("Auto reset level disabled");
            }
        }

        private void helpBtn_Click(object sender, EventArgs e)
        {
            QUtils.ShowInfo(QUtils.helpStr);
        }

        private void objectIDTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var modelId = objectIDTxt.Text;
                var modelName = QObjects.FindModelName(modelId);
                if (!String.IsNullOrEmpty(modelName)) objectIDLbl.Text = modelName;
            }
        }

        private void tabContainer_Selected(object sender, TabControlEventArgs e)
        {
            //Level Editor
            if (e.TabPageIndex == 0)
            {
                buildingSelectDD.SelectedIndex = objectSelectDD.SelectedIndex = 0;
            }

            //Object Editor
            if (e.TabPageIndex == 1)
            {
                qtaskList = QUtils.GetQTaskList(false, false, true);

                buildingsCount = qtaskList.Count(o => o.name.Contains("Building"));
                rigidObjCount = qtaskList.Count(o => o.name.Contains("EditRigidObj"));

                maxItemsLbl1.Text = maxItemsLbl2.Text = maxItemsLbl3.Text = maxItemsLbl4.Text = null;
                maxItemsLbl1.Text = maxItemsLbl3.Text = "Max Items :" + Convert.ToString(rigidObjCount);
                maxItemsLbl2.Text = maxItemsLbl4.Text = "Max Items :" + Convert.ToString(buildingsCount);
            }

            //A.I Editor.
            if (e.TabPageIndex == 3)
            {
                aiGraphIdDD.SelectedIndex = aiTypeDD.SelectedIndex = aiModelSelectDD.SelectedIndex = weaponAiDD.SelectedIndex = 0;
            }

            //Weapon Editor.
            if (e.TabPageIndex == 4)
            {
                weaponSelectDD.SelectedIndex = 0;
            }


            //Position Editor
            if (e.TabPageIndex == 7)
            {
                UpdateUIComponent(buildingPosDD, QUtils.buildingListStr);
                UpdateUIComponent(objectPosDD, QUtils.objectRigidListStr);
            }
        }

        private void removeObjsBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var qscData = QUtils.LoadFile();
                var itemsCount = Convert.ToInt32(ObjsRemTxt.Text);
                qscData = QObjects.RemoveAllObjects(qscData, false, true, itemsCount, true);
                compileStatus = QCompiler.CompileEx(qscData);
                if (compileStatus)
                    SetStatusText(itemsCount + " Objects removed successfully");
            }
            catch (Exception ex)
            {
                QUtils.ShowError(ex.Message ?? ex.StackTrace);
            }
        }

        private void removeBuildingsBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var qscData = QUtils.LoadFile();
                var itemsCount = Convert.ToInt32(buildingsRemTxt.Text);
                qscData = QObjects.RemoveAllObjects(qscData, true, false, itemsCount, true);
                compileStatus = QCompiler.CompileEx(qscData);
                if (compileStatus)
                    SetStatusText(itemsCount + " Buildings removed successfully");
            }
            catch (Exception ex)
            {
                QUtils.ShowError(ex.Message ?? ex.StackTrace);
            }
        }

        private void buildingSelectDD_Click(object sender, EventArgs e)
        {
            isBuildingDD = true;
            isObjectDD = false;
        }

        private void restartLevel_Click(object sender, EventArgs e)
        {
            GT.GT_SendKeys2Process(QMemory.gameName, "^r", false);
            //QMemory.UpdateHumanHealth(true);

            //Genrate random scriptId according to Level A.I.
            GenerateAIScriptId(QUtils.gGameLevel);
        }

        private void clearAllLvlBtn_Click(object sender, EventArgs e)
        {
            var voidLevelPath = QUtils.cfgVoidPath + "\\objects_void_" + QUtils.gGameLevel + QUtils.qscExt;

            if (File.Exists(QUtils.objectsQsc))
                File.Delete(QUtils.objectsQsc);
            File.Copy(voidLevelPath, QUtils.objectsQsc);

            var qscData = QCryptor.Decrypt(QUtils.objectsQsc);
            File.WriteAllText(QUtils.objectsQsc, qscData);
            QUtils.levelFlowData = File.ReadLines(QUtils.objectsQsc).Last();
            compileStatus = QCompiler.CompileEx(qscData);

            if (compileStatus)
                SetStatusText("Level cleared out success");
        }


        //Testing phase - remove in final build
        private void compileBtn_Click(object sender, EventArgs e)
        {
            string qscData = QUtils.LoadFile();
            if (!String.IsNullOrEmpty(qscData))
                compileStatus = QCompiler.CompileEx(qscData);
            if (compileStatus) SetStatusText("Compile success");
        }

        private void resetBuildingsBtn_Click(object sender, EventArgs e)
        {
            SetStatusText("Resetting please wait...");
            qtaskList = QUtils.GetQTaskList(false, false, true);
            var qTaskListCount = qtaskList.Count();
            var itemsCount = String.IsNullOrEmpty(buildingsResTxt.Text) ? 0 : Convert.ToInt32(buildingsResTxt.Text);
            int qTaskCount = 0;

            if (itemsCount > qTaskListCount)
            {
                QUtils.ShowError("Items count must be less than max items.");
                return;
            }

            for (int i = 0; i < qTaskListCount; i++)
            {
                var qtask = qtaskList[i];
                if (qtask.name == "\"Building\"")
                {
                    if (qTaskCount == itemsCount)
                        break;

                    var buildingModel = qtask.model;
                    AddObject(buildingModel, false, qtask.position, true, qtask.note, qtask.orientation.alpha, qtask.orientation.beta, qtask.orientation.gamma);
                    qTaskCount++;
                }
            }

            if (compileStatus)
                SetStatusText(itemsCount + " Buildings reset success");
        }

        private void cutsceneRemoveBtn_Click(object sender, EventArgs e)
        {
            var qscData = QMisc.RemoveCutscene(inputQscPath, gameLevel);
            if (!String.IsNullOrEmpty(qscData))
                compileStatus = QCompiler.CompileEx(qscData);

            if (compileStatus)
                SetStatusText("Cutscene removed success.");
        }

        private void objectPosDD_SelectedIndexChanged(object sender, EventArgs e)
        {
            isObjectDD = true;
            isBuildingDD = false;
        }

        private void buildingPosDD_SelectedIndexChanged(object sender, EventArgs e)
        {
            isBuildingDD = true;
            isObjectDD = false;
        }

        private void updateHumanSpeedBtn_Click(object sender, EventArgs e)
        {
            try
            {
                QUtils.movSpeed = Convert.ToDouble(movementSpeedTxt.Text);
                QUtils.forwardSpeed = Convert.ToDouble(forwardJumpTxt.Text);
                QUtils.upwardSpeed = Convert.ToDouble(upwardJumpTxt.Text);
                QUtils.inAirSpeed = Convert.ToDouble(inAirSpeedTxt.Text);
                QHuman.UpdateHumanPlayerSpeed(QUtils.movSpeed, QUtils.forwardSpeed, QUtils.upwardSpeed, QUtils.inAirSpeed);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("not in a correct format"))
                    QUtils.ShowError("Human speed parameters are empty or invalid");
                else
                    QUtils.ShowError(ex.Message ?? ex.StackTrace);
            }
        }

        private void updateHumanHealthBtn_Click(object sender, EventArgs e)
        {
            try
            {
                QUtils.healthScale = Convert.ToDouble(damageScaleTxt.Text);
                QUtils.healthScaleFence = Convert.ToDouble(damageScaleFenceTxt.Text);
                QHuman.UpdateHumanPlayerHealth(QUtils.healthScale, QUtils.healthScaleFence);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("not in a correct format"))
                    QUtils.ShowError("Human health parameters are empty or invalid");
                else
                    QUtils.ShowError(ex.Message ?? ex.StackTrace);
            }

        }

        private void updateHumanPeekBtn_Click(object sender, EventArgs e)
        {
            try
            {
                QUtils.peekLRLen = Convert.ToDouble(peekLRTxt.Text);
                QUtils.peekCrouchLen = Convert.ToDouble(peekCrouchTxt.Text);
                QUtils.peekTime = Convert.ToDouble(peekTimeTxt.Text);
                QHuman.UpdateHumanPlayerPeek(QUtils.peekLRLen, QUtils.peekCrouchLen, QUtils.peekTime);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("not in a correct format"))
                    QUtils.ShowError("Human Peek parameters are empty or invalid");
                else
                    QUtils.ShowError(ex.Message ?? ex.StackTrace);
            }
        }

        private void resetHumanBtn_Click(object sender, EventArgs e)
        {
            string humanFileName = QUtils.cfgHumanplayerPathQvm + @"\humanplayer.qvm";
            var outputHumanPlayerPath = QUtils.gameAbsPath + "\\humanplayer\\";
            var moveCmd = "copy " + humanFileName + " " + outputHumanPlayerPath + " /y";
            QUtils.ShellExec(moveCmd, true);

            //Reset params in UI.
            movementSpeedTxt.Text = "1.75";
            forwardJumpTxt.Text = "17.5";
            upwardJumpTxt.Text = "27";
            inAirSpeedTxt.Text = "0.5";
            peekLRTxt.Text = peekCrouchTxt.Text = "0.8500000238418579";
            peekTimeTxt.Text = " 0.25";
            damageScaleTxt.Text = "3.0";
            damageScaleFenceTxt.Text = "0.5";

            Thread.Sleep(1000);
            GT.GT_SendKeys2Process(QMemory.gameName, "^h", true);
            QMemory.SetStatusMsgText("Human parameters reset success");
        }

        private void readHumanBtn_Click(object sender, EventArgs e)
        {
            var humanPlayerFile = QUtils.cfgHumanplayerPathQsc + @"\humanplayer" + QUtils.qscExt;
            string humanFileName = "humanplayer.qsc";
            string humanPlayerData = QCryptor.Decrypt(humanPlayerFile);

            var outputHumanPlayerPath = QUtils.gameAbsPath + "\\humanplayer\\";

            QUtils.SaveFile(humanFileName, humanPlayerData);
            bool status = QCompiler.Compile(humanFileName, outputHumanPlayerPath, 0x0);
            File.Delete(humanFileName);

            if (status)
            {
                Thread.Sleep(1000);
                GT.GT_SendKeys2Process(QMemory.gameName, "^h", true);
                QMemory.SetStatusMsgText("Human parameters set success");
            }
        }

        private void resetObjectsBtn_Click(object sender, EventArgs e)
        {
            SetStatusText("Resetting please wait...");
            qtaskList = QUtils.GetQTaskList(false, false, true);
            var qTaskListCount = qtaskList.Count();
            var itemsCount = String.IsNullOrEmpty(ObjsResTxt.Text) ? 0 : Convert.ToInt32(ObjsResTxt.Text);
            int qTaskCount = 0;

            if (itemsCount > qTaskListCount)
            {
                QUtils.ShowError("Items count must be less than max items.");
                return;
            }

            for (int i = 0; i < qTaskListCount; i++)
            {
                var qtask = qtaskList[i];
                if (qtask.name == "\"EditRigidObj\"")
                {
                    if (qTaskCount == itemsCount)
                        break;

                    var rigidModel = qtask.model;
                    AddObject(rigidModel, true, qtask.position, true, qtask.note, qtask.orientation.alpha, qtask.orientation.beta, qtask.orientation.gamma);
                    qTaskCount++;
                }
            }

            if (compileStatus)
                SetStatusText(itemsCount + " Objects reset success");
        }

        private void igiSmallIconBtn_Click(object sender, EventArgs e)
        {
            int level = Convert.ToInt32(levelStartTxt.Text);
            StartGameLevel(level, false);
        }

        private void aiModelSelectDD_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                var aiModelName = QAI.GetAiModelNamesList(gameLevel)[aiModelSelectDD.SelectedIndex];
                var aiModelId = QAI.GetAiModelIdForName(aiModelName);

                var imgPath = aiModelName + QUtils.pngExt;
                var imgTmpPath = QUtils.cachePathAppImages + "\\" + imgPath;

                //Load image from Cache.
                if (File.Exists(imgTmpPath))
                {
                    using (var bmpTemp = new Bitmap(imgTmpPath))
                    {
                        aiImgBox.Image = new Bitmap(bmpTemp);
                    }
                    SetStatusText("Setting resource image success");
                }

                //Load image from Web.
                else
                {
                    //A.I image paths.
                    var imageId = QAI.GetAiImageId(aiModelName);
                    var imgUrl = baseImgBBUrl + imageId + aiModelName.Replace("_", "-") + QUtils.pngExt;

                    SetStatusText("Downloading resource please wait...");
                    QUtils.AddLog("Downloading image resource: URL : " + imgUrl);
                    LoadImgBoxWeb(imgUrl, aiImgBox);
                    QUtils.WebDownload(imgUrl, imgPath);
                }
            }
            catch (Exception ex)
            {
                aiImgBox.Image = null;
            }
        }

        private void weaponAiDD_SelectedValueChanged(object sender, EventArgs e)
        {
            PopulateWeaponDD(weaponAiDD.SelectedIndex, weaponAIImgBox);
        }

        private void aiTypeDD_SelectedValueChanged(object sender, EventArgs e)
        {
        }

        private void guardGeneratorCb_CheckedChanged(object sender, EventArgs e)
        {
            maxSpawnsTxt.Enabled = guardGeneratorCb.Checked;
        }

        private void aiGraphIdDD_SelectedValueChanged(object sender, EventArgs e)
        {
            int graphId = QUtils.aiGraphIdStr[aiGraphIdDD.SelectedIndex];
            string graphFile = QUtils.graphsPath + "\\" + "graph" + graphId + datExt;
            QUtils.AddLog("graphFile: '" + graphFile + "'");
            var nodeData = QGraphs.ReadGraphNodeData(graphFile);
            var graphPos = QGraphs.GetGraphPosition(graphId.ToString());

            if (visualiseNodesCb.Checked)
            {
                string qscData = null;
                QUtils.qtaskId = QUtils.GenerateTaskID(true);
                foreach (var node in nodeData)
                {
                    QUtils.AddLog("Node_" + node.NodeId + " X: " + node.NodePos.x + " Y: " + node.NodePos.y + " Z: " + node.NodePos.z + " Criteria: " + node.NodeCriteria);

                    var nodeRealPos = new Real64();
                    nodeRealPos.x = graphPos.x + node.NodePos.x;
                    nodeRealPos.y = graphPos.y + node.NodePos.y;
                    nodeRealPos.z = graphPos.z + node.NodePos.z;
                    string taskNote = "Graph #" + graphId + " Node #" + node.NodeId;

                    //Add Visualisation for Nodes.
                    var objModel = QUtils.objectRigidList[objectSelectDD.SelectedIndex].Values.ElementAt(0);
                    AddObject(objModel, true, nodeRealPos, false, taskNote);
                    var areaDim = new AreaDim(8000);
                    
                     qscData += QObjects.AddAreaActivate(QUtils.qtaskId++, objModel, null,"\"" + taskNote + "\"",ref nodeRealPos, ref areaDim);
                }
                qscData = QUtils.LoadFile() + "\n" + qscData;
                compileStatus = QCompiler.CompileEx(qscData);

                if (compileStatus) SetStatusText("Graph Visualisation added successfully");
            }

            string graphArea = QGraphs.GetGraphArea(graphId);
            graphAreaLbl.Text = graphArea;
            QUtils.AddLog("GraphId : " + graphId + " GraphArea: " + graphArea);
        }

        private void addAiBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var aiModelName = QAI.GetAiModelNamesList(gameLevel)[aiModelSelectDD.SelectedIndex];
                var aiModelId = QAI.GetAiModelIdForName(aiModelName);
                var aiWeaponModel = QUtils.weaponList[weaponAiDD.SelectedIndex].Values.ElementAt(0);
                int graphId = QUtils.aiGraphIdStr[aiGraphIdDD.SelectedIndex];
                string aiType = QUtils.aiTypes[aiTypeDD.SelectedIndex];
                int aiCount = Convert.ToInt32(aiCountTxt.Text);
                int maxSpawns = Convert.ToInt32(maxSpawnsTxt.Text);

                //Set human A.I properties.
                var humanAi = new HumanAi();
                humanAi.model = aiModelId;
                humanAi.weapon = aiWeaponModel;
                humanAi.graphId = graphId;
                humanAi.aiType = aiType;
                humanAi.aiCount = aiCount;
                humanAi.guardGenerator = guardGeneratorCb.Checked;
                humanAi.maxSpawns = maxSpawns;
                humanAi.invulnerability = aiinvulnerabilityCb.Checked;
                humanAi.advanceView = aiAdvanceViewCb.Checked;
                humanAi.friendly = aiFriendlyCb.Checked;

                string configOut = "You are about to Add A.I confirm ?\n";
                configOut += "aiCount : " + humanAi.aiCount + "\n";
                configOut += "aiType : " + humanAi.aiType + "\n";
                configOut += "graphId : " + humanAi.graphId + "\n";
                configOut += "weapon : " + humanAi.weapon + "\n";
                configOut += "model : " + humanAi.model + "\n";
                configOut += "friendly : " + humanAi.friendly + "\n";
                configOut += "guardGenerator : " + humanAi.guardGenerator + "\n";
                configOut += "maxSpawns : " + humanAi.maxSpawns + "\n";
                configOut += "invulnerability : " + humanAi.invulnerability + "\n";
                configOut += "advanceView : " + humanAi.advanceView + "\n";

                var dlgResult = QUtils.ShowDialog(configOut);

                if (dlgResult == DialogResult.Yes)
                {
                    var qscData = QAI.AddHumanSoldier(humanAi, humanAi.guardGenerator, humanAi.maxSpawns, humanAi.invulnerability, humanAi.advanceView);
                    qscData = QAI.AddAiTaskDetection(qscData);
                    if (!String.IsNullOrEmpty(qscData))
                        compileStatus = QCompiler.Compile(qscData, QUtils.gamePath, true, true); ;

                    if (compileStatus)
                        SetStatusText("AI " + aiModelName + " Added successfully");
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                SetStatusText("A.I data cannot be empty.");
            }
        }

        private void tabContainer_Selecting(object sender, TabControlCancelEventArgs e)
        {
            //Disable Editor Tabs.
            if (e.TabPageIndex == 1 || e.TabPageIndex == 5 || e.TabPageIndex == 6)
            {
                e.Cancel = true;
            }

        }

        private void customAiCb_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                SetStatusText("Add your custom scripts/path for A.I");
                QUtils.ShowInfo("Add your custom scripts for your A.I\n'XXXX' or 'YYYY' are Masking IDs dont replace them.");
                QUtils.ShellExec("notepad " + QUtils.appdataPath + "\\" + QUtils.customScriptFileQEd);
                QUtils.ShowInfo("Add your custom path for your A.I\n'XXXX' or 'YYYY' are Masking IDs dont replace them.");
                QUtils.ShellExec("notepad " + QUtils.appdataPath + "\\" + QUtils.customAiPathFileQEd);
                QUtils.customAiSelected = true;
            }
        }

        private void showAppLogBtn_Click(object sender, EventArgs e)
        {
            QUtils.ShellExec("notepad " + QUtils.appLogFileTmp + projAppName + ".log");
        }

        private void connectionCb_CheckedChanged(object sender, EventArgs e)
        {
            if ((((CheckBox)sender).Checked))
            {
                if (QUtils.IsNetworkAvailable())
                {
                    isEditorOnline = true;
                    connectionCb.Text = "Online";
                    connectionCb.ForeColor = Color.Green;
                    registeredUsersLbl.Visible = true;
                    registeredUsersLbl.Text = "Users: " + QUtils.GetRegisteredUsers();
                }
                else
                {
                    isEditorOnline = false;
                    connectionCb.Text = "Offline";
                    connectionCb.ForeColor = Color.Red;
                    registeredUsersLbl.Visible = false;
                    (((CheckBox)sender).Checked) = false;
                    SetStatusText("Please check your internet connection.");
                }
            }
            else
            {
                isEditorOnline = false;
                connectionCb.Text = "Offline";
                connectionCb.ForeColor = Color.Red;
                registeredUsersLbl.Visible = false;
            }
        }

        private void removeModelBtn_Click(object sender, EventArgs e)
        {
            var modelRegex = @"\d{3}_\d{2}_\d{1}";
            var valueRegex = Regex.Match(objectIDTxt.Text, modelRegex).Value;
            if (String.IsNullOrEmpty(valueRegex))
                QUtils.ShowError("Input Object Id is in wrong format. Check Help for proper format");
            else
            {
                var modelId = objectIDTxt.Text;
                var qscData = QUtils.LoadFile();
                qscData = QObjects.RemoveObject(qscData, modelId, true, true);

                if (!String.IsNullOrEmpty(qscData))
                {
                    compileStatus = QCompiler.CompileEx(qscData);

                    var modelName = QObjects.FindModelName(modelId);
                    if (!String.IsNullOrEmpty(modelName)) objectIDLbl.Text = modelName;

                    if (compileStatus)
                        SetStatusText("Object " + modelName + " removed successfully");
                }
            }
        }

        private void startGameBtn_Click(object sender, EventArgs e)
        {
            try
            {
                gameLevel = Convert.ToInt32(levelStartTxt.Text.ToString());
                InitPaths(gameLevel);
                StartGameLevel(gameLevel, true);
            }
            catch (Exception ex)
            {
                QUtils.ShowError(ex.Message ?? ex.StackTrace);
            }
        }

        private void disableWarningsCb_CheckedChanged(object sender, EventArgs e)
        {
            if (disableWarningsCb.Checked)
            {
                QMemory.DisableGameWarnings();
            }
        }

        private void StartGameLevel(int level, bool windowed = true)
        {
            LoadLevelDetails(level);
            QMemory.StartLevel(level, windowed);
            gameFound = true;
            Thread.Sleep(3000);
            QUtils.RestoreLevel(level);
            QUtils.ResetFile(level);
            GenerateAIScriptId(level);
            QUtils.aiScriptFiles.Clear();

            Thread.Sleep(5000);
            var qscData = QMisc.RemoveCutscene(inputQscPath, level);
            if (!String.IsNullOrEmpty(qscData))
                compileStatus = QCompiler.CompileEx(qscData);
            QUtils.InjectDllOnStart();
            RefreshUIComponents(level);

            //QMemory.UpdateHumanHealth(true);
        }

        private void RefreshUIComponents(int level)
        {
            InitUIComponents(level, false);
            UpdateUIComponent(buildingSelectDD, QUtils.buildingListStr);
            UpdateUIComponent(objectSelectDD, QUtils.objectRigidListStr);
            UpdateUIComponent(aiModelSelectDD, QUtils.aiModelsListStr);
            UpdateUIComponent(aiGraphIdDD, QUtils.aiGraphIdStr);
        }

        //Generic Update UI method for DropDowns,TextBox etc.
        private void UpdateUIComponent<T>(ComboBox itemDD, List<T> dataSrcList)
        {
            itemDD.DataSource = null;
            itemDD.Items.Clear();
            itemDD.DataSource = dataSrcList;
            itemDD.Refresh();
            itemDD.SelectedIndex = 0;
        }

        private static void LoadImgBoxWeb(string url, PictureBox imgBox)
        {
            if (!isEditorOnline) { editorRef.SetStatusText("Resource error Check your internet connection."); return; }
            try
            {
                var request = WebRequest.Create(url);

                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    imgBox.Image = Bitmap.FromStream(stream);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("The remote name could not be resolved"))
                {
                    QUtils.ShowError("Resource error Please check your internet connection and try Again");
                }
                else throw new Exception(ex.Message);
            }
        }

        private static void GenerateAIScriptId(int level)
        {
            switch (level)
            {
                case 1:
                    QUtils.aiScriptId = 510;
                    break;
                case 2:
                    QUtils.aiScriptId = 450;
                    break;
                case 3:
                    QUtils.aiScriptId = 563;
                    break;
                case 4:
                    QUtils.aiScriptId = 630;
                    break;
                case 5:
                    QUtils.aiScriptId = 500;
                    break;
                case 6:
                    QUtils.aiScriptId = 1000;
                    break;
                case 7:
                    QUtils.aiScriptId = 581;
                    break;
                case 8:
                    QUtils.aiScriptId = 667;
                    break;
                case 9:
                    QUtils.aiScriptId = 530;
                    break;
                case 10:
                    QUtils.aiScriptId = 533;
                    break;
                case 11:
                    QUtils.aiScriptId = 512;
                    break;
                case 12:
                case 13:
                    QUtils.aiScriptId = 400;
                    break;
                case 14:
                    QUtils.aiScriptId = 754;
                    break;
            }
        }

        private void AddObject(string model, bool rigidObj = false, Real64 objectPos = null, bool hasOrientation = false, string taskNote = "", float alpha = -9.9f, float beta = -9.9f, float gamma = -9.9f)
        {
            try
            {
                string qscData = null;

                if (hasOrientation)
                {
                    alpha = alpha == fltInvalidVal ? float.Parse(alphaTxt.Text) : alpha;
                    beta = beta == fltInvalidVal ? float.Parse(betaTxt.Text) : beta;
                    gamma = gamma == fltInvalidVal ? float.Parse(gammaTxt.Text) : gamma;

                    if (rigidObj)
                    {
                        var orientation = new Real32(alpha, beta, gamma);
                        qscData = QObjects.AddRigidObj(model, objectPos, orientation, false, taskNote);
                    }
                    else
                    {
                        var orientation = new Real32(alpha, beta, gamma);
                        qscData = QBuildings.AddBuilding(model, objectPos, orientation, false);
                    }
                }
                else
                {
                    if (rigidObj)
                        qscData = QObjects.AddRigidObj(model, objectPos, false, taskNote);
                    else
                        qscData = QBuildings.AddBuilding(model, objectPos);
                }

                //Compile the data with QCompiler.
                if (!String.IsNullOrEmpty(qscData))
                    compileStatus = QCompiler.Compile(qscData, QUtils.gamePath, true);
            }
            catch (NullReferenceException ex)
            {
                QUtils.ShowError("Values are null while adding object " + ex.StackTrace);
            }
            catch (Exception ex)
            {
                QUtils.ShowError(ex.Message);
            }
        }
    }
}
