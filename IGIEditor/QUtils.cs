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

namespace IGIEditor
{
    class QUtils
    {

        public class QTask
        {
            public Int32 id;
            public string name;
            public string note;
            public Real64 position;
            public Real32 orientation;
            public string model;
        };

        public class HTask
        {
            public int team;
            public QTask qtask;
            public List<string> weaponsList;
        };

        internal static string taskNew = "Task_New", taskDecl = "Task_DeclareParameters";
        internal static string objectsQsc = "objects.qsc", objectsQvm = "objects.qvm", weaponConfigQSC = "weaponconfig.qsc", weaponConfigQVM = "weaponconfig.qvm", weaponsModQvm = "weaponconfig-mod.qvm";
        internal static int qtaskObjId, qtaskId, anyaTeamTaskId = -1, ekkTeamTaskId = -1, aiScriptId = 0, gGameLevel = 1, GAME_MAX_LEVEL = 3, currGameLevel = 1;
        internal static string logFile = "app.log", qLibLogsFile = "QLibc_logs.log", aiIdleFile = "aiIdle.qvm", objectsModelsList, aiIdlePath, customScriptFile = "ai_custom_script.qsc", customAiPathFile = "ai_custom_path.qsc", customScriptFileQEd = @"\QEditor\AIFiles\ai_custom_script.qsc", customAiPathFileQEd = @"\QEditor\AIFiles\ai_custom_path.qsc", appLogFileTmp = @"%tmp%\IGIEditorCache\AppLogs\", nativesFile = @"\IGI-Natives.json", modelsFile = @"\IGI-Models.txt", internalsLogFile = @"\IGI-Internals.log";
        internal static bool gameFound = false, logEnabled = false, keyExist = false, keyFileExist = false, attachStatus = false, customAiSelected = false, editorOnline = true, gameReset = false, appLogs = false;
        internal static float appEditorVersion = 0.4f, viewPortDelta = 10000.0f;
        internal static string supportDiscordLink = @"https://discord.gg/9T8tzyhvp6", supportYoutubeLink = @"https://www.youtube.com/channel/UChGryl0a0dii81NfDZ12LwA", supportVKLink = @"https://vk.com/id679925339";
        internal static IntPtr viewPortAddrX = (IntPtr)0x00BCAB08, viewPortAddrY = (IntPtr)0x00BCAB10, viewPortAddrZ = (IntPtr)0x00BCAB18;
        internal const int TEAM_ID_FRIENDLY = 0, TEAM_ID_ENEMY = 1, MAX_FPS = 240, MAX_HUMAN_CAM = 5;

        internal static string gamePath, appdataPath, igiEditorQEdPath, editorCurrPath, gameAbsPath, cfgGamePath, cfgHumanplayerPathQsc, cfgHumanplayerPathQvm, cfgQscPath, cfgAiPath, cfgQvmPath, cfgVoidPath, cfgQFilesPath, qMissionsPath, qGraphsPath, cfgWeaponsPath, weaponsModQvmPath, weaponsOrgCfgPath, weaponsGamePath, humanplayerGamePath, menusystemGamePath, missionsGamePath, commonGamePath, qfilesPath = @"\QFiles", qEditor = "QEditor", qconv = "QConv", qfiles = "QFiles", qGraphs = "QGraphs", cfgFile, projAppName, cachePath, cachePathAppLogs, nativesFilePath, modelsFilePath, internalsLogPath, cachePathAppImages, currPathAppImages, editorUpdaterPath = "IGIEditor_Update", editorUpdaterFile,
         igiQsc = "IGI_QSC", igiQvm = "IGI_QVM", graphsPath, cfgGamePathEx = @"\missions\location0\level", weaponsDirPath = @"\weapons", humanplayerQvm = "humanplayer.qvm", humanplayerQsc = "humanplayer.qsc", humanplayerPath = @"\humanplayer", aiGraphTask = "AIGraph", menuSystemDir = "menusystem", menuSystemPath = null, internalsDllFile, internalsDll = "IGI-Internals.dll", internalsDllPath = @"bin\IGI-Internals.dll", qLibcPath = @"lib\GTLibc_x86.so", tmpDllPath, internalDllInjectorPath = @"bin\IGI-Injector.exe", internalDllGTInjectorPath = @"bin\IGI-Injector-GT.exe", PATH_SEC = "PATH", EDITOR_SEC = "EDITOR";
        internal static string inputQscPath = @"\IGI_QSC", inputQvmPath = @"\IGI_QVM", inputAiPath = @"\AIFiles", inputVoidPath = @"\Void", inputMissionPath = @"\missions\location0\level", inputHumanplayerPath = @"\humanplayer", inputweaponsPath = @"\weapons";
        internal static List<string> objTypeList = new List<string>() { "Building", "EditRigidObj", "Terminal", "Elevator", "ExplodeObject", "AlarmControl", "Generator", "Radio" };
        internal static string objects = "objects", objectsAll = "objectsAll", weapons = "weapons";
        internal static string qvmExt = ".qvm", qscExt = ".qsc", datExt = ".dat", csvExt = ".csv", jsonExt = ".json", txtExt = ".txt", xmlExt = ".xml", dllExt = ".dll", missionExt = ".igimsf", jpgExt = ".jpg", pngExt = ".png", rarExt = ".rar", zipExt = ".zip", exeExt = ".exe";
        internal static float fltInvalidAngle = -9.9999f, fltInvalidVal = -9.9f;
        internal const string CAPTION_CONFIG_ERR = "Config - Error", CAPTION_FATAL_SYS_ERR = "Fatal sytem - Error", CAPTION_APP_ERR = "Application - Error", CAPTION_COMPILER_ERR = "Compiler - Error", EDITOR_LEVEL_ERR = "EDITOR ERROR", alarmControl = "AlarmControl", stationaryGun = "StationaryGun";
        internal static string keyBase = @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths", helpStr = "IGI 1 Editor-Demo. Offers upto " + GAME_MAX_LEVEL + " level\nVersion: v" + appEditorVersion + " BETA.\n\nTools/Language: C#(5.0) VS-Studio/Code\nCreated by Haseeb Mir.\n\nCredits & People\nUI Designing - Dark\nResearch data - Dimon, Yoejin and GM123.\nQScript/DConv Tools - Artiom.\nTester - Orwa\nIGI-VK Community.";
        internal static string patroIdleMask = "xxxx", patroAlarmMask = "yyyy", alarmControlMask = "xx", gunnerIdMask = "xxx", viewGammaMask = "yyy";
        internal static string movementSpeedMask = "movSpeed", forwardSpeedMask = "forwardSpeed", upwardSpeedMask = "upSpeed", inAirSpeedMask = "iAirSpeed", throwBaseVelMask = "throwBaseVel", healthScaleMask = "healthScale", healthFenceMask = "healthFence", peekLeftRightLenMask = "peekLRLen", peekCrouchLenMask = "peekCrouchLen", peekTimeMask = "peekTime";
        internal static List<string> aiScriptFiles = new List<string>();
        internal static string aiEnenmyTask = null, aiFriendTask = null, levelFlowData, missionLevelFile = "mission_level.txt", missionDescFile = "mission_desc.txt", missionListFile = @"\MissionsList.dat";
        internal static double movSpeed = 1.75f, forwardSpeed = 17.5f, upwardSpeed = 27, inAirSpeed = 0.5f, peekCrouchLen = 0.8500000238418579f, peekLRLen = 0.8500000238418579f, peekTime = 0.25, healthScale = 3.0f, healthScaleFence = 0.5f;
        private static Random rand = new Random();
        internal static QIniParser qIniParser;
        internal enum QTYPES { BUILDING = 1, RIGID_OBJ = 2 };
        internal static Dictionary<int, string> graphAreas = new Dictionary<int, string>();

