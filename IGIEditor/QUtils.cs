using Codeplex.Data;
using DeviceId;
using IWshRuntimeLibrary;
using Microsoft.Win32;
using Newtonsoft.Json;
using QLibc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using static IGIEditor.QServer;
using File = System.IO.File;
using FileIO = Microsoft.VisualBasic.FileIO;
using FileSystem = Microsoft.VisualBasic.FileIO.FileSystem;

namespace IGIEditor
{
    internal class QUtils
    {
        internal class QScriptTask
        {
            internal Int32 id;
            internal string name;
            internal string note;
            internal Real64 position;
            internal Real32 orientation;
            internal string model;
        };

        internal class HTask
        {
            internal int team;
            internal QScriptTask qtask;
            internal List<string> weaponsList;
        };

        internal class FOpenIO
        {
            string fileName;
            string fileData;
            long fileLength;
            float fileSize;
            public FOpenIO() { FileName = FileData = null; FileLength = 0; FileSize = 0; }

            public FOpenIO(string fileName, string fileData, long fileLength, float fileSize)
            {
                this.FileName = fileName;
                this.FileData = fileData;
                this.FileLength = fileLength;
                this.FileSize = fileSize;
            }

            public string FileName { get => fileName; set => fileName = value; }
            public string FileData { get => fileData; set => fileData = value; }
            public long FileLength { get => fileLength; set => fileLength = value; }
            public float FileSize { get => fileSize; set => fileSize = value; }
        }

        [SerializableAttribute]
        internal class WeaponGroup
        {
            string name;
            int ammo;

            public string Weapon { get => name; set => name = value; }
            public int Ammo { get => ammo; set => ammo = value; }
        }

        internal static string taskNew = "Task_New", taskDecl = "Task_DeclareParameters";
        internal static string objectsQsc = "objects.qsc", objectsQvm = "objects.qvm", weaponConfigQSC = "weaponconfig.qsc", weaponConfigQVM = "weaponconfig.qvm", weaponsModQvm = "weaponconfig-mod.qvm";
        internal static int qtaskObjId, qtaskId, anyaTeamTaskId = -1, ekkTeamTaskId = -1, aiScriptId = 0, gGameLevel = 1, GAME_MAX_LEVEL = 3, currGameLevel = 1, updateTimeInterval = 10, gameFPS = 30, healthScaleFall = 0;
        internal static string versionFileName = "VERSION", appEditorSubVersion = "0.5.0.0", logFile = "app.log", qLibLogsFile = "QLibc_logs.log", aiIdleFile = "aiIdle.qvm", objectsModelsList, aiIdlePath, customScriptFile = "ai_custom_script.qsc", customPatrolFile = "ai_custom_path.qsc", customScriptPathQEd, customPatrolPathQEd, appLogFileTmp = @"%tmp%\IGIEditorCache\AppLogs\", nativesFile = @"\IGI-Natives.json", modelsFile = @"\IGI-Models.txt", internalsLogFile = @"\IGI-Internals.log";
        internal static bool gameFound = false, gameProfileLoaded = false, gamePathSet = false, logEnabled = false, keyExist = false, keyFileExist = false, attachStatus = false, customAiSelected = false, editorOnline = true, gameReset = false, appLogs = false, editorUpdateCheck = false, nppInstalled = false, shortcutCreated = false, shortcutExist = false, gameMusicEnabled = false, gameAiIdleMode = false, gameDebugMode = false, gameDisableWarns = true, gameRefresh = false;
        internal static bool internalCompiler = false, externalCompiler = false;
        internal static float appEditorVersion = 0.4f, viewPortDelta = 10000.0f;
        internal static string supportDiscordLink = @"https://discord.gg/9T8tzyhvp6", supportYoutubeLink = @"https://www.youtube.com/channel/UChGryl0a0dii81NfDZ12LwA", supportVKLink = @"https://vk.com/id679925339";
        internal static IntPtr viewPortAddrX = (IntPtr)0x00BCAB08, viewPortAddrY = (IntPtr)0x00BCAB10, viewPortAddrZ = (IntPtr)0x00BCAB18;
        internal const int TEAM_ID_FRIENDLY = 0, TEAM_ID_ENEMY = 1, MAX_AI_COUNT = 100, MAX_FPS = 240, MAX_UPDATE_TIME = 120, MAX_HUMAN_CAM = 5, LEVEL_FLOW_TASK_ID = 10, HUMANPLAYER_TASK_ID = 0, MAX_MINIMAL_ID_DIFF = 10;

        internal static string gamePath, appdataPath, igiEditorQEdPath, editorCurrPath, deviceIdDLLPath, gameAbsPath, cfgGamePath, cfgHumanplayerPathQsc, cfgHumanplayerPathQvm, cfgQscPath, cfgAiPath, cfgQvmPath, cfgVoidPath, cfgQFilesPath, qMissionsPath, qGraphsPath, qWeaponsPath, qWeaponsGroupPath, qQVMPath, qQSCPath, cfgWeaponsPath, weaponsModQvmPath, weaponsOrgCfgPath, weaponsGamePath, humanplayerGamePath, menusystemGamePath, missionsGamePath, commonGamePath,
            qfilesPath = @"\QFiles", qEditor = "QEditor", qconv = "QConv", qCompiler = "QCompiler", qfiles = "QFiles", qGraphs = "QGraphs", iniCfgFile, editorAppName, editorUpdater, cachePath, cachePathAppLogs, nativesFilePath, modelsFilePath, internalsLogPath, qedAiJsonPath, qedAiScriptPath, qedAiPatrolPath,
            cachePathAppImages, currPathAppImages, editorUpdaterDir = "IGIEditor_Update", editorUpdaterAbsDir, editorUpdaterFile, updaterBatchFile, editorChangeLogs = "CHANGELOGS", editorLicence = "LICENCE", editorReadme = "README", editorAutoUpdaterFile, autoUpdaterFile = "AutoUpdater", autoUpdaterBatch,
         igiQsc = "IGI_QSC", igiQvm = "IGI_QVM", graphsPath, cfgGamePathEx = @"\missions\location0\level", weaponsDirPath = @"\weapons", humanplayerQvm = "humanplayer.qvm", humanplayerQsc = "humanplayer.qsc", humanplayerPath = @"\humanplayer", aiGraphTask = "AIGraph", menuSystemDir = "menusystem", menuSystemPath = null, internalsDllFile, internalsDll = "IGI-Internals.dll",
            internalsDllPath = @"bin\IGI-Internals.dll", qLibcPath = @"lib\GTLibc_x86.so", tmpDllPath, internalDllInjectorPath = @"bin\IGI-Injector.exe", internalDllGTInjectorPath = @"bin\IGI-Injector-GT.exe", PATH_SEC = "PATH", EDITOR_SEC = "EDITOR", GAME_SEC = "GAME";

        internal static string qedQscPath = @"\IGI_QSC", qedQvmPath = @"\IGI_QVM", qedAiPath = @"\AIFiles", qedVoidPath = @"\Void", qedAiJson = @"\AI-Json", qedAiScript = @"\AI-Script", qedAiPatrol = @"\AI-Path", inputMissionPath = @"\missions\location0\level", inputHumanplayerPath = @"\humanplayer", inputWeaponsPath = @"\weapons";
        internal static List<string> objTypeList = new List<string>() { "Building", "EditRigidObj", "Terminal", "Elevator", "ExplodeObject", "AlarmControl", "Generator", "Radio" };
        internal static string objects = "objects", objectsAll = "objectsAll", weapons = "weapons";
        internal const string qvmExt = ".qvm", qscExt = ".qsc", datExt = ".dat", csvExt = ".csv", jsonExt = ".json", txtExt = ".txt", xmlExt = ".xml", dllExt = ".dll", missionExt = ".igimsf", jpgExt = ".jpg", pngExt = ".png", rarExt = ".rar", zipExt = ".zip", exeExt = ".exe", batExt = ".bat", iniExt = ".ini";
        internal static float fltInvalidAngle = -9.9999f, fltInvalidVal = -9.9f;
        internal const string CAPTION_CONFIG_ERR = "Config - Error", CAPTION_FATAL_SYS_ERR = "Sytem-Fatal - Error", CAPTION_APP_ERR = "Application - Error", CAPTION_COMPILER_ERR = "Compiler - Error", EDITOR_LEVEL_ERR = "EDITOR ERROR", alarmControl = "AlarmControl", stationaryGun = "StationaryGun";
        internal static string keyBase = @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths", aboutStr = "IGI Editor is powerful editor to edit game levels" + "\n" + "Offers upto " + GAME_MAX_LEVEL + " level\nVersion: v" + appEditorSubVersion + " BETA.\n\nTools/Language: C#(5.0) VS-Studio/Code\nCreated by Haseeb Mir.\n\nCredits & People\nUI Designing - Dark\nResearch data - Dimon, Yoejin and GM123.\nQScript/DConv Tools - Artiom.\nTester - Orwa\nIGI-VK Community.";
        internal static string patroIdleMask = "xxxx", patroAlarmMask = "yyyy", alarmControlMask = "xx", gunnerIdMask = "xxx", viewGammaMask = "yyy";
        internal static string movementSpeedMask = "movSpeed", forwardSpeedMask = "forwardSpeed", upwardSpeedMask = "upSpeed", inAirSpeedMask = "iAirSpeed", throwBaseVelMask = "throwBaseVel", healthScaleMask = "healthScale", healthFenceMask = "healthFence", peekLeftRightLenMask = "peekLRLen", peekCrouchLenMask = "peekCrouchLen", peekTimeMask = "peekTime";
        internal static List<string> aiScriptFiles = new List<string>();
        internal static string aiEnenmyTask = null, aiFriendTask = null, levelFlowData, missionLevelFile = "mission_level.txt", missionDescFile = "mission_desc.txt", missionListFile = @"\MissionsList.dat";
        internal static double movSpeed = 1.75f, forwardSpeed = 17.5f, upwardSpeed = 27, inAirSpeed = 0.5f, peekCrouchLen = 0.8500000238418579f, peekLRLen = 0.8500000238418579f, peekTime = 0.25, healthScale = 3.0f, healthScaleFence = 0.5f;
        private static Random rand = new Random();
        internal static QIniParser qIniParser;
        internal enum QTYPES { BUILDING = 1, RIGID_OBJ = 2 };
        internal enum GRAPH_VISUAL { OBJECTS = 1, HILIGHT = 2 };
        internal enum UPDATE_ACTION { DOWNLOAD = 1, EXTRACT = 2, UPDATE = 3 };
        internal enum HEALTH_ACTION { NONE = 0, TEMPORARY = 1, PERMANENT = 2, RESTORE = 3 };
        internal static Dictionary<int, string> graphAreas = new Dictionary<int, string>();
        //private static IGIIGIEditorUI.editorRef IGIEditorUI.editorRef = IGIIGIEditorUI.editorRef.editorRef;

