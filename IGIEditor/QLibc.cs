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

using IGIEditor;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace QLibc
{
    class GT
    {
        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/ms646270(v=vs.85).aspx
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct INPUT
        {
            public uint Type;
            public MOUSEKEYBDHARDWAREINPUT Data;
        }

        /// <summary>
        /// http://social.msdn.microsoft.com/Forums/en/csharplanguage/thread/f0e82d6e-4999-4d22-b3d3-32b25f61fb2a
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        internal struct MOUSEKEYBDHARDWAREINPUT
        {
            [FieldOffset(0)]
            public HARDWAREINPUT Hardware;
            [FieldOffset(0)]
            public KEYBDINPUT Keyboard;
            [FieldOffset(0)]
            public MOUSEINPUT Mouse;
        }

        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/ms646310(v=vs.85).aspx
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct HARDWAREINPUT
        {
            public uint Msg;
            public ushort ParamL;
            public ushort ParamH;
        }

        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/ms646310(v=vs.85).aspx
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct KEYBDINPUT
        {
            public ushort Vk;
            public ushort Scan;
            public uint Flags;
            public uint Time;
            public IntPtr ExtraInfo;
        }

        /// <summary>
        /// http://social.msdn.microsoft.com/forums/en-US/netfxbcl/thread/2abc6be8-c593-4686-93d2-89785232dacd
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct MOUSEINPUT
        {
            public int X;
            public int Y;
            public uint MouseData;
            public uint Flags;
            public uint Time;
            public IntPtr ExtraInfo;
        }

        public enum VK : ushort
        {
            #region Media

            /// <summary>
            /// Next track if a song is playing
            /// </summary>
            MEDIA_NEXT_TRACK = 0xb0,

            /// <summary>
            /// Play pause
            /// </summary>
            MEDIA_PLAY_PAUSE = 0xb3,

            /// <summary>
            /// Previous track
            /// </summary>
            MEDIA_PREV_TRACK = 0xb1,

            /// <summary>
            /// Stop
            /// </summary>
            MEDIA_STOP = 0xb2,

            #endregion

            #region math

            /// <summary>Key "+"</summary>
            ADD = 0x6b,
            /// <summary>
            /// "*" key
            /// </summary>
            MULTIPLY = 0x6a,

            /// <summary>
            /// "/" key
            /// </summary>
            DIVIDE = 0x6f,

            /// <summary>
            /// Subtract key "-"
            /// </summary>
            SUBTRACT = 0x6d,

            #endregion

            #region Browser
            /// <summary>
            /// Go Back
            /// </summary>
            BROWSER_BACK = 0xa6,
            /// <summary>
            /// Favorites
            /// </summary>
            BROWSER_FAVORITES = 0xab,
            /// <summary>
            /// Forward
            /// </summary>
            BROWSER_FORWARD = 0xa7,
            /// <summary>
            /// Home
            /// </summary>
            BROWSER_HOME = 0xac,
            /// <summary>
            /// Refresh
            /// </summary>
            BROWSER_REFRESH = 0xa8,
            /// <summary>
            /// browser search
            /// </summary>
            BROWSER_SEARCH = 170,
            /// <summary>
            /// Stop
            /// </summary>
            BROWSER_STOP = 0xa9,
            #endregion

            #region Numpad numbers
            /// <summary>
            /// 
            /// </summary>
            NUMPAD0 = 0x60,
            /// <summary>
            /// 
            /// </summary>
            NUMPAD1 = 0x61,
            /// <summary>
            /// 
            /// </summary>
            NUMPAD2 = 0x62,
            /// <summary>
            /// 
            /// </summary>
            NUMPAD3 = 0x63,
            /// <summary>
            /// 
            /// </summary>
            NUMPAD4 = 100,
            /// <summary>
            /// 
            /// </summary>
            NUMPAD5 = 0x65,
            /// <summary>
            /// 
            /// </summary>
            NUMPAD6 = 0x66,
            /// <summary>
            /// 
            /// </summary>
            NUMPAD7 = 0x67,
            /// <summary>
            /// 
            /// </summary>
            NUMPAD8 = 0x68,
            /// <summary>
            /// 
            /// </summary>
            NUMPAD9 = 0x69,

            #endregion

            #region Fkeys
            /// <summary>
            /// F1
            /// </summary>
            F1 = 0x70,
            /// <summary>
            /// F10
            /// </summary>
            F10 = 0x79,
            /// <summary>
            /// 
            /// </summary>
            F11 = 0x7a,
            /// <summary>
            /// 
            /// </summary>
            F12 = 0x7b,
            /// <summary>
            /// 
            /// </summary>
            F13 = 0x7c,
            /// <summary>
            /// 
            /// </summary>
            F14 = 0x7d,
            /// <summary>
            /// 
            /// </summary>
            F15 = 0x7e,
            /// <summary>
            /// 
            /// </summary>
            F16 = 0x7f,
            /// <summary>
            /// 
            /// </summary>
            F17 = 0x80,
            /// <summary>
            /// 
            /// </summary>
            F18 = 0x81,
            /// <summary>
            /// 
            /// </summary>
            F19 = 130,
            /// <summary>
            /// 
            /// </summary>
            F2 = 0x71,
            /// <summary>
            /// 
            /// </summary>
            F20 = 0x83,
            /// <summary>
            /// 
            /// </summary>
            F21 = 0x84,
            /// <summary>
            /// 
            /// </summary>
            F22 = 0x85,
            /// <summary>
            /// 
            /// </summary>
            F23 = 0x86,
            /// <summary>
            /// 
            /// </summary>
            F24 = 0x87,
            /// <summary>
            /// 
            /// </summary>
            F3 = 0x72,
            /// <summary>
            /// 
            /// </summary>
            F4 = 0x73,
            /// <summary>
            /// 
            /// </summary>
            F5 = 0x74,
            /// <summary>
            /// 
            /// </summary>
            F6 = 0x75,
            /// <summary>
            /// 
            /// </summary>
            F7 = 0x76,
            /// <summary>
            /// 
            /// </summary>
            F8 = 0x77,
            /// <summary>
            /// 
            /// </summary>
            F9 = 120,

            #endregion

            #region Other
            /// <summary>
            /// 
            /// </summary>
            OEM_1 = 0xba,
            /// <summary>
            /// 
            /// </summary>
            OEM_102 = 0xe2,
            /// <summary>
            /// 
            /// </summary>
            OEM_2 = 0xbf,
            /// <summary>
            /// 
            /// </summary>
            OEM_3 = 0xc0,
            /// <summary>
            /// 
            /// </summary>
            OEM_4 = 0xdb,
            /// <summary>
            /// 
            /// </summary>
            OEM_5 = 220,
            /// <summary>
            /// 
            /// </summary>
            OEM_6 = 0xdd,
            /// <summary>
            /// 
            /// </summary>
            OEM_7 = 0xde,
            /// <summary>
            /// 
            /// </summary>
            OEM_8 = 0xdf,
            /// <summary>
            /// 
            /// </summary>
            OEM_CLEAR = 0xfe,
            /// <summary>
            /// 
            /// </summary>
            OEM_COMMA = 0xbc,
            /// <summary>
            /// 
            /// </summary>
            OEM_MINUS = 0xbd,
            /// <summary>
            /// 
            /// </summary>
            OEM_PERIOD = 190,
            /// <summary>
            /// 
            /// </summary>
            OEM_PLUS = 0xbb,

            #endregion

            #region KEYS

            /// <summary>
            /// 
            /// </summary>
            KEY_0 = 0x30,
            /// <summary>
            /// 
            /// </summary>
            KEY_1 = 0x31,
            /// <summary>
            /// 
            /// </summary>
            KEY_2 = 50,
            /// <summary>
            /// 
            /// </summary>
            KEY_3 = 0x33,
            /// <summary>
            /// 
            /// </summary>
            KEY_4 = 0x34,
            /// <summary>
            /// 
            /// </summary>
            KEY_5 = 0x35,
            /// <summary>
            /// 
            /// </summary>
            KEY_6 = 0x36,
            /// <summary>
            /// 
            /// </summary>
            KEY_7 = 0x37,
            /// <summary>
            /// 
            /// </summary>
            KEY_8 = 0x38,
            /// <summary>
            /// 
            /// </summary>
            KEY_9 = 0x39,
            /// <summary>
            /// 
            /// </summary>
            KEY_A = 0x41,
            /// <summary>
            /// 
            /// </summary>
            KEY_B = 0x42,
            /// <summary>
            /// 
            /// </summary>
            KEY_C = 0x43,
            /// <summary>
            /// 
            /// </summary>
            KEY_D = 0x44,
            /// <summary>
            /// 
            /// </summary>
            KEY_E = 0x45,
            /// <summary>
            /// 
            /// </summary>
            KEY_F = 70,
            /// <summary>
            /// 
            /// </summary>
            KEY_G = 0x47,
            /// <summary>
            /// 
            /// </summary>
            KEY_H = 0x48,
            /// <summary>
            /// 
            /// </summary>
            KEY_I = 0x49,
            /// <summary>
            /// 
            /// </summary>
            KEY_J = 0x4a,
            /// <summary>
            /// 
            /// </summary>
            KEY_K = 0x4b,
            /// <summary>
            /// 
            /// </summary>
            KEY_L = 0x4c,
            /// <summary>
            /// 
            /// </summary>
            KEY_M = 0x4d,
            /// <summary>
            /// 
            /// </summary>
            KEY_N = 0x4e,
            /// <summary>
            /// 
            /// </summary>
            KEY_O = 0x4f,
            /// <summary>
            /// 
            /// </summary>
            KEY_P = 80,
            /// <summary>
            /// 
            /// </summary>
            KEY_Q = 0x51,
            /// <summary>
            /// 
            /// </summary>
            KEY_R = 0x52,
            /// <summary>
            /// 
            /// </summary>
            KEY_S = 0x53,
            /// <summary>
            /// 
            /// </summary>
            KEY_T = 0x54,
            /// <summary>
            /// 
            /// </summary>
            KEY_U = 0x55,
            /// <summary>
            /// 
            /// </summary>
            KEY_V = 0x56,
            /// <summary>
            /// 
            /// </summary>
            KEY_W = 0x57,
            /// <summary>
            /// 
            /// </summary>
            KEY_X = 0x58,
            /// <summary>
            /// 
            /// </summary>
            KEY_Y = 0x59,
            /// <summary>
            /// 
            /// </summary>
            KEY_Z = 90,

            #endregion

            #region volume
            /// <summary>
            /// Decrese volume
            /// </summary>
            VOLUME_DOWN = 0xae,

            /// <summary>
            /// Mute volume
            /// </summary>
            VOLUME_MUTE = 0xad,

            /// <summary>
            /// Increase volue
            /// </summary>
            VOLUME_UP = 0xaf,

            #endregion


            /// <summary>
            /// Take snapshot of the screen and place it on the clipboard
            /// </summary>
            SNAPSHOT = 0x2c,

            /// <summary>Send right click from keyboard "key that is 2 keys to the right of space bar"</summary>
            RightClick = 0x5d,

            /// <summary>
            /// Go Back or delete
            /// </summary>
            BACKSPACE = 8,

            /// <summary>
            /// Control + Break "When debuging if you step into an infinite loop this will stop debug"
            /// </summary>
            CANCEL = 3,
            /// <summary>
            /// Caps lock key to send cappital letters
            /// </summary>
            CAPS_LOCK = 20,
            /// <summary>
            /// Ctlr key
            /// </summary>
            CONTROL = 0x11,

            /// <summary>
            /// Alt key
            /// </summary>
            ALT = 18,

            /// <summary>
            /// "." key
            /// </summary>
            DECIMAL = 110,

            /// <summary>
            /// Delete Key
            /// </summary>
            DELETE = 0x2e,


            /// <summary>
            /// Arrow down key
            /// </summary>
            DOWN = 40,

            /// <summary>
            /// End key
            /// </summary>
            END = 0x23,

            /// <summary>
            /// Escape key
            /// </summary>
            ESC = 0x1b,

            /// <summary>
            /// Home key
            /// </summary>
            HOME = 0x24,

            /// <summary>
            /// Insert key
            /// </summary>
            INSERT = 0x2d,

            /// <summary>
            /// Open my computer
            /// </summary>
            LAUNCH_APP1 = 0xb6,
            /// <summary>
            /// Open calculator
            /// </summary>
            LAUNCH_APP2 = 0xb7,

            /// <summary>
            /// Open default email in my case outlook
            /// </summary>
            LAUNCH_MAIL = 180,

            /// <summary>
            /// Opend default media player (itunes, winmediaplayer, etc)
            /// </summary>
            LAUNCH_MEDIA_SELECT = 0xb5,

            /// <summary>
            /// Left control
            /// </summary>
            LCONTROL = 0xa2,

            /// <summary>
            /// Left arrow
            /// </summary>
            LEFT = 0x25,

            /// <summary>
            /// Left shift
            /// </summary>
            LSHIFT = 160,

            /// <summary>
            /// left windows key
            /// </summary>
            LWIN = 0x5b,


            /// <summary>
            /// Next "page down"
            /// </summary>
            PGDN = 0x22,

            /// <summary>
            /// Num lock to enable typing numbers
            /// </summary>
            NUMLOCK = 0x90,

            /// <summary>
            /// Page up key
            /// </summary>
            PGUP = 0x21,

            /// <summary>
            /// Right control
            /// </summary>
            RCONTROL = 0xa3,

            /// <summary>
            /// Return key
            /// </summary>
            ENTER = 13,

            /// <summary>
            /// Right arrow key
            /// </summary>
            RIGHT = 0x27,

            /// <summary>
            /// Right shift
            /// </summary>
            RSHIFT = 0xa1,

            /// <summary>
            /// Right windows key
            /// </summary>
            RWIN = 0x5c,

            /// <summary>
            /// Shift key
            /// </summary>
            SHIFT = 0x10,

            /// <summary>
            /// Space back key
            /// </summary>
            SPACE_BAR = 0x20,

            /// <summary>
            /// Tab key
            /// </summary>
            TAB = 9,

            /// <summary>
            /// Up arrow key
            /// </summary>
            UP = 0x26,

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

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint numberOfInputs, INPUT[] inputs, int sizeOfInputStructure);

        internal static void ActivateApp(string processName)
        {
            Process[] p = Process.GetProcessesByName(processName);

            // Activate the first application we find with this name
            if (p.Count() > 0)
                SetForegroundWindow(p[0].MainWindowHandle);
        }

        internal static void MultiKeyPress(VK[] keys)
        {
            INPUT[] inputs = new INPUT[keys.Count() * 2];
            for (int i = 0; i < keys.Count(); ++i)
            {
                for (int j = 0; j < 2; ++j)
                {
                    inputs[(j == 0) ? i : inputs.Count() - 1 - i].Type = 1;
                    inputs[(j == 0) ? i : inputs.Count() - 1 - i].Data.Keyboard = new KEYBDINPUT()
                    {
                        Vk = (ushort)keys[i],
                        Scan = 0,
                        Flags = Convert.ToUInt32((j == 0) ? 0 : 2),
                        Time = 0,
                        ExtraInfo = IntPtr.Zero,
                    };
                }
            }

            SendInput(Convert.ToUInt32(inputs.Count()), inputs, Marshal.SizeOf(typeof(INPUT)));
        }

        public static void GT_SendKeyStroke(string strKey, bool ctrl = false, bool alt = false, bool shift = false)
        {
            string keyString = "";
            if (ctrl) keyString = "^" + keyString;
            if (shift) keyString = "+" + keyString;
            if (alt) keyString = "%" + keyString;
            keyString += "{" + strKey + "}";

            GT_SendKeys2Process(QMemory.gameName, keyString, false);
        }

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

                GT_DoKeyPress(key);
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
