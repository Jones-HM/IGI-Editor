﻿using Microsoft.VisualBasic;
using Newtonsoft.Json;
using QLibc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using UXLib.UX;
using static IGIEditor.QUtils;

namespace IGIEditor
{
    public partial class IGIEditorUI : Form
    {
        //Variables section.
        private bool compileStatus = true;
        private List<QUtils.QTask> qtaskList = new List<QUtils.QTask>();
        private int buildingsCount = 0, rigidObjCount = 0;
        private static bool isBuildingDD = false, isObjectDD = true;
        private static string model3d, blenderModel;
        private static int randPosOffset = 0, posOffset = 3000;
        string inputQvmPath, inputQscPath;
        private static int gameLevel = 1, graphId = 1, nodeId = 1;
        private Real64 graphPos, nodeRealPos;
        private GraphNode graphNode;
        private int graphTotalNodes;
        private QServer.QMissionsData missionData;

        static internal IGIEditorUI editorRef;
        BackgroundWorker addGraphNodeWorker, uiRefreshWorker, graphTraverseWorker, nodesTraverseWorker, downloadWorker, uploadWorker;

        //Main-Start - Ctr.
        public IGIEditorUI()
        {
            var editorPosTimer = new System.Windows.Forms.Timer();
            var fileCheckerTimer = new System.Windows.Forms.Timer();
            var internalsFileTimer = new System.Windows.Forms.Timer();

            InitializeComponent();
            UXWorker formMover = new UXWorker();
            formMover.CustomFormMover(formMoverPnl, this);
            editorRef = this;

#if TESTING
            nodesObjectsCb.Visible = nodesHilight.Visible = true;
            compileBtn.Enabled = true;
            appSupportBtn.Visible = false;
            GAME_MAX_LEVEL = 14;
            versionLbl.Text = appEditorVersion + " TEST";
            editorTabs.TabPages.RemoveByKey("threeDEditor");
#else
            editorTabs.TabPages.RemoveByKey("objectEditor");
            editorTabs.TabPages.RemoveByKey("threeDEditor");
            editorTabs.TabPages.RemoveByKey("graphEditor");
            versionLbl.Text = appEditorVersion + " BETA";
#endif

            //Start Position timer.
            editorPosTimer.Tick += new EventHandler(UpdatePositionTimer);
            editorPosTimer.Interval = 500;
            editorPosTimer.Start();

            //Start File Integrity timer.
            fileCheckerTimer.Tick += new EventHandler(FileIntegrityCheckerTimer);
            fileCheckerTimer.Interval = 120000;
            fileCheckerTimer.Start();

            //Start File Integrity timer.
            internalsFileTimer.Tick += new EventHandler(InternalsAttachedTimer);
            internalsFileTimer.Interval = 15000;//120000;
            internalsFileTimer.Start();


            //Adding Background Worker threads
            addGraphNodeWorker = new BackgroundWorker();
            uiRefreshWorker = new BackgroundWorker();
            graphTraverseWorker = new BackgroundWorker();
            nodesTraverseWorker = new BackgroundWorker();
            downloadWorker = new BackgroundWorker();
            uploadWorker = new BackgroundWorker();

            addGraphNodeWorker.DoWork += AddGraphNodesBackground;
            uiRefreshWorker.DoWork += UpdateUIBackground;
            graphTraverseWorker.DoWork += GraphTraverseBackground;
            nodesTraverseWorker.DoWork += NodeTraverseBackground;
            downloadWorker.DoWork += DownloadBackground;
            uploadWorker.DoWork += UploadBackground;

            graphTraverseWorker.WorkerSupportsCancellation = nodesTraverseWorker.WorkerSupportsCancellation = true;

            //Disabling Errors and Warnings.
            GT.GT_SuppressErrors(true);
            GT.GT_SuppressWarnings(true);

            //Get Game level from start.
            gameLevel = Convert.ToInt32(levelStartTxt.Text.ToString());

            //Initialize application data and paths.
            InitEditorApp();
            InitEditorPaths(gameLevel);

            //FileInegrity.GenerateDirHashes(new List<string> { QUtils.cfgAiPath, QUtils.cfgQFilesPath, QUtils.cfgVoidPath });
            //QUtils.ShowInfo("Hashes generated");

            //QCryptor.Encrypt(QUtils.internalDllPath);
            //return;

            if (QMemory.FindGame())
            {
                QUtils.gameFound = true;
                gameLevel = QUtils.gGameLevel = QMemory.GetCurrentLevel();
                if (gameLevel < 0 || gameLevel > GAME_MAX_LEVEL) gameLevel = 1;
            }
            else
            {
                QMemory.StartLevel(gameLevel, true);
                QUtils.gameFound = true;
                Thread.Sleep(3000);

                if (autoResetCb.Checked)
                {
                    QUtils.RestoreLevel(gameLevel);
                    QUtils.ResetFile(gameLevel);
                }

                GenerateAIScriptId(gameLevel);
                QUtils.aiScriptFiles.Clear();
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
            if (QUtils.gameFound)
            {
                gameLevel = QMemory.GetCurrentLevel();
                if (gameLevel < 0 || gameLevel > GAME_MAX_LEVEL) gameLevel = 1;
                levelStartTxt.Text = gameLevel.ToString();
                SetStatusText("Game running...");
                LoadLevelDetails(gameLevel);
#if TESTING
                QMemory.UpdateHumanHealth(false);
#endif
            }
            else
                SetStatusText("Game not running...");

            //Init Dropdown,List UI components.
            InitUIComponents(gameLevel);
        }

        private void InternalsAttachedTimer(object sender, EventArgs e)
        {
            QUtils.gameFound = QMemory.FindGame();

            if (!gameFound)
                startGameBtn_Click(sender, e);
            

            var internalsAttached = QUtils.CheckInternalsAttached();

            if (!internalsAttached)
            {
                SetStatusText("Internals not attached Attaching now....");
                QUtils.AddLog("Internals were not attached with the game Attaching internals now.");
                QUtils.attachStatus = QUtils.AttachInternals();
                if (QUtils.attachStatus)
                    SetStatusText("Internals attached success....");
            }

        }

        private void UploadBackground(object sender, DoWorkEventArgs e)
        {
            try
            {
                OpenFileDialog fileDlg = new OpenFileDialog();
                fileDlg.Filter = "IGI 1 Mission files (*.igimsf)|*.igimsf|All files (*.*)|*.*";
                fileDlg.RestoreDirectory = true;
                fileDlg.Title = "Select IGI Mission file";
                fileDlg.DefaultExt = ".igimsf";
                fileDlg.Multiselect = false;
                fileDlg.CheckFileExists = fileDlg.RestoreDirectory = fileDlg.AddExtension = true;
                string missionNamePath = null, missionName = null;

                DialogResult resultDlg = DialogResult.OK;
                Invoke((Action)(() => { resultDlg = fileDlg.ShowDialog(); }));

                if (resultDlg == DialogResult.OK)
                {
                    SetStatusText("Mission Uploading...");
                    missionNamePath = fileDlg.FileName;
                    missionName = Path.GetFileName(missionNamePath);

                    string missionUrl = "/" + QServer.serverBaseURL + QServer.missionDir + "/" + missionName;
                    //ShowInfo("Mission Path: " + missionUrl + "\nmissionNamePath: " + missionNamePath);

                    var missionAuthor = QUtils.GetCurrentUserName().Replace(" ", "-");
                    var missionFullName = Path.GetFileNameWithoutExtension(missionName) + "@" + missionAuthor + "#" + gameLevel + missionExt;
                    missionFullName = HttpUtility.UrlEncode(missionFullName);
                    string missionUrlPath = "/" + QServer.missionDir + "/" + missionFullName;

                    QUtils.AddLog("UploadMission(): Url: '" + missionUrl + "' file '" + missionNamePath + "'");
                    var status = QServer.Upload(missionUrlPath, missionNamePath);
                    if (status) SetStatusText("Mission was uploaded successfully.");
                }
            }
            catch (Exception ex)
            {
                QUtils.ShowError(ex.Message ?? ex.StackTrace);
            }
        }

        private void DownloadBackground(object sender, DoWorkEventArgs e)
        {
            try
            {
                SetStatusText("Mission Downloading...");
                string missionUrl = QServer.serverBaseURL + QServer.missionDir;
                string missionName = missionData.MissionName + "@" + missionData.MissionAuthor + "#" + missionData.MissionLevel;

                string missionEscapeUri = HttpUtility.UrlEncode(missionName) + missionExt;
                missionUrl = missionUrl + "/" + missionEscapeUri;
                string missionNamePath = missionsOnlineDD.Text + missionExt;
                string missionUrlPath = "/" + QServer.missionDir + "/" + missionEscapeUri;

                QUtils.AddLog("DownloadMission(): Url: '" + missionUrl + "' file '" + missionNamePath + "'");
                var status = QServer.Download(missionUrlPath, missionNamePath, QUtils.qMissionsPath);
                if (status) SetStatusText("Mission was downloaded successfully.");
            }
            catch (Exception ex)
            {
                QUtils.ShowError(ex.Message ?? ex.StackTrace);
            }
        }

        private void GraphTraverseBackground(object sender, DoWorkEventArgs e)
        {
            if (((BackgroundWorker)sender).CancellationPending && ((BackgroundWorker)sender).IsBusy)
            {
                e.Cancel = true;
                return;
            }
            SetStatusText("Traversing Graphs now...");
            foreach (var graphId in aiGraphIdStr)
            {
                if (autoTeleportGraphCb.Checked)
                {
                    var graphPos = QGraphs.GetGraphPosition(graphId);
                    QUtils.UpdateViewPort(graphPos);
                    string graphArea = QGraphs.GetGraphArea(graphId);
                    QInternals.StatusMessageShow("Graph Area " + graphArea);
                    QUtils.Sleep(10);
                }
                else break;
            }
            QInternals.StatusMessageShow("Graph #" + graphId + " nodes traversed.");
            viewPortEnableCb.Checked = false;
        }

        private void NodeTraverseBackground(object sender, DoWorkEventArgs e)
        {
            if (((BackgroundWorker)sender).CancellationPending && ((BackgroundWorker)sender).IsBusy)
            {
                e.Cancel = true;
                return;
            }
            string graphArea = null;
            SetStatusText("Traversing Nodes now...");
            foreach (var nodeId in QUtils.aiGraphNodeIdStr)
            {
                if (autoTeleportNodeCb.Checked)
                {
                    var graphNodeData = QGraphs.GetGraphNodeData(graphId, nodeId);
                    //Get Node Real Position.
                    var nodePos = new Real64();
                    nodePos.x = graphPos.x + graphNodeData.NodePos.x;
                    nodePos.y = graphPos.y + graphNodeData.NodePos.y;
                    nodePos.z = graphPos.z + graphNodeData.NodePos.z + QUtils.viewPortDelta;

                    QUtils.UpdateViewPort(nodePos);
                    graphArea = QGraphs.GetGraphArea(graphId);
                    QInternals.StatusMessageShow("Graph " + graphArea + " Node #" + nodeId);
                    QUtils.Sleep(8.5f);
                }
                else break;
            }
            QInternals.StatusMessageShow("Graph #" + graphId + " Area " + graphArea + " traversed.");
            viewPortEnableCb.Checked = false;
        }

        private void UpdateUIBackground(object sender, DoWorkEventArgs e)
        {
            RefreshUIComponents(gameLevel);
        }

        private void AddGraphNodesBackground(object sender, DoWorkEventArgs e)
        {
            if (((BackgroundWorker)sender).CancellationPending && ((BackgroundWorker)sender).IsBusy)
            {
                e.Cancel = true;
                return;
            }

            string qscData = null;
            if (nodesObjectsCb.Checked)
            {
                var objModel = QUtils.objectRigidList[objectSelectDD.SelectedIndex].Values.ElementAt(0);
                qscData = QGraphs.ShowGraphNodesVisual(graphId, 1, objModel);
                nodesObjectsCb.Checked = false;
            }
            else if (nodesHilight.Checked)
            {
                qscData = QGraphs.ShowGraphNodesVisual(graphId, 2);
                nodesHilight.Checked = false;
            }

            qscData = QUtils.LoadFile() + "\n" + qscData;
            compileStatus = QCompiler.CompileEx(qscData);

            if (compileStatus) SetStatusText("Graph Nodes Visualisation added successfully");
        }

        private void FileIntegrityCheckerTimer(object sender, EventArgs e)
        {
            FileInegrity.RunFileInegrityCheck(null, new List<string> { QUtils.cfgAiPath, QUtils.cfgQFilesPath, QUtils.cfgVoidPath });
        }

        private void UpdatePositionTimer(object sender, EventArgs e)
        {
            //this.Enabled = QMemory.FindGame();

            if (QUtils.gameFound)
            {
                if (posMetersCb.Checked)
                {
                    var meterPos = (editorModeCb.Checked) ? QUtils.GetViewPortPos() : QHuman.GetPositionInMeter(false);
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
            weaponDD.Items.Clear();
            aiWeaponDD.Items.Clear();

            QUtils.weaponList = QHuman.GetWeaponsList();
            foreach (var weapon in QUtils.weaponList)
            {
                var weaponName = weapon.Keys.ElementAt(0);
                weaponDD.Items.Add(weaponName);
                aiWeaponDD.Items.Add(weaponName);
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
            QUtils.aiGraphNodeIdStr.Clear();

            var graphIdList = QGraphs.GetGraphIds(level);

            foreach (var graphId in graphIdList)
            {
                //var nodesList = QGraphs.GetAllNodes4mGraph(graphId);
                aiGraphIdStr.Add(graphId);
                if (initialInit) aiGraphIdDD.Items.Add(graphId);

                ////Remove Cutscenes Graph.
                //if (nodesList != null)
                //{
                //    if (nodesList.Count > 1)
                //    {
                //        aiGraphIdStr.Add(graphId);
                //        if (initialInit) aiGraphIdDD.Items.Add(graphId);
                //    }
                //}
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
            buildingSelectDD.SelectedIndex = objectSelectDD.SelectedIndex = aiGraphIdDD.SelectedIndex = 0;

            // Init Online Missions list.
            InitMissionsOnline(initialInit, true, false);
        }

        internal void InitMissionsOnline(bool initialInit, bool useCache = true, bool showWarning = true)
        {
            //Init Online Missions list.
            if (!QUtils.editorOnline)
            {
                if (showWarning) QUtils.ShowWarning("Editor is not in online mode to get missions list from server.");
                downloadMissionBtn.Enabled = uploadMissionBtn.Enabled = false;
                return;
            }

            //if (missionNameListStr.Count == 0) return;

            missionNameListStr.Clear();
            List<QServer.QMissionsData> missionsData;
            if (File.Exists(QUtils.missionListFile) && useCache)
                missionsData = QSerialize.ReadFromBinaryFile<List<QServer.QMissionsData>>(QUtils.missionListFile);
            else
                missionsData = QServer.GetMissionsData(useCache);

            foreach (var mission in missionsData)
            {
                missionNameListStr.Add(mission.MissionName);
                if (initialInit) missionsOnlineDD.Items.Add(mission.MissionName);
            }
            if (!File.Exists(QUtils.missionListFile) || useCache) QSerialize.WriteToBinaryFile(QUtils.missionListFile, missionsData);
            QUtils.qServerMissionDataList = missionsData;
            missionsOnlineDD.SelectedIndex = 0;
        }

        private void InitEditorApp()
        {
            //Read paths from config.
            QUtils.ParseConfig();

            //Initialize app data for QEditor.
            QUtils.InitAppData();

            //Initialize Lib/Bin setting and modules.
            QUtils.InitLibBin();

            string userName = QUtils.GetCurrentUserName();
            string keyFileAbsPath = QUtils.igiEditorQEdPath + Path.DirectorySeparatorChar + QUtils.projAppName + "Key.txt";
            string deviceKey = QUtils.GetMachineDeviceId();
            string inputKey = null, welcomeMsg = "Welcome " + userName + " to IGI 1 Editor";

            //Setting Options.
            appLogsCb.Checked = appLogs;
            autoResetCb.Checked = gameReset;
            connectionCb.Checked = editorOnline;

            bool initUser;
            //Check if user key exist.
            keyFileExist = File.Exists(keyFileAbsPath);

            //Initialize user info - Offline.
            if (!editorOnline) initUser = true;

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

            if (!editorOnline) QUtils.keyExist = (inputKey == deviceKey);

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
            var humanplayer_abs_path = game_abs_path + Path.DirectorySeparatorChar + QUtils.humanplayerPath + Path.DirectorySeparatorChar + QUtils.humanplayerQvm;
            if (File.Exists(QUtils.humanplayerQvm) && !File.Exists(humanplayer_abs_path))
                File.Move(Path.GetFullPath(QUtils.humanplayerQvm), humanplayer_abs_path);

            //Creates shortcut of game in current directory.
            QUtils.CreateGameShortcut();

        }
        delegate void SetTextCallback(string text);

        internal void SetStatusText(string text)
        {
            try
            {
                if (this.statusLbl.InvokeRequired)
                {
                    var d = new SetTextCallback(SetStatusText);
                    this.Invoke(d, new object[] { text });
                }
                else
                {
                    statusLbl.Text = null;
                    statusLbl.Text = text;
                }
            }
            catch (Exception)
            {
                statusLbl.Text = text;
            }
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


        private void InitEditorPaths(int gameLevel)
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
                QUtils.WebDownload(imgUrl, imgPath, QUtils.cachePathAppImages);
                LoadImgBoxWeb(imgUrl, levelImgBox);
            }
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void addWeaponBtn_Click(object sender, EventArgs e)
        {
            //if (!editorModeCb.Checked && !liveEditorCb.Checked)
            //{
            //    var result = QUtils.ShowEditModeDialog();
            //    if (result) editorModeCb.Checked = true;
            //    else return;
            //}

            string weaponName = null, weaponModel = null, weaponStatus = null;
            int weaponIndex = 0;
            if (liveEditorCb.Checked)
            {
                weaponIndex = weaponList[weaponDD.SelectedIndex].Values.ElementAt(0);
                weaponName = weaponList[weaponDD.SelectedIndex].Keys.ElementAt(0);
                QInternals.WeaponPickup(weaponIndex.ToString());
                weaponStatus = "Weapon " + weaponName + " added successfully.";
            }
            else
            {
                weaponModel = QUtils.weaponId + weaponList[weaponDD.SelectedIndex].Keys.ElementAt(0);
                weaponName = weaponList[weaponDD.SelectedIndex].Keys.ElementAt(0);
                int weaponAmmo = 999;
                if (!String.IsNullOrEmpty(weaponAmmoTxt.Text))
                    weaponAmmo = Convert.ToInt32(weaponAmmoTxt.Text);

                string qscData = QHuman.AddWeapon(weaponModel, weaponAmmo, true);
                if (!String.IsNullOrEmpty(qscData))
                    compileStatus = QCompiler.Compile(qscData, QUtils.gamePath, false, false);

                if (compileStatus) weaponStatus = "Weapon " + weaponName + " added successfully";
            }
            QInternals.StatusMessageShow(weaponStatus);
            SetStatusText(weaponStatus);
        }

        private void addBuildingBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!editorModeCb.Checked && !liveEditorCb.Checked)
                {
                    var result = QUtils.ShowEditModeDialog();
                    if (result) editorModeCb.Checked = true;
                    else return;
                }

                var buildingName = QUtils.buildingList[buildingSelectDD.SelectedIndex].Keys.ElementAt(0);
                var buildingModel = QUtils.buildingList[buildingSelectDD.SelectedIndex].Values.ElementAt(0);
                var buildingPos = QUtils.GetViewPortPos(); //QHuman.GetPositionInMeter();

                bool hasOrientation = String.IsNullOrEmpty(alphaTxt.Text) && String.IsNullOrEmpty(betaTxt.Text) && String.IsNullOrEmpty(gammaTxt.Text);
                if (buildingPos.x != 0.0f || buildingPos.y != 0.0f)
                {
                    AddObject(buildingModel, false, buildingPos, !hasOrientation);
                    if (compileStatus) SetStatusText("Buildiing " + buildingName + " added successfully");
                    string buildingInfoMsg = buildingName + " Model: " + buildingModel + " Added.";
                    QInternals.StatusMessageShow(buildingInfoMsg);
                }
                else SetStatusText("Error: Buildiing positions are invalid.");
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
                //QUtils.ShowInfo(buildingModel);
                //GT.GT_WriteMemory((IntPtr)0x00401043, "string", buildingModel+'\0');
                //Thread.Sleep(1000);
                //GT.GT_SendKeyStroke("r", true, true, false);

                //return;

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
                if (!editorModeCb.Checked && !liveEditorCb.Checked)
                {
                    var result = QUtils.ShowEditModeDialog();
                    if (result) editorModeCb.Checked = true;
                    else return;
                }


                var objectRigidModel = QUtils.objectRigidList[objectSelectDD.SelectedIndex].Values.ElementAt(0);
                var objectRigidName = QUtils.objectRigidList[objectSelectDD.SelectedIndex].Keys.ElementAt(0);
                var objectPos = QUtils.GetViewPortPos();//QHuman.GetPositionInMeter();
                bool hasOrientation = String.IsNullOrEmpty(alphaTxt.Text) && String.IsNullOrEmpty(betaTxt.Text) && String.IsNullOrEmpty(gammaTxt.Text);

                if (objectPos.x != 0.0f || objectPos.y != 0.0f)
                {
                    AddObject(objectRigidModel, true, objectPos, !hasOrientation);
                    if (compileStatus) SetStatusText("Object " + objectRigidName + " added successfully");
                }
                else SetStatusText("Error: Object position is invalid.");

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Index was out of range"))
                    QUtils.ShowError("No Building or Object have been selected to perfom this operation.");
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
            if (level < 0 || level > GAME_MAX_LEVEL) level = 1;
            QUtils.RestoreLevel(level);
            QUtils.ResetFile(level);
            QMemory.RestartLevel(true);
            CleanUpAiFiles();
            GenerateAIScriptId(level);
        }

        private void resetAllLevelsBtn_Click(object sender, EventArgs e)
        {
            for (int level = 1; level <= GAME_MAX_LEVEL; ++level)
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
            QUtils.gameFound = QMemory.FindGame();
            if (!QUtils.gameFound)
            {
                QMemory.StartGame();
                SetStatusText("Game not found! starting new game");
                QUtils.Sleep(8);

                QUtils.gGameLevel = gameLevel = QMemory.GetCurrentLevel();
                levelStartTxt.Text = gameLevel.ToString();
                InitEditorPaths(QUtils.gGameLevel);
                QUtils.gameFound = true;
            }
            else
                SetStatusText("Game found success");

            if (QUtils.gameFound)
            {
                QUtils.gGameLevel = gameLevel = QMemory.GetCurrentLevel();
                LoadLevelDetails(gameLevel);
                RefreshUIComponents(gameLevel);
                CleanUpAiFiles();
                InitEditorPaths(gameLevel);

                if (autoResetCb.Checked)
                    QUtils.ResetFile(gameLevel);

                if (!QUtils.attachStatus)
                    QUtils.AttachInternals();

                QUtils.graphAreas.Clear();
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

                double xpos, ypos, zpos;
                string qscData = null;

                if (posCurrentCb.Checked)
                {
                    var meterPos = QUtils.GetViewPortPos(); //QHuman.GetPositionInMeter();

                    xPosObjTxt.Text = meterPos.x.ToString();
                    yPosObjTxt.Text = meterPos.y.ToString();
                    zPosObjTxt.Text = meterPos.z.ToString();
                }

                xpos = Convert.ToDouble(xPosObjTxt.Text);
                ypos = Convert.ToDouble(yPosObjTxt.Text);
                zpos = Convert.ToDouble(zPosObjTxt.Text);

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
            if (humanPosOffCb.Checked) humanPosMeterCb.Checked = resetPosCb.Checked = false;
        }

        private void humanPosMeterCb_CheckedChanged(object sender, EventArgs e)
        {
            if (humanPosMeterCb.Checked) humanPosOffCb.Checked = resetPosCb.Checked = false;
        }

        private void updateHumaPosition_Click(object sender, EventArgs e)
        {
            try
            {
                string qscData = null;

                var humanPos = new Real64();
                if (resetPosCb.Checked)
                {
                    humanPos = QHuman.GetHumanTaskList(true).qtask.position;
                    xPosTxt_H.Text = humanPos.x.ToString();
                    yPosTxt_H.Text = humanPos.y.ToString();
                    zPosTxt_H.Text = humanPos.z.ToString();
                }
                else
                {
                    Double xpos = Convert.ToDouble(xPosTxt_H.Text);
                    Double ypos = Convert.ToDouble(yPosTxt_H.Text);
                    Double zpos = Convert.ToDouble(zPosTxt_H.Text);
                    humanPos = new Real64(xpos, ypos, zpos);
                }

                if (humanPosOffCb.Checked) qscData = QHuman.UpdatePositionOffset(humanPos);

                else if (humanPosMeterCb.Checked || resetPosCb.Checked) qscData = QHuman.UpdatePositionInMeter(humanPos);

                if (!string.IsNullOrEmpty(qscData)) compileStatus = QCompiler.Compile(qscData, QUtils.gamePath, false, true, false);

                if (compileStatus) SetStatusText("Human position updated successfully");
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
            PopulateWeaponDD(weaponDD.SelectedIndex, weaponImgBox);
        }

        private void PopulateWeaponDD(int index, PictureBox imgBox)
        {
            string weaponModel = null, weaponName = null, imgUrl = null, imgPath = null;
            try
            {
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
                    QUtils.WebDownload(imgUrl, imgPath, QUtils.cachePathAppImages);
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
                    QUtils.WebDownload(imgUrl, imgPath, QUtils.cachePathAppImages);
                }
                else
                    imgBox.Image = null;
            }
        }

        private void removeWeaponBtn_Click(object sender, EventArgs e)
        {
            if (!editorModeCb.Checked && !liveEditorCb.Checked)
            {
                var result = QUtils.ShowEditModeDialog();
                if (result) editorModeCb.Checked = true;
                else return;
            }

            var weaponModel = QUtils.weaponId + QUtils.weaponList[weaponDD.SelectedIndex].Keys.ElementAt(0);
            var weaponName = weaponList[weaponDD.SelectedIndex].Keys.ElementAt(0);
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
            string cacheDlg = "Clearing cache will delete all resources/data application uses.\nDo you want to continue?";
            var dlgResult = QUtils.ShowDialog(cacheDlg);

            if (dlgResult == DialogResult.Yes)
            {
                if (Directory.Exists(QUtils.cachePath))
                {
                    QUtils.DeleteWholeDir(QUtils.cachePath);
                    SetStatusText("Application cache cleared.");
                }
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
            if (e.TabPage.Name == "levelEditor")
            {
                try
                {
                    buildingSelectDD.SelectedIndex = objectSelectDD.SelectedIndex = 0;
                }
                catch (Exception ex) { }
            }

            //Object Editor
            if (e.TabPage.Name == "objectEditor")
            {
                try
                {
                    SetStatusText(e.TabPage.Name.ToLower() + " supports live editor features.");
                    qtaskList = QUtils.GetQTaskList(false, false, true);

                    buildingsCount = qtaskList.Count(o => o.name.Contains("Building"));
                    rigidObjCount = qtaskList.Count(o => o.name.Contains("EditRigidObj"));

                    maxItemsLbl1.Text = maxItemsLbl2.Text = maxItemsLbl3.Text = maxItemsLbl4.Text = null;
                    maxItemsLbl1.Text = maxItemsLbl3.Text = "Max Items :" + Convert.ToString(rigidObjCount);
                    maxItemsLbl2.Text = maxItemsLbl4.Text = "Max Items :" + Convert.ToString(buildingsCount);
                }
                catch (Exception ex) { }
            }

            //A.I Editor.
            if (e.TabPage.Name == "aiEditor")
            {
                try
                {
                    UpdateUIComponent(aiGraphIdDD, QUtils.aiGraphIdStr);
                    aiTypeDD.SelectedIndex = aiModelSelectDD.SelectedIndex = aiWeaponDD.SelectedIndex = 0;
                }
                catch (Exception) { }
            }

            //Weapon Editor.
            if (e.TabPage.Name == "weaponEditor")
            {
                try
                {
                    weaponDD.SelectedIndex = 0;
                    SetStatusText(e.TabPage.Name.ToLower() + " supports live editor features.");
                }
                catch (Exception) { }
            }


            //Graph Editor.
            if (e.TabPage.Name == "graphEditor")
            {
                try
                {
                    UpdateUIComponent(graphIdDD, QUtils.aiGraphIdStr);
                    UpdateUIComponent(nodeIdDD, QUtils.aiGraphNodeIdStr);
                    graphIdDD.SelectedIndex = nodeIdDD.SelectedIndex = 0;
                }
                catch (Exception) { }
            }

            //Position Editor
            if (e.TabPage.Name == "positionEditor")
            {
                try
                {
                    UpdateUIComponent(buildingPosDD, QUtils.buildingListStr);
                    UpdateUIComponent(objectPosDD, QUtils.objectRigidListStr);
                }
                catch (Exception) { }
            }

            //MissionEditor Editor
            if (e.TabPage.Name == "missionEditor")
            {
                //if (editorOnline) UpdateUIComponent(missionsOnlineDD, QUtils.missionNameListStr);
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
            QInternals.RestartLevel();

            //Genrate random scriptId according to Level A.I.
            GenerateAIScriptId(QUtils.gGameLevel);
        }

        private void clearAllLevelBtn_Click(object sender, EventArgs e)
        {
            var voidLevelPath = QUtils.cfgVoidPath + "\\objects_void_" + QUtils.gGameLevel + QUtils.qscExt;

            if (File.Exists(QUtils.objectsQsc))
                File.Delete(QUtils.objectsQsc);
            File.Copy(voidLevelPath, QUtils.objectsQsc);

            var qscData = QCryptor.Decrypt(QUtils.objectsQsc);
            File.WriteAllText(QUtils.objectsQsc, qscData);
            QUtils.levelFlowData = File.ReadLines(QUtils.objectsQsc).Last();
            compileStatus = QCompiler.CompileEx(qscData);

            if (compileStatus) SetStatusText("Level cleared out success");
        }


        //Testing phase - remove in final build
        private void compileBtn_Click(object sender, EventArgs e)
        {
            //string qscData = QUtils.LoadFile();
            //if (!String.IsNullOrEmpty(qscData)) compileStatus = QCompiler.Compile(qscData, QUtils.gamePath, false, false, false);

            //string file = "LOCAL:missions/location0/level" + gameLevel + "//objects.qsc";
            string qscData = QUtils.LoadFile();
            QCompiler.CompileEx(qscData);

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
                    AddObject(buildingModel, false, qtask.position, true, -1, qtask.note, true, qtask.orientation.alpha, qtask.orientation.beta, qtask.orientation.gamma);
                    qTaskCount++;
                }
            }

            if (compileStatus)
                SetStatusText(itemsCount + " Buildings reset success");
        }

        private void cutsceneRemoveBtn_Click(object sender, EventArgs e)
        {
            var qscData = QMisc.RemoveCutscene();
            if (!String.IsNullOrEmpty(qscData)) compileStatus = QCompiler.CompileEx(qscData);

            if (compileStatus) SetStatusText("Cutscene removed success.");
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
                QInternals.StatusMessageShow("Humanplayer speed updated.");
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
                QInternals.StatusMessageShow("Humanplayer health updated.");
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
                QInternals.StatusMessageShow("Humanplayer peek updated.");
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

            QInternals.HumanplayerLoad();
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
                QInternals.HumanplayerLoad();
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
                    AddObject(rigidModel, true, qtask.position, true, -1, qtask.note, true, qtask.orientation.alpha, qtask.orientation.beta, qtask.orientation.gamma);
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
                var aiModelQualifyName = aiModelName.Contains("_") ? aiModelName.Substring(0, aiModelName.IndexOf("_")) : aiModelName;
                aiModelNameLbl.Text = aiModelQualifyName;

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
                    QUtils.WebDownload(imgUrl, imgPath, QUtils.cachePathAppImages);
                }
            }
            catch (Exception ex)
            {
                aiImgBox.Image = null;
            }
        }

        private void weaponAiDD_SelectedValueChanged(object sender, EventArgs e)
        {
            PopulateWeaponDD(aiWeaponDD.SelectedIndex, weaponAIImgBox);
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
            if (aiGraphIdDD.SelectedIndex == -1) aiGraphIdDD.SelectedIndex = 0;
            int graphAiId = QUtils.aiGraphIdStr[aiGraphIdDD.SelectedIndex];

            string graphArea = QGraphs.GetGraphArea(graphAiId);
            graphAreaAiLbl.Text = graphArea;
            QUtils.AddLog("aiGraphIdDD() GraphId : " + graphId + " GraphArea: " + graphArea);
        }

        private void addAiBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!editorModeCb.Checked && !liveEditorCb.Checked)
                {
                    var result = QUtils.ShowEditModeDialog();
                    if (result) editorModeCb.Checked = true;
                    else return;
                }

                var aiModelName = QAI.GetAiModelNamesList(gameLevel)[aiModelSelectDD.SelectedIndex];
                var aiModelId = QAI.GetAiModelIdForName(aiModelName);
                var aiWeaponModel = QUtils.weaponId + QUtils.weaponList[aiWeaponDD.SelectedIndex].Keys.ElementAt(0);
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
                    if (String.IsNullOrEmpty(qscData))
                    {
                        SetStatusText("Error: Adding " + aiModelName + " A.I to level.");
                        return;
                    }

                    qscData = QAI.AddAiTaskDetection(qscData);
                    if (!String.IsNullOrEmpty(qscData))
                        compileStatus = QCompiler.Compile(qscData, QUtils.gamePath, true, true); ;

                    if (compileStatus) SetStatusText("AI " + aiModelName + " Added successfully");
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                SetStatusText("A.I data cannot be empty.");
            }
            catch (Exception ex)
            {
                QUtils.ShowError("Exception: " + ex.Message ?? ex.StackTrace, "A.I ERROR");
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
            //QInternals.ResourceSaveInfo();
        }