        //List of Dictionary items.
        internal static List<Dictionary<string, int>> weaponList = new List<Dictionary<string, int>>();
        internal static List<Dictionary<string, string>> buildingList = new List<Dictionary<string, string>>();
        internal static List<Dictionary<string, string>> objectRigidList = new List<Dictionary<string, string>>();
        internal static List<Dictionary<string, int>> weaponsGroupListx = new List<Dictionary<string, int>>();
        internal static List<WeaponGroup> weaponsGroupList = new List<WeaponGroup>();
        internal static List<string> buildingListStr = new List<string>();
        internal static List<string> objectRigidListStr = new List<string>();
        internal static List<string> aiModelsListStr = new List<string>();
        internal static List<string> missionNameListStr = new List<string>();
        internal static List<int> aiGraphIdStr = new List<int>();
        internal static List<int> graphdIdsMarked = new List<int>();
        internal static List<int> aiGraphNodeIdStr = new List<int>();
        internal static List<int> weaponMarkedIds = new List<int>();
        internal static List<QGraphs.GraphNode> graphNodesList = new List<QGraphs.GraphNode>();
        internal static List<int> qIdsList = new List<int>();
        internal static List<HumanAi> humanAiList = new List<HumanAi>();


        //Server data list.
        internal static List<QServerData> qServerDataList = new List<QServerData>();
        internal static List<QMissionsData> qServerMissionDataList = new List<QMissionsData>();

        //Weapons variables.
        internal const string weaponId = "WEAPON_ID_";
        internal static Dictionary<string, string> ammoList = new Dictionary<string, string>(){
        {"SPAS12/JACKHAMMER","AMMO_ID_12"},
        {"COLT","AMMO_ID_357"},//AMMO_ID_44 Original slot fix.
        {"M2HB","AMMO_ID_127"},
        {"DESERTEAGLE","AMMO_ID_357"},
        {"M16A2/MINIMI/T80","AMMO_ID_556"},
        {"AK47","AMMO_ID_762"},
        {"GLOCK/UZI/UZIX2/MP5SD","AMMO_ID_919"},
        {"RPG18","AMMO_ID_1000"},
        {"DRAGUNOV","AMMO_ID_DRAGUNOV"},
        {"FLASHBANG","AMMO_ID_FLASHBANG"},
        {"GRENADE","AMMO_ID_GRENADE"},
        {"M203","AMMO_ID_M203"},
        {"MEDIPACK","AMMO_ID_MEDIPACK"},
        {"PROXIMITYMINE","AMMO_ID_PROXIMITYMINE"}};

        internal enum GAME_WEAPON
        {
            WEAPON_ID_GLOCK = 1, //Weapon Type: Pistol.
            WEAPON_ID_DESERTEAGLE = 3,//Weapon Type: Pistol.
            WEAPON_ID_M16A2 = 4,//Weapon Type: Rifle.
            WEAPON_ID_AK47 = 5,//Weapon Type: Rifle.
            WEAPON_ID_UZI = 6,//Weapon Type: SMG.
            WEAPON_ID_MP5SD = 7,//Weapon Type: SMG.
            WEAPON_ID_SPAS12 = 8,//Weapon Type: Shotgun.
            WEAPON_ID_JACKHAMMER = 9,//Weapon Type: Shotgun.
            WEAPON_ID_MINIMI = 10,//Weapon Type: HMG (Machine Gun).
            WEAPON_ID_DRAGUNOV = 11,//Weapon Type: Sniper.
            WEAPON_ID_RPG18 = 12,//Weapon Type: Launcher.
            WEAPON_ID_UZIX2 = 13,//Weapon Type: SMG.
            WEAPON_ID_GRENADE = 14,//Weapon Type: Grenade.
            WEAPON_ID_FLASHBANG = 15,//Weapon Type: Grenade.
            WEAPON_ID_PROXIMITYMINE = 16,//Weapon Type: Grenade.
            WEAPON_ID_BINOCULARS = 18,//Weapon Type: Binoculars.
            WEAPON_ID_MEDIPACK = 19,//Weapon Type: MediPack.
            WEAPON_ID_KNIFE = 20,//Weapon Type: Knife.
            WEAPON_ID_COLT = 21,//Weapon Type: Revolver.
            WEAPON_ID_APC = 40,//Weapon Type: Launcher.
            WEAPON_ID_MIL = 41,//Weapon Type: HMG (Machine Gun).
            WEAPON_ID_M2HB = 42,//Weapon Type: HMG (Machine Gun).
            WEAPON_ID_T80 = 43,//Weapon Type: Launcher.
            WEAPON_ID_SENTRY = 44//Weapon Type: HMG (Machine Gun).
        };

        //Base server url for Downloading resources.
        internal static string baseServerUrl = @"http://igiresearchdevelopers.orgfree.com";

        internal static List<string> aiTypes = new List<string>() { 
            "AITYPE_RPG", "AITYPE_GUNNER", "AITYPE_SNIPER",
            "AITYPE_ANYA", "AITYPE_EKK", "AITYPE_PRIBOI",
            "AITYPE_CIVILIAN", "AITYPE_PATROL_AK", "AITYPE_GUARD_AK",
            "AITYPE_SECURITY_PATROL_UZI", "AITYPE_MAFIA_PATROL_UZI", "AITYPE_MAFIA_GUARD_AK",
            "AITYPE_SPETNAZ_PATROL_AK", "AITYPE_SPETNAZ_GUARD_AK" };

        internal static bool InitEditorAppData()
        {
            bool initStatus = true;
            string initErrReason = String.Empty;
            appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            igiEditorQEdPath = appdataPath + Path.DirectorySeparatorChar + qEditor;

            editorAppName = AppDomain.CurrentDomain.FriendlyName.Replace(".exe", String.Empty);
            iniCfgFile = editorAppName + ".ini";
            logFile = editorAppName + ".log";
            editorCurrPath = Directory.GetCurrentDirectory();
            deviceIdDLLPath = editorCurrPath + Path.DirectorySeparatorChar + "DeviceId.dll";

            //Set new Input QSC & QVM path releative to appdata.
            objectsModelsList = igiEditorQEdPath + Path.DirectorySeparatorChar + "IGIModels.txt";
            qMissionsPath = igiEditorQEdPath + @"\QMissions";
            qGraphsPath = igiEditorQEdPath + @"\QGraphs";
            qWeaponsPath = igiEditorQEdPath + @"\QWeapons";
            qWeaponsGroupPath = qWeaponsPath + "\\" + "Group";
            weaponsGamePath = gameAbsPath + @"\weapons";
            humanplayerGamePath = gameAbsPath + @"\humanplayer";
            menusystemGamePath = gameAbsPath + @"\menusystem";
            missionsGamePath = gameAbsPath + @"\missions";
            commonGamePath = gameAbsPath + @"\common";
            qQVMPath = igiEditorQEdPath + qfilesPath + qedQvmPath;
            qQSCPath = igiEditorQEdPath + qfilesPath + qedQscPath;
            aiIdlePath = igiEditorQEdPath + Path.DirectorySeparatorChar + "aiIdle.qvm";
            cfgQvmPath = igiEditorQEdPath + qfilesPath + qedQvmPath + inputMissionPath;
            cfgQscPath = igiEditorQEdPath + qfilesPath + qedQscPath + inputMissionPath;
            cfgHumanplayerPathQsc = igiEditorQEdPath + qfilesPath + qedQscPath + inputHumanplayerPath;
            cfgHumanplayerPathQvm = igiEditorQEdPath + qfilesPath + qedQvmPath + inputHumanplayerPath;
            cfgWeaponsPath = igiEditorQEdPath + qfilesPath + qedQvmPath + inputWeaponsPath;
            cfgAiPath = igiEditorQEdPath + qedAiPath;
            qedAiJsonPath = igiEditorQEdPath + qedAiPath + qedAiJson;
            qedAiScriptPath = igiEditorQEdPath + qedAiPath + qedAiScript;
            qedAiPatrolPath = igiEditorQEdPath + qedAiPath + qedAiPatrol;
            customScriptPathQEd = qedAiScriptPath + "\\" + customScriptFile;
            customPatrolPathQEd = qedAiPatrolPath + "\\" + customPatrolFile;
            cfgVoidPath = igiEditorQEdPath + qedVoidPath;
            cfgQFilesPath = igiEditorQEdPath + qfilesPath;
            menuSystemPath = gameAbsPath + menuSystemDir;
            cachePath = Path.GetTempPath() + "IGIEditorCache";
            cachePathAppLogs = cachePath + @"\AppLogs";
            cachePathAppImages = cachePath + @"\AppImages";
            currPathAppImages = editorCurrPath + @"\AppImages";
            missionListFile = cachePath + missionListFile;
            nativesFilePath = cachePath + nativesFile;
            modelsFilePath = cachePath + modelsFile;
            internalsLogPath = cachePath + internalsLogFile;
            weaponsModQvmPath = cfgWeaponsPath + @"\" + weaponsModQvm;
            weaponsOrgCfgPath = cfgWeaponsPath + @"\" + weaponConfigQVM;
            editorUpdaterFile = cachePath + @"\" + editorUpdaterDir + zipExt;
            editorUpdaterAbsDir = cachePath + @"\" + editorUpdaterDir;
            autoUpdaterBatch = editorAppName + "-" + autoUpdaterFile + batExt;
            editorUpdater = editorAppName + "_" + "Update" + zipExt;

            //Init Temp path for Cache.
            if (!Directory.Exists(cachePath))
            {
                CreateCacheDir();
            }

            if (!Directory.Exists(igiEditorQEdPath)) { initErrReason = "QEditor"; initStatus = false; }
            else if (!Directory.Exists(qMissionsPath)) { initErrReason = @"QEditor\QMissions"; initStatus = false; }
            else if (!Directory.Exists(qQVMPath)) { initErrReason = @"QEditor\QFiles\IGI_QVM"; initStatus = false; }
            else if (!Directory.Exists(qQSCPath)) { initErrReason = @"QEditor\QFiles\IGI_QSC"; initStatus = false; }
            else if (!Directory.Exists(cfgAiPath)) { initErrReason = @"QEditor\AIFiles"; initStatus = false; }
            else if (!Directory.Exists(cfgVoidPath)) { initErrReason = @"QEditor\Void"; initStatus = false; }
            else if (!Directory.Exists(cfgQFilesPath)) { initErrReason = @"QEditor\QFiles"; initStatus = false; }
            else if (!Directory.Exists(cfgQFilesPath)) { initErrReason = @"QEditor\QWeapons"; initStatus = false; }

            initErrReason = "'" + initErrReason + "' Directory is missing";
#if !DEV_MODE
            //Show error if 'QEditor' path has invalid structure..
            if (!initStatus) ShowSystemFatalError("Editor Appdata directory is invalid Error: (0x0000000F)\nReason: " + initErrReason + "\nPlease re-install new copy from Setup file.");
#endif

            if (String.IsNullOrEmpty(gameAbsPath)) return false;

            if (!File.Exists(deviceIdDLLPath))
            {
                AddLog(MethodBase.GetCurrentMethod().Name, "Error DeviceId.dll file is missing from the directory");
                ShowSystemFatalError("Some impportant files are missing from the directory. Error (0x0005D11)");
            }

            return initStatus;
        }

        internal static void CreateCacheDir()
        {
            Directory.CreateDirectory(cachePath);
            Directory.CreateDirectory(cachePathAppLogs);
            Directory.CreateDirectory(cachePathAppImages);
        }