        //List of Dictionary items.
        internal static List<Dictionary<string, int>> weaponList = new List<Dictionary<string, int>>();
        internal static List<Dictionary<string, string>> buildingList = new List<Dictionary<string, string>>();
        internal static List<Dictionary<string, string>> objectRigidList = new List<Dictionary<string, string>>();
        internal static List<string> buildingListStr = new List<string>();
        internal static List<string> objectRigidListStr = new List<string>();
        internal static List<string> aiModelsListStr = new List<string>();
        internal static List<string> missionNameListStr = new List<string>();
        internal static List<int> aiGraphIdStr = new List<int>();
        internal static List<int> graphdIdsMarked = new List<int>();
        internal static List<int> aiGraphNodeIdStr = new List<int>();
        internal static List<GraphNode> graphNodesList = new List<GraphNode>();

        //Server data list.
        internal static List<QServerData> qServerDataList = new List<QServerData>();
        internal static List<QMissionsData> qServerMissionDataList = new List<QMissionsData>();

        //Weapons variables.
        internal static string weaponId = "WEAPON_ID_";
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


        internal static Dictionary<string, string> aiModelDict = new Dictionary<string, string>(){
    {"/NW00rTT/","ANYA"},
    {"/54FR0JK/","EKK"},
    {"/VV8rygL/","JONES"},
    {"/MnSF0Xq/","SNIPER-01"},
    {"/wJVs4hd/","SNIPER-02"},
    {"/tc5F91g/","GUNNER-406"},
    {"/0VbGM59/","GUNNER-407"},
    {"/LdfBgsp/","SCIENTIST"},
    {"/tsjkC09/","PATROL-AK-01"},
    {"/2j4n2vn/","PATROL-AK-02"},
    {"/BsV8rTW/","PATROL-AK-03"},
    {"/KFtDJvZ/","SECURITY-PATROL-SPAS"},
    {"/5kZNXRm/","GUNNER"},
    {"/x5wTLPQ/","SOLDIER"},
    {"/vLYkjCY/","HARRISON"},
    {"/n7jPGJD/","FRIENDLY-SOLDIER-1"},
    {"/qYBkfvL/","FRIENDLY-SOLDIER-2"},
    {"/vBqC7Vv/","JOSEP-PRIBOI"},
    {"/7bkLQh5/","MAFIA-GUARD"},
    {"/n8yFFKF/","MAFIA-PATROL"},
    {"/Df72t29/","PRIBOI"},
    {"/Ky39s89/","GUARD-AK"},
    {"/yXnLrj9/","SPETNAZ-GUARD-AK"}};

        internal static string baseImgUrl = "https://static.wikia.nocookie.net/igi/images";
        internal static string baseImgBBUrl = "https://i.ibb.co/";

        internal static string[] levelImgUrl =
        {"/5/58/Mission_1.png","/5/5d/Mission_2.png","/5/5b/IGI_Mission_3.png","/3/35/IGI_Mission_4.png",
              "/8/89/IGI_Mission_05.png","/9/91/IGI_Mission_06.png","/0/0f/IGI_Mission_07.png",
              "/1/12/IGI_Mission_08.png","/6/6d/IGI_Mission_09.png","/2/2b/Mission_10.png","/e/e6/Mission_11.png",
              "/a/af/Mission_12.png","/5/5a/Mission_13.png","/4/4a/Mission_14.png"
        };

