/*
BRIEF : QLibcs is library to make game trainer in C# it provide all the necessary methods to make simple game trainer in
windows using win32-API with ease.
It uses only WIN32 API methods instead of CRT method because this is intended to work on Windows system only
and not shall be portable or to target other OS like Linux,MAC OS etc.

From the beginning of trainer development till end, it provides all necessary methods needed for game trainer.

*****************************
*******Main components.******
*****************************

1)Finding game --> use GT_FindGameProcess()  or GT_FindGameWindow() method.
2)Reading values Health,XP,Ammos from game --> use GT_ReadAddress() or GT_ReadAddressoffset() methods.
3)Writing values Health,XP,Ammos to  game --> use GT_WriteAddress() or GT_WriteAddressOffset() methods.
4)Creating Hot-keys for trainer --> use GT_HotKeysPressed() MACRO or GT_IsKeyPressed()/GT_IsKeyToggled() methods.

*****************************
****Additional components.***
*****************************

5)Additional Automation of scripting for trainer --> use GT_DoMousePress() and GT_DoKeyPress() methods.
6)Cheat code applying tool included in this library --> use GT_SetCheatCode() method.
7)Offset area searching tool included in this library --> use GT_SearchOffsetArea() method.

***********************************************
****Advanced components for Game Hacking.*****
**********************************************

8)Opcode injection tool included in this library --> use GT_InjectOpcode()/GT_InjectOpcodes() methods.
9)NOP instruction Filling tool included in this library --> use GT_WriteNOP()/GT_WriteNOPs() methods.
10)Write JMP/CALL assembly instruction --> use  GT_WriteJmpOrCall() method.
11)Shellcode injection tool included in this library --> use GT_InjectShellCode() method.
12)DLL injection tool included in this library --> use GT_InjectDLL() method.

NOTE : This ain't memory scanning,hooking,analyzing library, it won't provide methods for scanning/signature or dumping RAW memory.

AIM : The aim of this library is only to provide the most efficient way of creating game trainer
and to provide a layer on top of WIN-32 API cumbersome methods and to make reading/writing ,finding Game process easier and convenient.

DOCUMENTATION INFO :
All Public and Semi-Private methods are well documented.
but private methods are not documented as it was not necessary to do so.

VERSION INFO :
GTLIBC Version : V 1.0

WHATS NEW IN THIS VERSION  v1.0 :
[+] Added QLibc library.
[+] Added all basic memory read,write features.
[+] Add advanced inject DLL,ShellCode features.
[+] Added Method to get static address.
[+] Added new method to read write memory for CS, (GT_WriteMemory
GT_ReadString,GT_ReadDouble,GT_ReadFloat,GT_ReadInt)

V 1.0 -> Dated : 13/10/2019

Written by Ha5eeB Mir (haseebmir.hm@gmail.com)
*/

using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace QLibc
{
    class GT
    {
        public enum VK
        {
            F1 = 0x70,
            F2 = 0x71,
            F3 = 0x72,
            F4 = 0x73,
            F5 = 0x74,
            F6 = 0x75,
            F7 = 0x76,
            F8 = 0x77,
            F9 = 0x78,
            F10 = 0x79,
            CTRL = 0x11,
            ALT = 0x12,
            ESC = 0x1B,
            TAB = 0x09,
            END = 0x23
        }

        public enum MOUSE_CODE
        {
            FROM_LEFT_1ST_BUTTON_PRESSED = 0x0001,
            FROM_LEFT_2ND_BUTTON_PRESSED = 0x0004,
            FROM_LEFT_3RD_BUTTON_PRESSED = 0x0008,
            FROM_LEFT_4TH_BUTTON_PRESSED = 0x0010,
            RIGHTMOST_BUTTON_PRESSED = 0x0002
        }

        public enum GT_OPCODE
        {
            GT_OP_SHORT_JUMP = 0x1,
            GT_OP_NEAR_JUMP = 0x2,
            GT_OP_CALL = 0x3
        }

        public enum GT_SHELL
        {
            GT_ORIGINAL_SHELL,
            GT_PATCHED_SHELL,
        }

#if WIN64
        private const string static_lib  = @"lib\GTLibc_x64.so";
#elif WIN32
        private const string static_lib = @"lib\GTLibc_x86.so";
#else
#error Conditional compilation symbols not defined in current project
        private const string static_lib = @"lib\GTLibc_x86.so";
#endif

        /****************************************************************************/
        /*********************-PUBLIC-METHODS-***************************************/
        /****************************************************************************/
        /*Public methods to find Game process/window.*/
        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr GT_FindGameProcess(string game_name);
        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr GT_FindGameWindow(string window_name);

        /*Public methods to Read/Write values from/at Address.*/
        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] unsafe public static extern void* GT_ReadAddress(IntPtr address);
        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] unsafe public static extern void* GT_ReadAddressOffset(IntPtr address, uint offset);
        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] unsafe public static extern IntPtr GT_ReadAddressOffsets(IntPtr address, uint[] offsets, uint size_offset);

        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] unsafe public static extern bool GT_WriteAddress(IntPtr address, void* value);
        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] unsafe public static extern bool GT_WriteAddressOffset(IntPtr address, uint offset, void* value);
        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] unsafe public static extern bool GT_WriteAddressOffsets(IntPtr address, uint[] offsets, uint size_offset, void* value);

        /*Public methods to Read/Write pointer from/at Address.*/
        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr GT_ReadPointerOffset(IntPtr address, uint offset);
        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr GT_ReadPointerOffsets(IntPtr address, uint[] offsets, uint size_offset);

        /*Public getter methods to get Game Name,Handle,Process ID,base address.*/
        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] public static extern string GT_GetGameName();
        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] public static extern uint GT_GetProcessID();
        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr GT_GetGameHandle4mHWND(IntPtr hwnd);
        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] public static extern uint GT_GetProcessID4mHWND(IntPtr hwnd);
        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr GT_GetGameBaseAddress(uint pid);
        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr GT_GetGameHandle();
        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr GT_GetGameHWND();
        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr GT_GetStaticAddress(uint static_base, uint[] offsets, uint sz_offset, Int32 add_offset);