        private void connectionCb_CheckedChanged(object sender, EventArgs e)
        {
            if ((((CheckBox)sender).Checked))
            {
                if (QUtils.IsNetworkAvailable())
                {
                    editorOnline = true;
                    connectionCb.Text = "Online";
                    connectionCb.ForeColor = Color.Green;
                    registeredUsersLbl.Visible = true;
                    downloadMissionBtn.Enabled = uploadMissionBtn.Enabled = missionsOnlineDD.Enabled = missionRefreshBtn.Enabled = true;
                    registeredUsersLbl.Text = "Users: " + QUtils.GetRegisteredUsers();
                    InitMissionsOnline(true);
                    SetStatusText("Editor online mode enabled...");
                }
                else
                {
                    editorOnline = false;
                    connectionCb.Text = "Offline";
                    connectionCb.ForeColor = Color.Red;
                    registeredUsersLbl.Visible = false;
                    (((CheckBox)sender).Checked) = false;
                    downloadMissionBtn.Enabled = uploadMissionBtn.Enabled = missionsOnlineDD.Enabled = missionRefreshBtn.Enabled = false;
                    SetStatusText("Please check your internet connection.");
                }
            }
            else
            {
                editorOnline = false;
                connectionCb.Text = "Offline";
                connectionCb.ForeColor = Color.Red;
                registeredUsersLbl.Visible = false;
                downloadMissionBtn.Enabled = uploadMissionBtn.Enabled = missionsOnlineDD.Enabled = missionRefreshBtn.Enabled = false;
                SetStatusText("Editor offline mode enabled...");
            }
        }

