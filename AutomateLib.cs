using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace UXLib
{
    class Automate
    {
        //Add process to the controls of a control
        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetWindowPos(IntPtr hwnd, long hWndInsertAfter, long x, long y, long cx, long cy, long wFlags);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);

        IntPtr AETC1;

        public void AddExeToControl(Control ControlContainer, String GameProcessName, int TimeToWait_ForExeToOpen, string Argumentss)
        {
            try
            {
                ProcessStartInfo ps1 = new ProcessStartInfo(GameProcessName);
                ps1.Arguments = Argumentss;
                ps1.WindowStyle = ProcessWindowStyle.Minimized;
                Process p1 = Process.Start(ps1);
                Thread.Sleep(TimeToWait_ForExeToOpen);
                AETC1 = p1.MainWindowHandle;
                SetParent(AETC1, ControlContainer.Handle);
                MoveWindow(AETC1, 0, 0, ControlContainer.Width, ControlContainer.Height, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

    }
}