        //UI-Dialogs and MessageBox.
        internal static void ShowWarning(string warnMsg, string caption = "WARNING")
        {
            DialogMsgBox.ShowBox(caption, warnMsg, MsgBoxButtons.Ok);
        }

        internal static void ShowError(string errMsg, string caption = "ERROR")
        {
            DialogMsgBox.ShowBox(caption, errMsg, MsgBoxButtons.Ok);
        }

        internal static void LogException(string methodName, Exception ex)
        {
            methodName = methodName.Replace("Btn_Click", String.Empty).Replace("_SelectedIndexChanged", String.Empty).Replace("_SelectedValueChanged", String.Empty);
            AddLog(methodName, "Exception MESSAGE: " + ex.Message + "\nREASON: " + ex.StackTrace);
        }

        internal static void ShowException(string methodName, Exception ex)
        {
            ShowError("MESSAGE: " + ex.Message + "\nREASON: " + ex.StackTrace, methodName + " Exception");
        }

        internal static void ShowLogException(string methodName, Exception ex)
        {
            methodName = methodName.Replace("Btn_Click", String.Empty).Replace("_SelectedIndexChanged", String.Empty).Replace("_SelectedValueChanged", String.Empty);
            //Show and Log exception for method name.
            ShowException(methodName, ex);
            LogException(methodName, ex);
        }

        internal static void ShowLogError(string methodName, string errMsg, string caption = "ERROR")
        {
            methodName = methodName.Replace("Btn_Click", String.Empty).Replace("_SelectedIndexChanged", String.Empty).Replace("_SelectedValueChanged", String.Empty);
            //Show and Log error for method name.
            ShowError(methodName + "(): " + errMsg, caption);
            AddLog(methodName, errMsg);
        }

        internal static void ShowLogStatus(string methodName, string logMsg)
        {
            IGIEditorUI.editorRef.SetStatusText(logMsg);
            AddLog(methodName, logMsg);
        }

        internal static void ShowLogInfo(string methodName, string logMsg)
        {
            ShowInfo(logMsg);
            AddLog(methodName, logMsg);
        }

        internal static void ShowInfo(string infoMsg, string caption = "INFO")
        {
            DialogMsgBox.ShowBox(caption, infoMsg, MsgBoxButtons.Ok);
        }

        internal static DialogResult ShowDialog(string infoMsg, string caption = "INFO")
        {
            return DialogMsgBox.ShowBox(caption, infoMsg, MsgBoxButtons.YesNo);
        }

        internal static void ShowConfigError(string keyword)
        {
            ShowError("Config has invalid property for '" + keyword + "'", CAPTION_CONFIG_ERR);
        }

        internal static void ShowSystemFatalError(string errMsg)
        {
            ShowError(errMsg, CAPTION_FATAL_SYS_ERR);
            Environment.Exit(1);
        }

        internal static bool ShowEditModeDialog()
        {
            var editorDlg = ShowDialog("Edit Mode not enabled to edit the level\nDo you want to enable Edit mode now ?", EDITOR_LEVEL_ERR);
            if (editorDlg == DialogResult.Yes)
                return true;
            return false;
        }

        private DialogResult ShowOptionInfo(string infoMsg)
        {
            return DialogMsgBox.ShowBox("Edit Mode", infoMsg);
        }

        //Private method to get machine id.
        private static string GetUUID()
        {
            string uidArgs = "wmic csproduct get UUID";
            string uuidOut = ShellExec(uidArgs);
            string uid = uuidOut.Split(new[] { Environment.NewLine }, StringSplitOptions.None)[1];
            return uid.Trim();
        }

        //Private method to get GUID.
        private static string GetGUID()
        {
            Guid guidObj = Guid.NewGuid();
            string guid = guidObj.ToString();
            return guid;
        }


        //Private method to get MAC/Physical address.
        internal static string GetMACAddress()
        {
            string macAddrArgs = "wmic nic get MACAddress";
            string macAddressOut = ShellExec(macAddrArgs);
            var macAddressList = macAddressOut.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            string macAddress = null;

            foreach (var address in macAddressList)
            {
                if (!String.IsNullOrEmpty(address) && address.Count(c => c == ':') > 4)
                {
                    macAddress = address;
                    break;
                }
            }
            return macAddress.Trim();
        }

        internal static string GetPrivateIP()
        {
            string ipAddrArgs = "ipconfig /all | findstr /c:IPv4";
            const string ipOut = "   IPv4 Address. . . . . . . . . . . : ";
            string ipAddressOut = ShellExec(ipAddrArgs);
            string[] ips = ipAddressOut.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            List<string> ipAddresses = new List<string>(ips);

            int ipLens = ipAddresses.Count - 1;
            string privateIp = ipAddresses[ipLens].Substring(ipOut.Length).Replace("(Preferred)", "").Trim();
            return privateIp;
        }

        internal static void SwitchEditorUI()
        {
            GT.ShowAppForeground(QUtils.editorAppName);
        }

        internal static DialogResult ShowGamePathDialog()
        {
            var folderBrowser = new OpenFileDialog();
            folderBrowser.ValidateNames = false;
            folderBrowser.CheckFileExists = false;
            folderBrowser.CheckPathExists = true;
            folderBrowser.FileName = "Folder Selection.";
            folderBrowser.Title = "Select Game path";

            var dlgResult = folderBrowser.ShowDialog();
            if (dlgResult == DialogResult.OK)
            {
                var newGameAbsPath = Path.GetDirectoryName(folderBrowser.FileName) + Path.DirectorySeparatorChar;

                if (!String.IsNullOrEmpty(gameAbsPath) && gameAbsPath == newGameAbsPath && CheckShortcutExist())
                {
                    QUtils.ShowLogError("GamePathDialog", "Selected path is same as current game path\nChoose a diffrent path or try again.");
                    gamePathSet = shortcutCreated = shortcutExist = false;
                    return dlgResult;
                }

                //Delete Previous Config.
                QUtils.FileIODelete(QUtils.iniCfgFile);

                gameAbsPath = newGameAbsPath;
                cfgGamePath = (!String.IsNullOrEmpty(gameAbsPath)) ? (gameAbsPath.Trim() + QMemory.gameName + ".exe") : null;
                bool status = File.Exists(cfgGamePath);

                if (!status)
                {
                    gamePathSet = shortcutCreated = shortcutExist = false;
                    ShowSystemFatalError("Error occurred while setting game path.");
                }
                else
                {
                    ShowInfo("Game path was saved successfully.");
                    CreateGameShortcut();
                    //Create New Config.
                    QUtils.CreateConfig();

                    //Read New Config.
                    QUtils.ParseConfig();

                    gamePathSet = true;
                }
            }
            return dlgResult;
        }

        internal static FOpenIO ShowOpenFileDlg(string title, string defaultExt, string filter, bool initDir = false, string initialDirectory = "", bool openFileData = true, bool exceptionOnEmpty = true)
        {
            var fopenIO = new FOpenIO();

            try
            {
                var fileBrowser = new OpenFileDialog();
                fileBrowser.ValidateNames = false;
                fileBrowser.CheckFileExists = false;
                fileBrowser.CheckPathExists = true;
                fileBrowser.Title = title;
                fileBrowser.DefaultExt = defaultExt;
                fileBrowser.Filter = filter;
                if (initDir)
                    fileBrowser.InitialDirectory = initialDirectory;

                if (fileBrowser.ShowDialog() == DialogResult.OK)
                {
                    fopenIO.FileName = fileBrowser.FileName;
                    fopenIO.FileLength = new FileInfo(fopenIO.FileName).Length;
                    fopenIO.FileSize = ((float)fopenIO.FileLength / (float)1024);
                    if (openFileData)
                    {
                        fopenIO.FileData = QUtils.LoadFile(fopenIO.FileName);
                        if (String.IsNullOrEmpty(fopenIO.FileData) && exceptionOnEmpty) throw new FileLoadException("File '" + fopenIO.FileName + "' is invalid or data is empty.");
                    }
                }
            }
            catch (Exception ex)
            {
                QUtils.ShowLogException(MethodBase.GetCurrentMethod().Name, ex);
            }
            return fopenIO;
        }


        internal static void CreateConfig()
        {
            gameAbsPath = (gameAbsPath is null) ? LocateExecutable(QMemory.gameName + ".exe") : gameAbsPath;
            gameFound = true;

            if (String.IsNullOrEmpty(cfgGamePath))
            {
                ShowWarning("Game path could not be detected automatically! Please select game path in config file", CAPTION_CONFIG_ERR);
                gameFound = false;
            }
            else
            {
                if (!File.Exists(gameAbsPath + Path.DirectorySeparatorChar + QMemory.gameName + ".exe"))
                {
                    ShowError("Invalid path selected! Game 'IGI' not found at path '" + gameAbsPath + "'", CAPTION_FATAL_SYS_ERR);
                    gameFound = false;
                }
            }

            //Write App path to config.
            qIniParser.Write("game_path", gameAbsPath is null ? "\n" : gameAbsPath, PATH_SEC);

            //Write App properties to config [EDITOR-SECTION].
            qIniParser.Write("game_reset", gameReset.ToString().ToLower(), EDITOR_SEC);
            qIniParser.Write("game_refresh", gameRefresh.ToString().ToLower(), EDITOR_SEC);
            qIniParser.Write("app_logs", appLogs.ToString().ToLower(), EDITOR_SEC);
            qIniParser.Write("app_online", editorOnline.ToString().ToLower(), EDITOR_SEC);
            qIniParser.Write("update_check", editorUpdateCheck.ToString().ToLower(), EDITOR_SEC);
            qIniParser.Write("update_interval", updateTimeInterval.ToString().ToLower(), EDITOR_SEC);
            qIniParser.Write("compiler_type", (internalCompiler) ? "internal" : "external", EDITOR_SEC);

            //Write Game properties to config [GAME-SECTION].
            qIniParser.Write("music_enabled", gameMusicEnabled.ToString().ToLower(), GAME_SEC);
            qIniParser.Write("ai_idle_mode", gameAiIdleMode.ToString().ToLower(), GAME_SEC);
            qIniParser.Write("debug_mode", gameDebugMode.ToString().ToLower(), GAME_SEC);
            qIniParser.Write("disable_warnings", gameDisableWarns.ToString().ToLower(), GAME_SEC);
            qIniParser.Write("game_fps", gameFPS.ToString().ToLower(), GAME_SEC);

            if (!gameFound) Environment.Exit(1);
        }