        private void removeModelBtn_Click(object sender, EventArgs e)
        {
            if (!editorModeCb.Checked && !liveEditorCb.Checked)
            {
                var result = QUtils.ShowEditModeDialog();
                if (result) editorModeCb.Checked = true;
                else return;
            }

            if (liveEditorCb.Checked)
            {
                var modelId = objectIDTxt.Text;
                QInternals.MEF_ModelRemove(modelId);
                var modelName = QObjects.FindModelName(modelId);
                if (!String.IsNullOrEmpty(modelName)) objectIDLbl.Text = modelName;
                SetStatusText("Object " + modelName + " removed successfully");
            }
            else
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
        }

        private void restoreModelBtn_Click(object sender, EventArgs e)
        {
            if (!editorModeCb.Checked && !liveEditorCb.Checked)
            {
                var result = QUtils.ShowEditModeDialog();
                if (result) editorModeCb.Checked = true;
                else return;
            }
            if (liveEditorCb.Checked)
            {
                var modelId = objectIDTxt.Text;
                QInternals.MEF_ModelRestore();
                var modelName = QObjects.FindModelName(modelId);
                if (!String.IsNullOrEmpty(modelName)) objectIDLbl.Text = modelName;
                SetStatusText("Object " + modelName + " restored successfully");
            }
        }