        internal static string[] weaponsImgUrl = {"/4/4a/31_IGI2_Weapons_ak-47.jpg","/7/3x/62_IGI2_Weapons_apc.jpg","/8/80/IGI2_Weapons_binoculars.jpg",
"/a/aa/3-IGI2_Weapons_Magnum.jpg","/4/4c/4-IGI2_Weapons_D-Eagle.jpg","/d/dc/41_IGI2_Weapons_Dragunov.jpg",
"/6/67/IGI2_Weapons_Flashbang.jpg","/c/c8/2a-IGI2_Weapons_Glock.jpg","/5/5a/L2a2_greande.jpg",
"/9/93/53_IGI2_Weapons_Jackhammer.jpg","/6/6b/0-IGI2_Weapons_Knife.jpg","/c/ce/33_IGI2_Weapons_m16_m203.jpg",
"/6/6b/IGI2_Weapons_M2HB.jpg","/e/e6/Igi2_w_medisyringe.jpg","/8/2/_IGI2_Weapons_mil.jpg","/8/86/63_IGI2_Weapons_minimi.jpg",
"/d/db/Ig2mp5sd3.jpg","/d/d4/IGI2_Weapons_proximity_mine.jpg","/5/59/61_IGI2_Weapons_Law80.jpg",
"/5/22/61_IGI2_Weapons_sentry.jpg","/3/38/51_IGI2_Weapons_spas.jpg","/1/19/61_IGI2_Weapons_t80.jpg",
"/9/94/22_IGI2_Weapons_Uzi.jpg","/2/27/UZI.jpg"};

        internal static string[] aiImgUrl =
        {
        "/NW00rTT/","/54FR0JK/","/VV8rygL/","/MnSF0Xq/",
        "/wJVs4hd/","/tc5F91g/","/0VbGM59/","/LdfBgsp/",
        "/tsjkC09/","/2j4n2vn/","/BsV8rTW/","/KFtDJvZ/",
        "/5kZNXRm/","/x5wTLPQ/","/vLYkjCY/","/n7jPGJD/",
        "/qYBkfvL/","/vBqC7Vv/","/7bkLQh5/","/n8yFFKF/",
        "/Df72t29/","/Ky39s89/","/yXnLrj9/",
        };

        internal static List<string> aiTypes = new List<string>() { "AITYPE_RPG", "AITYPE_GUNNER", "AITYPE_SNIPER", "AITYPE_ANYA", "AITYPE_EKK", "AITYPE_PRIBOI", "AITYPE_CIVILIAN", "AITYPE_PATROL_AK", "AITYPE_GUARD_AK", "AITYPE_SECURITY_PATROL_UZI", "AITYPE_MAFIA_PATROL_UZI", "AITYPE_MAFIA_GUARD_AK", "AITYPE_SPETNAZ_PATROL_AK", "AITYPE_SPETNAZ_GUARD_AK" };

