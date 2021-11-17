﻿using QLibc;
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

                mutex = new Mutex(true, projAppName, out instanceCount);
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

            //Move Logs and data to cache.
            if (File.Exists(QUtils.logFile))
            {
                QUtils.ShellExec("move /Y " + QUtils.logFile + " " + QUtils.cachePathAppLogs, true);
                QUtils.ShellExec("move /Y " + QUtils.qLibLogsFile + " " + QUtils.cachePathAppLogs, true);
                QUtils.Sleep(2);
            }
            if (File.Exists(QUtils.internalsLogPath))
                QUtils.ShellExec("move /Y " + QUtils.internalsLogPath + " " + QUtils.cachePathAppLogs, true);

            //Cleanup A.I script files.
            QUtils.CleanUpAiFiles();

            //Deattach internals on Exit.
            QUtils.DeattachInternals();

            //Cleanup tmp files in Cache.
            QUtils.CleanUpTmpFiles();
        }
    }
}