        private void startGameBtn_Click(object sender, EventArgs e)
        {
            try
            {
                gameLevel = Convert.ToInt32(levelStartTxt.Text.ToString());
                InitEditorPaths(gameLevel);
                QUtils.graphAreas.Clear();
                CleanUpAiFiles();
                if (liveEditorCb.Checked)
                {
                    QUtils.gameFound = QMemory.FindGame();
                    if (QUtils.gameFound)
                        QInternals.StartLevel(gameLevel.ToString());
                    else
                    {
                        QUtils.ShowWarning("Live Editor - Error starting level game not running.");
                        StartGameLevel(gameLevel, true);
                    }
                }
                else
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

        private void showNodesBtn_Click(object sender, EventArgs e)
        {
#if TESTING
            if (nodesObjectsCb.Checked || nodesHilight.Checked)
            {
                SetStatusText("Adding Nodes Visualisation....Please Wait");
                addGraphNodeWorker.RunWorkerAsync();
            }
            else if (!nodesObjectsCb.Checked || !nodesHilight.Checked)
            {
                ShowError("Showing Nodes visual needs type to be selected first.");
            }
#endif
        }

        private void graphIdDD_SelectedIndexChanged(object sender, EventArgs e)
        {
            graphId = QUtils.aiGraphIdStr[graphIdDD.SelectedIndex];
            graphPos = QGraphs.GetGraphPosition(graphId);

            aiGraphNodeIdStr = QGraphs.GetNodesForGraph(graphId);
            graphTotalNodes = aiGraphNodeIdStr.Count;

            //Update Graph Nodes.
            UpdateUIComponent(nodeIdDD, QUtils.aiGraphNodeIdStr);

            string graphArea = QGraphs.GetGraphArea(graphId);
            graphAreaLbl.Text = graphArea;
            graphTotalNodesTxt.Text = graphTotalNodes.ToString();
            QUtils.AddLog("graphIdDD() GraphId : " + graphId + " GraphArea: " + graphArea + " TotalNodes: " + graphTotalNodes);
        }

        private void viewPortEnableCb_CheckedChanged(object sender, EventArgs e)
        {
            unsafe
            {
                IntPtr viewPortAddr = (IntPtr)0x00497E94;

                if (viewPortEnableCb.Checked)
                {
                    GT.GT_WriteNOP(viewPortAddr, 2);
                    QInternals.HumanInputDisable();
                    viewPortEnableCb.Text = "ViewPort - Enabled";
                }
                else
                {
                    GT.GT_WriteMemory(viewPortAddr, "2bytes", "42483");
                    QInternals.HumanInputEnable();
                    viewPortEnableCb.Text = "ViewPort - Disabled";
                }

            }
        }

        private void addLevelFlowBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (QUtils.anyaTeamTaskId == -1)
                {
                    QUtils.ShowError("You need to add objects (A.I/Buildings) to your custom level first");
                }
                else
                {
                    if (String.IsNullOrEmpty(QUtils.levelFlowData)) throw new Exception("Mission levelflow data is empty. Couldn't add custom level");

                    if (String.IsNullOrEmpty(missionPlayTimeTxt.Text)) throw new ArgumentNullException("Play time must be entered in correct format(Seconds).");

                    float maxPlayTime = float.Parse(missionPlayTimeTxt.Text);
                    bool enableTimer = missionLevelFlowTimerCb.Checked;

                    //Add Complete and Failed mission tasks.
                    string completeMissionTask = "StatusMessage_" + QUtils.ekkTeamTaskId + ".isSendt";
                    string failedMissionTask = "StatusMessage_" + QUtils.anyaTeamTaskId + ".isSendt";

                    var qscData = QUtils.LoadFile();
                    var levelFlowTask = QMission.AddLevelFlow(completeMissionTask, failedMissionTask, maxPlayTime, enableTimer);
                    qscData = qscData.Replace(QUtils.levelFlowData, levelFlowTask);
                    if (!String.IsNullOrEmpty(qscData))
                        compileStatus = QCompiler.CompileEx(qscData);
                }
            }
            catch (Exception ex)
            {
                QUtils.ShowError(ex.Message ?? ex.StackTrace);
            }
        }