#if GT_BUILD_DLL
        [DllImport(static_lib,CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr GT_InjectAsm(IntPtr address, IntPtr asm_func, int asm_len, int inject_type);
        [DllImport(static_lib,CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr GT_GetJmpBackAddress(IntPtr address,int length,string jmp_back_buf = "");
#endif

        /*Public methods for creating hot-keys*/
        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] public static extern bool GT_HotKeysDown(VK key, params object[] args);
        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] public static extern bool GT_IsKeyPressed(VK key);
        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] public static extern bool GT_IsKeyToggled(VK key);

        /****************************************************************************/
        /****************-SEMI-PRIVATE-METHODS-**************************************/
        /****************************************************************************/
        /*Semi-private methods for pressing Keyboard and Mouse keys*/
        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] public static extern void GT_DoMousePress(MOUSE_CODE mouse);
        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] public static extern void GT_DoKeyPress(VK key);

        /*Semi-private Tool for Applying cheat codes*/
        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] public static extern void GT_SetCheatCode(string cheat);

        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] public static extern string GT_SearchOffsetArea(IntPtr address, uint offset_limit, uint offset_size, uint search);

        /*Private Tool for Injecting custom code/DLL*/
        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] unsafe private static extern bool GT_InjectOpcode(IntPtr address, void* opcode, uint size);

        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] public static extern bool GT_InjectDLL(string dll_name, string process_name);

        /*Semi-private Tool for writing assembly NOP instruction*/
        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] public static extern bool GT_WriteNOP(IntPtr address, uint len);
        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] public static extern bool GT_WriteNOPs(IntPtr[] addresses, uint[] lens, uint n_addresses);

        /*Semi-private Tool for writing assembly JMP or CALL instruction*/
        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] public static extern bool GT_WriteJmpOrCall(IntPtr source, IntPtr destination, GT_OPCODE opcode_type, uint nops_amount);

        /*Semi-private Tool for injecting custom shellcode into game*/
        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] unsafe private static extern IntPtr GT_InjectShellCode(IntPtr lp_address, IntPtr lp_shell, uint sz_shell, uint nops_amount, GT_SHELL shell_type, GT_OPCODE opcode_type);

        /*Semi private method for enabling/disabling Logs*/
        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] public static extern bool GT_EnableLogs();
        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] public static extern bool GT_DisableLogs();

        /*Semi private method for enabling/disabling Errors/Warnings*/
        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] public static extern void GT_SuppressErrors(bool errAction);
        [DllImport(static_lib, CallingConvention = CallingConvention.Cdecl)] public static extern void GT_SuppressWarnings(bool warnAction);


        [DllImport("kernel32.dll")]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, UIntPtr nSize, IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out IntPtr lpNumberOfBytesRead);

        [DllImport("User32.dll")]
        private static extern int SetForegroundWindow(IntPtr handle);

        public static void GT_SendKeys2Process(string process_name, string keys, bool appForeground = true)
        {
            var process = System.Diagnostics.Process.GetProcessesByName(process_name).FirstOrDefault();
            if (process != null)
            {
                IntPtr handle = process.MainWindowHandle;
                if (appForeground)
                    SetForegroundWindow(handle);
                Thread.Sleep(500);
                System.Windows.Forms.SendKeys.SendWait(keys);
            }
        }

        public static void GT_SendKeys2Process(string process_name, VK key)
        {
            var process = System.Diagnostics.Process.GetProcessesByName(process_name).FirstOrDefault();
            if (process != null)
            {
                IntPtr handle = process.MainWindowHandle;
                SetForegroundWindow(handle);
                Thread.Sleep(500);
                GT.GT_DoKeyPress(key);
            }
        }

        unsafe public static Int64 GT_ReadInt(IntPtr address)
        {
            Int64 value = 0;
            value = *(int*)GT_ReadAddress(address);
            return value;
        }

        unsafe public static float GT_ReadFloat(IntPtr address)
        {
            float value = 0.0f;
            value = *(float*)GT_ReadAddress(address);
            return value;
        }

        unsafe public static double GT_ReadDouble(IntPtr address)
        {
            double value = 0.0;
            value = *(double*)GT_ReadAddress(address);
            return value;
        }

        unsafe public static string GT_ReadString(IntPtr address)
        {
            string value = null;
            byte[] lp_buffer = new byte[260];
            IntPtr num_bytes;
            ReadProcessMemory(GT.GT_GetGameHandle(), address, lp_buffer, 260, out num_bytes);
            value = System.Text.Encoding.UTF8.GetString(lp_buffer);
            return value;
        }

        public static bool GT_WriteMemory(IntPtr address, string type, string value)
        {
            byte[] real_value = new byte[4];
            int size = 4;

            if (type == "float")
            {
                real_value = BitConverter.GetBytes(Convert.ToSingle(value));
                size = 4;
            }
            else if (type == "int")
            {
                real_value = BitConverter.GetBytes(Convert.ToInt32(value));
                size = 4;
            }
            else if (type == "byte")
            {
                real_value = new byte[1];
                real_value[0] = Convert.ToByte(value, 16);
                size = 1;
            }
            else if (type == "2bytes")
            {
                real_value = new byte[2];
                real_value[0] = (byte)(Convert.ToInt32(value) % 256);
                real_value[1] = (byte)(Convert.ToInt32(value) / 256);
                size = 2;
            }
            else if (type == "bytes")
            {
                if (value.Contains(",") || value.Contains(" "))
                {
                    string[] stringBytes;
                    if (value.Contains(","))
                        stringBytes = value.Split(',');
                    else
                        stringBytes = value.Split(' ');

                    int c = stringBytes.Count();
                    real_value = new byte[c];
                    for (int i = 0; i < c; i++)
                    {
                        real_value[i] = Convert.ToByte(stringBytes[i], 16);
                    }
                    size = stringBytes.Count();
                }
                else
                {
                    real_value = new byte[1];
                    real_value[0] = Convert.ToByte(value, 16);
                    size = 1;
                }
            }
            else if (type == "double")
            {
                real_value = BitConverter.GetBytes(Convert.ToDouble(value));
                size = 8;
            }
            else if (type == "long")
            {
                real_value = BitConverter.GetBytes(Convert.ToInt64(value));
                size = 8;
            }
            else if (type == "string")
            {
                real_value = new byte[value.Length];
                real_value = System.Text.Encoding.UTF8.GetBytes(value);
                size = value.Length;
            }
            IntPtr gHandle = GT_GetGameHandle();
            return WriteProcessMemory(gHandle, address, real_value, (UIntPtr)size, IntPtr.Zero);
        }

        unsafe public static bool GT_InjectCode(IntPtr address, char opcode, uint size)
        {
            void* ptr = (void*)&opcode;
            return GT_InjectOpcode(address, ptr, size);
        }

        unsafe public static bool GT_InjectOpcodes(IntPtr[] addresses, char[] lp_opcode, uint[] sz_opcode_lens, uint n_addresses)
        {
            uint index = 0;
            bool inject_status = false;

            for (index = 0; index < n_addresses; index++)
            {
                inject_status = GT_InjectCode(addresses[index], lp_opcode[index], sz_opcode_lens[index]);
            }
            return inject_status;
        }
        public static IntPtr GT_InjectShell(IntPtr lp_address, byte[] lp_shell, uint sz_shell, uint nops_amount, GT_SHELL shell_type, GT_OPCODE opcode_type)
        {
            IntPtr ptr_str = Marshal.AllocHGlobal(lp_shell.Length);
            Marshal.Copy(lp_shell, 0, ptr_str, lp_shell.Length);
            IntPtr status = GT_InjectShellCode(lp_address, ptr_str, sz_shell, nops_amount, shell_type, opcode_type);
            Marshal.FreeHGlobal(ptr_str);
            return status;
        }

    }
}
