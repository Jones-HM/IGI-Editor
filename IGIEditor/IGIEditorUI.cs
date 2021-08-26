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
using System.Threading;
using System.Windows.Forms;
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
        static bool gameReset = false, gameLogs = false, gameFound = false;
        static int width = Console.LargestWindowWidth - 150;
        static int height = Console.LargestWindowHeight - 5;
        static string model3d, blenderModel;
        public static int randPosOffset = 0, posOffset = 3000;
        string inputQvmPath, inputQscPath;

        static internal IGIEditorUI editorRef;
        System.Windows.Forms.Timer posTimer = new System.Windows.Forms.Timer();

        public IGIEditorUI()
        {
            InitializeComponent();
            UXWorker formMover = new UXWorker();
            formMover.CustomFormMover(formMoverPnl, this);
            editorRef = this;

            posTimer.Tick += new EventHandler(UpdatePositionTimer);
            posTimer.Interval = 500;
            posTimer.Start();

            //Disabling Errors and Warnings.
            GT.GT_SuppressErrors(true);
            GT.GT_SuppressWarnings(true);

            int level = Convert.ToInt32(levelStartTxt.Text.ToString());
            InitApp();
            InitializePaths(level);

            int gameLevel = 1;

            if (QMemory.FindGame())
            {
                gameFound = true;
                gameLevel = QMemory.GetCurrentLevel();
                if (gameLevel == 0) gameLevel = 1;
                QUtils.InjectDllOnStart();
            }
            else
            {
                QMemory.StartLevel(gameLevel, true);
                gameFound = true;
                Thread.Sleep(3000);
                QUtils.RestoreLevel(gameLevel);
                QUtils.ResetFile(gameLevel);
                GenerateRandScriptId(gameLevel);
                QUtils.aiScriptFiles.Clear();
                Thread.Sleep(5000);
                var qscData = QMisc.RemoveCutscene(QUtils.cfgInputQscPath + gameLevel + "\\" + QUtils.objectsQsc, gameLevel);
                if (!String.IsNullOrEmpty(qscData))
                    QCompiler.CompileEx(qscData);
                QUtils.InjectDllOnStart();
            }

            var inputQscPath = QUtils.cfgInputQscPath + gameLevel + "\\" + QUtils.objectsQsc;

            if (!File.Exists(QUtils.objectsQsc))
            {
                File.Copy(inputQscPath, QUtils.objectsQsc);

                //Decrypt data onLoad.
                QCryptor.Decrypt(QUtils.objectsQsc, null);
            }

            if (gameReset)
                QUtils.ResetFile(gameLevel);

            if (gameLogs)
            {
                QUtils.EnableLogs();
                QLibc.GT.GT_EnableLogs();
            }

            //Get current level selected.
            if (gameFound)
            {
                gameLevel = QMemory.GetCurrentLevel();
                levelStartTxt.Text = gameLevel.ToString();
                SetStatusText("Game running...");
                LoadLevelDetails(gameLevel);
            }
            else
                SetStatusText("Game not running...");

            //Init Dropdown,List UI components.
            Thread.Sleep(3000);
            InitUIComponents(level);
        }

        private void UpdatePositionTimer(object sender, EventArgs e)
        {
            if (gameFound)
            {
                DisableLogs();
                var realPos = QMemory.GetGamePositions();
                xPosLbl.Text = realPos.alpha.ToString("0.0000");
                yPosLbl.Text = realPos.beta.ToString("0.0000");
                zPosLbl.Text = realPos.gamma.ToString("0.0000");
                if (logEnabled)
                    EnableLogs();
            }
        }

        private void InitUIComponents(int level, bool initialInit = true)
        {
            //Init Weapons list.
            QUtils.weaponList = QHuman.GetWeaponsList();
            foreach (var weapon in QUtils.weaponList)
                weaponSelectDD.Items.Add(weapon.Keys.ElementAt(0));

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
                    objectSelectDD.Items.Add(rigidObj.Keys.ElementAt(0));
            }
        }

        private void InitApp()
        {
            //Read paths from config.
            ParseConfig();

            //Initialize app data for QEditor.
            QUtils.InitAppData();

            //Initialize QCompiler setting and modules.
            QUtils.InitQCompiler();

            string userName = QUtils.GetCurrentUserName();
            string keyFileAbsPath = QUtils.igieditorTmpPath + Path.DirectorySeparatorChar + QUtils.projAppName + "Key.txt";
            string deviceKey = QUtils.GetMachineDeviceId();
            string inputKey = null, welcomeMsg = "Welcome " + userName + " to IGI 1 Editor";

            //Setting Options.
            appLogsCb.Checked = gameLogs;
            autoResetCb.Checked = gameReset;

            headerLbl.Text += " " + userName;
            SetStatusText(welcomeMsg);

            //Initialize user info.
            var initUser = QUtils.InitUserInfo();
            QUtils.AddLog("Device Id Exist : " + QUtils.keyExist);

            //Check if Machine key is already saved.
            if (QUtils.keyFileExist)
            {
                inputKey = File.ReadAllText(keyFileAbsPath);
            }

            if (QUtils.keyFileExist && !QUtils.keyExist)
            {
                QUtils.AddLog("KeyFileExist : " + QUtils.keyFileExist + "\nkeyExist : " + QUtils.keyExist);
                QUtils.ShowSystemFatalError("No such user found in our database, ERROR_NO_USER_FOUND (0x525).");
            }
            else
            {
                if (String.IsNullOrEmpty(inputKey))
                {
                    inputKey = Interaction.InputBox(welcomeMsg, "IGI EDITOR KEY", "Enter your personal IGI 1 Editor key Here", 50, 40);

                    if (deviceKey != inputKey)
                    {
                        QUtils.ShowSystemFatalError("Wrong IGI Key encountered! ERROR_INVALID_KEY_USAGE (Error : 0x35FA)");
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
            GenerateRandScriptId(QUtils.gGameLevel);

            string game_path_tmp = QUtils.cfgGamePath;
            var game_abs_path = game_path_tmp.Slice(0, game_path_tmp.IndexOf("\\", game_path_tmp.IndexOf("\\") + 1));

            //Move Humanplayer config file for Weapon ammo limitations.
            var humanplayer_abs_path = game_abs_path + Path.DirectorySeparatorChar + QUtils.humanplayerPath + Path.DirectorySeparatorChar + QUtils.humanplayer;
            if (File.Exists(QUtils.humanplayer) && !File.Exists(humanplayer_abs_path))
                File.Move(Path.GetFullPath(QUtils.humanplayer), humanplayer_abs_path);

            //Creates shortcut of game in current directory.
            QUtils.CreateShortcut();

        }

        internal void SetStatusText(string text)
        {
            statusLbl.Text = text;
        }

        private static void ParseConfigProperty(string config_path, ref bool property_name, string keyword)
        {
            if (config_path.Trim().Contains("true")) property_name = true;
            else if (config_path.Trim().Contains("false")) property_name = false;
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

        static void ParseConfig()
        {
            QUtils.projAppName = Assembly.GetEntryAssembly().GetName().Name;
            QUtils.cfgFile = QUtils.projAppName + ".ini";
            QUtils.logFile = QUtils.projAppName + ".log";
            QUtils.currPath = Directory.GetCurrentDirectory();

            var keywords = new List<string>() { "game_path", "game_logs", "game_reset", };
            if (File.Exists(QUtils.cfgFile))
            {
                var cfgData = File.ReadAllText(cfgFile);
                var split_data = cfgData.Split('\n');

                if (!keywords.All(o => cfgData.Contains(o)))
                {
                    QUtils.ShowError("Config file '" + QUtils.cfgFile + "' doesn't contain proper keywords", QUtils.CAPTION_CONFIG_ERR);
                    Environment.Exit(1);
                }

                foreach (var data in split_data)
                {
                    if (keywords.Any(o => data.Contains(o)))
                    {
                        for (int i = 0; i < keywords.Count; i++)
                        {
                            if (data.Contains(keywords[i]))
                            {
                                var config_path = data.Substring(keywords[i].Length + 0x3);

                                if (i == 0)
                                {
                                    if (String.IsNullOrEmpty(config_path) || String.IsNullOrWhiteSpace(config_path))
                                        QUtils.ShowConfigError(keywords[i]);
                                    else
                                    {
                                        var g_path = config_path.Trim();
                                        if (!File.Exists(g_path + Path.DirectorySeparatorChar + QMemory.gameName + ".exe"))
                                        {
                                            QUtils.ShowError("Invalid path selected! Game 'IGI' not found at path '" + g_path + "'", QUtils.CAPTION_FATAL_SYS_ERR);
                                            Environment.Exit(1);
                                        }
                                        QUtils.gameAbsPath = config_path.Trim();
                                        QUtils.cfgGamePath = config_path.Trim() + QUtils.cfgGamePathEx;
                                    }
                                }
                                else if (i == 1)
                                    ParseConfigProperty(config_path, ref gameLogs, keywords[i]);

                                else if (i == 2)
                                    ParseConfigProperty(config_path, ref gameReset, keywords[i]);

                            }

                        }
                    }
                }
            }
            else
            {
                QUtils.ShowWarning("Config file not found in current directory", QUtils.CAPTION_CONFIG_ERR);
                QUtils.CreateConfig(QUtils.cfgFile);
            }
        }


        private void InitializePaths(int gameLevel)
        {
            QUtils.gamePath = QUtils.cfgGamePath + gameLevel;
            inputQvmPath = QUtils.cfgInputQvmPath + gameLevel + "\\" + QUtils.objectsQvm;
            inputQscPath = QUtils.cfgInputQscPath + gameLevel + "\\" + QUtils.objectsQsc;
            QUtils.gGameLevel = gameLevel;
        }

        public void LoadLevelDetails(int level)
        {

            //load level Description.
            levelNameLbl.Text = QMission.GetMissionInfo(level);

            //Load level image.
            var imgUrl = baseImgUrl + levelImgUrl[level - 1];
            var request = WebRequest.Create(imgUrl);

            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                levelImgBox.Image = Bitmap.FromStream(stream);
            }
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void refreshLevel_Click(object sender, EventArgs e)
        {
            GT.GT_SendKeys2Process(QMemory.gameName, "^r", false);
        }

        private void addWeaponBtn_Click(object sender, EventArgs e)
        {
            var weaponsModel = QUtils.weaponList[weaponSelectDD.SelectedIndex].Values.ElementAt(0);

        }

        private void addBuildingBtn_Click(object sender, EventArgs e)
        {
            var buildingName = QUtils.buildingList[buildingSelectDD.SelectedIndex].Keys.ElementAt(0);
            var buildingModel = QUtils.buildingList[buildingSelectDD.SelectedIndex].Values.ElementAt(0);
            AddBuildingRigidObj(buildingModel);

            if (compileStatus)
                SetStatusText("Buildiing " + buildingName + " added successfully");
        }

        private void removeBuildingBtn_Click(object sender, EventArgs e)
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

        private void addObjectBtn_Click(object sender, EventArgs e)
        {
            var objectRigidModel = QUtils.objectRigidList[objectSelectDD.SelectedIndex].Values.ElementAt(0);
            var objectRigidName = QUtils.objectRigidList[objectSelectDD.SelectedIndex].Keys.ElementAt(0);
            AddBuildingRigidObj(objectRigidModel, true);

            if (compileStatus)
                SetStatusText("Object " + objectRigidName + " added successfully");
        }

        private void removeObjectBtn_Click(object sender, EventArgs e)
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

        private void minimizeBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void customSettingsCb_CheckedChanged(object sender, EventArgs e)
        {
            alphaTxt.Enabled = betaTxt.Enabled = gammaTxt.Enabled = customSettingsCb.Checked;
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
            QUtils.RestoreLevel(level);
            QUtils.ResetFile(level);
            QMemory.RestartLevel(true);
            GenerateRandScriptId(level);
        }

        private void resetAllLevelsBtn_Click(object sender, EventArgs e)
        {
            for (int level = 1; level <= 14; ++level)
            {
                inputQvmPath = QUtils.cfgInputQvmPath + level + "\\" + QUtils.objectsQvm;
                QUtils.RestoreLevel(level);
            }
        }

        private void removeModelBtn_Click(object sender, EventArgs e)
        {

        }

        private void startGameBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int level = Convert.ToInt32(levelStartTxt.Text.ToString());
                if (level <= 0 || level > 14)
                    throw new ArgumentOutOfRangeException("Level should be between [1 - 14]");
                LoadLevelDetails(level);

                QMemory.StartLevel(level, true);
                gameFound = true;
                Thread.Sleep(3000);
                QUtils.RestoreLevel(level);
                QUtils.ResetFile(level);
                GenerateRandScriptId(level);
                QUtils.aiScriptFiles.Clear();

                Thread.Sleep(5000);
                var qscData = QMisc.RemoveCutscene(inputQscPath, level);
                if (!String.IsNullOrEmpty(qscData))
                    compileStatus = QCompiler.CompileEx(qscData);
                QUtils.InjectDllOnStart();
                RefreshUIComponents(level);
            }
            catch (Exception ex)
            {
                QUtils.ShowError(ex.Message ?? ex.StackTrace);
            }
        }

        private void RefreshUIComponents(int level)
        {
            InitUIComponents(level, false);
            UpdateUIComponent(buildingSelectDD, false);
            UpdateUIComponent(objectSelectDD, true);
        }

        private void UpdateUIComponent(ComboBox itemDD, bool itemObj)
        {
            itemDD.DataSource = null;
            itemDD.Items.Clear();
            itemDD.DataSource = itemObj ? QUtils.objectRigidListStr : QUtils.buildingListStr;
            itemDD.Refresh();
        }

        private static void GenerateRandScriptId(int level)
        {
            switch (level)
            {
                case 1:
                    QUtils.randGenScriptId = 510;
                    break;
                case 2:
                    QUtils.randGenScriptId = 450;
                    break;
                case 3:
                    QUtils.randGenScriptId = 563;
                    break;
                case 4:
                    QUtils.randGenScriptId = 630;
                    break;
                case 5:
                    QUtils.randGenScriptId = 500;
                    break;
                case 6:
                    QUtils.randGenScriptId = 1000;
                    break;
                case 7:
                    QUtils.randGenScriptId = 581;
                    break;
                case 8:
                    QUtils.randGenScriptId = 667;
                    break;

            }
        }

        private void AddBuildingRigidObj(string model, bool rigidObj = false)
        {
            var objectPos = QMemory.GetRealPositions();
            string qscData;
            float alpha, beta, gamma;

            if (customSettingsCb.Checked)
            {
                alpha = float.Parse(alphaTxt.Text);
                beta = float.Parse(betaTxt.Text);
                gamma = float.Parse(gammaTxt.Text);

                if (rigidObj)
                {
                    var orientation = new Real32(alpha, beta, gamma);
                    qscData = QObjects.AddRigidObj(model, objectPos, orientation, false);
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
                    qscData = QObjects.AddRigidObj(model, objectPos);
                else
                    qscData = QBuildings.AddBuilding(model, objectPos);
            }

            //Compile the data with QCompiler.
            if (!String.IsNullOrEmpty(qscData))
                compileStatus = QCompiler.Compile(qscData, QUtils.gamePath, true);
        }
    }
}