        private void installMissionBtn_Click(object sender, EventArgs e)
        {
            var missionName = missionNameTxt.Text;
            try
            {
                OpenFileDialog fileDlg = new OpenFileDialog();
                fileDlg.Filter = "IGI 1 Mission files (*.igimsf)|*.igimsf|All files (*.*)|*.*";
                fileDlg.RestoreDirectory = true;
                fileDlg.Title = "Select IGI Mission file";
                fileDlg.DefaultExt = ".igimsf";
                fileDlg.Multiselect = false;
                fileDlg.CheckFileExists = fileDlg.RestoreDirectory = fileDlg.AddExtension = true;
                string missionNamePath = null;

                DialogResult resultDlg = fileDlg.ShowDialog();
                if (resultDlg == DialogResult.OK)
                {
                    missionNamePath = fileDlg.FileName;
                    missionName = Path.GetFileName(missionNamePath);

                    if (String.IsNullOrEmpty(missionName)) throw new ArgumentNullException("Mission name cannot be empty");

                    //string missionNameExt = missionName + QUtils.missionExt;
                    var mFilesList = new List<string>() { QUtils.missionLevelFile, QUtils.missionDescFile, QUtils.objectsQsc };

                    if (File.Exists(QUtils.qMissionsPath + @"\" + missionName))
                    {
                        QUtils.ShowError("Mission '" + missionName + "' already exist in your missions directory");
                        return;
                    }
                    bool isValidMission = QZip.FilesExist(missionNamePath, mFilesList);
                    isValidMission = QZip.FileExist(missionNamePath, null, QUtils.qscExt);

                    if (!isValidMission) throw new Exception("Mission '" + missionName + "' is invalid mission file and cannot be installed");

                    File.Move(missionNamePath, QUtils.qMissionsPath + @"\" + missionName);
                    QUtils.AddLog("InstallMission(): Mission '" + missionName + "' was installed.");
                    SetStatusText("Mission '" + missionName + "' was installed.");
                }
            }
            catch (Exception ex)
            {
                QUtils.ShowError("Exception: " + ex.Message ?? ex.StackTrace);
            }
        }

        private void removeMissionBtn_Click(object sender, EventArgs e)
        {
            if (QUtils.aiScriptFiles.Count == 0)
            {
                QUtils.ShowError("No Mission was installed in your Missions directory.");
                return;
            }

            QUtils.CleanUpAiFiles();
            QUtils.RestoreLevel(gameLevel);
            QUtils.ResetFile(gameLevel);
            QMemory.RestartLevel();
            missionNameTxt.Text = missionDescTxt.Text = String.Empty;
            SetStatusText("Mission removed successfully.");
        }

        private void saveMissionBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var missionName = missionNameTxt.Text;
                var missionDesc = missionDescTxt.Text;

                if (missionName is null) throw new ArgumentNullException("Mission name cannot be empty");

                QMission.ChangeMissionDetails(QUtils.gGameLevel, missionName, missionDesc);
                var missionNameExt = missionName + QUtils.missionExt;

                //Create mission folder.
                Directory.CreateDirectory(missionName);
                Directory.CreateDirectory(missionName + @"\ai");

                QUtils.SaveFile(QUtils.missionDescFile, missionDesc);

                //Save A.I Files and Objects to mission.
                var outputAiPath = QUtils.cfgGamePath + QUtils.gGameLevel + "\\ai\\";
                QUtils.SaveFile(QUtils.missionLevelFile, gameLevel.ToString());

                foreach (var scriptFile in QUtils.aiScriptFiles)
                {
                    var missionAiPath = missionName + @"\ai\" + scriptFile;
                    if (!File.Exists(missionAiPath))
                        File.Copy(outputAiPath + scriptFile, missionAiPath);
                }
                QCryptor.Encrypt(QUtils.objectsQsc);

                File.Copy(QUtils.objectsQsc, missionName + @"\" + QUtils.objectsQsc);
                File.Move(QUtils.missionDescFile, missionName + @"\" + QUtils.missionDescFile);
                File.Move(QUtils.missionLevelFile, missionName + @"\" + QUtils.missionLevelFile);

                QZip.Create(missionName, QUtils.missionExt, true);
                File.Move(missionNameExt, QUtils.qMissionsPath + @"\" + missionNameExt);
                QUtils.ShowInfo("Mission '" + missionName + "' was saved successfully");
            }
            catch (Exception ex)
            {
                QUtils.ShowError("Exception: " + ex.Message ?? ex.StackTrace);
            }
        }

        private void loadMissionBtn_Click(object sender, EventArgs e)
        {
            try
            {
                missionNameTxt.Text = missionDescTxt.Text = String.Empty;
                string missionName = null;

                OpenFileDialog fileDlg = new OpenFileDialog();
                fileDlg.Filter = "IGI 1 Mission files (*.igimsf)|*.igimsf|All files (*.*)|*.*";
                fileDlg.InitialDirectory = QUtils.qMissionsPath;
                fileDlg.RestoreDirectory = true;
                fileDlg.Title = "Select MSF file";
                fileDlg.DefaultExt = ".igimsf";
                fileDlg.Multiselect = false;
                fileDlg.CheckFileExists = fileDlg.RestoreDirectory = fileDlg.AddExtension = true;

                var resultDlg = fileDlg.ShowDialog();
                if (resultDlg == DialogResult.OK)
                {
                    var fileName = fileDlg.FileName;
                    missionName = Path.GetFileNameWithoutExtension(fileName);
                }

                if (String.IsNullOrEmpty(missionName)) throw new ArgumentNullException("Mission name cannot be empty");

                QUtils.CleanUpAiFiles();//Cleanup previous mission files.
                if (missionName is null) throw new ArgumentNullException("Mission name cannot be empty");

                //Append level to mission name.
                var missionPath = QUtils.qMissionsPath + Path.DirectorySeparatorChar + missionName;

                var missionDescFile = missionPath + @"\" + QUtils.missionDescFile;
                var missionPathExt = missionPath + QUtils.missionExt;

                //if (!File.Exists(missionPathExt)) throw new FileNotFoundException("Mission '" + missionName + "' does not exist");
                bool missionAiExist = false;
                //Extract the mission directory.
                QZip.Extract(missionPathExt, QUtils.qMissionsPath);
                Thread.Sleep(1000);

                if (Directory.Exists(missionPath + @"\ai"))
                {
                    if (QUtils.IsDirectoryEmpty(missionPath + @"\ai"))
                    {
                        QUtils.ShowWarning("Mission directory doesn't include A.I Files.");
                        missionAiExist = false;
                    }
                    else missionAiExist = true;
                }

                var missionLevelFile = missionPath + @"\" + QUtils.missionLevelFile;
                int missionLevel = Convert.ToInt32(QUtils.LoadFile(missionLevelFile));
                bool levelSwitched = false;
                gameLevel = QUtils.gGameLevel = QMemory.GetCurrentLevel();
                levelStartTxt.Text = gameLevel.ToString();

                QMemory.DisableGameWarnings();
                if (missionLevel != gameLevel)
                {
                    var dlgResult = QUtils.ShowDialog("Mission level error do you want to switch level to '" + missionLevel + "' to continue ?", "Error");
                    if (dlgResult == DialogResult.Yes)
                    {
                        QUtils.ResetFile(missionLevel);
                        QUtils.RestoreLevel(missionLevel);
                        levelStartTxt.Text = missionLevel.ToString();
                        gameLevel = QUtils.gGameLevel = missionLevel;
                        levelSwitched = true;
                    }
                    else
                        levelSwitched = false;
                }

                var missionOutAiPath = QUtils.cfgGamePath + QUtils.gGameLevel + "\\ai\\";

                var missionDesc = QUtils.LoadFile(missionDescFile);
                QMission.ChangeMissionDetails(missionLevel, missionName, missionDesc);

                if (File.Exists(QUtils.objectsQsc)) File.Delete(QUtils.objectsQsc);

                File.Copy(missionPath + @"\" + QUtils.objectsQsc, QUtils.objectsQsc);
                var qData = QCryptor.Decrypt(QUtils.objectsQsc);
                QUtils.SaveFile(QUtils.objectsQsc, qData);

                //Copy all A.I Files.
                var missionAiPath = missionPath + @"\ai";
                if (missionAiExist)
                {
                    foreach (var outputPath in Directory.GetFiles(missionAiPath, "*.qvm*", SearchOption.AllDirectories))
                    {
                        string outputAiPath = outputPath.Replace(missionAiPath, missionOutAiPath);
                        File.Copy(outputPath, outputAiPath, true);
                        string aiFileName = outputPath.Substring(outputPath.LastIndexOf("\\") + 1);
                    }
                }

                var qscData = QUtils.LoadFile();
                qscData += QUtils.AddStatusMsg(-1, "Mission : " + missionName, "!HumanPlayer_0.isDead", ';');
                qscData += QUtils.AddStatusMsg(-1, "Description : " + missionDesc, "!HumanPlayer_0.isDead", ';');

                if (levelSwitched)
                {
                    startGameBtn_Click(sender, e);
                }

                if (!String.IsNullOrEmpty(qscData)) compileStatus = QCompiler.CompileEx(qscData);

                var aiFiles = Directory.GetFiles(missionPath, "*.qvm", SearchOption.AllDirectories);
                var aiFilesCount = aiFiles.Length;
                var missionsAiList = aiFiles.ToList();

                //Update AI Scirpts and Index.
                QUtils.aiScriptFiles.Clear();
                string aiScriptStr = null;
                foreach (var aiScript in missionsAiList)
                {
                    aiScriptStr = aiScript.Substring(aiScript.LastIndexOf(@"\") + 1);
                    QUtils.aiScriptFiles.Add(aiScriptStr);//Update script file from Mission A.I.
                }

                QUtils.aiScriptId += aiFilesCount * 2;//Update A.I Script Index.
                QUtils.AddLog("LoadMission() Ai Script Id : " + aiScriptId);

                QUtils.DeleteWholeDir(missionPath);
                missionNameTxt.Text = missionName;
                missionDescTxt.Text = missionDesc;
                SetStatusText("Mission '" + missionName + "' was loaded.");

            }
            catch (Exception ex)
            {
                QUtils.ShowError("Exception: " + ex.Message ?? ex.StackTrace);
            }
        }

        private void removeAllAi_Click(object sender, EventArgs e)
        {
            if (QUtils.aiScriptFiles.Count == 0)
            {
                QUtils.ShowError("No A.I Soldier was added yet.");
                return;
            }

            QUtils.CleanUpAiFiles();
            QUtils.RestoreLevel(gameLevel);
            QUtils.ResetFile(gameLevel);
            QMemory.RestartLevel();
            SetStatusText("All A.I soldiers were removed successfully.");
        }

        private void teleportToGraphBtn_Click(object sender, EventArgs e)
        {
            if (!viewPortEnableCb.Checked) { QUtils.ShowWarning("ViewPort was not enabled yet."); viewPortEnableCb.Checked = true; }
            //Graph Teleport section - MANUAL.
            if (manualTeleportGraphCb.Checked) QUtils.UpdateViewPort(graphPos);

            //Graph Teleport section - AUTO.
            else if (autoTeleportGraphCb.Checked)
            {
                if (!graphTraverseWorker.IsBusy)
                {
                    SetStatusText("Graph traversing started...");
                    QInternals.HumanInputDisable();
                    graphTraverseWorker.RunWorkerAsync();
                }
            }
        }

        private void teleportToNodeBtn_Click(object sender, EventArgs e)
        {
            if (!viewPortEnableCb.Checked) { QUtils.ShowWarning("ViewPort was not enabled yet."); viewPortEnableCb.Checked = true; }

            //Graph Teleport section - MANUAL.
            if (manualTeleportNodeCb.Checked) QUtils.UpdateViewPort(nodeRealPos);

            //Graph Teleport section - AUTO.
            else if (autoTeleportNodeCb.Checked)
            {
                if (!nodesTraverseWorker.IsBusy)
                {
                    SetStatusText("Node traversing started...");
                    QInternals.HumanInputDisable();
                    nodesTraverseWorker.RunWorkerAsync();
                }
            }
        }

        private void objectSelectDD_SelectedIndexChanged(object sender, EventArgs e)
        {
            var objectRigidModel = QUtils.objectRigidList[objectSelectDD.SelectedIndex].Values.ElementAt(0);
            PopulateImageBox(objectRigidModel, objectImgBox);
        }

        private void buildingSelectDD_SelectedIndexChanged(object sender, EventArgs e)
        {
            var buildingModel = QUtils.buildingList[buildingSelectDD.SelectedIndex].Values.ElementAt(0);
            PopulateImageBox(buildingModel, objectImgBox);
        }

        private void aiModelSelectDD_SelectedIndexChanged(object sender, EventArgs e)
        {
            var aiModelName = QAI.GetAiModelNamesList(gameLevel)[aiModelSelectDD.SelectedIndex];
            var aiModelId = QAI.GetAiModelIdForName(aiModelName);
            PopulateImageBox(aiModelId, aiImgBox);
        }

        private void manualTeleportGraphCb_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                teleportToGraphBtn.Text = "Teleport To Graph";
                stopTraversingNodesBtn.Enabled = false;
                autoTeleportGraphCb.Checked = false;
            }
        }

        private void autoTeleportGraphCb_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                stopTraversingNodesBtn.Enabled = true;
                teleportToGraphBtn.Text = "Traverse Graphs";
                manualTeleportGraphCb.Checked = false;
            }
        }

        private void manualTeleportNodeCb_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                teleportToNodeBtn.Text = "Teleport To Node";
                stopTraversingNodesBtn.Enabled = false;
                autoTeleportNodeCb.Checked = false;
            }
        }

        private void missionRefreshBtn_Click(object sender, EventArgs e)
        {
            try
            {
                InitMissionsOnline(true, false);
                UpdateUIComponent(missionsOnlineDD, QUtils.missionNameListStr);
            }
            catch (Exception ex) { }
        }

        private void gunPickupBtn_Click(object sender, EventArgs e)
        {


            //unsafe
            //{
            //    IntPtr weaponIdAddr = (IntPtr)0x08B90FBE;
            //    GT.GT_WriteMemory(weaponIdAddr, "4bytes", weaponIndex.ToString());
            //}

        }

        private void mapViewCb_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                //QUtils.SetAIEventIdle(true);
                //Thread.Sleep(3000);
                QInternals.HumanFreeCam();
                ((CheckBox)sender).Text = "Edit Mode";
                ((CheckBox)sender).ForeColor = Color.SpringGreen;
                QInternals.StatusMessageShow("Editor mode enabled. use Arrows keys to move ALT/SPACE change height");
            }
            else
            {
                GT.GT_SendKeyStroke("HOME");
                Thread.Sleep(500);
                //QUtils.SetAIEventIdle(false);
                //QInternals.RestartLevel();
                ((CheckBox)sender).Text = "Play Mode";
                ((CheckBox)sender).ForeColor = Color.Tomato;
                QInternals.StatusMessageShow("Play mode enabled - Play level.");
            }
            SetStatusText(((CheckBox)sender).Text + " enabled");
        }

