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
        private static float deltaToGround = 7000.0f;
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

        static internal Real32 GetGamePositions()
        {
            uint posBaseAddr = (uint)GetHumanBaseAddress() + (uint)0x24;
            QUtils.AddLog("GetGamePositions() : posBaseAddr : " + posBaseAddr);

            IntPtr xPosAddr = (IntPtr)posBaseAddr + 0x0;
            IntPtr yPosAddr = (IntPtr)posBaseAddr + 0x8;
            IntPtr zPosAddr = (IntPtr)posBaseAddr + 0x10;

            QUtils.AddLog("GetGamePositions() xPosAddr : " + xPosAddr);
            QUtils.AddLog("GetGamePositions() yPosAddr : " + yPosAddr);
            QUtils.AddLog("GetGamePositions() zPosAddr : " + zPosAddr);

            var xpos = GT.GT_ReadFloat(xPosAddr);
            var ypos = GT.GT_ReadFloat(yPosAddr);
            var zpos = GT.GT_ReadFloat(zPosAddr);

            QUtils.AddLog("GetGamePositions() xpos : " + xpos);
            QUtils.AddLog("GetGamePositions() ypos : " + ypos);
            QUtils.AddLog("GetGamePositions() zpos : " + zpos);

            var position = new Real32(xpos, ypos, zpos);
            QUtils.AddLog("GetRealPositions() : position: " + position);
            return position;
        }

        static internal Real64 GetRealPositions()
        {
            uint posBaseAddr = (uint)0x005CA138;
            IntPtr xPosAddr = (IntPtr)posBaseAddr + 0x0;
            IntPtr yPosAddr = (IntPtr)posBaseAddr + 0x8;
            IntPtr zPosAddr = (IntPtr)posBaseAddr + 0x10;

            var xpos = GT.GT_ReadDouble(xPosAddr);
            var ypos = GT.GT_ReadDouble(yPosAddr);
            var zpos = GT.GT_ReadDouble(zPosAddr);

            double x = Convert.ToDouble(Decimal.Truncate(Convert.ToDecimal(xpos)));
            double y = Convert.ToDouble(Decimal.Truncate(Convert.ToDecimal(ypos)));
            double z = Convert.ToDouble(Decimal.Truncate(Convert.ToDecimal(zpos)));
            
            QUtils.AddLog("GetRealPositions() : xpos: " + xpos + " ypos: " + ypos + " zpos: " + zpos);
            QUtils.AddLog("GetRealPositions() : x: " + x + " y: " + y + " z: " + z);

            //Fix this angle for Ground reference.
            var position = new Real64(x, y, z - deltaToGround);
            QUtils.AddLog("GetRealPositions() : position: " + position);
            return position;
        }

        private static IntPtr GetHumanBaseAddress()
        {
            uint humanStaticPtr = (uint)0x0016E210;
            uint[] humanAddrOffs = { 0x8, 0x7CC, 0x14 };
            IntPtr humanBasePtr = IntPtr.Zero, humanBaseAddr = IntPtr.Zero;

            humanBasePtr = GT.GT_ReadPointerOffset(gtGameBase, humanStaticPtr);
            QUtils.AddLog("GetHumanBaseAddress() human_base_pointer 0x" + humanBasePtr);
            humanBaseAddr = GT.GT_ReadPointerOffsets(humanBasePtr, humanAddrOffs, (uint)humanAddrOffs.Count() * sizeof(int));
            QUtils.AddLog("GetHumanBaseAddress () human_base_address  : 0x" + humanBaseAddr);
            return humanBaseAddr;
        }

        internal static IntPtr GetStatusMsgAddr()
        {
            uint status_msg_static_pointer = (uint)0x001C8A20;
            uint[] status_msg_address_offsets = { 0x8, 0x8, 0x8, 0x4E4 };
            IntPtr status_msg_base_pointer = IntPtr.Zero, status_msg_address = IntPtr.Zero;

            status_msg_base_pointer = GT.GT_ReadPointerOffset(gtGameBase, status_msg_static_pointer);
            status_msg_address = GT.GT_ReadPointerOffsets(status_msg_base_pointer, status_msg_address_offsets, (uint)status_msg_address_offsets.Count() * sizeof(int)) + 0x4C;
            return status_msg_address;
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
            IntPtr human_base_address = GetHumanBaseAddress() + (int)0x348;
            var angleAddrH = human_base_address + 0x1C4;
            var angleAddrV = human_base_address + 0xBF4;

            float angle = GT.GT_ReadFloat(angleAddrH);
            QUtils.AddLog("GetRealAngle() Address : 0x" + angleAddrH.ToString());
            QUtils.AddLog("GetRealAngle() Value : " + angle);

            return angle;
        }

        internal static void StartLevel(int level, bool windowed = false)
        {
            //if (level <= 0 || level > 3) throw new ArgumentNullException("Level must be between 1-3");

            var igi_proc = Process.GetProcessesByName(gameName);
            if (igi_proc.Length > 0)
                igi_proc[0].Kill();

            string igi_level_cmd = "start igi_" + (windowed ? "window" : "full") + ".lnk level" + level;
            QUtils.ShellExec(igi_level_cmd);
            FindGame(QUtils.logEnabled);
        }

        internal static void RestartLevel(bool save_position = true)
        {
            if (save_position)
            {
                //Set the human position.
                var human_pos = GetRealPositions();
                var human_angle = GetRealAngle();
                string qsc_data = QHuman.UpdatePositionNoOffset(human_pos, human_angle);
                if (!String.IsNullOrEmpty(qsc_data))
                    QCompiler.Compile(qsc_data, QUtils.gamePath);
            }

            Thread.Sleep(1000);
            GT.GT_SendKeys2Process(gameName, "^r", false);
        }
    }
}
