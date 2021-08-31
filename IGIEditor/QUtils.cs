using DeviceId;
using IWshRuntimeLibrary;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
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
        internal static string objectsQsc = "objects.qsc", objectsQvm = "objects.qvm", weaponConfigQSC = "weaponconfig.qsc";
        internal static int qtaskObjId, qtaskId, anyaTeamTaskId = -1, ekkTeamTaskId = -1, randScriptId = 0, gGameLevel = 1;
        internal static string logFile = "app.log", qLibLogsFile = "GTLibc_logs.log", aiIdleFile = "aiIdle.qvm", objectsMasterList, aiIdlePath;
        internal static bool logEnabled = false, keyExist = false, keyFileExist = false, mapViewerMode = false;

        internal static string gamePath, appdataPath, igiEditorTmpPath, appCurrPath, gameAbsPath, cfgGamePath, cfgHumanplayerPathQsc, cfgHumanplayerPathQvm, cfgQscPath, cfgAiPath, cfgQvmPath, cfgVoidPath, cfgQFilesPath, qMissionsPath, qfilesPath = @"\QFiles", qEditor = "QEditor", qconv = "QConv", qfiles = "QFiles", cfgFile, projAppName, cachePath, cachePathAppLogs, cachePathAppImages,
         igiQsc = "IGI_QSC", igiQvm = "IGI_QVM", cfgGamePathEx = @"\missions\location0\level", weaponsDirPath = @"\weapons", humanplayer = "humanplayer.qvm", humanplayerPath = @"\humanplayer", aiGraphTask = "AIGraph", menuSystemDir = "menusystem", menuSystemPath = null, internalDllPath = @"bin\igi1ed.dat", tmpDllPath, internalDllInjectorPath = @"bin\igi1edInj.exe";
        internal static string inputQscPath = @"\IGI_QSC", inputQvmPath = @"\IGI_QVM", inputAiPath = @"\AIFiles", inputVoidPath = @"\Void", inputMissionPath = @"\missions\location0\level", inputHumanplayerPath = @"\humanplayer";
        internal static List<string> objTypeList = new List<string>() { "Building", "EditRigidObj", "Terminal", "Elevator", "ExplodeObject", "AlarmControl", "Generator", "Radio" };
        internal static string objects = "objects", objectsAll = "objectsAll", weapons = "weapons";
        internal static string qvmExt = ".qvm", qscExt = ".qsc", csvExt = ".csv", jsonExt = ".json", txtExt = ".txt", xmlExt = ".xml", dllExt = ".dll", missionExt = ".mission", jpgExt = ".jpg", pngExt = ".png";
        internal static float fltInvalidAngle = -9.9999f, fltInvalidVal = -9.9f;
        internal const string CAPTION_CONFIG_ERR = "Config - Error", CAPTION_FATAL_SYS_ERR = "Fatal sytem - Error", CAPTION_APP_ERR = "Application - Error", CAPTION_COMPILER_ERR = "Compiler - Error", alarmControl = "AlarmControl", stationaryGun = "StationaryGun";
        internal static string keyBase = @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths", helpStr = "IGI 1 Editor - HELP\nObject ID format: xxx_xx_x like 435_01_1";
        internal static string patroIdleMask = "xxxx", patroAlarmMask = "yyyy", alarmControlMask = "zzzz", gunnerIdMask = "aaaa", viewGammaMask = "bbbb";
        internal static string movementSpeedMask = "movSpeed", forwardSpeedMask = "forwardSpeed", upwardSpeedMask = "upSpeed", inAirSpeedMask = "iAirSpeed", throwBaseVelMask = "throwBaseVel", healthScaleMask = "healthScale", healthFenceMask = "healthFence", peekLeftRightLenMask = "peekLRLen", peekCrouchLenMask = "peekCrouchLen", peekTimeMask = "peekTime";
        internal static List<string> aiScriptFiles = new List<string>();
        internal static string aiEnenmyTask = null, aiFriendTask = null, levelFlowData, missionsListFile = "MissionsList.txt", missionLevelFile = "missionLevel.txt", missionDescFile = "missionDesc.txt", missionListFile = "MissionsList.txt";
        internal static double movSpeed = 1.75f, forwardSpeed = 17.5f, upwardSpeed = 27, inAirSpeed = 0.5f, peekCrouchLen = 0.8500000238418579f, peekLRLen = 0.8500000238418579f, peekTime = 0.25, healthScale = 3.0f, healthScaleFence = 0.5f;
        private static Random rand = new Random();
        internal enum QTYPES { BUILDING = 1, RIGID_OBJ = 2 };

        //List of Dictionary items.
        internal static List<Dictionary<string, string>> weaponList = new List<Dictionary<string, string>>();
        internal static List<Dictionary<string, string>> buildingList = new List<Dictionary<string, string>>();
        internal static List<Dictionary<string, string>> objectRigidList = new List<Dictionary<string, string>>();
        internal static List<string> buildingListStr = new List<string>();
        internal static List<string> objectRigidListStr = new List<string>();
        internal static List<string> aiModelsListStr = new List<string>();

        //Weapons variables.
        internal static string weaponId = "WEAPON_ID_";
        internal static List<string> weaponsList = new List<string>() { "AK47", "APC", "BINOCULARS", "COLT", "DESERTEAGLE", "DRAGUNOV", "FLASHBANG", "GLOCK", "GRENADE", "JACKHAMMER", "KNIFE", "M16A2", "M2HB", "MEDIPACK", "MIL", "MINIMI", "MP5SD", "PROXIMITYMINE", "RPG18", "SENTRY", "SPAS12", "T80", "UZI", "UZIX2" };
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

        internal static List<string> aiTypes = new List<string>() { "AITYPE_RPG", "AITYPE_GUNNER", "AITYPE_SNIPER", "AITYPE_ANYA", "AITYPE_EKK", "AITYPE_PRIBOI", "AITYPE_CIVILIAN", "AITYPE_PATROL_UZI", "AITYPE_PATROL_AK", "AITYPE_PATROL_SPAS", "AITYPE_PATROL_PISTOL", "AITYPE_GUARD_UZI", "AITYPE_GUARD_AK", "AITYPE_GUARD_SPAS", "AITYPE_GUARD_PISTOL", "AITYPE_SECURITY_PATROL_UZI", "AITYPE_SECURITY_PATROL_SPAS", "AITYPE_MAFIA_PATROL_UZI", "AITYPE_MAFIA_PATROL_AK", "AITYPE_MAFIA_PATROL_SPAS", "AITYPE_MAFIA_GUARD_UZI", "AITYPE_MAFIA_GUARD_AK", "AITYPE_MAFIA_GUARD_SPAS", "AITYPE_SPETNAZ_PATROL_UZI", "AITYPE_SPETNAZ_PATROL_AK", "AITYPE_SPETNAZ_PATROL_SPAS", "AITYPE_SPETNAZ_GUARD_UZI", "AITYPE_SPETNAZ_GUARD_AK", "AITYPE_SPETNAZ_GUARD_SPAS" };

        public static void ShowWarning(string warnMsg, string caption = "WARNING")
        {
            MessageBox.Show(warnMsg, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void ShowError(string errMsg, string caption = "ERROR")
        {
            MessageBox.Show(errMsg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        internal static void CreateConfig(string configFile)
        {
            cfgGamePath = LocateExecutable(QMemory.gameName + ".exe");
            bool gameFound = true;

            if (String.IsNullOrEmpty(cfgGamePath))
            {
                ShowWarning("Game path could not be detected automatically! Please select game path in config file", CAPTION_CONFIG_ERR);
                gameFound = false;
            }
            else
            {
                cfgGamePath = cfgGamePath.Substring(0, cfgGamePath.LastIndexOf("\\"));

                if (!File.Exists(cfgGamePath + Path.DirectorySeparatorChar + QMemory.gameName + ".exe"))
                {
                    ShowError("Invalid path selected! Game 'IGI' not found at path '" + cfgGamePath + "'", CAPTION_FATAL_SYS_ERR);
                    gameFound = false;
                }
            }

            string configData = "[GAME_PATH]\n" +
                "game_path = " + cfgGamePath + "\n\n" +
                "[GAME_VARS]\n" +
                "game_logs = false\n" +
                "game_reset = false";

            File.WriteAllText(configFile, configData);
            if (!gameFound)
                Environment.Exit(1);
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

        internal static void CreateShortcut()
        {
            CreateGameShortcut(QMemory.gameName + "_full", gameAbsPath);
            CreateGameShortcut(QMemory.gameName + "_window", gameAbsPath, "window");
        }

        internal static bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }

        internal static void InitQCompiler()
        {
            QCompiler.CheckQConvExist();

            if (!Directory.Exists(QCompiler.compilePath + @"\input") || !Directory.Exists(QCompiler.compilePath + @"\output") &&
            !Directory.Exists(QCompiler.decompilePath + @"\input") || !Directory.Exists(QCompiler.decompilePath + @"\output"))
            {
                ShowSystemFatalError("IGI QEditor directory has illegal structure");
            }

            //Check if python modules are properly installed.
            //var pyModulesList = new List<string>() { "numpy", "pillow", "lxml" };
            //string pyModuleOutFile = "pyModuleOutput.txt";

            //foreach (var pyModule in pyModulesList)
            //{
            //    string pyModuleCmd = "pip show " + pyModule + " > " + pyModuleOutFile + " 2>&1";
            //    ShellExec(pyModuleCmd);
            //    var shellOut = File.ReadAllText(pyModuleOutFile);
            //    if (shellOut.Contains("not found"))
            //    {
            //        ShowSystemFatalError("Python module named " + pyModule + " was not found in your system");
            //    }
            //    File.WriteAllText(pyModuleOutFile, String.Empty);
            //}
            //File.Delete(pyModuleOutFile);
        }

        internal static void InitAppData()
        {
            appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            igiEditorTmpPath = appdataPath + Path.DirectorySeparatorChar + qEditor;

            //Set new Input QSC & QVM path releative to appdata.
            objectsMasterList = igiEditorTmpPath + Path.DirectorySeparatorChar + "IGIMasterList.txt";
            qMissionsPath = igiEditorTmpPath + @"\QMissions";
            aiIdlePath = igiEditorTmpPath + Path.DirectorySeparatorChar + "aiIdle.qvm";
            cfgQvmPath = igiEditorTmpPath + qfilesPath + inputQvmPath + inputMissionPath;
            cfgQscPath = igiEditorTmpPath + qfilesPath + inputQscPath + inputMissionPath;
            cfgHumanplayerPathQsc = igiEditorTmpPath + qfilesPath + inputQscPath + inputHumanplayerPath;
            cfgHumanplayerPathQvm = igiEditorTmpPath + qfilesPath + inputQvmPath + inputHumanplayerPath;
            cfgAiPath = igiEditorTmpPath + inputAiPath;
            cfgVoidPath = igiEditorTmpPath + inputVoidPath;
            cfgQFilesPath = igiEditorTmpPath + qfilesPath;
            menuSystemPath = gameAbsPath + menuSystemDir;
            cachePath = Path.GetTempPath() + "IGIEditorCache";
            cachePathAppLogs = cachePath + @"\AppLogs";
            cachePathAppImages = cachePath + @"\AppImages";

            //if (Directory.Exists(menuSystemDir))
            //{
            //    DeleteWholeDir(menuSystemPath);
            //    MoveDir(menuSystemDir, menuSystemPath);
            //}

            //Init QEditor - QFiles.
            if (Directory.Exists(qEditor) && !Directory.Exists(igiEditorTmpPath))
            {
                MoveDir(qEditor, appdataPath);

                if (Directory.Exists(qEditor) && Directory.Exists(igiEditorTmpPath))
                {
                    DeleteWholeDir(qEditor);
                    ShowSystemFatalError("Application couldn't be initialized properly! Please try again later (Error: 0xCD00005F");
                }
            }

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
                ShowError(ex.Message ?? ex.StackTrace);
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
                    ShellExec(mvCmd, "powershell.exe");
                    if (Directory.Exists(srcPath))
                        //#3 solution to move with 'move' command.
                        ShellExec(moveCmd);
                }
            }
        }

        private static string Reverse(string str)
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
                    ShowSystemFatalError("Please check your internet connection");
                else
                    ShowError(ex.Message, "Application Error");
            }
            return strContent;
        }

        internal static void WebDownload(string url, string fileName)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFile(url, fileName);
                if (!Directory.Exists(QUtils.cachePathAppImages))
                    CreateCacheDir();
                ShellExec("move /Y " + fileName + " " + QUtils.cachePathAppImages);
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

        internal static void EnableMapView(bool enableMap)
        {
            IntPtr binoAddr = (IntPtr)0x00470B8F;
            IntPtr noClipAddr = (IntPtr)0x004C8806;

            if (enableMap)
            {
                bool idleStatus = SetAIEventIdle(true);
                if (idleStatus)
                {
                    QMemory.StartLevel(gGameLevel, true);
                    Thread.Sleep(5000);
                }

                //Enable NoClip and remove Bino.
                QLibc.GT.GT_WriteNOP(noClipAddr, 3);
                QLibc.GT.GT_WriteNOP(binoAddr, 10);

                //Add Map objects info.
                float areaVal = 50000.0f;
                AreaDim areaDim = new AreaDim(areaVal);
                var qscData = QObjects.SetAllAreaActivated(areaDim, "Building", -1, 5.0f, false);
                if (!String.IsNullOrEmpty(qscData))
                    QCompiler.CompileEx(qscData);

                ShowInfo("MapViewer mode enabled - Use Binoculars to use MapView");
                QLibc.GT.GT_SendKeys2Process(QMemory.gameName, QLibc.GT.VK.TAB);
            }

            else
            {
                //Bino & NoClip Opcodes in hex bytes.
                byte[] binoOps = new byte[] { 0xC7, 0x85, 0xB0, 0x01, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00 };
                byte[] noClipOps = new byte[] { 0xDC, 0x46, 0x20 };
                uint binoOpsLen = (uint)binoOps.Length;
                uint noClipOpsLen = (uint)noClipOps.Length;

                //Inject Shell for Editor mode.
                QLibc.GT.GT_InjectShell(binoAddr, binoOps, binoOpsLen, 0, QLibc.GT.GT_SHELL.GT_ORIGINAL_SHELL, QLibc.GT.GT_OPCODE.GT_OP_SHORT_JUMP);
                QLibc.GT.GT_InjectShell(noClipAddr, noClipOps, noClipOpsLen, 0, QLibc.GT.GT_SHELL.GT_ORIGINAL_SHELL, QLibc.GT.GT_OPCODE.GT_OP_SHORT_JUMP);

                bool idleStatus = SetAIEventIdle(false);
                if (idleStatus)
                {
                    QMemory.StartLevel(gGameLevel, true);
                    Thread.Sleep(10000);
                    RestoreLevel(gGameLevel);
                    ResetFile(gGameLevel);
                    QMemory.RestartLevel(true);
                }

                ShowInfo("MapViewer mode disabled");
            }
        }


        internal static bool SetAIEventIdle(bool aiEvent)
        {
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
                    ShellExec(copyDirCmd);
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
            string tok1 = "bea" + (2 + 1).ToString() + (5 + 1).ToString() + "bceb";
            string tok2 = Reverse("bb" + (30 + 1).ToString() + "f5f98");
            string tok3 = "";
            return tok1 + tok2 + tok3;
        }

        protected static HttpClient CreateHttpClient()
        {
            string authBearer = Reverse("48" + (30 + 1).ToString() + "d81550") + InitAuthBearer();
            authBearer += "7121a" + (5 + 1).ToString() + "e575" + 'f' + (10 - 9).ToString();

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Chrome");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + authBearer);
            return client;
        }
        protected static string EditContent(string Description, string TargetFileName, string Content)
        {
            //dynamic Result = new DynamicJson();
            //dynamic file = new DynamicJson();
            //Result.description = Description;
            //Result.files = new { };
            //Result.files[TargetFileName] = new { content = Content };
            //return Result.ToString();
            return null;
        }

        public static bool RegisterUser(string content)
        {
            string id = "b2b8" + (70 + 4).ToString() + "09f578a0" + (1000 + 70).ToString() + "a46e65b" + (5 + 4).ToString() + "b1f01e";

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
            string bitLyUrl = "$K" + "u@" + "Fa#Si" + (2 + 1).ToString();
            bitLyUrl = bitLyUrl.Replace("$", String.Empty).Replace("@", String.Empty).Replace("#", String.Empty);
            string srcUrl = "http://" + Reverse("tib") + ".ly/" + Reverse(bitLyUrl);
            return srcUrl;
        }

        internal static bool InitUserInfo()
        {

            string srcUrl = InitSrcUrl();
            bool status = true;
            string infoStr = "</users-info>";
            string srcData = null;//Changed WebReader(srcUrl);
            //changed
            var userDataContent = new StringBuilder(srcData);
            //int infoStrIndex = srcData.IndexOf(infoStr);
            //if (infoStrIndex == -1) ShowSystemFatalError("Invalid data encountered from backend. (ERROR : 0x7FFFFFFF)"); ;
            //userDataContent = userDataContent.Remove(infoStrIndex, infoStr.Length);

            //Check if user exist.
            string keyFileAbsPath = igiEditorTmpPath + Path.DirectorySeparatorChar + projAppName + "Key.txt";
            keyFileExist = File.Exists(keyFileAbsPath);
            keyExist = true;
            return true;//Change

            //Get machine properties.
            string userName = GetCurrentUserName();
            string machineId = GetUUID();
            string macAddress = GetMACAddress();
            string ipAddress = GetPrivateIP();
            string city = null;// EpicInfo.GetUserCity();
            string country = null;//EpicInfo.GetUserCountry();

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

        internal static int GetRegisteredUsers(string srcUrl)
        {
            int totalRegisteredUsers = 0;
            string srcData = WebReader(srcUrl);
            return totalRegisteredUsers = new Regex(Regex.Escape("<user>")).Matches(srcData).Count;
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
        internal static string ShellExec(string cmdArgs, string shell = "cmd.exe")
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.CreateNoWindow = true;
            startInfo.FileName = shell;
            startInfo.Arguments = "/c " + cmdArgs;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            process.StartInfo = startInfo;
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
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

        internal static void ResetFile(int gameLevel)
        {
            var inputQscPath = cfgQscPath + gameLevel + "\\" + objectsQsc;

            if (File.Exists(objectsQsc))
                File.Delete(objectsQsc);

            File.Copy(inputQscPath, objectsQsc);

            var fileData = QCryptor.Decrypt(objectsQsc);
            File.WriteAllText(objectsQsc, fileData);
            levelFlowData = File.ReadLines(objectsQsc).Last();
        }

        internal static void RestoreLevel(int gameLevel)
        {
            gamePath = cfgGamePath + gameLevel;
            string outputQvmPath = gamePath + "\\" + objectsQvm;
            string inputQvmPath = cfgQvmPath + gameLevel + "\\" + objectsQvm;

            File.Delete(outputQvmPath);
            File.Copy(inputQvmPath, outputQvmPath);

            var inFileData = File.ReadAllText(inputQvmPath);
            var outFileData = File.ReadAllText(outputQvmPath);

            if (inFileData == outFileData)
            {
                IGIEditorUI.editorRef.SetStatusText("Restrore of level '" + gameLevel + "' success");
            }
            else
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
            AddLog("CheckModelExist() called with model : " + model + " for level : " + gameLevel);
            var inputQscPath = cfgQscPath + gameLevel + "\\" + objectsQsc;
            string qscData = QCryptor.Decrypt(inputQscPath);
            bool modelExist = false;

            if (!String.IsNullOrEmpty(model))
            {
                if (!model.Contains("\""))
                    model = "\"" + model + "\"";
            }

            var modelList = Regex.Matches(qscData, model).Cast<Match>().Select(m => m.Value);
            foreach (var modelObj in modelList)
                AddLog("Models list : " + modelObj);

            if (!String.IsNullOrEmpty(model))
            {
                if (modelList.Any(o => o.Contains(model)))
                    modelExist = true;
            }

            AddLog("CheckModelExist() returned : " + (modelExist ? "Model exist" : "Model doesn't exist"));
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
            return qtaskList;
        }


        //Parse only Objects.
        private static List<QTask> ParseObjects(string qscData)
        {
            //Remove all whitespaces.
            qscData = qscData.Replace("\t", String.Empty);
            string[] qscDataSplit = qscData.Split('\n');

            var qtaskList = new List<QTask>();
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
            return qtaskList;
        }

        internal static QTask GetQTask(string taskName)
        {
            AddLog("GetQTask(taskName) called");
            var qtaskList = GetQTaskList();

            foreach (var qtask in qtaskList)
            {
                if (qtask.model.Contains(taskName))
                {
                    AddLog("GetQTask() returned value for Model " + taskName);
                    return qtask;
                }
            }
            AddLog("GetQTask() returned : null");
            return null;
        }

        internal static QTask GetQTask(int taskId)
        {
            AddLog("GetQTask(id) called");
            var qtaskList = GetQTaskList();
            foreach (var qtask in qtaskList)
            {
                if (qtask.id == taskId)
                {
                    AddLog("GetQTask() returned value for Task_Id " + taskId);
                    return qtask;
                }
            }
            AddLog("GetQTask() returned : null");
            return null;
        }

        internal static List<QTask> GetQTaskList(bool fullQtaskList = false, bool distinct = false, bool fromBackup = false)
        {
            int level = QMemory.GetCurrentLevel();
            string inputQscPath = cfgQscPath + level + "\\" + objectsQsc;
            AddLog("GetQTaskList() : called with level : " + level + " fullList : " + fullQtaskList.ToString() + " distinct : " + distinct.ToString() + " backup : " + fromBackup);
            string qscData = fromBackup ? QCryptor.Decrypt(inputQscPath) : LoadFile();

            var qtaskList = fullQtaskList ? ParseAllOjects(qscData) : ParseObjects(qscData);
            if (distinct)
                qtaskList = qtaskList.GroupBy(p => p.model).Select(g => g.First()).ToList();
            return qtaskList;
        }

        internal static List<QTask> GetQTaskList(int level, bool fullQtaskList = false, bool distinct = false, bool fromBackup = false)
        {
            string inputQscPath = cfgQscPath + level + "\\" + objectsQsc;
            AddLog("GetQTaskList() level : called with level : " + level + " fullList : " + fullQtaskList.ToString() + " distinct : " + distinct.ToString() + " backup : " + fromBackup);
            string qscData = fromBackup ? QCryptor.Decrypt(inputQscPath) : LoadFile();

            var qtaskList = fullQtaskList ? ParseAllOjects(qscData) : ParseObjects(qscData);
            if (distinct)
                qtaskList = qtaskList.GroupBy(p => p.model).Select(g => g.First()).ToList();
            return qtaskList;
        }

        internal static int GenerateTaskID(bool minimalId = false)
        {
            List<int> qidsList = new List<int>();
            AddLog("GenerateTaskID called minimalId : " + minimalId);

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
                    AddLog("GenerateTaskID  maxVal : " + maxVal + "\tminVal : " + minVal + "\tdiffVal : " + diffVal);

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

            return qtaskId;
        }

        internal static void GenerateObjDataList(string variablesFile, List<QTask> qtaskList)
        {

            if (qtaskList.Count <= 0)
            {
                IGIEditorUI.editorRef.SetStatusText("Qtask list is empty");
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
            Thread.Sleep(1000);

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

        internal static void InjectDllOnStart()
        {
            tmpDllPath = Path.GetTempFileName() + dllExt;
            AddLog("InjectDllOnStart() tmpDllPath : " + tmpDllPath);
            QCryptor.Decrypt(QUtils.internalDllPath, tmpDllPath);

            string dllShellCmd = internalDllInjectorPath + " " + tmpDllPath;
            ShellExec(dllShellCmd);
            AddLog("InjectDllOnStart() dllShellCmd : " + dllShellCmd);
        }

        internal static string AddStatusMsg(int taskId = -1, string statusMsg = null, string varString = null, char terminator = ',', bool isCutsceneBool = false, bool sendOnce = true, float statusDuration = 5.0f)
        {
            var isCutscene = isCutsceneBool.ToString().ToUpperInvariant();
            var isSendt = sendOnce.ToString().ToUpperInvariant();
            string statusMsgTask = "Task_New(" + taskId + ",\"StatusMessage\",\"StatusMsg\",0,0,0,0,0,0,\"" + varString + "\",\"" + statusMsg + "\",\"\"," + "\"message\"," + isSendt + "," + isCutscene + "," + statusDuration + ")" + terminator + "\n";
            return statusMsgTask;
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

        internal static void AddLog(string logMsg)
        {
            if (logEnabled)
                File.AppendAllText(logFile, "[" + DateTime.Now.ToString("yyyy-MM-dd - HH:mm:ss") + "] " + logMsg + "\n");
        }

        public static string GenerateRandStr(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[rand.Next(s.Length)]).ToArray());
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
        PATROLPATHCOMMAND_ANIMATION,
        PATROLPATHCOMMAND_DELAY,
        PATROLPATHCOMMAND_WALKTO,
        PATROLPATHCOMMAND_RUNTO,
        PATROLPATHCOMMAND_CROUCH,
        PATROLPATHCOMMAND_LOOKATNODE,
        PATROLPATHCOMMAND_END,
        PATROLPATHCOMMAND_QUIT,
        PATROLPATHCOMMAND_SETSPEED
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

    };

}