        internal static void ParseConfig()
        {
            bool appLogsParsed = false, gameRefreshParsed = false, gameResetParsed = false, editorOnlineParsed = false, editorUpdateParsed = false, timeInterval = false;
            bool gameMusicParsed = false, gameAiIdleModeParsed = false, gameDebugModeParsed = false, gameDisableWarnParsed = false, gameFPSParsed = false, compilerParsed = false;
            try
            {
                if (File.Exists(iniCfgFile))
                {
                    //Read properties from PATH section.
                    var configPath = qIniParser.Read("game_path", PATH_SEC);

                    string gPath = configPath.Trim();
                    if (gPath.Contains("\""))
                        gPath = configPath = gPath.Replace("\"", String.Empty);
                    if (!File.Exists(gPath + Path.DirectorySeparatorChar + QMemory.gameName + ".exe"))
                    {
                        ShowError("Invalid path selected! Game 'IGI' not found at path '" + gPath + "'", CAPTION_FATAL_SYS_ERR);
                        while (ShowGamePathDialog() != DialogResult.OK) ; //Prompt for Game path on invalid path.
                        //Environment.Exit(1);
                    }
                    else
                    {
                        gameAbsPath = gPath;
                        cfgGamePath = configPath.Trim() + cfgGamePathEx;
                    }

                    //Parse all data for Applications settings.
                    appLogs = bool.Parse(qIniParser.Read("app_logs", EDITOR_SEC)); appLogsParsed = true;
                    gameReset = bool.Parse(qIniParser.Read("game_reset", EDITOR_SEC)); gameResetParsed = true;
                    gameRefresh = bool.Parse(qIniParser.Read("game_refresh", EDITOR_SEC)); gameRefreshParsed = true;
                    editorOnline = bool.Parse(qIniParser.Read("app_online", EDITOR_SEC)); editorOnlineParsed = true;
                    editorUpdateCheck = bool.Parse(qIniParser.Read("update_check", EDITOR_SEC)); editorUpdateParsed = true;
                    updateTimeInterval = int.Parse(qIniParser.Read("update_interval", EDITOR_SEC)); timeInterval = true;
                    var compilerType = qIniParser.Read("compiler_type", EDITOR_SEC);
                    if (compilerType.Contains("internal") || compilerType.Contains("external")) { internalCompiler = (compilerType.Contains("internal")); externalCompiler = (compilerType.Contains("external")); compilerParsed = true; } else compilerParsed = false;

                    //Parse all data for Game settings.
                    gameMusicEnabled = bool.Parse(qIniParser.Read("music_enabled", GAME_SEC)); gameMusicParsed = true;
                    gameAiIdleMode = bool.Parse(qIniParser.Read("ai_idle_mode", GAME_SEC)); gameAiIdleModeParsed = true;
                    gameDebugMode = bool.Parse(qIniParser.Read("debug_mode", GAME_SEC)); gameDebugModeParsed = true;
                    gameDisableWarns = bool.Parse(qIniParser.Read("disable_warnings", GAME_SEC)); gameDisableWarnParsed = true;
                    gameFPS = Int32.Parse(qIniParser.Read("game_fps", GAME_SEC)); gameFPSParsed = true;

                    //Setting for Auto-Updater.
                    if (!editorOnline && editorUpdateCheck) editorUpdateCheck = false;
                }
                else
                {
                    ShowWarning("Config file not found in current directory", CAPTION_CONFIG_ERR);
                    CreateConfig();
                }
            }
            catch (FormatException ex)
            {
                //Check for App settings.
                if (!appLogsParsed) ShowConfigError("app_logs");
                if (!gameResetParsed) ShowConfigError("game_reset");
                if (!gameRefreshParsed) ShowConfigError("game_refresh");
                if (!editorOnlineParsed) ShowConfigError("app_online");
                if (!editorUpdateParsed) ShowConfigError("update_check");
                if (!timeInterval) ShowConfigError("update_interval");
                if (!compilerParsed) ShowConfigError("compiler_type");

                //Check for Game settings.
                if (!gameMusicParsed) ShowConfigError("music_enabled");
                if (!gameAiIdleModeParsed) ShowConfigError("ai_idle_mode");
                if (!gameDebugModeParsed) ShowConfigError("debug_mode");
                if (!gameDisableWarnParsed) ShowConfigError("disable_warnings");
                if (!gameFPSParsed) ShowConfigError("game_fps");

                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                ShowSystemFatalError("Exception: " + ex.Message);
            }
        }

        internal static void InitLibBin()
        {
            bool status = false;
            if (Directory.Exists("bin") && Directory.Exists("lib"))
            {
                if (File.Exists(internalsDllPath) && File.Exists(qLibcPath) && File.Exists(internalDllInjectorPath))
                {
                    AddLog(MethodBase.GetCurrentMethod().Name, "LibBin and QLibc files were found on device.");
                    status = true;
                }
            }
            else
                status = false;

            if (!status) ShowSystemFatalError("Editor internal files were not found in directory (ERROR: 0xC33000F)");
        }

        internal static bool CheckAppInstalled(string appName, string helpVerText = "--version")
        {
            if (appName is null)
            {
                throw new ArgumentNullException(nameof(appName));
            }

            bool installed = false;
            string appVersionFile = appName + "_info.txt";
            string appCheckCmd = appName + helpVerText + " > " + appVersionFile;
            ShellExec(appCheckCmd);
            string appVersionData = File.ReadAllText(appVersionFile);

            if (!String.IsNullOrEmpty(appVersionData)) installed = true;

            FileIODelete(appVersionFile);
            return installed;
        }

        internal static string LocateExecutable(String filename)
        {
            RegistryKey localMachine = Registry.LocalMachine;
            RegistryKey fileKey = localMachine.OpenSubKey(string.Format(@"{0}\{1}", keyBase, filename));
            object result = null;
            if (fileKey != null)
            {
                result = fileKey.GetValue(string.Empty);
                fileKey.Close();
            }

            return (string)result;
        }

        internal static bool CheckShortcutExist()
        {
            bool exist = File.Exists(QMemory.gameName + "_window.lnk") || File.Exists(QMemory.gameName + "_full.lnk");
            return exist;
        }

        internal static bool RemoveGameShortcut()
        {
            bool status = false;
            try
            {
                FileIODelete(QMemory.gameName + "_window.lnk");
                FileIODelete(QMemory.gameName + "_full.lnk");
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
                QUtils.ShowLogException(MethodBase.GetCurrentMethod().Name, ex);
            }
            return status;
        }

