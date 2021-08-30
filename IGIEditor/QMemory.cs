using QLibc;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace IGIEditor
{
    class QMemory
    {
        internal static string gameName = "IGI";
        internal static float deltaToGround = 7000.0f;
        internal static IntPtr gtGameBase = (IntPtr)0x00400000; //Game base address.

        internal static void StartGame(string args = "window")
        {
            Process.Start(gameName + "_" + args);
            GT.GT_FindGameProcess(gameName);
        }

        internal static bool FindGame(bool enableLogs = false)
        {
            bool gameFound = false;
            try
            {
                if (enableLogs)
                    GT.GT_EnableLogs();

                Process[] pname = Process.GetProcessesByName(gameName);
                if (pname.Length == 0)
                    gameFound = false;
                else
                {
                    if (GT.GT_FindGameProcess(gameName) != IntPtr.Zero)
                        gameFound = true;
                }
            }
            catch (Exception ex)
            {
                QUtils.ShowError(ex.Message);
            }
            return gameFound;
        }

        internal static int GetCurrentLevel()
        {
            IntPtr levelAddr = (IntPtr)0x00539560;
            long level = GT.GT_ReadInt(levelAddr);
            //if (level > 3) QUtils.ShowSystemFatalError("IGI Editor demo limited to only 3 levels");
            return (int)level;
        }

        static IntPtr GetGameBaseAddr()
        {
            var pid = GT.GT_GetProcessID();
            return GT.GT_GetGameBaseAddress(pid);
        }

        internal static IntPtr GetHumanHealthAddr()
        {
            var humanAddr = GetHumanBaseAddress() + (int)0x254;
            return humanAddr;
        }

        internal static IntPtr GetHumanBaseAddress(bool addLog = true)
        {
            uint humanStaticPtr = (uint)0x0016E210;
            uint[] humanAddrOffs = { 0x8, 0x7CC, 0x14 };
            IntPtr humanBasePtr = IntPtr.Zero, humanBaseAddr = IntPtr.Zero;

            humanBasePtr = GT.GT_ReadPointerOffset(gtGameBase, humanStaticPtr);
            humanBaseAddr = GT.GT_ReadPointerOffsets(humanBasePtr, humanAddrOffs, (uint)humanAddrOffs.Count() * sizeof(int));

            if (addLog)
            {
                QUtils.AddLog("GetHumanBaseAddress() humanBasePointer 0x" + humanBasePtr);
                QUtils.AddLog("GetHumanBaseAddress () humanBaseAddress  : 0x" + humanBaseAddr);
            }
            return humanBaseAddr;
        }

        internal static IntPtr GetStatusMsgAddr()
        {
            uint statusMsgStaticPointer = (uint)0x001C8A20;
            uint[] statusMsgAddressOffsets = { 0x8, 0x8, 0x8, 0x4E4 };
            IntPtr statusMsgBasePointer = IntPtr.Zero, statusMsgAddress = IntPtr.Zero;

            statusMsgBasePointer = GT.GT_ReadPointerOffset(gtGameBase, statusMsgStaticPointer);
            statusMsgAddress = GT.GT_ReadPointerOffsets(statusMsgBasePointer, statusMsgAddressOffsets, (uint)statusMsgAddressOffsets.Count() * sizeof(int)) + 0x4C;
            return statusMsgAddress;
        }

        internal static bool SetStatusMsgText(string statusMsgTxt)
        {
            var statusMsgAddr = GetStatusMsgAddr();
            return GT.GT_WriteMemory(statusMsgAddr, "string", statusMsgTxt);
        }

        internal static string GetStatusMsgText()
        {
            var statusMsgAddr = GetStatusMsgAddr();
            string statusMsg = GT.GT_ReadString(statusMsgAddr);
            return statusMsg;
        }


        static internal float GetRealAngle()
        {
            IntPtr humanBaseAddress = GetHumanBaseAddress() + (int)0x348;
            var angleAddrH = humanBaseAddress + 0x1C4;
            var angleAddrV = humanBaseAddress + 0xBF4;

            float angle = GT.GT_ReadFloat(angleAddrH);
            QUtils.AddLog("GetRealAngle() Address : 0x" + angleAddrH.ToString());
            QUtils.AddLog("GetRealAngle() Value : " + angle);

            return angle;
        }

        internal static void UpdateHumanHealth(bool permanent = true)
        {
            if (permanent)
            {
                //Enable Fall damage scale.
                IntPtr jumpHealthAddr = (IntPtr)0x0040864E;
                GT.GT_WriteNOP(jumpHealthAddr, 6);

                //Enable normal and fence damage scale. 
                QHuman.UpdateHumanPlayerHealth(float.MaxValue, 0.0f);
            }
            unsafe
            {
                var healthAddr = GetHumanHealthAddr();
                float healthValue = float.NaN;
                GT.GT_WriteAddress(healthAddr, (void*)&healthValue);
            }
        }

        internal static void StartLevel(int level, bool windowed = false)
        {
            //if (level <= 0 || level > 3) throw new ArgumentNullException("Level must be between 1-3");

            var igiProc = Process.GetProcessesByName(gameName);
            if (igiProc.Length > 0)
                igiProc[0].Kill();

            string igiLevelCmd = "start igi_" + (windowed ? "window" : "full") + ".lnk level" + level;
            QUtils.ShellExec(igiLevelCmd);
            FindGame(QUtils.logEnabled);
        }

        internal static void RestartLevel(bool savePosition = true)
        {
            if (savePosition)
            {
                //Set the human position.
                var humanPos = QHuman.GetPositionInMeter();
                var humanAngle = GetRealAngle();
                string qscData = QHuman.UpdatePositionInMeter(humanPos, humanAngle);
                if (!String.IsNullOrEmpty(qscData))
                    QCompiler.Compile(qscData, QUtils.gamePath);
            }

            Thread.Sleep(1000);
            GT.GT_SendKeys2Process(gameName, "^r", false);
        }
    }
}