        private void aiIdleCb_CheckedChanged(object sender, EventArgs e)
        {
            QUtils.SetAIEventIdle(((CheckBox)sender).Checked);
        }

        private void updateTeamIdBtn_Click(object sender, EventArgs e)
        {
            int teamId = 0;
            if (!String.IsNullOrEmpty(teamIdTxt.Text))
            {
                teamId = Convert.ToInt32(teamIdTxt.Text);
                if (teamId != 0)
                {
                    var qscData = QHuman.UpdateTeamId(teamId);

                    if (!String.IsNullOrEmpty(qscData)) compileStatus = QCompiler.Compile(qscData, QUtils.gamePath, false, true, false);
                    if (compileStatus) SetStatusText("Human team updated successfully");
                }
            }
            if (!String.IsNullOrEmpty(humanViewCamTxt.Text)) QInternals.HumanCameraView(humanViewCamTxt.Text);
            QInternals.StatusMessageShow("Human camera updated.");
        }

        private void setFramesBtn_Click(object sender, EventArgs e)
        {
            string frames = framesTxt.Text;
            QInternals.FramesSet(frames);
        }

        private void quitLevelBtn_Click(object sender, EventArgs e)
        {
            QInternals.QuitLevel();
        }

        private void debugModeCb_CheckedChanged(object sender, EventArgs e)
        {
            QInternals.DebugMode();
        }

