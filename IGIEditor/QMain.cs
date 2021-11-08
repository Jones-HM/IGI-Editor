using QLibc;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace IGIEditor
{
    static class QClass
    {
        [STAThread]
        static void Main()
        {
            try
            {
                bool instanceCount = false;
                Mutex mutex = null;
                var projAppName = AppDomain.CurrentDomain.FriendlyName;
                AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnAppExit);

                mutex = new Mutex(true,projAppName, out instanceCount);
                if (instanceCount)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new IGIEditorUI());
                    mutex.ReleaseMutex();
                }
                else
                {
                    QUtils.ShowError("IGI Editor is already running");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception:" + ex.Message + "\nStack: " + ex.StackTrace, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void OnAppExit(object sender, EventArgs e)
        {
            //Update config on exit.
            QUtils.CreateConfig();

            if (File.Exists(QUtils.logFile))
            {
                QUtils.ShellExec("move /Y " + QUtils.logFile + " " + QUtils.cachePathAppLogs,true);
                QUtils.ShellExec("move /Y " + QUtils.qLibLogsFile + " " + QUtils.cachePathAppLogs,true);
                Thread.Sleep(2000);
            }
            QUtils.CleanUpAiFiles();

            //Eject Dll on Exit.
            QLibc.GT.GT_SendKeys2Process(QMemory.gameName, QLibc.GT.VK.END);
        }
    }
}
