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
            public List<string> weapons_list;
        };

        internal static string taskNew = "Task_New", taskDecl = "Task_DeclareParameters";
        internal static string objectsQsc = "objects.qsc", objectsQvm = "objects.qvm", weaponConfigQSC = "weaponconfig.qsc";
        internal static int qtaskObjId, qtaskId, anyaTeamTaskId = -1, ekkTeamTaskId = -1, randGenScriptId = 0, gGameLevel = 1;
        internal static string logFile = "app.log", aiIdleFile = "ai_idle.qvm", objectsMasterList, aiIdlePath;
        internal static bool logEnabled = false, keyExist = false, keyFileExist = false, mapViewerMode = false;

        internal static string gamePath, appdataPath, igieditorTmpPath, currPath, gameAbsPath, cfgGamePath, cfgInputHumanplayerPath, cfgInputQscPath, cfgInputAiPath, cfgInputQvmPath, cfgVoidPath, qMissionsPath, qfilesPath = @"\QFiles", igiEditor = "QEditor", qconv = "QConv", qfiles = "QFiles", cfgFile, projAppName,
         igiQsc = "IGI_QSC", igiQvm = "IGI_QVM", cfgGamePathEx = @"\missions\location0\level", weaponsDirPath = @"\weapons", humanplayer = "humanplayer.qvm", humanplayerPath = @"\humanplayer", aiGraphTask = "AIGraph", menuSystemDir = "menusystem", menuSystemPath = null, internalDllPath = @"bin\igi1ed.dat", tmpDllPath, internalDllInjectorPath = @"bin\igi1ed_inj.exe";
        internal static string inputQscPath = @"\IGI_QSC", inputQvmPath = @"\IGI_QVM", inputAiPath = @"\AIFiles", inputVoidPath = @"\Void", inputMissionPath = @"\missions\location0\level", inputHumanplayerPath = @"\humanplayer";
        internal static List<string> objTypeList = new List<string>() { "Building", "EditRigidObj", "Terminal", "Elevator", "ExplodeObject", "AlarmControl", "Generator", "Radio" };
        internal static string objects = "objects", objects_all = "objects_all", weapons = "weapons";
        internal static string qvmExt = ".qvm", qscExt = ".qsc", csvExt = ".csv", jsonExt = ".json", txtExt = ".txt", xmlExt = ".xml", dllExt = ".dll", missionExt = ".mission";
        internal static float fltInvalidAngle = -9.9999f;
        internal const string CAPTION_CONFIG_ERR = "Config - Error", CAPTION_FATAL_SYS_ERR = "Fatal sytem - Error", CAPTION_APP_ERR = "Application - Error", CAPTION_COMPILER_ERR = "Compiler - Error", alarmControl = "AlarmControl", stationaryGun = "StationaryGun";
        internal static string keyBase = @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths";
        internal static string patroIdleMask = "xxxx", patroAlarmMask = "yyyy", alarmControlMask = "zzzz", gunnerIdMask = "aaaa", viewGammaMask = "bbbb";
        internal static string speedXMask = "xx", speedYMask = "yy", speedZMask = "zz", inAir1Mask = "iair1", inAir2Mask = "iair2", health1Mask = "hh1", health2Mask = "hh2", health3Mask = "hh3";
        internal static List<string> aiScriptFiles = new List<string>();
        internal static string aiEnenmyTask = null, aiFriendTask = null, levelFlowData, missionsListFile = "MissionsList.txt", missionLevelFile = "mission_level.txt", missionDescFile = "mission_desc.txt", missionListFile = "MissionsList.txt";
        internal static double speedX = 1.75f, speedY = 17.5f, speedZ = 27, inAirVel1 = 0.5f, inAirVel2 = 0.8500000238418579f, healthScale1 = 3.0f, healthScale2 = 0.5f, healthScale3 = 0.5f;
        private static Random rand = new Random();
        internal enum QTYPES { BUILDING = 1, RIGID_OBJ = 2 };

        //List of Dictionary items.
        internal static List<Dictionary<string, string>> weaponList = new List<Dictionary<string, string>>();
        internal static List<Dictionary<string, string>> buildingList = new List<Dictionary<string, string>>();
        internal static List<Dictionary<string, string>> objectRigidList = new List<Dictionary<string, string>>();
        internal static List<string> buildingListStr = new List<string>();
        internal static List<string> objectRigidListStr = new List<string>();


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

        internal static List<string> aiTypes = new List<string>() { "AITYPE_RPG", "AITYPE_GUNNER", "AITYPE_SNIPER", "AITYPE_ANYA", "AITYPE_EKK", "AITYPE_PRIBOI", "AITYPE_CIVILIAN", "AITYPE_PATROL_UZI", "AITYPE_PATROL_AK", "AITYPE_PATROL_SPAS", "AITYPE_PATROL_PISTOL", "AITYPE_GUARD_UZI", "AITYPE_GUARD_AK", "AITYPE_GUARD_SPAS", "AITYPE_GUARD_PISTOL", "AITYPE_SECURITY_PATROL_UZI", "AITYPE_SECURITY_PATROL_SPAS", "AITYPE_MAFIA_PATROL_UZI", "AITYPE_MAFIA_PATROL_AK", "AITYPE_MAFIA_PATROL_SPAS", "AITYPE_MAFIA_GUARD_UZI", "AITYPE_MAFIA_GUARD_AK", "AITYPE_MAFIA_GUARD_SPAS", "AITYPE_SPETNAZ_PATROL_UZI", "AITYPE_SPETNAZ_PATROL_AK", "AITYPE_SPETNAZ_PATROL_SPAS", "AITYPE_SPETNAZ_GUARD_UZI", "AITYPE_SPETNAZ_GUARD_AK", "AITYPE_SPETNAZ_GUARD_SPAS" };

        public static void ShowWarning(string warn_msg, string caption = "WARNING")
        {
            MessageBox.Show(warn_msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void ShowError(string err_msg, string caption = "ERROR")
        {
            MessageBox.Show(err_msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowInfo(string info_msg, string caption = "INFO")
        {
            MessageBox.Show(info_msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult ShowDialog(string info_msg, string caption = "INFO")
        {
            return MessageBox.Show(info_msg, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }

        public static void ShowConfigError(string keyword)
        {
            ShowError("Config file has invalid property for '" + keyword + "'", CAPTION_CONFIG_ERR);
            Environment.Exit(1);
        }

        public static void ShowSystemFatalError(string err_msg)
        {
            ShowError(err_msg, CAPTION_FATAL_SYS_ERR);
            Environment.Exit(1);
        }

        private DialogResult ShowOptionInfo(string info_msg)
        {
            return MessageBox.Show(info_msg, "INFO", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }

        //Private method to get machine id.
        private static string GetUUID()
        {
            string uid_args = "wmic csproduct get UUID";
            string uuid_out = ShellExec(uid_args);
            string uid = uuid_out.Split(new[] { Environment.NewLine }, StringSplitOptions.None)[1];
            return uid.Trim();
        }

        //Private method to get GUID.
        private static string GetGUID()
        {
            Guid guid_obj = Guid.NewGuid();
            string guid = guid_obj.ToString();
            return guid;
        }


        //Private method to get MAC/Physical address.
        internal static string GetMACAddress()
        {
            string mac_addr_args = "wmic nic get MACAddress";
            string mac_address_out = ShellExec(mac_addr_args);
            var mac_address_list = mac_address_out.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            string mac_address = null;

            foreach (var address in mac_address_list)
            {
                if (!String.IsNullOrEmpty(address) && address.Count(c => c == ':') > 4)
                {
                    mac_address = address;
                    break;
                }
            }
            return mac_address.Trim();
        }

        internal static string GetPrivateIP()
        {
            string ip_addr_args = "ipconfig /all | findstr /c:IPv4";
            const string ip_out = "   IPv4 Address. . . . . . . . . . . : ";
            string ip_address_out = ShellExec(ip_addr_args);
            string[] ips = ip_address_out.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            List<string> ip_addresses = new List<string>(ips);

            int ip_lens = ip_addresses.Count - 1;
            string private_ip = ip_addresses[ip_lens].Substring(ip_out.Length).Replace("(Preferred)", "").Trim();
            return private_ip;
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

        private static void CreateGameShortcut(string link_name, string path_to_app, string game_args = "")
        {
            var shell = new WshShell();
            string shortcut_address = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + link_name + ".lnk";

            if (File.Exists(shortcut_address))
                File.Delete(shortcut_address);

            var shortcut = (IWshShortcut)shell.CreateShortcut(shortcut_address);
            shortcut.Description = "Shortcut for IGI";
            shortcut.Hotkey = "Ctrl+ALT+I";
            shortcut.Arguments = game_args;
            shortcut.WorkingDirectory = path_to_app;
            shortcut.TargetPath = path_to_app + Path.DirectorySeparatorChar + "igi.exe";
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

            if (!Directory.Exists(QCompiler.compile_path + @"\input") || !Directory.Exists(QCompiler.compile_path + @"\output") &&
            !Directory.Exists(QCompiler.decompile_path + @"\input") || !Directory.Exists(QCompiler.decompile_path + @"\output"))
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
            igieditorTmpPath = appdataPath + Path.DirectorySeparatorChar + igiEditor;

            //Set new Input QSC & QVM path releative to appdata.
            objectsMasterList = igieditorTmpPath + Path.DirectorySeparatorChar + "igi_master_list.txt";
            qMissionsPath = igieditorTmpPath + @"\QMissions";
            aiIdlePath = igieditorTmpPath + Path.DirectorySeparatorChar + "ai_idle.qvm";
            cfgInputQvmPath = igieditorTmpPath + qfilesPath + inputQvmPath + inputMissionPath;
            cfgInputQscPath = igieditorTmpPath + qfilesPath + inputQscPath + inputMissionPath;
            cfgInputHumanplayerPath = igieditorTmpPath + qfilesPath + inputQscPath + inputHumanplayerPath;
            cfgInputAiPath = igieditorTmpPath + inputAiPath;
            cfgVoidPath = igieditorTmpPath + inputVoidPath;
            menuSystemPath = gameAbsPath + menuSystemDir;


            if (Directory.Exists(menuSystemDir))
            {
                DeleteWholeDir(menuSystemPath);
                MoveDir(menuSystemDir, menuSystemPath);
            }

            if (Directory.Exists(igiEditor) && !Directory.Exists(igieditorTmpPath))
            {
                MoveDir(igiEditor, appdataPath);

                if (Directory.Exists(igiEditor) && Directory.Exists(igieditorTmpPath))
                {
                    DeleteWholeDir(igiEditor);
                    ShowSystemFatalError("Application couldn't be initialized properly! Please try again later");
                }
            }

        }

        internal static void DeleteWholeDir(string dir_path)
        {
            DirectoryInfo di = new DirectoryInfo(dir_path);
            foreach (FileInfo file in di.GetFiles())
                file.Delete();
            foreach (DirectoryInfo dir in di.GetDirectories())
                dir.Delete(true);
            Directory.Delete(dir_path);
        }

        internal static void MoveDir(string src_path, string dest_path)
        {
            var mv_cmd = "mv " + src_path + " " + dest_path;
            var move_cmd = "move " + src_path + " " + dest_path + " /y";

            try
            {
                //#1 solution to move with same root directory.
                Directory.Move(src_path, dest_path + Path.DirectorySeparatorChar + igiEditor);
            }
            catch (IOException ex)
            {
                if (ex.Message.Contains("already exist"))
                {
                    DeleteWholeDir(src_path);
                }
                else
                {
                    //#2 solution to move with POSIX 'mv' command.
                    ShellExec(mv_cmd, "powershell.exe");
                    if (Directory.Exists(src_path))
                        //#3 solution to move with 'move' command.
                        ShellExec(move_cmd);
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
            string str_content = null;
            try
            {
                var web_request = WebRequest.Create(url);
                using (var response = web_request.GetResponse())
                using (var content = response.GetResponseStream())
                using (var reader = new StreamReader(content))
                {
                    str_content = reader.ReadToEnd();
                    return str_content;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("remote"))
                    ShowSystemFatalError("Please check your internet connection");
                else
                    ShowError(ex.Message, "Application Error");
            }
            return str_content;
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
        protected static string EditContent(string _description, string _targetFileName, string _content)
        {
            //dynamic _result = new DynamicJson();
            //dynamic _file = new DynamicJson();
            //_result.description = _description;
            //_result.files = new { };
            //_result.files[_targetFileName] = new { content = _content };
            //return _result.ToString();
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
            string keyFileAbsPath = igieditorTmpPath + Path.DirectorySeparatorChar + projAppName + "_key.txt";
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
        internal static string ShellExec(string cmd_args, string shell = "cmd.exe")
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.CreateNoWindow = true;
            startInfo.FileName = shell;
            startInfo.Arguments = "/c " + cmd_args;
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

        internal static string LoadFile(string file_name)
        {
            string data = null;
            if (File.Exists(file_name))
                data = File.ReadAllText(file_name);
            return data;
        }

        internal static void SaveFile(string data = null, bool append_data = false)
        {
            SaveFile(objectsQsc, data, append_data);
        }

        internal static void SaveFile(string file_name, string data, bool append_data = false)
        {
            if (append_data)
                File.AppendAllText(file_name, data);
            else
                File.WriteAllText(file_name, data);
        }

        internal static void ResetFile(int game_level)
        {
            var input_qsc_path = cfgInputQscPath + game_level + "\\" + objectsQsc;

            if (File.Exists(objectsQsc))
                File.Delete(objectsQsc);

            File.Copy(input_qsc_path, objectsQsc);

            var fileData = QCryptor.Decrypt(objectsQsc);
            File.WriteAllText(objectsQsc, fileData);
            levelFlowData = File.ReadLines(objectsQsc).Last();
        }

        internal static void RestoreLevel(int game_level)
        {
            gamePath = cfgGamePath + game_level;
            string output_qvm_path = gamePath + "\\" + objectsQvm;
            string input_qvm_path = cfgInputQvmPath + game_level + "\\" + objectsQvm;

            File.Delete(output_qvm_path);
            File.Copy(input_qvm_path, output_qvm_path);

            var in_file_data = File.ReadAllText(input_qvm_path);
            var out_file_data = File.ReadAllText(output_qvm_path);

            if (in_file_data == out_file_data)
            {
                IGIEditorUI.editorRef.SetStatusText("Restrore of level '" + game_level + "' success");
            }
            else
                IGIEditorUI.editorRef.SetStatusText("Error in restroing level : " + game_level);
        }


        internal static int FindIndex(string temp, string source_data, int source_index, int qtask_index)
        {
            for (int i = 0; source_index < temp.Length; i++)
            {
                source_index = temp.IndexOf(source_data) + i;
                if ((temp[source_index - 1] == ' ' || temp[source_index - 1] == ',')
                     && temp[source_index + 1] == ',')
                {
                    break;
                }
            }

            //Add the index + offset.
            source_index += qtask_index;
            return source_index;
        }


        internal static bool CheckModelExist(string model)
        {
            int game_level = QMemory.GetCurrentLevel();
            AddLog("CheckModelExist() called with model : " + model + " for level : " + game_level);
            var input_qsc_path = cfgInputQscPath + game_level + "\\" + objectsQsc;
            string qsc_data = QCryptor.Decrypt(input_qsc_path);
            bool model_exist = false;

            if (!String.IsNullOrEmpty(model))
            {
                if (!model.Contains("\""))
                    model = "\"" + model + "\"";
            }

            var model_list = Regex.Matches(qsc_data, model).Cast<Match>().Select(m => m.Value);
            foreach (var model_obj in model_list)
                AddLog("Models list : " + model_obj);

            if (!String.IsNullOrEmpty(model))
            {
                if (model_list.Any(o => o.Contains(model)))
                    model_exist = true;
            }

            AddLog("CheckModelExist() returned : " + (model_exist ? "Model exist" : "Model doesn't exist"));
            return model_exist;
        }

        internal static int GetModelCount(string model)
        {
            if (!CheckModelExist(model)) return 0;
            var qtask_list = GetQTaskList(false, true);
            int count = qtask_list.Count(o => String.Compare(o.model, model, true) == 0);
            IGIEditorUI.editorRef.SetStatusText("Model count : " + count);
            return count;
        }

        internal static bool CheckModelExist(int task_id)
        {
            var qtask_list = GetQTaskList();
            if (qtask_list.Count == 0)
                throw new Exception("QTask list is empty");

            if (task_id != -1)
            {
                foreach (var qtask in qtask_list)
                {
                    if (qtask.id == task_id)
                        return true;
                }
            }
            return false;
        }


        //Parse the Objects.
        private static List<QTask> ParseAllOjects(string qsc_data)
        {
            //Remove all whitespaces.
            qsc_data = qsc_data.Replace("\t", String.Empty);
            string[] qsc_data_split = qsc_data.Split('\n');
            var model_regex = @"\d{3}_\d{2}_\d{1}";

            var qtask_list = new List<QTask>();
            foreach (var data in qsc_data_split)
            {
                if (data.Contains(taskNew))
                {
                    QTask qtask = new QTask();

                    string[] task_new = data.Split(',');
                    int task_index = 0;

                    foreach (var task in task_new)
                    {
                        if (task_index == (int)QTASKINFO.QTASK_ID)
                        {
                            var task_id = task.Substring(task.IndexOf('(') + 1);
                            qtask.id = Convert.ToInt32(task_id);
                        }
                        else if (task_index == (int)QTASKINFO.QTASK_NAME)
                            qtask.name = task.Trim();

                        else if (task_index == (int)QTASKINFO.QTASK_NOTE)
                            qtask.note = task.Trim();

                        else if (task_index == (int)QTASKINFO.QTASK_MODEL)
                            qtask.model = Regex.Match(task.Trim(), model_regex).Value;

                        task_index++;
                    }
                    qtask_list.Add(qtask);
                }
            }
            return qtask_list;
        }


        //Parse the Objects.
        private static List<QTask> ParseObjects(string qsc_data)
        {
            //Remove all whitespaces.
            qsc_data = qsc_data.Replace("\t", String.Empty);
            string[] qsc_data_split = qsc_data.Split('\n');

            var qtask_list = new List<QTask>();
            foreach (var data in qsc_data_split)
            {
                if (data.Contains(taskNew))
                {
                    var start_index = data.IndexOf(',') + 1;
                    var end_index = data.IndexOf(',', start_index);
                    var task_name = data.Slice(start_index, end_index).Trim().Replace("\"", String.Empty);

                    if (data.Contains("Building") && task_name != "Building")
                    {
                        start_index = data.IndexOf(',') + 1;
                        end_index = data.IndexOf(',', start_index);
                        task_name = data.Slice(start_index, end_index).Trim().Replace("\"", String.Empty);
                    }

                    if (objTypeList.Any(o => o.Contains(task_name)))
                    {

                        QTask qtask = new QTask();
                        Real32 orientation = new Real32();
                        Real64 position = new Real64();

                        string[] task_new = data.Split(',');
                        int task_index = 0;

                        foreach (var task in task_new)
                        {
                            if (task_index == (int)QTASKINFO.QTASK_ID)
                            {
                                var task_id = task.Substring(task.IndexOf('(') + 1);
                                qtask.id = Convert.ToInt32(task_id);
                            }
                            else if (task_index == (int)QTASKINFO.QTASK_NAME)
                                qtask.name = task.Trim();

                            else if (task_index == (int)QTASKINFO.QTASK_NOTE)
                                qtask.note = task.Trim();

                            else if (task_index == (int)QTASKINFO.QTASK_POSX)
                                position.x = Double.Parse(task);

                            else if (task_index == (int)QTASKINFO.QTASK_POSY)
                                position.y = Double.Parse(task);

                            else if (task_index == (int)QTASKINFO.QTASK_POSZ)
                                position.z = Double.Parse(task);

                            else if (task_index == (int)QTASKINFO.QTASK_ALPHA)
                                orientation.alpha = float.Parse(task);

                            else if (task_index == (int)QTASKINFO.QTASK_BETA)
                                orientation.beta = float.Parse(task);

                            else if (task_index == (int)QTASKINFO.QTASK_GAMMA)
                                orientation.gamma = float.Parse(task);

                            else if (task_index == (int)QTASKINFO.QTASK_MODEL)
                                qtask.model = task.Trim().Replace(")", String.Empty);

                            qtask.position = position;
                            qtask.orientation = orientation;
                            task_index++;
                        }
                        qtask_list.Add(qtask);
                    }
                }
            }
            return qtask_list;
        }

        internal static QTask GetQTask(string task_name)
        {
            AddLog("GetQTask(task_name) called");
            var qtask_list = GetQTaskList();

            foreach (var qtask in qtask_list)
            {
                if (qtask.model.Contains(task_name))
                {
                    AddLog("GetQTask() returned value for Model " + task_name);
                    return qtask;
                }
            }
            AddLog("GetQTask() returned : null");
            return null;
        }

        internal static QTask GetQTask(int task_id)
        {
            AddLog("GetQTask(id) called");
            var qtask_list = GetQTaskList();
            foreach (var qtask in qtask_list)
            {
                if (qtask.id == task_id)
                {
                    AddLog("GetQTask() returned value for Task_Id " + task_id);
                    return qtask;
                }
            }
            AddLog("GetQTask() returned : null");
            return null;
        }

        internal static List<QTask> GetQTaskList(bool full_qtask_list = false, bool distinct = false, bool from_backup = false)
        {
            int level = QMemory.GetCurrentLevel();
            string input_qsc_path = cfgInputQscPath + level + "\\" + objectsQsc;
            AddLog("GetQTaskList() : called with level : " + level + " full_list : " + full_qtask_list.ToString() + " distinct : " + distinct.ToString() + " backup : " + from_backup);
            string qsc_data = from_backup ? QCryptor.Decrypt(input_qsc_path) : LoadFile();

            var qtask_list = full_qtask_list ? ParseAllOjects(qsc_data) : ParseObjects(qsc_data);
            if (distinct)
                qtask_list = qtask_list.GroupBy(p => p.model).Select(g => g.First()).ToList();
            return qtask_list;
        }

        internal static List<QTask> GetQTaskList(int level, bool full_qtask_list = false, bool distinct = false, bool from_backup = false)
        {
            string input_qsc_path = cfgInputQscPath + level + "\\" + objectsQsc;
            AddLog("GetQTaskList() level : called with level : " + level + " full_list : " + full_qtask_list.ToString() + " distinct : " + distinct.ToString() + " backup : " + from_backup);
            string qsc_data = from_backup ? QCryptor.Decrypt(input_qsc_path) : LoadFile();

            var qtask_list = full_qtask_list ? ParseAllOjects(qsc_data) : ParseObjects(qsc_data);
            if (distinct)
                qtask_list = qtask_list.GroupBy(p => p.model).Select(g => g.First()).ToList();
            return qtask_list;
        }

        internal static int GenerateTaskID(bool minimalId = false)
        {
            List<int> qids_list = new List<int>();
            AddLog("GenerateTaskID called minimalId : " + minimalId);

            var qsc_data = LoadFile();
            qsc_data = qsc_data.Replace("\t", String.Empty);
            string[] qsc_data_split = qsc_data.Split('\n');

            foreach (var data in qsc_data_split)
            {
                if (data.Contains(taskNew))
                {
                    var start_index = data.IndexOf(',', 14) + 1;
                    var task_name = (data.Slice(13, start_index));

                    string[] task_new = data.Split(',');
                    int task_index = 0;

                    foreach (var task in task_new)
                    {
                        if (task_index == (int)QTASKINFO.QTASK_ID)
                        {
                            var task_id = task.Substring(task.IndexOf('(') + 1);
                            int qid = Convert.ToInt32(task_id);
                            if (qid > 10)//10 reserved for LevelFlow Id.
                                qids_list.Add(qid);
                            break;
                        }
                    }

                }
            }

            qids_list.Sort();

            int qtask_id = qids_list[0] + 1;

            int maxVal = qids_list[0], minVal = qids_list[1];

            if (minimalId)
            {
                int diffVal = 0;
                for (int index = 0; index < qids_list.Count; index++)
                {
                    minVal = qids_list[index];
                    maxVal = qids_list[index + 1];
                    diffVal = Math.Abs(maxVal - minVal);
                    AddLog("GenerateTaskID  maxVal : " + maxVal + "\tminVal : " + minVal + "\tdiffVal : " + diffVal);

                    if (diffVal >= 50)
                    {
                        qtask_id = minVal + 1;
                        break;
                    }
                }
            }
            else
            {
                qids_list.Reverse();
                maxVal = qids_list[0];
                qtask_id = maxVal + 1;
            }

            return qtask_id;
        }

        internal static void GenerateObjDataList(string variables_file, List<QTask> qtask_list)
        {

            if (qtask_list.Count <= 0)
            {
                IGIEditorUI.editorRef.SetStatusText("Qtask list is empty");
                return;
            }



            //Write Constants data.
            foreach (var qtask in qtask_list)
            {
                File.AppendAllText(variables_file, qtask.model + "\n");
                string var_data = null;
                if (String.IsNullOrEmpty(qtask.model) || qtask.model == "" || qtask.model.Length < 3)
                    continue;
                else
                {
                    if (String.IsNullOrEmpty(qtask.note) || qtask.note == "" || qtask.note.Length < 3)
                        var_data = "const string " + qtask.name.Replace("\"", String.Empty).Replace(" ", "_").ToUpperInvariant() + " = " + qtask.model + ";\n";
                    else
                        var_data = "const string " + qtask.note.Replace("\"", String.Empty).Replace(" ", "_").ToUpperInvariant() + " = " + qtask.model + ";\n";
                    File.AppendAllText(variables_file, var_data);
                }
            }
        }

        internal static void ExportCSV(string csv_file, List<QTask> qtask_list, bool all_objects = true)
        {
            if (qtask_list.Count <= 0)
            {
                IGIEditorUI.editorRef.SetStatusText("Qtask list is empty");
                return;
            }

            //Write CSV Header.
            string csv_header = "Task_ID," + "Task_Name," + "Task_Note," + "X_Pos," + "Y_Pos," + "Z_Pos," + "Alpha," + "Beta," + "Gamma," + "Model" + "\n";
            File.AppendAllText(csv_file, csv_header);

            //Write CSV data.
            foreach (var qtask in qtask_list)
            {
                string csv_data = null;
                if (all_objects == false)
                {
                    if (objTypeList.Any(o => o.Contains(qtask.name)))
                        csv_data = "" + qtask.id + "," + qtask.name + "," + qtask.note + "," + qtask.position.x + "," + qtask.position.y + "," + qtask.position.z + "," + qtask.orientation.alpha + "," + qtask.orientation.beta + "," + qtask.orientation.gamma + "," + qtask.model + "\n";
                }

                else
                {
                    csv_data = "" + qtask.id + "," + qtask.name + "," + qtask.note + "," + qtask.position.x + "," + qtask.position.y + "," + qtask.position.z + "," + qtask.orientation.alpha + "," + qtask.orientation.beta + "," + qtask.orientation.gamma + "," + qtask.model + "\n";
                }
                File.AppendAllText(csv_file, csv_data);
            }
        }

        internal static void ExportCSV(string csv_file, List<Weapon> weapon_task_list, bool advanced_data = false)
        {
            if (weapon_task_list.Count <= 0)
            {
                IGIEditorUI.editorRef.SetStatusText("Weapon list is empty");
                return;
            }

            //Write CSV Header.
            string csv_header = "Name," + "Id," + "WeaponType," + "AmmoType," + "Mass," + "Damage," + "Power," + "ReloadTime," + "Bullets/Round," + "RPM," + "Magazine," + "Range," + "Burts," + "Muzzle velocity" + "\n";
            File.AppendAllText(csv_file, csv_header);

            //Write CSV data.
            foreach (var weapon_task in weapon_task_list)
            {
                string csv_data = null;
                if (!advanced_data)
                {
                    csv_data = "" + weapon_task.name + "," + weapon_task.script_id + "," + weapon_task.type_enum + "," + weapon_task.ammo_disp_type + "," + weapon_task.mass + "," + weapon_task.damage + "," + weapon_task.power + "," + weapon_task.reload_time + "," + weapon_task.bullets + "," + weapon_task.rpm + "," + weapon_task.clips + "," + weapon_task.range + "," + weapon_task.burst + "," + weapon_task.muzzle_velocity + "," + "\n";
                }

                else
                {
                }
                File.AppendAllText(csv_file, csv_data);
            }
        }

        internal static void ExportXML(string file_name)
        {
            var qtask_list = GetQTaskList();
            string csv_file = null;

            if (File.Exists(csv_file))
                csv_file = LoadFile(csv_file);
            else
            {
                csv_file = objects + csvExt;
                ExportCSV(csv_file, qtask_list);
            }

            var lines = File.ReadAllLines(csv_file);
            string[] headers = lines[0].Split(',').Select(x => x.Trim('\"')).ToArray();

            var xml = new XElement("root",
               lines.Where((line, index) => index > 0).Select(line => new XElement("Item",
                  line.Split(',').Select((column, index) => new XElement(headers[index], column)))));

            xml.Save(file_name);
            File.Delete(csv_file);
        }

        internal static void ExportJson(string file_name)
        {
            string xml_file = objects + xmlExt;
            string xml_data = null;

            if (File.Exists(xml_file))
                xml_data = LoadFile(xml_file);
            else
            {
                xml_file = objects + xmlExt;
                ExportXML(xml_file);
                xml_data = LoadFile(xml_file);
            }

            string json_data = null;
            Thread.Sleep(1000);

            if (File.Exists(xml_file))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml_data);

                json_data = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented);
                SaveFile(file_name, json_data);
            }
            else throw new FileNotFoundException("File 'objects.xml' was not found in current directory");

            File.Delete(xml_file);
        }

        internal static void InjectDllOnStart()
        {
            //return;
            tmpDllPath = @"bin\" + GenerateRandStr(QUtils.internalDllPath.Length) + dllExt;
            AddLog("InjectDllOnStart() tmpDllPath : " + tmpDllPath);
            QCryptor.Decrypt(QUtils.internalDllPath, tmpDllPath);

            string dllShellCmd = internalDllInjectorPath + " " + tmpDllPath;
            ShellExec(dllShellCmd);
            AddLog("InjectDllOnStart() dllShellCmd : " + dllShellCmd);
        }

        internal static string AddStatusMsg(int taskId = -1, string statusMsg = null, string varString = null, char terminator = ',', bool is_cutscene = false, bool send_once = true, float status_duration = 5.0f)
        {
            var isCutscene = is_cutscene.ToString().ToUpperInvariant();
            var isSendt = send_once.ToString().ToUpperInvariant();
            string status_msg_task = "Task_New(" + taskId + ",\"StatusMessage\",\"StatusMsg\",0,0,0,0,0,0,\"" + varString + "\",\"" + statusMsg + "\",\"\"," + "\"message\"," + isSendt + "," + isCutscene + "," + status_duration + ")" + terminator + "\n";
            return status_msg_task;
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

        internal static void AddLog(string log_msg)
        {
            if (logEnabled)
                File.AppendAllText(logFile, "[" + DateTime.Now.ToString("yyyy-MM-dd - HH:mm:ss") + "] " + log_msg + "\n");
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