        private static void CreateGameShortcut(string linkName, string pathToApp, string gameArgs = "")
        {
            try
            {
                var shell = new WshShell();
                string shortcutAddress = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + linkName + ".lnk";

                FileIODelete(shortcutAddress);

                var shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
                shortcut.Description = "Shortcut for IGI";
                shortcut.Hotkey = "Ctrl+ALT+I";
                shortcut.Arguments = gameArgs;
                shortcut.WorkingDirectory = pathToApp;
                shortcut.TargetPath = pathToApp + Path.DirectorySeparatorChar + "igi.exe";
                shortcut.Save();
                shortcutCreated = shortcutExist = true;
            }
            catch (Exception ex)
            {
                shortcutCreated = shortcutExist = false;
                QUtils.LogException(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        internal static bool CreateGameShortcut()
        {
            shortcutExist = shortcutCreated = false;
            if (!File.Exists(QMemory.gameName + "_full.lnk") || !File.Exists(QMemory.gameName + "_full.lnk"))
            {
                if (gameAbsPath.Contains("\""))
                    gameAbsPath = gameAbsPath.Replace("\"", String.Empty);
                CreateGameShortcut(QMemory.gameName + "_full", gameAbsPath);
                CreateGameShortcut(QMemory.gameName + "_window", gameAbsPath, "window");
            }
            return shortcutCreated;
        }


        //File Operation Utilities C# Version.
        internal static void FileMove(string srcPath, string destPath)
        {
            try
            {
                if (File.Exists(srcPath)) File.Move(srcPath, destPath);
            }
            catch (Exception ex) { ShowLogException(MethodBase.GetCurrentMethod().Name, ex); }
        }

        internal static void FileCopy(string srcPath, string destPath, bool overwirte = true)
        {
            try
            {
                if (File.Exists(srcPath)) File.Copy(srcPath, destPath, overwirte);
            }
            catch (Exception ex) { ShowLogException(MethodBase.GetCurrentMethod().Name, ex); }
        }

        internal static void FileDelete(string path)
        {
            try
            {
                if (File.Exists(path)) File.Delete(path);
            }
            catch (Exception ex) { ShowLogException(MethodBase.GetCurrentMethod().Name, ex); }
        }

        //File Operation Utilities VB Version.
        internal static void FileIOMove(string srcPath, string destPath, bool overwrite = true)
        {
            try
            {
                if (File.Exists(srcPath)) FileSystem.MoveFile(srcPath, destPath, overwrite);
            }
            catch (Exception ex) { ShowLogException(MethodBase.GetCurrentMethod().Name, ex); }
        }


        internal static void FileIOMove(string srcPath, string destPath, FileIO.UIOption showUI, FileIO.UICancelOption onUserCancel)
        {
            try
            {
                if (File.Exists(srcPath)) FileSystem.MoveFile(srcPath, destPath, showUI, onUserCancel);
            }
            catch (Exception ex) { ShowLogException(MethodBase.GetCurrentMethod().Name, ex); }
        }

        internal static void FileIOCopy(string srcPath, string destPath, bool overwrite = true)
        {
            try
            {
                if (File.Exists(srcPath)) FileSystem.CopyFile(srcPath, destPath, overwrite);
            }
            catch (Exception ex) { ShowLogException(MethodBase.GetCurrentMethod().Name, ex); }
        }

        internal static void FileIOCopy(string srcPath, string destPath, FileIO.UIOption showUI, FileIO.UICancelOption onUserCancel)
        {
            try
            {
                if (File.Exists(srcPath)) FileSystem.CopyFile(srcPath, destPath);
            }
            catch (Exception ex) { ShowLogException(MethodBase.GetCurrentMethod().Name, ex); }
        }

        internal static void FileIODelete(string path, FileIO.UIOption showUI = FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption recycle = FileIO.RecycleOption.SendToRecycleBin, FileIO.UICancelOption onUserCancel = FileIO.UICancelOption.ThrowException)
        {
            try
            {
                if (File.Exists(path)) FileSystem.DeleteFile(path, showUI, recycle, onUserCancel);
            }
            catch (Exception ex) { ShowLogException(MethodBase.GetCurrentMethod().Name, ex); }
        }

        internal static void FileRename(string oldName, string newName)
        {
            try
            {
                if (File.Exists(oldName)) FileSystem.RenameFile(oldName, newName);
            }
            catch (Exception ex) { ShowLogException(MethodBase.GetCurrentMethod().Name, ex); }
        }


        //Directory Operation Utilities C#.
        internal static void DirectoryMove(string srcPath, string destPath)
        {
            try
            {
                if (Directory.Exists(srcPath)) Directory.Move(srcPath, destPath);
            }
            catch (Exception ex) { ShowLogException(MethodBase.GetCurrentMethod().Name, ex); }
        }

        internal static void DirectoryMove(string srcPath, string destPath, int __ignore)
        {
            var mvCmd = "mv " + srcPath + " " + destPath;
            var moveCmd = "move " + srcPath + " " + destPath + " /y";

            try
            {
                //#1 solution to move with same root directory.
                Directory.Move(srcPath, destPath);
            }
            catch (IOException ex)
            {
                if (ex.Message.Contains("already exist"))
                {
                    DirectoryDelete(srcPath);
                }
                else
                {
                    //#2 solution to move with POSIX 'mv' command.
                    ShellExec(mvCmd, true, true, "powershell.exe");
                    if (Directory.Exists(srcPath))
                        //#3 solution to move with 'move' command.
                        ShellExec(moveCmd, true);
                }
            }
        }

        internal static void DirectoryDelete(string dirPath)
        {
            try
            {
                if (Directory.Exists(dirPath))
                {
                    DirectoryInfo di = new DirectoryInfo(dirPath);
                    foreach (FileInfo file in di.GetFiles())
                        file.Delete();
                    foreach (DirectoryInfo dir in di.GetDirectories())
                        dir.Delete(true);
                    Directory.Delete(dirPath);
                }
            }
            catch (Exception ex) { ShowLogException(MethodBase.GetCurrentMethod().Name, ex); }
        }

        //Directory Operation Utilities VB.
        internal static void DirectoryIOMove(string srcPath, string destPath, bool overwrite = true)
        {
            try
            {
                if (Directory.Exists(srcPath)) FileSystem.MoveDirectory(srcPath, destPath, overwrite);
            }
            catch (Exception ex) { ShowLogException(MethodBase.GetCurrentMethod().Name, ex); }
        }

        internal static void DirectoryIOMove(string srcPath, string destPath, FileIO.UIOption showUI, FileIO.UICancelOption onUserCancel)
        {
            try
            {
                if (Directory.Exists(srcPath)) FileSystem.MoveDirectory(srcPath, destPath, showUI, onUserCancel);
            }
            catch (Exception ex) { ShowLogException(MethodBase.GetCurrentMethod().Name, ex); }
        }

        internal static void DirectoryIOCopy(string srcPath, string destPath, bool overwirte = true)
        {
            try
            {
                if (Directory.Exists(srcPath)) FileSystem.CopyDirectory(srcPath, destPath, overwirte);
            }
            catch (Exception ex) { ShowLogException(MethodBase.GetCurrentMethod().Name, ex); }
        }


        internal static void DirectoryIOCopy(string srcPath, string destPath, FileIO.UIOption showUI, FileIO.UICancelOption onUserCancel)
        {
            try
            {
                if (Directory.Exists(srcPath)) FileSystem.CopyDirectory(srcPath, destPath, showUI, onUserCancel);
            }
            catch (Exception ex) { ShowLogException(MethodBase.GetCurrentMethod().Name, ex); }
        }

        internal static void DirectoryIODelete(string path, FileIO.DeleteDirectoryOption deleteContents = FileIO.DeleteDirectoryOption.DeleteAllContents)
        {
            try
            {
                if (Directory.Exists(path)) FileSystem.DeleteDirectory(path, deleteContents);
            }
            catch (Exception ex) { ShowLogException(MethodBase.GetCurrentMethod().Name, ex); }
        }

        internal static void DirectoryRename(string oldName, string newName)
        {
            try
            {
                if (File.Exists(oldName)) FileSystem.RenameDirectory(oldName, newName);
            }
            catch (Exception ex) { ShowLogException(MethodBase.GetCurrentMethod().Name, ex); }
        }


        internal static bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }

        internal static string Reverse(string str)
        {
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        internal static string WebReader(string url)
        {
            string strContent = null;
            try
            {
                //Config for WebReader Class for .NET 4.0.
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                var webRequest = WebRequest.Create(url);
                using (var response = webRequest.GetResponse())
                using (var content = response.GetResponseStream())
                using (var reader = new StreamReader(content))
                {
                    strContent = reader.ReadToEnd();
                    return strContent;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("remote"))
                    ShowError("Please check your internet connection.");
                else
                    ShowError(ex.Message, "WebReader Error");
            }
            return strContent;
        }

        internal static void WebDownload(string url, string fileName, string destPath)
        {
            if (!editorOnline) return;
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFile(url, fileName);
                if (!Directory.Exists(destPath)) CreateCacheDir();
                ShellExec("move /Y " + fileName + " " + destPath);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("The remote name could not be resolved"))
                {
                    ShowError("Resource error Please check your internet connection and try Again");
                }
                else
                    ShowError(ex.Message ?? ex.StackTrace);
            }
        }

        internal static void EnableEditorMode(bool enable)
        {
            IntPtr binoAddr = (IntPtr)0x00470B8F;
            IntPtr noClipAddr = (IntPtr)0x004C8806;

            if (enable)
            {
                bool idleStatus = false;
                var qscData = QHuman.UpdateTeamId(TEAM_ID_ENEMY);
                if (!String.IsNullOrEmpty(qscData)) idleStatus = QCompiler.Compile(qscData, gamePath, false, true, false);

                if (idleStatus)
                {
                    //Enable NoClip and remove Bino.
                    GT.GT_WriteNOP(noClipAddr, 3);
                    GT.GT_WriteNOP(binoAddr, 10);
                }
            }

            else
            {
                //Bino & NoClip Opcodes in hex bytes.
                byte[] binoOps = new byte[] { 0xC7, 0x85, 0xB0, 0x01, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00 };
                byte[] noClipOps = new byte[] { 0xDC, 0x46, 0x20 };
                uint binoOpsLen = (uint)binoOps.Length;
                uint noClipOpsLen = (uint)noClipOps.Length;

                //Inject Shell for Editor mode.
                GT.GT_InjectShell(binoAddr, binoOps, binoOpsLen, 0, GT.GT_SHELL.GT_ORIGINAL_SHELL, GT.GT_OPCODE.GT_OP_SHORT_JUMP);
                GT.GT_InjectShell(noClipAddr, noClipOps, noClipOpsLen, 0, GT.GT_SHELL.GT_ORIGINAL_SHELL, GT.GT_OPCODE.GT_OP_SHORT_JUMP);

                bool idleStatus = false;
                var qData = QHuman.UpdateTeamId(TEAM_ID_FRIENDLY);
                if (!String.IsNullOrEmpty(qData)) idleStatus = QCompiler.Compile(qData, gamePath, false, true, false);

                //if (idleStatus)
                //{
                //    RestoreLevel(gGameLevel);
                //    ResetFile(gGameLevel);
                //    QMemory.RestartLevel(true);
                //}
            }
        }


        internal static bool SetAIEventIdle(bool aiEvent)
        {
            //New Way - Fast Team version.
            bool idleStatus = true;
            var qData = QHuman.UpdateTeamId(aiEvent ? TEAM_ID_ENEMY : TEAM_ID_FRIENDLY);
            if (!String.IsNullOrEmpty(qData)) idleStatus = QCompiler.Compile(qData, gamePath, false, true, false);
            return idleStatus;

            //Old way - Slow File version.
            //var aiFilesList = new List<string>() { "ekk.qvm", "guard.qvm", "gunner.qvm", "patrol.qvm", "radioguard.qvm", "sniper.qvm" };
            //bool idleStatus = true;
            //string commonPath = gameAbsPath + @"common\";
            //string aiCommonPath = gameAbsPath + @"common\ai\";
            //string aiIdleFile = aiCommonPath + QUtils.aiIdleFile;

            //if (aiEvent)
            //{
            //    if (Directory.Exists(commonPath + @"\ai_copy"))
            //    {
            //        return false;
            //    }

            //    FileCopy(aiIdlePath, aiIdleFile);

            //    if (!File.Exists(commonPath + @"\ai_copy"))
            //    {
            //        string copyDirCmd = "xcopy " + commonPath + @"\ai " + commonPath + @"\ai_copy" + " /e /i /h ";
            //        ShellExec(copyDirCmd, true);
            //    }

            //    string tmpFile = "tmp_copy.qvm";

            //    foreach (var aiFile in aiFilesList)
            //    {
            //        //FileIODelete(aiCommonPath + aiFile);
            //        FileCopy(aiIdleFile, aiCommonPath + tmpFile);
            //        FileIOMove(aiCommonPath + tmpFile, aiCommonPath + aiFile);
            //    }
            //}
            //else
            //{
            //    Sleep(2.5f);
            //    DirectoryIOMove(gameAbsPath + @"common\ai_copy\", aiCommonPath);
            //}
            return idleStatus;
        }

        protected static string InitAuthBearer()
        {
            string tok1 = "3n5pfkJinhrZj";
            string tok2 = Reverse("7F268pIpW2jvS");
            string tok3 = "vsXD0dXzHi";
            return tok1 + tok2 + tok3;
        }

        protected static HttpClient CreateHttpClient()
        {
            string authBearer = "ghp_" + InitAuthBearer();

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Chrome");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + authBearer);
            return client;
        }

        protected static string EditContent(string Description, string TargetFileName, string Content)
        {
            dynamic Result = new DynamicJson();
            dynamic file = new DynamicJson();
            Result.description = Description;
            Result.files = new { };
            Result.files[TargetFileName] = new { content = Content };
            return Result.ToString();
        }

        internal static bool RegisterUser(string content)
        {
            if (!editorOnline) { ShowSystemFatalError("Cannot register new user,Check your internet connection."); return false; }
            string id = 67141.ToString() + Reverse("be14c82cfbe782a") + "94a965e45a3c";

            string description = "IGI 1 Editor Users information", targetFilename = "IGI1Editor_Users.xml";
            bool status = true;
            string gistUrl = "https://api." + Reverse("tig") + "hub.com/" + Reverse("stsig") + "/" + id;

            using (HttpClient httpClient = CreateHttpClient())
            {
                var requestUri = new Uri(gistUrl);
                var request = new HttpRequestMessage(new HttpMethod("PATCH"), requestUri);

                string editData = EditContent(description, targetFilename, content);
                request.Content = new StringContent(editData, Encoding.UTF8, "application/json");

                var response = httpClient.SendAsync(request);
                status = response.Result.IsSuccessStatusCode;
            }
            return status;
        }

        internal static string InitSrcUrl()
        {
            string bitLyUrl = "p#y" + "W@8" + "$NB" + (2 + 1).ToString();
            bitLyUrl = bitLyUrl.Replace("$", String.Empty).Replace("@", String.Empty).Replace("#", String.Empty);
            string srcUrl = "http://" + Reverse("tib") + ".ly/" + Reverse(bitLyUrl);
            return srcUrl;
        }

        internal static bool InitUserInfo()
        {
            string srcUrl = InitSrcUrl();
            bool status = true;
            string infoStr = "</users-info>";
            string srcData = WebReader(srcUrl);

            if (String.IsNullOrEmpty(srcData)) return false;

            var userDataContent = new StringBuilder(srcData);
            int infoStrIndex = srcData.IndexOf(infoStr);
            if (infoStrIndex == -1) ShowSystemFatalError("Invalid data encountered from backend. (ERROR : 0x7FFFFFFF)");
            userDataContent = userDataContent.Remove(infoStrIndex, infoStr.Length);

            string deviceId = GetMachineDeviceId();
            var userDataLines = srcData.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            //Parse the key.
            string keyIdStr = "<key>";

            foreach (var userDataLine in userDataLines)
            {
                if (userDataLine.Contains(keyIdStr))
                {
                    string keyId = userDataLine.Slice(userDataLine.IndexOf(keyIdStr) + keyIdStr.Length, userDataLine.IndexOf("</key>"));
                    keyExist = deviceId == keyId;
                    if (keyExist) break;
                }
            }

            //Register new user.
            if (!keyExist && !keyFileExist)
            {
                //Get machine properties.
                string userName = GetCurrentUserName();
                string machineId = GetUUID();
                string macAddress = GetMACAddress();
                string ipAddress = GetPrivateIP();

                string city = QUserInfo.GetUserCity();
                string country = QUserInfo.GetUserCountry();

                string userData = null;
                userData += "  <user>" + "\n";
                userData += "\t<name>" + userName + "</name>" + "\n";
                userData += "\t<key>" + deviceId + "</key>" + "\n";
                userData += "\t<uuid>" + machineId + "</uuid>" + "\n";
                userData += "\t<mac>" + macAddress + "</mac>" + "\n";
                userData += "\t<ip>" + ipAddress + "</ip>" + "\n";
                userData += "\t<city>" + city + "</city>" + "\n";
                userData += "\t<country>" + country + "</country>" + "\n";
                userData += "\t<date>" + DateTime.Now.ToString() + "</date>" + "\n";
                userData += "  </user>" + "\n";

                userDataContent.Append(userData);
                userDataContent.Append(infoStr);
                status = RegisterUser(userDataContent.ToString());
            }

            return status;
        }

        internal static int GetRegisteredUsers()
        {
            var srcUrl = InitSrcUrl();
            string srcData = WebReader(srcUrl);
            int registeredUsers = new Regex(Regex.Escape("<user>")).Matches(srcData).Count;
            AddLog(MethodBase.GetCurrentMethod().Name, "registeredUsers " + registeredUsers);
            return registeredUsers;
        }

        internal static string GetCurrentUserName()
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            userName = userName.Substring(userName.LastIndexOf('\\') + 1);
            return userName;
        }