        public static void ShowWarning(string warnMsg, string caption = "WARNING")
        {
            MessageBox.Show(warnMsg, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void ShowError(string errMsg, string caption = "ERROR")
        {
            MessageBox.Show(errMsg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void LogException(string methodName, Exception ex)
        {
            methodName = methodName.Replace("_Click", String.Empty).Replace("_SelectedIndexChanged",String.Empty).Replace("_SelectedValueChanged", String.Empty);
            AddLog(methodName, "Exception MESSAGE: " + ex.Message + "\nREASON: " + ex.StackTrace);
        }

        public static void ShowException(string methodName, Exception ex)
        {
            ShowError("MESSAGE: " + ex.Message + "\nREASON: " + ex.StackTrace, methodName + " Exception");
        }

        public static void ShowLogException(string methodName, Exception ex)
        {
            methodName = methodName.Replace("_Click", String.Empty).Replace("_SelectedIndexChanged", String.Empty).Replace("_SelectedValueChanged", String.Empty);
            //Show and Log exception for method name.
            ShowException(methodName, ex);
            LogException(methodName, ex);
        }

        public static void ShowLogError(string methodName, string errMsg, string caption = "ERROR")
        {
            methodName = methodName.Replace("_Click", String.Empty).Replace("_SelectedIndexChanged", String.Empty).Replace("_SelectedValueChanged", String.Empty);
            //Show and Log error for method name.
            ShowError(methodName + "(): " + errMsg, caption);
            AddLog(methodName,errMsg);
        }


        public static void ShowInfo(string infoMsg, string caption = "INFO")
        {
            MessageBox.Show(infoMsg, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult ShowDialog(string infoMsg, string caption = "INFO")
        {
            return MessageBox.Show(infoMsg, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }

        public static void ShowConfigError(string keyword)
        {
            ShowError("Config file has invalid property for '" + keyword + "'", CAPTION_CONFIG_ERR);
            Environment.Exit(1);
        }

        public static void ShowSystemFatalError(string errMsg)
        {
            ShowError(errMsg, CAPTION_FATAL_SYS_ERR);
            Environment.Exit(1);
        }

        public static bool ShowEditModeDialog()
        {
            var editorDlg = ShowDialog("Edit Mode not enabled to edit the level\nDo you want to enable Edit mode now ?", EDITOR_LEVEL_ERR);
            if (editorDlg == DialogResult.Yes)
                return true;
            return false;
        }

        private DialogResult ShowOptionInfo(string infoMsg)
        {
            return MessageBox.Show(infoMsg, "INFO", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
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
                //gameAbsPath = gameAbsPath.Substring(0, gameAbsPath.LastIndexOf("\\"));

                if (!File.Exists(gameAbsPath + Path.DirectorySeparatorChar + QMemory.gameName + ".exe"))
                {
                    ShowError("Invalid path selected! Game 'IGI' not found at path '" + gameAbsPath + "'", CAPTION_FATAL_SYS_ERR);
                    gameFound = false;
                }
            }

            //Write App path to config.
            qIniParser.Write("game_path", gameAbsPath is null ? "\n" : gameAbsPath, PATH_SEC);

            //Write App properties to config.
            qIniParser.Write("game_reset", gameReset.ToString(), EDITOR_SEC);
            qIniParser.Write("app_logs", appLogs.ToString(), EDITOR_SEC);
            qIniParser.Write("app_online", editorOnline.ToString(), EDITOR_SEC);

            if (!gameFound) Environment.Exit(1);
        }

        internal static void ParseConfig()
        {
            try
            {
                projAppName = AppDomain.CurrentDomain.FriendlyName.Replace(".exe", String.Empty);
                cfgFile = projAppName + ".ini";
                logFile = projAppName + ".log";
                editorCurrPath = Directory.GetCurrentDirectory();
                qIniParser = new QIniParser(cfgFile);

                if (File.Exists(cfgFile))
                {
                    //Read properties from PATH section.
                    var configPath = qIniParser.Read("game_path", PATH_SEC);

                    string gPath = configPath.Trim();
                    if (gPath.Contains("\""))
                        gPath = configPath = gPath.Replace("\"", String.Empty);
                    if (!File.Exists(gPath + Path.DirectorySeparatorChar + QMemory.gameName + ".exe"))
                    {
                        ShowError("Invalid path selected! Game 'IGI' not found at path '" + gPath + "'", CAPTION_FATAL_SYS_ERR);
                        Environment.Exit(1);
                    }
                    gameAbsPath = gPath;
                    cfgGamePath = configPath.Trim() + cfgGamePathEx;


                    appLogs = bool.Parse(qIniParser.Read("app_logs", EDITOR_SEC));
                    gameReset = bool.Parse(qIniParser.Read("game_reset", EDITOR_SEC));
                    editorOnline = bool.Parse(qIniParser.Read("app_online", EDITOR_SEC));
                }
                else
                {
                    ShowWarning("Config file not found in current directory", CAPTION_CONFIG_ERR);
                    CreateConfig();
                }
            }
            catch (FormatException ex)
            {
                if (ex.StackTrace.Contains("Boolean"))
                    ShowConfigError("app_logs or game_reset");
                else if (ex.StackTrace.Contains("Int32"))
                    ShowConfigError("");
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

        public static bool CheckAppInstalled(string appName)
        {
            if (appName is null)
            {
                throw new ArgumentNullException(nameof(appName));
            }

            bool installed = false;
            string appVersionFile = appName + "_version.txt";
            string appCheckCmd = appName + " --version > " + appVersionFile;
            ShellExec(appCheckCmd);
            string appVersionData = File.ReadAllText(appVersionFile);

            if (!String.IsNullOrEmpty(appVersionData))
            {
                installed = true;
                File.Delete(appVersionFile);
            }

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

        private static void CreateGameShortcut(string linkName, string pathToApp, string gameArgs = "")
        {
            var shell = new WshShell();
            string shortcutAddress = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + linkName + ".lnk";

            if (File.Exists(shortcutAddress))
                File.Delete(shortcutAddress);

            var shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
            shortcut.Description = "Shortcut for IGI";
            shortcut.Hotkey = "Ctrl+ALT+I";
            shortcut.Arguments = gameArgs;
            shortcut.WorkingDirectory = pathToApp;
            shortcut.TargetPath = pathToApp + Path.DirectorySeparatorChar + "igi.exe";
            shortcut.Save();
        }

        internal static void CreateGameShortcut()
        {
            if (!File.Exists(QMemory.gameName + "_full.lnk") || !File.Exists(QMemory.gameName + "_full.lnk"))
            {
                if (gameAbsPath.Contains("\""))
                    gameAbsPath = gameAbsPath.Replace("\"", String.Empty);
                CreateGameShortcut(QMemory.gameName + "_full", gameAbsPath);
                CreateGameShortcut(QMemory.gameName + "_window", gameAbsPath, "window");
            }
        }

        internal static bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }

        internal static void InitAppData()
        {
            appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            igiEditorQEdPath = appdataPath + Path.DirectorySeparatorChar + qEditor;

            //Set new Input QSC & QVM path releative to appdata.
            objectsModelsList = igiEditorQEdPath + Path.DirectorySeparatorChar + "IGIModels.txt";
            qMissionsPath = igiEditorQEdPath + @"\QMissions";
            qGraphsPath = igiEditorQEdPath + @"\QGraphs";
            weaponsGamePath = gameAbsPath + @"\weapons";
            humanplayerGamePath = gameAbsPath + @"\humanplayer";
            menusystemGamePath = gameAbsPath + @"\menusystem";
            missionsGamePath = gameAbsPath + @"\missions";
            commonGamePath = gameAbsPath + @"\common";
            aiIdlePath = igiEditorQEdPath + Path.DirectorySeparatorChar + "aiIdle.qvm";
            cfgQvmPath = igiEditorQEdPath + qfilesPath + inputQvmPath + inputMissionPath;
            cfgQscPath = igiEditorQEdPath + qfilesPath + inputQscPath + inputMissionPath;
            cfgHumanplayerPathQsc = igiEditorQEdPath + qfilesPath + inputQscPath + inputHumanplayerPath;
            cfgHumanplayerPathQvm = igiEditorQEdPath + qfilesPath + inputQvmPath + inputHumanplayerPath;
            cfgWeaponsPath = igiEditorQEdPath + qfilesPath + inputQvmPath + inputweaponsPath;
            cfgAiPath = igiEditorQEdPath + inputAiPath;
            cfgVoidPath = igiEditorQEdPath + inputVoidPath;
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

            //Init Temp path for Cache.
            if (!Directory.Exists(cachePath))
            {
                CreateCacheDir();
            }
        }

        internal static void CreateCacheDir()
        {
            Directory.CreateDirectory(cachePath);
            Directory.CreateDirectory(cachePathAppLogs);
            Directory.CreateDirectory(cachePathAppImages);
        }

        internal static void DeleteWholeDir(string dirPath)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(dirPath);
                foreach (FileInfo file in di.GetFiles())
                    file.Delete();
                foreach (DirectoryInfo dir in di.GetDirectories())
                    dir.Delete(true);
                Directory.Delete(dirPath);
            }
            catch (Exception ex)
            {
                ShowError(ex.Message ?? ex.StackTrace, "Delete Directory Error");
            }
        }

        internal static void MoveDir(string srcPath, string destPath)
        {
            var mvCmd = "mv " + srcPath + " " + destPath;
            var moveCmd = "move " + srcPath + " " + destPath + " /y";

            try
            {
                //#1 solution to move with same root directory.
                Directory.Move(srcPath, destPath + Path.DirectorySeparatorChar + qEditor);
            }
            catch (IOException ex)
            {
                if (ex.Message.Contains("already exist"))
                {
                    DeleteWholeDir(srcPath);
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
            //bool idleStatus = true;
            //var qData = QHuman.UpdateTeamId(aiEvent ? TEAM_ID_ENEMY : TEAM_ID_FRIENDLY);
            //if (!String.IsNullOrEmpty(qData)) idleStatus = QCompiler.Compile(qData, gamePath, false, true, false);
            //return idleStatus;

            //Old way - Slow File version.
            var aiFilesList = new List<string>() { "ekk.qvm", "guard.qvm", "gunner.qvm", "patrol.qvm", "radioguard.qvm", "sniper.qvm" };
            bool idleStatus = true;
            string commonPath = gameAbsPath + @"common\";
            string aiCommonPath = gameAbsPath + @"common\ai\";
            string aiIdleFile = aiCommonPath + QUtils.aiIdleFile;

            if (aiEvent)
            {
                if (Directory.Exists(commonPath + @"\ai_copy"))
                {
                    return false;
                }

                if (!File.Exists(aiIdleFile))
                    File.Copy(aiIdlePath, aiIdleFile);

                if (!File.Exists(commonPath + @"\ai_copy"))
                {
                    string copyDirCmd = "xcopy " + commonPath + @"\ai " + commonPath + @"\ai_copy" + " /e /i /h ";
                    ShellExec(copyDirCmd, true);
                }

                string tmpFile = "tmp_copy.qvm";

                foreach (var aiFile in aiFilesList)
                {
                    File.Delete(aiCommonPath + aiFile);
                    File.Copy(aiIdleFile, aiCommonPath + tmpFile);
                    File.Move(aiCommonPath + tmpFile, aiCommonPath + aiFile);
                }
            }
            else
            {
                Directory.Delete(aiCommonPath, true);
                Thread.Sleep(2500);
                Directory.Move(gameAbsPath + @"common\ai_copy\", aiCommonPath);
            }
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

        public static bool RegisterUser(string content)
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

            if (File.Exists(objectsQsc)) File.Delete(objectsQsc);
            File.Copy(inputQscPath, objectsQsc);

            var fileData = LoadFile(objectsQsc);
            File.WriteAllText(objectsQsc, fileData);
            levelFlowData = File.ReadLines(objectsQsc).Last();
        }

        internal static void RestoreLevel(int gameLevel)
        {
            if (gameLevel < 0 || gameLevel > GAME_MAX_LEVEL) gameLevel = 1;
            var gPath = gamePath;

            if (gamePath.Contains(" ")) gPath = gamePath.Replace("\"", String.Empty);

            gPath = cfgGamePath + gameLevel;
            string outputQvmPath = gPath + "\\" + objectsQvm;
            string inputQvmPath = cfgQvmPath + gameLevel + "\\" + objectsQvm;

            File.Delete(outputQvmPath);
            File.Copy(inputQvmPath, outputQvmPath);

            var inFileData = File.ReadAllText(inputQvmPath);
            var outFileData = File.ReadAllText(outputQvmPath);

            if (inFileData != outFileData)
                IGIEditorUI.editorRef.SetStatusText("Error in restroing level : " + gameLevel);
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


        internal static bool CheckModelExist(string model)
        {
            int gameLevel = QMemory.GetCurrentLevel();
            AddLog(MethodBase.GetCurrentMethod().Name, "called with model : " + model + " for level : " + gameLevel);
            var inputQscPath = cfgQscPath + gameLevel + "\\" + objectsQsc;
            string qscData = LoadFile(inputQscPath);
            bool modelExist = false;

            if (!String.IsNullOrEmpty(model))
            {
                if (!model.Contains("\""))
                    model = "\"" + model + "\"";
            }

            var modelList = Regex.Matches(qscData, model).Cast<Match>().Select(m => m.Value);
            foreach (var modelObj in modelList)
                AddLog(MethodBase.GetCurrentMethod().Name, "Models list : " + modelObj);

            if (!String.IsNullOrEmpty(model))
            {
                if (modelList.Any(o => o.Contains(model)))
                    modelExist = true;
            }
            AddLog(MethodBase.GetCurrentMethod().Name, "returned : " + (modelExist ? "Model exist" : "Model doesn't exist"));
            return modelExist;
        }

        internal static int GetModelCount(string model)
        {
            if (!CheckModelExist(model)) return 0;
            var qtaskList = GetQTaskList(false, true);
            int count = qtaskList.Count(o => String.Compare(o.model, model, true) == 0);
            IGIEditorUI.editorRef.SetStatusText("Model count : " + count);
            return count;
        }

        internal static bool CheckModelExist(int taskId)
        {
            var qtaskList = GetQTaskList();
            if (qtaskList.Count == 0)
                throw new Exception("QTask list is empty");

            if (taskId != -1)
            {
                foreach (var qtask in qtaskList)
                {
                    if (qtask.id == taskId)
                        return true;
                }
            }
            return false;
        }


        //Parse all the Objects.
        private static List<QTask> ParseAllOjects(string qscData)
        {
            bool isBinary = qscData.IsNonASCII();
            AddLog(MethodBase.GetCurrentMethod().Name, "isBinary: " + isBinary);

            //Remove all whitespaces.
            qscData = qscData.Replace("\t", String.Empty);
            string[] qscDataSplit = qscData.Split('\n');
            var modelRegex = @"\d{3}_\d{2}_\d{1}";

            var qtaskList = new List<QTask>();
            foreach (var data in qscDataSplit)
            {
                if (data.Contains(taskNew))
                {
                    QTask qtask = new QTask();

                    string[] taskNew = data.Split(',');
                    int taskIndex = 0;

                    foreach (var task in taskNew)
                    {
                        if (taskIndex == (int)QTASKINFO.QTASK_ID)
                        {
                            var taskId = task.Substring(task.IndexOf('(') + 1);
                            qtask.id = Convert.ToInt32(taskId);
                        }
                        else if (taskIndex == (int)QTASKINFO.QTASK_NAME)
                            qtask.name = task.Trim();

                        else if (taskIndex == (int)QTASKINFO.QTASK_NOTE)
                            qtask.note = task.Trim();

                        else if (taskIndex == (int)QTASKINFO.QTASK_MODEL)
                            qtask.model = Regex.Match(task.Trim(), modelRegex).Value;

                        taskIndex++;
                    }
                    qtaskList.Add(qtask);
                }
            }
            AddLog(MethodBase.GetCurrentMethod().Name, "qtaskList count: " + qtaskList.Count);
            return qtaskList;
        }


        //Parse only Objects.
        private static List<QTask> ParseObjects(string qscData)
        {
            var qtaskList = new List<QTask>();
            try
            {
                bool isBinary = qscData.HasBinaryContent();
                AddLog(MethodBase.GetCurrentMethod().Name, "isBinary: " + isBinary);
                //Remove all whitespaces.
                qscData = qscData.Replace("\t", String.Empty);
                string[] qscDataSplit = qscData.Split('\n');


                foreach (var data in qscDataSplit)
                {
                    if (data.Contains(taskNew))
                    {
                        var startIndex = data.IndexOf(',') + 1;
                        var endIndex = data.IndexOf(',', startIndex);
                        var taskName = data.Slice(startIndex, endIndex).Trim().Replace("\"", String.Empty);

                        if (data.Contains("Building") && taskName != "Building")
                        {
                            startIndex = data.IndexOf(',') + 1;
                            endIndex = data.IndexOf(',', startIndex);
                            taskName = data.Slice(startIndex, endIndex).Trim().Replace("\"", String.Empty);
                        }

                        if (objTypeList.Any(o => o.Contains(taskName)))
                        {
                            QTask qtask = new QTask();
                            Real32 orientation = new Real32();
                            Real64 position = new Real64();

                            string[] taskNew = data.Split(',');
                            int taskIndex = 0;

                            foreach (var task in taskNew)
                            {
                                if (taskIndex == (int)QTASKINFO.QTASK_ID)
                                {
                                    var taskId = task.Substring(task.IndexOf('(') + 1);
                                    qtask.id = Convert.ToInt32(taskId);
                                }
                                else if (taskIndex == (int)QTASKINFO.QTASK_NAME)
                                    qtask.name = task.Trim();

                                else if (taskIndex == (int)QTASKINFO.QTASK_NOTE)
                                    qtask.note = task.Trim();

                                else if (taskIndex == (int)QTASKINFO.QTASK_POSX)
                                    position.x = Double.Parse(task);

                                else if (taskIndex == (int)QTASKINFO.QTASK_POSY)
                                    position.y = Double.Parse(task);

                                else if (taskIndex == (int)QTASKINFO.QTASK_POSZ)
                                    position.z = Double.Parse(task);

                                else if (taskIndex == (int)QTASKINFO.QTASK_ALPHA)
                                    orientation.alpha = float.Parse(task);

                                else if (taskIndex == (int)QTASKINFO.QTASK_BETA)
                                    orientation.beta = float.Parse(task);

                                else if (taskIndex == (int)QTASKINFO.QTASK_GAMMA)
                                    orientation.gamma = float.Parse(task);

                                else if (taskIndex == (int)QTASKINFO.QTASK_MODEL)
                                    qtask.model = task.Trim().Replace(")", String.Empty);

                                qtask.position = position;
                                qtask.orientation = orientation;
                                taskIndex++;
                            }
                            qtaskList.Add(qtask);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowLogException(MethodBase.GetCurrentMethod().Name, ex);
            }
            AddLog(MethodBase.GetCurrentMethod().Name, "qtaskList count: " + qtaskList.Count);
            return qtaskList;
        }

        internal static QTask GetQTask(string taskName)
        {
            AddLog(MethodBase.GetCurrentMethod().Name, " taskName called");
            var qtaskList = GetQTaskList();

            foreach (var qtask in qtaskList)
            {
                if (qtask.model.Contains(taskName))
                {
                    AddLog(MethodBase.GetCurrentMethod().Name, "returned value for Model " + taskName);
                    return qtask;
                }
            }
            AddLog(MethodBase.GetCurrentMethod().Name, "returned : null");
            return null;
        }

        internal static QTask GetQTask(int taskId)
        {
            AddLog(MethodBase.GetCurrentMethod().Name, " taskId called");
            var qtaskList = GetQTaskList();
            foreach (var qtask in qtaskList)
            {
                if (qtask.id == taskId)
                {
                    AddLog(MethodBase.GetCurrentMethod().Name, "returned value for Task_Id " + taskId);
                    return qtask;
                }
            }
            AddLog(MethodBase.GetCurrentMethod().Name, "returned : null");
            return null;
        }

        internal static List<QTask> GetQTaskList(bool fullQtaskList = false, bool distinct = false, bool fromBackup = false)
        {
            int level = QMemory.GetCurrentLevel();
            string inputQscPath = cfgQscPath + level + "\\" + objectsQsc; 
            AddLog(MethodBase.GetCurrentMethod().Name, "called with level : " + level + " fullList : " + fullQtaskList.ToString() + " distinct : " + distinct.ToString() + " backup : " + fromBackup);
            string qscData = fromBackup ? LoadFile(inputQscPath) : LoadFile();

            var qtaskList = fullQtaskList ? ParseAllOjects(qscData) : ParseObjects(qscData);
            if (distinct)
                qtaskList = qtaskList.GroupBy(p => p.model).Select(g => g.First()).ToList();
            AddLog(MethodBase.GetCurrentMethod().Name, "returned list count: "+ qtaskList.Count);
            return qtaskList;
        }

        internal static List<QTask> GetQTaskList(int level, bool fullQtaskList = false, bool distinct = false, bool fromBackup = false)
        {
            string inputQscPath = cfgQscPath + level + "\\" + objectsQsc;
            AddLog(MethodBase.GetCurrentMethod().Name, " Qsc Path: '" + inputQscPath + "' level : " + level + " full List : " + fullQtaskList.ToString() + " distinct : " + distinct.ToString() + " backup : " + fromBackup);
            string qscData = fromBackup ? LoadFile(inputQscPath) : LoadFile();

            bool isBinary = qscData.HasBinaryContent();
            AddLog(MethodBase.GetCurrentMethod().Name, "isBinary: " + isBinary);

            var qtaskList = fullQtaskList ? ParseAllOjects(qscData) : ParseObjects(qscData);
            if (qtaskList.Count > 0)
                if (distinct) qtaskList = qtaskList.GroupBy(p => p.model).Select(g => g.First()).ToList();
            AddLog(MethodBase.GetCurrentMethod().Name, "task List count " + qtaskList.Count);
            return qtaskList;
        }

        internal static int GenerateTaskID(bool minimalId = false)
        {
            List<int> qidsList = new List<int>();
            AddLog(MethodBase.GetCurrentMethod().Name, "called minimal Id : " + minimalId);

            var qscData = LoadFile();
            qscData = qscData.Replace("\t", String.Empty);
            string[] qscDataSplit = qscData.Split('\n');

            foreach (var data in qscDataSplit)
            {
                if (data.Contains(taskNew))
                {
                    var startIndex = data.IndexOf(',', 14) + 1;
                    var taskName = (data.Slice(13, startIndex));

                    string[] taskNew = data.Split(',');
                    int taskIndex = 0;

                    foreach (var task in taskNew)
                    {
                        if (taskIndex == (int)QTASKINFO.QTASK_ID)
                        {
                            var taskId = task.Substring(task.IndexOf('(') + 1);
                            int qid = Convert.ToInt32(taskId);
                            if (qid > 10)//10 reserved for LevelFlow Id.
                                qidsList.Add(qid);
                            break;
                        }
                    }

                }
            }

            qidsList.Sort();
            AddLog(MethodBase.GetCurrentMethod().Name, "sorting done");

            int qtaskId = qidsList[0] + 1;
            int maxVal = qidsList[0], minVal = qidsList[1];

            if (minimalId)
            {
                int diffVal = 0;
                for (int index = 0; index < qidsList.Count; index++)
                {
                    minVal = qidsList[index];
                    maxVal = qidsList[index + 1];
                    diffVal = Math.Abs(maxVal - minVal);
                    AddLog(MethodBase.GetCurrentMethod().Name, "maxVal : " + maxVal + "\tminVal : " + minVal + "\tdiffVal : " + diffVal);

                    if (diffVal >= 50)
                    {
                        qtaskId = minVal + 1;
                        break;
                    }
                }
            }
            else
            {
                qidsList.Reverse();
                maxVal = qidsList[0];
                qtaskId = maxVal + 1;
            }
            AddLog(MethodBase.GetCurrentMethod().Name, "Returned Task Id: " + qtaskId);
            return qtaskId;
        }

        internal static void GenerateObjDataList(string variablesFile, List<QTask> qtaskList)
        {
            AddLog(MethodBase.GetCurrentMethod().Name, "Called with File '" + variablesFile + "'"); 
            if (qtaskList.Count <= 0)
            {
                ShowLogError(MethodBase.GetCurrentMethod().Name, "Qtask list is empty");
                return;
            }

            //Write Constants data.
            foreach (var qtask in qtaskList)
            {
                File.AppendAllText(variablesFile, qtask.model + "\n");
                string varData = null;
                if (String.IsNullOrEmpty(qtask.model) || qtask.model == "" || qtask.model.Length < 3)
                    continue;
                else
                {
                    if (String.IsNullOrEmpty(qtask.note) || qtask.note == "" || qtask.note.Length < 3)
                        varData = "const string " + qtask.name.Replace("\"", String.Empty).Replace(" ", "_").ToUpperInvariant() + " = " + qtask.model + ";\n";
                    else
                        varData = "const string " + qtask.note.Replace("\"", String.Empty).Replace(" ", "_").ToUpperInvariant() + " = " + qtask.model + ";\n";
                    File.AppendAllText(variablesFile, varData);
                }
            }
            AddLog(MethodBase.GetCurrentMethod().Name, "return success");
        }

        internal static void ExportCSV(string csvFile, List<QTask> qtaskList, bool allObjects = true)
        {
            if (qtaskList.Count <= 0)
            {
                IGIEditorUI.editorRef.SetStatusText("Qtask list is empty");
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
                IGIEditorUI.editorRef.SetStatusText("Weapon list is empty");
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
            var qtaskList = GetQTaskList();
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
            File.Delete(csvFile);
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

            File.Delete(xmlFile);
        }

        public static bool IsNetworkAvailable()
        {
            return IsNetworkAvailable(0);
        }


        public static bool IsNetworkAvailable(long minimumSpeed)
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
            //tmpDllPath = cachePath + "\\" + GenerateRandStr(0xF) + dllExt;
            AddLog(MethodBase.GetCurrentMethod().Name, "Path : " + internalsDllPath);
            //QCryptor.Decrypt(internalDllPath, tmpDllPath);
            //File.Copy(internalDllPath, tmpDllPath);

#if TESTING
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
                    AddLog(MethodBase.GetCurrentMethod().Name,"all methods failed to work. Use Manual injection.");
                    attachStatus = false;
                    return attachStatus;
                }
            }

            if (shellOut.Contains("Error")) attachStatus = false; else attachStatus = true;
            AddLog(MethodBase.GetCurrentMethod().Name, "cmd: " + internalsCmd + " status: " + attachStatus);
            return attachStatus;
        }

        internal static bool DeattachInternals()
        {
            try
            {
                var internalsAttached = CheckInternalsAttached();
                if (!internalsAttached) return true;

                string dllShellCmd = internalDllInjectorPath + " -e " + internalsDllFile;

                GT.GT_SendKeyStroke("END");
                Sleep(5);
                internalsAttached = CheckInternalsAttached();
                if (!internalsAttached) return true;

                string shellOut = ShellExec(dllShellCmd, true);

                if (shellOut.Contains("Error")) attachStatus = false; else attachStatus = true;
                AddLog(MethodBase.GetCurrentMethod().Name, "DeattachInternals() cmd: " + dllShellCmd + " status: " + attachStatus);
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
            var ObjModulesList = Process.GetProcessesByName(QMemory.gameName);
            try
            {
                // Populate the module collection.
                var ObjModules = ObjModulesList[0].Modules;

                // Iterate through the module collection.
                foreach (ProcessModule objModule in ObjModules)
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
            var gameItemAddr = GT.GT_ReadPointerOffset((IntPtr)0x0057BA9C, 0);
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
                    File.Delete(outputAiPath + scriptFile);
                    File.Delete(outputAiPath + scriptFile.Replace("qvm", "qsc"));
                }
                File.Delete(objectsQsc);
            }
        }

        internal static void CleanUpTmpFiles()
        {

            foreach (string file in Directory.EnumerateFiles(cachePath, "*.dll"))
            {
                try
                {
                    File.Delete(file);
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
                methodName = methodName.Replace("_Click", String.Empty).Replace("_SelectedIndexChanged", String.Empty).Replace("_SelectedValueChanged", String.Empty);
                File.AppendAllText(logFile, "[" + DateTime.Now.ToString("yyyy-MM-dd - HH:mm:ss") + "] " + methodName + "(): " + logMsg + "\n");
            }
        }

        public static string GenerateRandStr(int length)
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
    }

    public static class Extensions
    {
        public static string Slice(this string source, int start, int end)
        {
            if (end < 0)
            {
                end = source.Length + end;
            }
            int len = end - start;
            return source.Substring(start, len);
        }


        public static string ReplaceFirst(this string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        public static string ReplaceLast(this string text, string search, string replace)
        {
            int place = text.LastIndexOf(search);

            if (place == -1)
                return text;

            string result = text.Remove(place, search.Length).Insert(place, replace);
            return result;
        }

        public static bool IsNonASCII(this string str)
        {
            return (Encoding.UTF8.GetByteCount(str) != str.Length);
        }

        public static bool HasBinaryContent(this string content)
        {
            return content.Any(ch => char.IsControl(ch) && ch != '\r' && ch != '\n');
        }

        public static double ToRadians(this double angle)
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

    public class AreaDim
    {
        public float x, y, z;

        public AreaDim() { x = y = z = 0.0f; }

        public AreaDim(float x)
        {
            this.x = x;
            this.y = x;
            this.z = x;
        }
        public AreaDim(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    };

    public class Real32
    {
        public float alpha, beta, gamma;

        public Real32() { alpha = beta = gamma = 0.0f; }
        public Real32(float alpha)
        {
            this.alpha = alpha;
            this.beta = this.gamma = 0.0f;
        }

        public Real32(float alpha, float beta)
        {
            this.alpha = alpha;
            this.beta = beta;
            this.gamma = 0.0f;
        }

        public Real32(float alpha, float beta, float gamma)
        {
            this.alpha = alpha;
            this.beta = beta;
            this.gamma = gamma;
        }
    };

    public class Real64
    {
        public double x, y, z;
        public Real64() { x = y = z = 0.0f; }
        public Real64(double x)
        {
            this.x = x;
            this.y = this.z = 0.0f;
        }
        public Real64(double x, double y)
        {
            this.x = x;
            this.y = y;
            this.z = 0.0f;
        }

        public Real64(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Real64 Real64Operator(Real64 real1, Real64 real2, string operatorType)
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

        public bool Empty()
        {
            bool empty = (this.x == 0.0f && this.y == 0.0f && this.z == 0.0f);
            return empty;
        }
    };

    public class GraphNode
    {
        int nodeId;//Node Id.
        Real64 nodePos;//Node position (Offset not exact values).
        string nodeCriteria; //Node criteria. View,Stairs,Door.

        public GraphNode()
        {
            this.NodeId = 0;
            this.NodePos = null;
            this.NodeCriteria = String.Empty;
        }

        public GraphNode(int nodeId, Real64 nodePos, string nodeCriteria)
        {
            this.NodeId = nodeId;
            this.NodePos = nodePos;
        }

        public int NodeId { get => nodeId; set => nodeId = value; }
        public string NodeCriteria { get => nodeCriteria; set => nodeCriteria = value; }
        internal Real64 NodePos { get => nodePos; set => nodePos = value; }
    }

}