        private void udpateMusicBtn_Click(object sender, EventArgs e)
        {
            float musicVal = (float)musicTrackBar.Value / 10.0f;
            string musicVolStr = musicVal.ToString();

            if (String.IsNullOrEmpty(musicVolStr)) return;
            else
            {
                if (sfxMusicCb.Checked)
                {
                    QInternals.MusicSFXVolumeSet(musicVolStr);
                    SetStatusText("SFX Volume set to " + musicVolStr);
                }
                else if (musicSoundCb.Checked)
                {
                    QInternals.MusicVolumeSet(musicVolStr);
                    SetStatusText("Game Volume set to " + musicVolStr);
                }
            }
        }

        private void enableMusicCb_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                QInternals.MusicEnable();
                ((CheckBox)sender).Text = "Music Enabled";
                ((CheckBox)sender).ForeColor = Color.Green;
            }
            else
            {
                QInternals.MusicDisable();
                ((CheckBox)sender).Text = "Music Disabled";
                ((CheckBox)sender).ForeColor = Color.Tomato;
            }
            SetStatusText(((CheckBox)sender).Text + " successfully.");
        }

        private void musicSoundCb_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked) sfxMusicCb.Checked = false;
        }

        private void sfxMusicCb_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked) musicSoundCb.Checked = false;
        }

        private void internalsAttachBtn_Click(object sender, EventArgs e)
        {
            var internalsAttached = QUtils.CheckInternalsAttached();
            if (!internalsAttached)
            {
                QUtils.AttachInternals();
                SetStatusText("Internals attach success");
            }
            else
                SetStatusText("Internals already attached");
        }

        private void internalsDeattachBtn_Click(object sender, EventArgs e)
        {
            //DeAttach internals.
            GT.GT_SendKeys2Process(QMemory.gameName, GT.VK.END);
            QUtils.Sleep(1.5f);

            QUtils.attachStatus = QUtils.CheckInternalsAttached();
            if (QUtils.attachStatus) QUtils.DeattachInternals();

            SetStatusText("Internals deattach success");
        }

        private void liveEditorCb_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                ((CheckBox)sender).ForeColor = Color.SpringGreen;
                SetStatusText("Live Editor enabled.");
            }
            else
            {
                ((CheckBox)sender).ForeColor = Color.DeepSkyBlue;
                SetStatusText("Live Editor disabled.");
            }
        }

        private void gfxResetBtn_Click(object sender, EventArgs e)
        {
            QInternals.GraphicsReset();
            QInternals.StatusMessageShow("Graphics settings reset");
        }

        private void resetPosCb_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked) humanPosOffCb.Checked = humanPosMeterCb.Checked = false;
        }

        private void aiTypeDD_SelectedIndexChanged(object sender, EventArgs e)
        {
            var aiModelName = QAI.GetAiModelNamesList(gameLevel)[aiModelSelectDD.SelectedIndex];
            var aiTypeName = ((ComboBox)sender).SelectedItem.ToString();

            //HumanSoldierFemale exceptions.
            if (aiModelName == "ANYA" && (aiTypeName != "ANYA" && aiTypeName != "EKK"))
            {
                QUtils.ShowWarning("HumanSoldier-Female 'ANYA' Type should also be of female soldier.", "A.I WARNING");
                addAiBtn.Enabled = false;
            }
            else if (aiModelName == "EKK" && (aiTypeName != "ANYA" && aiTypeName != "EKK"))
            {
                QUtils.ShowWarning("HumanSoldier-Female 'EKK' Type should also be of female soldier.", "A.I WARNING");
                addAiBtn.Enabled = false;
            }
            else
                addAiBtn.Enabled = true;
        }

        private void nodeIdMetreCb_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked) nodeIdOffsetCb.Checked = false;
        }

        private void nodeIdOffsetCb_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked) nodeIdMetreCb.Checked = false;
        }

        private void modWeaponBtn_Click(object sender, EventArgs e)
        {
            var weaponsCfgPathFile = QUtils.weaponsGamePath + @"\" + QUtils.weaponConfigQVM;
            if (File.Exists(weaponsCfgPathFile))
            {
                File.Delete(weaponsCfgPathFile);
            }
            File.Copy(QUtils.weaponsModQvmPath, weaponsCfgPathFile);

            QInternals.WeaponConfigRead();
            string weaponMsg = "Weapons Modded loaded successfully.";
            SetStatusText(weaponMsg);
            QInternals.StatusMessageShow(weaponMsg);
        }

        private void resetModWeaponBtn_Click(object sender, EventArgs e)
        {
            var weaponsCfgPathFile = QUtils.weaponsGamePath + @"\" + QUtils.weaponConfigQVM;
            if (File.Exists(weaponsCfgPathFile))
            {
                File.Delete(weaponsCfgPathFile);
            }
            File.Copy(QUtils.weaponsOrgCfgPath, weaponsCfgPathFile);
            QInternals.WeaponConfigRead();

            string weaponMsg = "Weapons reset success.";
            SetStatusText(weaponMsg);
            QInternals.StatusMessageShow(weaponMsg);
        }

        private void framesTxt_TextChanged(object sender, EventArgs e)
        {
            var frames = ((TextBox)sender).Text;
            int fps = Convert.ToInt32(frames);
            //Check for Max FPS
            if (fps < 0 || fps > MAX_FPS)
            {
                fps = 30;
                ((TextBox)sender).Text = fps.ToString();
            }
        }

        private void objectIDTxt_TextChanged(object sender, EventArgs e)
        {
            var modelId = ((TextBox)sender).Text;
            var modelName = QObjects.FindModelName(modelId);
            if (!String.IsNullOrEmpty(modelName)) objectIDLbl.Text = modelName;
        }

        private void stopTraversingNodesBtn_Click(object sender, EventArgs e)
        {
            if (graphTraverseWorker.IsBusy)
            {
                autoTeleportGraphCb.Checked = viewPortEnableCb.Checked = false;
                graphTraverseWorker.CancelAsync();
                QInternals.HumanInputEnable();
                SetStatusText("Graphs traversing canceled.");
            }

            if (nodesTraverseWorker.IsBusy)
            {
                autoTeleportNodeCb.Checked = viewPortEnableCb.Checked = false;
                nodesTraverseWorker.CancelAsync();
                QInternals.HumanInputEnable();
                SetStatusText("Nodes traversing canceled.");
            }
        }

        private void missionsOnlineDD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!QUtils.editorOnline)
            {
                QUtils.ShowWarning("Editor is not in online mode to get missions list from server.");
                return;
            }

            if (QUtils.qServerMissionDataList.Count > 0)
            {
                int index = missionsOnlineDD.SelectedIndex;
                missionData = QUtils.qServerMissionDataList[index];

                missionSizeLbl.Text = "Size: " + missionData.MissionSize.ToString() + " Kb";
                missionLevelLbl.Text = "Level: " + missionData.MissionLevel.ToString();
                missionAuthorLbl.Text = "Author: " + missionData.MissionAuthor.ToString();
            }
        }

        private void autoTeleportNodeCb_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                stopTraversingNodesBtn.Enabled = true;
                teleportToNodeBtn.Text = "Traverse Nodes";
                manualTeleportNodeCb.Checked = false;
            }
        }

        private void downloadMissionBtn_Click(object sender, EventArgs e)
        {
            downloadWorker.RunWorkerAsync();
        }

        private void uploadMissionBtn_Click(object sender, EventArgs e)
        {
            uploadWorker.RunWorkerAsync();
        }

        private void nodeIdDD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (nodeIdDD.SelectedIndex == -1) nodeIdDD.SelectedIndex = 0;
            nodeId = QUtils.aiGraphNodeIdStr[nodeIdDD.SelectedIndex];
            graphNode = QGraphs.GetGraphNodeData(graphId, nodeId);

            //Setting Node properties.
            nodeCriteriaTxt.Text = graphNode.NodeCriteria;

            if (nodeIdOffsetCb.Checked)
            {
                nodeXTxt.Text = graphNode.NodePos.x.ToString("0.0000");
                nodeYTxt.Text = graphNode.NodePos.y.ToString("0.0000");
                nodeZTxt.Text = graphNode.NodePos.z.ToString("0.0000");
            }
            else
            {
                nodeXTxt.Text = graphPos.x + graphNode.NodePos.x.ToString("R");
                nodeYTxt.Text = graphPos.y + graphNode.NodePos.y.ToString("R");
                nodeZTxt.Text = graphPos.z + graphNode.NodePos.z.ToString("R");
            }

            //Get Node Real Position.
            nodeRealPos = new Real64();
            nodeRealPos.x = graphPos.x + graphNode.NodePos.x;
            nodeRealPos.y = graphPos.y + graphNode.NodePos.y;
            nodeRealPos.z = graphPos.z + graphNode.NodePos.z + QUtils.viewPortDelta;
        }

        private void appSupportBtn_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(QUtils.supportDiscordLink);
            System.Diagnostics.Process.Start(QUtils.supportYoutubeLink);
            System.Diagnostics.Process.Start(QUtils.supportVKLink);
        }

        private void StartGameLevel(int level, bool windowed = true)
        {
            LoadLevelDetails(level);
            QMemory.StartLevel(level, windowed);
            QUtils.gameFound = true;
            QUtils.Sleep(5);

            //Reset only if checked.
            if (QUtils.gameReset)
            {
                QUtils.RestoreLevel(level);
                QUtils.ResetFile(level);
            }

            GenerateAIScriptId(level);
            QUtils.aiScriptFiles.Clear();

            RefreshUIComponents(level);

#if TESTING
            QMemory.UpdateHumanHealth(false);
#endif
        }

        private void RefreshUIComponents(int level)
        {
            try
            {
                InitUIComponents(level, false);
                UpdateUIComponent(buildingSelectDD, QUtils.buildingListStr);
                UpdateUIComponent(objectSelectDD, QUtils.objectRigidListStr);
                UpdateUIComponent(aiModelSelectDD, QUtils.aiModelsListStr);
                UpdateUIComponent(aiGraphIdDD, QUtils.aiGraphIdStr);
                UpdateUIComponent(graphIdDD, QUtils.aiGraphIdStr);
                UpdateUIComponent(nodeIdDD, QUtils.aiGraphNodeIdStr);
            }
            catch (Exception ex) { }
        }

        //Generic Update UI method for DropDowns,TextBox etc.
        private void UpdateUIComponent<T>(ComboBox itemDD, List<T> dataSrcList)
        {
            itemDD.DataSource = null;
            itemDD.Items.Clear();
            //itemDD.DataSource = dataSrcList;
            //itemDD.Invoke(new Action(() => itemDD.DataSource = dataSrcList));
            itemDD.Invoke((MethodInvoker)delegate
            {
                // Running on the UI thread
                itemDD.DataSource = dataSrcList;
            });
            itemDD.Invalidate();
            itemDD.Update();
            itemDD.Refresh();
            itemDD.SelectedIndex = 0;
            Application.DoEvents();
        }

        private static void LoadImgBoxWeb(string url, PictureBox imgBox)
        {
            if (!editorOnline) { editorRef.SetStatusText("Resource error Check your internet connection."); return; }
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

        internal void PopulateImageBox(string modelId, PictureBox imgBox)
        {
            var imgTmpPath = QUtils.cachePathAppImages + "\\" + modelId + QUtils.pngExt;

            //Load image from Cache.
            if (File.Exists(imgTmpPath))
            {
                using (var bmpTemp = new Bitmap(imgTmpPath))
                {
                    imgBox.Image = new Bitmap(bmpTemp);
                }
            }
        }

        internal void AddObject(string model, bool rigidObj = false, Real64 objectPos = null, bool hasOrientation = false, int taskId = -1, string taskNote = "", bool objCompile = true, float alpha = -9.9f, float beta = -9.9f, float gamma = -9.9f)
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
                        qscData = QBuildings.AddBuilding(model, objectPos, orientation, false, taskId);
                    }
                }
                else
                {
                    if (rigidObj)
                        qscData = QObjects.AddRigidObj(model, objectPos, false, taskNote);
                    else
                        qscData = QBuildings.AddBuilding(model, objectPos, false, taskId, taskNote);
                }

                //Compile the data with QCompiler.
                if (objCompile)
                {
                    if (!String.IsNullOrEmpty(qscData)) compileStatus = QCompiler.Compile(qscData, QUtils.gamePath, true);
                }
                else
                {
                    QUtils.SaveFile(qscData, true);
                }
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