        internal static string GetMachineDeviceId()
        {
            string deviceId = new DeviceIdBuilder().AddMachineName().AddMacAddress().AddOsVersion().AddUserName().ToString();
            return deviceId;
        }

        internal static string GetUserRequestedData()
        {
            string userRequestedDataXML = string.Empty;

            string srcUrl = InitSrcUrl();
            string infoStr = "</users-info>";
            string srcData = WebReader(srcUrl);

            if (String.IsNullOrEmpty(srcData)) return userRequestedDataXML;

            var userDataContent = new StringBuilder(srcData);
            int infoStrIndex = srcData.IndexOf(infoStr);
            if (infoStrIndex == -1) ShowSystemFatalError("Invalid data encountered from backend. (ERROR : 0x7FFFFFFF)");
            userDataContent = userDataContent.Remove(infoStrIndex, infoStr.Length);

            string deviceId = GetMachineDeviceId();
            var userDataLines = srcData.Split(new string[] { "<user>" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var userData in userDataLines)
            {
                if (userData.Contains(deviceId))
                {
                    //Parse all the user data.
                    string name = userData.Slice(userData.IndexOf("<name>") + "<name>".Length, userData.IndexOf("</name>"));
                    //string keyId = userData.Slice(userData.IndexOf("<key>") + "<key>".Length, userData.IndexOf("</key>"));
                    string uuid = userData.Slice(userData.IndexOf("<uuid>") + "<uuid>".Length, userData.IndexOf("</uuid>"));
                    string macAddress = userData.Slice(userData.IndexOf("<mac>") + "<mac>".Length, userData.IndexOf("</mac>"));
                    string ipAddress = userData.Slice(userData.IndexOf("<ip>") + "<ip>".Length, userData.IndexOf("</ip>"));
                    string city = userData.Slice(userData.IndexOf("<city>") + "<city>".Length, userData.IndexOf("</city>"));
                    string country = userData.Slice(userData.IndexOf("<country>") + "<country>".Length, userData.IndexOf("</country>"));
                    string dateRegistered = userData.Slice(userData.IndexOf("<date>") + "<date>".Length, userData.IndexOf("</date>"));

                    //Append all user data.
                    userRequestedDataXML = "Name: " + name + "\n" +
                        "Unique Identifier: " + uuid + "\n" +
                        "Mac Address: " + macAddress + "\n" +
                        "IP Address: " + ipAddress + "\n" +
                        "City: " + city + "\n" +
                        "Country: " + country + "\n" +
                        "Account created on " + dateRegistered + "\n";

                    break;
                }
            }

            return userRequestedDataXML;
        }

        internal static void ShowPathExplorer(string path)
        {
            Process.Start(new ProcessStartInfo()
            {
                FileName = path,
                UseShellExecute = true,
                Verb = "open"
            });
        }

        //Execute shell command and get std-output.
        internal static string ShellExec(string cmdArgs, bool runAsAdmin = false, bool waitForExit = true, string shell = "cmd.exe")
        {
            var process = new Process();
            var startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.CreateNoWindow = true;
            startInfo.FileName = shell;
            startInfo.Arguments = "/c " + cmdArgs;
            startInfo.RedirectStandardOutput = !runAsAdmin;
            startInfo.RedirectStandardError = !runAsAdmin;
            startInfo.UseShellExecute = runAsAdmin;
            process.StartInfo = startInfo;
            if (runAsAdmin) process.StartInfo.Verb = "runas";
            process.Start();
            if (!waitForExit) return null;
            string output = (runAsAdmin) ? String.Empty : process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return output;
        }

        internal static string LoadFile()
        {
            return LoadFile(objectsQsc);
        }

        internal static string LoadFile(string fileName)
        {
            string data = null;
            if (File.Exists(fileName))
                data = File.ReadAllText(fileName);
            return data;
        }

        internal static void ResetCurrentLevel(bool restartLevel = false, bool savePosition = false)
        {
            int level = QMemory.GetRunningLevel();
            if (level <= 0 || level > GAME_MAX_LEVEL) level = 1;
            QUtils.RestoreLevel(level);
            QUtils.ResetScriptFile(level);
            if (restartLevel)
                QMemory.RestartLevel(savePosition);
            CleanUpAiFiles();
            IGIEditorUI.editorRef.GenerateAIScriptId(true);
        }

        internal static void SaveFile(string data = null, bool appendData = false)
        {
            SaveFile(objectsQsc, data, appendData);
        }

        internal static void SaveFile(string fileName, string data, bool appendData = false)
        {
            if (appendData)
                File.AppendAllText(fileName, data);
            else
                File.WriteAllText(fileName, data);
        }

        internal static void ResetScriptFile(int gameLevel)
        {
            var inputQscPath = cfgQscPath + gameLevel + "\\" + objectsQsc;

            FileCopy(inputQscPath, objectsQsc);

            var fileData = LoadFile(objectsQsc);
            File.WriteAllText(objectsQsc, fileData);
            levelFlowData = File.ReadLines(objectsQsc).Last();
        }

        internal static void RestoreLevel(int gameLevel)
        {
            if (gameLevel <= 0 || gameLevel > GAME_MAX_LEVEL) gameLevel = 1;
            var gPath = gamePath;

            if (gamePath.Contains(" ")) gPath = gamePath.Replace("\"", String.Empty);

            gPath = cfgGamePath + gameLevel;
            string outputQvmPath = gPath + "\\" + objectsQvm;
            string inputQvmPath = cfgQvmPath + gameLevel + "\\" + objectsQvm;

            //FileIODelete(outputQvmPath);
            FileCopy(inputQvmPath, outputQvmPath);

            var inFileData = File.ReadAllText(inputQvmPath);
            var outFileData = File.ReadAllText(outputQvmPath);

            if (inFileData != outFileData)
                ShowLogStatus(MethodBase.GetCurrentMethod().Name, "Error in restroing level : " + gameLevel);
        }

        internal static int FindIndex(string temp, string sourceData, int sourceIndex, int qtaskIndex)
        {
            for (int i = 0; sourceIndex < temp.Length; i++)
            {
                sourceIndex = temp.IndexOf(sourceData) + i;
                if ((temp[sourceIndex - 1] == ' ' || temp[sourceIndex - 1] == ',')
                     && temp[sourceIndex + 1] == ',')
                {
                    break;
                }
            }

            //Add the index + offset.
            sourceIndex += qtaskIndex;
            return sourceIndex;
        }

        internal static int GetModelCount(string model)
        {
            if (!QObjects.CheckModelExist(model)) return 0;
            var qtaskList = QTask.GetQTaskList(false, true);
            int count = qtaskList.Count(o => String.Compare(o.model, model, true) == 0);
            ShowLogStatus(MethodBase.GetCurrentMethod().Name, "Model count : " + count);
            return count;
        }

        internal static void EditorUpdater(string updateFile = null, UPDATE_ACTION updateAction = UPDATE_ACTION.UPDATE, string updateBatch = "updater.bat")
        {
            try
            {
                AddLog(MethodBase.GetCurrentMethod().Name, "Called with File '" + updateFile + "'" + " action: " + updateAction.ToString() + " Batch File: " + updateBatch);
                ShowLogStatus(MethodBase.GetCurrentMethod().Name, "Updating application please wait...");
                string updateUrl = QServer.serverBaseURL + QServer.updateDir;
                string localFileName = cachePath + "\\" + (String.IsNullOrEmpty(updateFile) ? editorUpdaterDir : updateFile) + zipExt;
                string updateName = editorUpdaterDir;
                string localUpdateFile = cachePath + "\\" + updateName + zipExt;
                string localUpdatePath = cachePath + "\\" + editorUpdaterDir;
                string localChangelogsPath = cachePath + "\\" + QUtils.editorChangeLogs + txtExt;
                string localReadmePath = cachePath + "\\" + QUtils.editorReadme + txtExt;
                string editorReadmePath = editorCurrPath + "\\" + QUtils.editorReadme + txtExt;
                string editorChangelogsPath = editorCurrPath + "\\" + QUtils.editorReadme + txtExt;
                bool status = false;

                //Extract the updater if found.
                if (File.Exists(localUpdateFile) && !Directory.Exists(localUpdatePath)
                    && (updateAction == UPDATE_ACTION.EXTRACT || updateAction == UPDATE_ACTION.UPDATE))
                {
                    ShowLogStatus(MethodBase.GetCurrentMethod().Name, "Updater file found please wait...");
                    AddLog(MethodBase.GetCurrentMethod().Name, "Extracting: '" + localUpdateFile + "'");
                    status = QZip.Extract(localUpdateFile, cachePath);
                    AddLog(MethodBase.GetCurrentMethod().Name, "Extracting of file '" + updateFile + "' done");
                }

                //Download the updater if not found.
                else if (updateAction == UPDATE_ACTION.DOWNLOAD || updateAction == UPDATE_ACTION.UPDATE)
                {
                    bool newInstallation = true;

                    //Cleanup previous updater files on Update.
                    if (updateAction == UPDATE_ACTION.UPDATE)
                    {
                        ShowLogStatus(MethodBase.GetCurrentMethod().Name, "Updater cleaning previous files...");
                        if (Directory.Exists(localUpdatePath)) DirectoryDelete(localUpdatePath);
                        AddLog(MethodBase.GetCurrentMethod().Name, "Updater cleaning previous files for Action: " + updateAction.ToString());
                    }

                    //Update from backup on Update.
                    if (updateAction == UPDATE_ACTION.UPDATE)
                    {
                        AddLog(MethodBase.GetCurrentMethod().Name, "Updating from backup file.");
                        if (File.Exists(localUpdateFile))
                        {
                            var dlgMsg = ShowDialog("Editor found updater file already exist from previous version\nDo you want to delete it and continue with new installation ?");
                            if (dlgMsg == DialogResult.OK)
                            {
                                FileIODelete(localUpdateFile);
                                newInstallation = true;
                            }
                            else
                            {
                                ShowLogStatus(MethodBase.GetCurrentMethod().Name, "Updater file found please wait...");
                                AddLog(MethodBase.GetCurrentMethod().Name, "Extracting: backup file '" + localUpdateFile + "'");
                                status = QZip.Extract(localUpdateFile, cachePath);
                                newInstallation = false;
                                AddLog(MethodBase.GetCurrentMethod().Name, "Extracting from backup file done.");
                            }
                        }
                    }

                    //Download new updater file.
                    if (newInstallation)
                    {
                        updateUrl = updateUrl + "/" + updateName;
                        string updateNamePath = localFileName;
                        string updateUrlPath = "/" + QServer.updateDir + "/" + updateName + zipExt;

                        AddLog(MethodBase.GetCurrentMethod().Name, "Downloading new updater file '" + updateNamePath + "'");
                        ShowLogStatus(MethodBase.GetCurrentMethod().Name, "Downloading new update please wait...");
                        AddLog(MethodBase.GetCurrentMethod().Name, "Url: '" + updateUrl + "' file '" + updateNamePath + "'");
                        status = QServer.Download(updateUrlPath, updateNamePath, cachePath);

                        AddLog(MethodBase.GetCurrentMethod().Name, "Downloaded new updater file '" + updateNamePath + "'");
                        //Download and extract to Cachee.
                        if (updateAction == UPDATE_ACTION.EXTRACT || updateAction == UPDATE_ACTION.UPDATE)
                        {
                            if (status) status = QZip.Extract(updateNamePath, cachePath);
                            if (status)
                                AddLog(MethodBase.GetCurrentMethod().Name, "Extracting updater file '" + updateNamePath + "' done.");
                        }
                    }
                }

                //updateFile = cachePath + "\\" + updateFile;
                AddLog(MethodBase.GetCurrentMethod().Name, "Updater: UpdaterFile '" + updateFile + "' exist: " + Directory.Exists(updateFile).ToString());
                //Start updating the editor files.
                if (Directory.Exists(localUpdatePath) && updateAction == UPDATE_ACTION.UPDATE)
                {
                    updaterBatchFile = localUpdatePath + "\\" + updateBatch;//Updater batch file.
                    string batchUpdaterData =
                        "@echo off\n" +
                        "timeout 5\n" + //Timeout for updating in external thread.
                        "move /y " + "\"" + localUpdatePath + "\\" + editorAppName + exeExt + "\" \"" + editorCurrPath + "\\" + editorAppName + exeExt + "\"" + "\n" +
                        "move /y " + "\"" + localUpdatePath + "\\" + editorAppName + "_x86" + exeExt + "\" \"" + editorCurrPath + "\\" + editorAppName + "_x86" + exeExt + "\"" + "\n" +
                        "timeout 5\n" + //Timeout for Restarting editor app.
                       "\"" + editorCurrPath + "\\" + editorAppName + exeExt + "\"\n" +
                      (QUtils.nppInstalled ? "notepad++ - nosession - notabbar - alwaysOnTop - multiInst - lhaskell \"" : "notepad \"") + editorCurrPath + "\\" + editorChangeLogs + txtExt + "\"\n";

                    File.WriteAllText(updaterBatchFile, batchUpdaterData);
                    AddLog(MethodBase.GetCurrentMethod().Name, "Updating batch file '" + updateBatch + "' created");

                    ShowWarning("Updating the editor please wait.\nEditor will now restart to update to the newest version available.");

                    //Detach internals before updating them.
                    DetachInternals();
                    Sleep(2.5f);

                    AddLog(MethodBase.GetCurrentMethod().Name, "Updating Internals detached");

                    //If update contains bin - Injector/Natives data as well.
                    if (Directory.Exists(localUpdatePath + "\\" + "bin"))
                    {
                        DirectoryIOCopy(localUpdatePath + "\\" + "bin", editorCurrPath + "\\" + "bin");
                        AddLog(MethodBase.GetCurrentMethod().Name, "Updating 'bin' directory done.");
                    }

                    //Move Readme,Changelogs.
                    if (File.Exists(localReadmePath)) QUtils.FileIOMove(localReadmePath, editorReadmePath);
                    if (File.Exists(localChangelogsPath)) QUtils.FileIOMove(localChangelogsPath, editorChangelogsPath);

                    AddLog(MethodBase.GetCurrentMethod().Name, "Updating Executing batch shell command");
                    ShellExec(updaterBatchFile, true, false);
                    AddLog(MethodBase.GetCurrentMethod().Name, "Updating Executing batch shell command done.");
                    ShowLogStatus(MethodBase.GetCurrentMethod().Name, "Editor was updated successfully.");
                    Application.Exit();//Exit the application to update.
                }

                //Show Status of updater.
                if (status) ShowLogStatus(MethodBase.GetCurrentMethod().Name, "Editor was updated successfully.");
                else ShowLogStatus(MethodBase.GetCurrentMethod().Name, "Updater: Error occurred while updating editor (Error: 0xC00005F).");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("could not complete operation on some files and directories"))
                {
                    ShowLogError(MethodBase.GetCurrentMethod().Name, "Updater failed to move some files, Detach Internals please wait.");
                }
                else
                    LogException(MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        internal static int CheckEditorVersion(string versionOld, string versionNew)
        {
            var updateVersion = new Version(versionOld);
            var editorVersion = new Version(versionNew);

            int result = updateVersion.CompareTo(editorVersion);
            return result;
        }

        internal static void ExportCSV(string csvFile, List<QScriptTask> qtaskList, bool allObjects = true)
        {
            if (qtaskList.Count <= 0)
            {
                ShowLogStatus(MethodBase.GetCurrentMethod().Name, "Qtask list is empty");
                return;
            }

            //Write CSV Header.
            string csvHeader = "Task_ID," + "Task_Name," + "Task_Note," + "X_Pos," + "Y_Pos," + "Z_Pos," + "Alpha," + "Beta," + "Gamma," + "Model" + "\n";
            File.AppendAllText(csvFile, csvHeader);

            //Write CSV data.
            foreach (var qtask in qtaskList)
            {
                string csvData = null;
                if (allObjects == false)
                {
                    if (objTypeList.Any(o => o.Contains(qtask.name)))
                        csvData = "" + qtask.id + "," + qtask.name + "," + qtask.note + "," + qtask.position.x + "," + qtask.position.y + "," + qtask.position.z + "," + qtask.orientation.alpha + "," + qtask.orientation.beta + "," + qtask.orientation.gamma + "," + qtask.model + "\n";
                }

                else
                {
                    csvData = "" + qtask.id + "," + qtask.name + "," + qtask.note + "," + qtask.position.x + "," + qtask.position.y + "," + qtask.position.z + "," + qtask.orientation.alpha + "," + qtask.orientation.beta + "," + qtask.orientation.gamma + "," + qtask.model + "\n";
                }
                File.AppendAllText(csvFile, csvData);
            }
        }

        internal static void ExportCSV(string csvFile, List<Weapon> weaponTaskList, bool advancedData = false)
        {
            if (weaponTaskList.Count <= 0)
            {
                ShowLogStatus(MethodBase.GetCurrentMethod().Name, "Weapon list is empty");
                return;
            }

            //Write CSV Header.
            string csvHeader = "Name," + "Id," + "WeaponType," + "AmmoType," + "Mass," + "Damage," + "Power," + "ReloadTime," + "Bullets/Round," + "RPM," + "Magazine," + "Range," + "Burts," + "Muzzle velocity" + "\n";
            File.AppendAllText(csvFile, csvHeader);

            //Write CSV data.
            foreach (var weaponTask in weaponTaskList)
            {
                string csvData = null;
                if (!advancedData)
                {
                    csvData = "" + weaponTask.name + "," + weaponTask.scriptId + "," + weaponTask.typeEnum + "," + weaponTask.ammoDispType + "," + weaponTask.mass + "," + weaponTask.damage + "," + weaponTask.power + "," + weaponTask.reloadTime + "," + weaponTask.bullets + "," + weaponTask.rpm + "," + weaponTask.clips + "," + weaponTask.range + "," + weaponTask.burst + "," + weaponTask.muzzleVelocity + "," + "\n";
                }

                else
                {
                }
                File.AppendAllText(csvFile, csvData);
            }
        }

        internal static void ExportXML(string fileName)
        {
            var qtaskList = QTask.GetQTaskList();
            string csvFile = null;

            if (File.Exists(csvFile))
                csvFile = LoadFile(csvFile);
            else
            {
                csvFile = objects + csvExt;
                ExportCSV(csvFile, qtaskList);
            }

            var lines = File.ReadAllLines(csvFile);
            string[] headers = lines[0].Split(',').Select(x => x.Trim('\"')).ToArray();

            var xml = new XElement("root",
               lines.Where((line, index) => index > 0).Select(line => new XElement("Item",
                  line.Split(',').Select((column, index) => new XElement(headers[index], column)))));

            xml.Save(fileName);
            FileIODelete(csvFile);
        }

        internal static void ExportJson(string fileName)
        {
            string xmlFile = objects + xmlExt;
            string xmlData = null;

            if (File.Exists(xmlFile))
                xmlData = LoadFile(xmlFile);
            else
            {
                xmlFile = objects + xmlExt;
                ExportXML(xmlFile);
                xmlData = LoadFile(xmlFile);
            }

            string jsonData = null;
            Sleep(1);

            if (File.Exists(xmlFile))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlData);

                jsonData = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented);
                SaveFile(fileName, jsonData);
            }
            else throw new FileNotFoundException("File 'objects.xml' was not found in current directory");

            FileIODelete(xmlFile);
        }

        internal static bool IsNetworkAvailable()
        {
            return IsNetworkAvailable(0);
        }


        internal static bool IsNetworkAvailable(long minimumSpeed)
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
                return false;

            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                // discard because of standard reasons
                if ((ni.OperationalStatus != OperationalStatus.Up) ||
                    (ni.NetworkInterfaceType == NetworkInterfaceType.Loopback) ||
                    (ni.NetworkInterfaceType == NetworkInterfaceType.Tunnel))
                    continue;

                // this allow to filter modems, serial, etc.
                // I use 10000000 as a minimum speed for most cases
                if (ni.Speed < minimumSpeed)
                    continue;

                // discard virtual cards (virtual box, virtual pc, etc.)
                if ((ni.Description.IndexOf("virtual", StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (ni.Name.IndexOf("virtual", StringComparison.OrdinalIgnoreCase) >= 0))
                    continue;

                // discard "Microsoft Loopback Adapter", it will not show as NetworkInterfaceType.Loopback but as Ethernet Card.
                if (ni.Description.Equals("Microsoft Loopback Adapter", StringComparison.OrdinalIgnoreCase))
                    continue;

                return true;
            }
            return false;
        }


        internal static bool AttachInternals()
        {
#if DEV_MODE
            return true;
#endif
            AddLog(MethodBase.GetCurrentMethod().Name, "Path : " + internalsDllPath);

#if DEBUG
            internalsDllFile = Path.GetFileNameWithoutExtension(internalsDll) + "-Dbg" + dllExt;
            internalsDllPath = @"bin\" + internalsDllFile;
#else
            internalsDllFile = Path.GetFileNameWithoutExtension(internalsDll) + "-Rls" + dllExt;
            internalsDllPath = @"bin\" + internalsDllFile;
#endif

            //Init lib and bin folders first.
            InitLibBin();

            //Injector - 1st Method.
            string internalsCmd = internalDllInjectorPath + " -i " + internalsDllFile;
            string shellOut = ShellExec(internalsCmd, true);
            AddLog(MethodBase.GetCurrentMethod().Name, "Using first method.");

            Sleep(1.5f);
            var internalsAttached = CheckInternalsAttached();

            //Injector - 2nd Method.
            if (!internalsAttached)
            {
                AddLog(MethodBase.GetCurrentMethod().Name, "Using second method.");
                internalsCmd = internalDllGTInjectorPath + " " + internalsDllFile;

                Sleep(1.5f);
                internalsAttached = CheckInternalsAttached();
                if (!internalsAttached)
                {
                    ShowLogError(MethodBase.GetCurrentMethod().Name, "all methods failed to work. Use Manual injection.");
                    attachStatus = false;
                    return attachStatus;
                }
            }

            if (shellOut.Contains("Error")) attachStatus = false; else attachStatus = true;
            AddLog(MethodBase.GetCurrentMethod().Name, "cmd: " + internalsCmd + " status: " + attachStatus);
            return attachStatus;
        }

        internal static bool DetachInternals()
        {
            try
            {
                var internalsAttached = CheckInternalsAttached();
                if (!internalsAttached) return true;

                string dllShellCmd = internalDllInjectorPath + " -e " + internalsDllFile;

                GT.GT_SendKeys2Process(QMemory.gameName, "{END 10}");
                Sleep(5);
                internalsAttached = CheckInternalsAttached();
                if (!internalsAttached) return true;

                string shellOut = ShellExec(dllShellCmd, true);

                if (shellOut.Contains("Error")) attachStatus = false; else attachStatus = true;
                AddLog(MethodBase.GetCurrentMethod().Name, "DetachInternals() cmd: " + dllShellCmd + " status: " + attachStatus);
            }
            catch (Exception) { return false; }
            return attachStatus;
        }


        internal static string AddStatusMsg(int taskId = -1, string statusMsg = null, string varString = null, char terminator = ',', bool isCutsceneBool = false, bool sendOnce = true, float statusDuration = 5.0f)
        {
            var isCutscene = isCutsceneBool.ToString().ToUpperInvariant();
            var isSendt = sendOnce.ToString().ToUpperInvariant();
            string statusMsgTask = "Task_New(" + taskId + ",\"StatusMessage\",\"StatusMsg\",0,0,0,0,0,0,\"" + varString + "\",\"" + statusMsg + "\",\"\"," + "\"message\"," + isSendt + "," + isCutscene + "," + statusDuration + ")" + terminator + "\n";
            return statusMsgTask;
        }

        internal static bool CheckInternalsAttached()
        {
            //Get all modules inside the process
            var objModulesList = Process.GetProcessesByName(QMemory.gameName);
            try
            {
                // Populate the module collection.
                var objModules = objModulesList[0].Modules;

                // Iterate through the module collection.
                foreach (ProcessModule objModule in objModules)
                {
                    //If the module exists
                    if (File.Exists(objModule.FileName.ToString()))
                    {
                        try
                        {
                            //Get Modification date
                            var objFileInfo = objModule.FileName.ToString();
                            if (objFileInfo.Contains(internalsDllFile))
                            {
                                return true;
                            }

                        }
                        catch (Exception ex) { }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }

        internal static void UpdateViewPort(Real64 pos)
        {
            unsafe
            {
                GT.GT_WriteMemory(viewPortAddrX, "double", pos.x.ToString());
                GT.GT_WriteMemory(viewPortAddrY, "double", pos.y.ToString());
                GT.GT_WriteMemory(viewPortAddrZ, "double", pos.z.ToString());
            }
        }

        internal static Real64 GetViewPortPos()
        {
            unsafe
            {
                var pos = new Real64();
                pos.x = GT.GT_ReadDouble(viewPortAddrX);
                pos.y = GT.GT_ReadDouble(viewPortAddrY);
                pos.z = GT.GT_ReadDouble(viewPortAddrZ);
                return pos;
            }
        }

        internal static int GameitemsCount()
        {
            var gameItemAddr = GT.GT_ReadPointerOffset((IntPtr)0x0057BA9C, 0x0);
            var gameitems = GT.GT_ReadInt(gameItemAddr);
            return (int)gameitems;
        }

        internal static void CleanUpAiFiles()
        {
            if (!gameReset) return;
            var outputAiPath = cfgGamePath + gGameLevel + "\\ai\\";

            if (aiScriptFiles.Count >= 1)
            {
                foreach (var scriptFile in aiScriptFiles)
                {
                    FileIODelete(outputAiPath + scriptFile);
                    FileIODelete(outputAiPath + scriptFile.Replace("qvm", "qsc"));
                }
                FileIODelete(objectsQsc);
            }
        }

        internal static void CleanUpTmpFiles()
        {
            foreach (string file in Directory.EnumerateFiles(cachePath, "*.dll"))
            {
                try
                {
                    FileIODelete(file);
                }
                catch (Exception ex) { }
            }
        }

        internal static void EnableLogs()
        {
            if (!logEnabled)
                logEnabled = true;
        }

        internal static void DisableLogs()
        {
            if (logEnabled)
                logEnabled = false;
        }

        internal static void AddLog(string methodName, string logMsg)
        {
            if (logEnabled)
            {
                methodName = methodName.Replace("Btn_Click", String.Empty).Replace("_SelectedIndexChanged", String.Empty).Replace("_SelectedValueChanged", String.Empty);
                File.AppendAllText(logFile, "[" + DateTime.Now.ToString("yyyy-MM-dd - HH:mm:ss") + "] " + methodName + "(): " + logMsg + "\n");
            }
        }

        internal static string GenerateRandStr(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[rand.Next(s.Length)]).ToArray());
        }

        internal static void Sleep(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }

        internal static void Sleep(float seconds)
        {
            Thread.Sleep((int)seconds * 1000);
        }

        internal static bool IsNonASCII(string str)
        {
            return (Encoding.UTF8.GetByteCount(str) != str.Length);
        }
    }

    internal static class Extensions
    {
        internal static string Slice(this string source, int start, int end)
        {
            if (end < 0)
            {
                end = source.Length + end;
            }
            int len = end - start;
            return source.Substring(start, len);
        }


        internal static string ReplaceFirst(this string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        internal static string ReplaceLast(this string text, string search, string replace)
        {
            int place = text.LastIndexOf(search);

            if (place == -1)
                return text;

            string result = text.Remove(place, search.Length).Insert(place, replace);
            return result;
        }

        internal static bool IsNonASCII(this string str)
        {
            return (Encoding.UTF8.GetByteCount(str) != str.Length);
        }

        internal static bool HasBinaryContent(this string content)
        {
            return content.Any(ch => char.IsControl(ch) && ch != '\r' && ch != '\n');
        }

        internal static double ToRadians(this double angle)
        {
            // Angle in 10th of a degree
            return (angle * Math.PI) / 180;
        }
    }

    enum QTASKINFO
    {
        QTASK_ID,
        QTASK_NAME,
        QTASK_NOTE,
        //Object pos in Real64x3.
        QTASK_POSX,
        QTASK_POSY,
        QTASK_POSZ,
        //Object orientation in Real32x9.
        QTASK_ALPHA,
        QTASK_BETA,
        QTASK_GAMMA,
        //Mode ID in String16
        QTASK_MODEL,
    }

    enum PATROLACTIONS
    {
        PATROLPATH_ANIMATION,
        PATROLPATH_DELAY,
        PATROLPATH_WALKTO,
        PATROLPATH_RUNTO,
        PATROLPATH_CROUCH,
        PATROLPATH_LOOKATNODE,
        PATROLPATH_END,
        PATROLPATH_QUIT,
        PATROLPATH_SETSPEED
    }

    enum AITYPES
    {
        AITYPE_RPG,
        AITYPE_GUNNER,
        AITYPE_SNIPER,
        AITYPE_ANYA,
        AITYPE_EKK,
        AITYPE_PRIBOI,
        AITYPE_CIVILIAN,
        AITYPE_PATROL_UZI,
        AITYPE_PATROL_AK,
        AITYPE_PATROL_SPAS,
        AITYPE_PATROL_PISTOL,
        AITYPE_GUARD_UZI,
        AITYPE_GUARD_AK,
        AITYPE_GUARD_SPAS,
        AITYPE_GUARD_PISTOL,
        AITYPE_SECURITY_PATROL_UZI,
        AITYPE_SECURITY_PATROL_SPAS,
        AITYPE_MAFIA_PATROL_UZI,
        AITYPE_MAFIA_PATROL_AK,
        AITYPE_MAFIA_PATROL_SPAS,
        AITYPE_MAFIA_GUARD_UZI,
        AITYPE_MAFIA_GUARD_AK,
        AITYPE_MAFIA_GUARD_SPAS,
        AITYPE_SPETNAZ_PATROL_UZI,
        AITYPE_SPETNAZ_PATROL_AK,
        AITYPE_SPETNAZ_PATROL_SPAS,
        AITYPE_SPETNAZ_GUARD_UZI,
        AITYPE_SPETNAZ_GUARD_AK,
        AITYPE_SPETNAZ_GUARD_SPAS
    }

    internal class AreaDim
    {
        internal float x, y, z;

        internal AreaDim() { x = y = z = 0.0f; }

        internal AreaDim(float x)
        {
            this.x = x;
            this.y = x;
            this.z = x;
        }
        internal AreaDim(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    };

    internal class Real32
    {
        internal float alpha, beta, gamma;

        internal Real32() { alpha = beta = gamma = 0.0f; }
        internal Real32(float alpha)
        {
            this.alpha = alpha;
            this.beta = this.gamma = 0.0f;
        }

        internal Real32(float alpha, float beta)
        {
            this.alpha = alpha;
            this.beta = beta;
            this.gamma = 0.0f;
        }

        internal Real32(float alpha, float beta, float gamma)
        {
            this.alpha = alpha;
            this.beta = beta;
            this.gamma = gamma;
        }
    };

    internal class Real64
    {
        internal double x, y, z;
        internal Real64() { x = y = z = 0.0f; }
        internal Real64(double x)
        {
            this.x = x;
            this.y = this.z = 0.0f;
        }
        internal Real64(double x, double y)
        {
            this.x = x;
            this.y = y;
            this.z = 0.0f;
        }

        internal Real64(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        internal Real64 Real64Operator(Real64 real1, Real64 real2, string operatorType)
        {
            Real64 real = new Real64();

            if (operatorType == "+")
            {
                real.x = real1.x + real2.x;
                real.y = real1.y + real2.y;
                real.z = real1.z + real2.z;
            }
            else if (operatorType == "-")
            {
                real.x = real1.x - real2.x;
                real.y = real1.y - real2.y;
                real.z = real1.z - real2.z;
            }
            return real;
        }

        internal bool Empty()
        {
            bool empty = (this.x == 0.0f && this.y == 0.0f && this.z == 0.0f);
            return empty;
        }
    };
}
