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
            bool instance_count = false;
            Mutex mutex = null;
            const string PROJECT_NAME = "$(ProjectName)";
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnAppExit);

            try
            {
                mutex = new Mutex(true, PROJECT_NAME, out instance_count);
                if (instance_count)
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
                QUtils.ShowError(ex.Message, "Application Error");
            }

        }

        private static void OnAppExit(object sender, EventArgs e)
        {
            //CleanUpFiles();
        }

        private static void CleanUpFiles()
        {
            var outputAiPath = QUtils.cfgGamePath + QUtils.gGameLevel + "\\ai\\";

            //Eject Dll on Exit.
            GT.GT_SendKeys2Process(QMemory.gameName, GT.VK.END);
            Thread.Sleep(3000);

            if (File.Exists(QUtils.tmpDllPath))
                File.Delete(QUtils.tmpDllPath);

            if (QUtils.aiScriptFiles.Count > 1)
            {
                foreach (var scriptFile in QUtils.aiScriptFiles)
                    File.Delete(outputAiPath + scriptFile);
                File.Delete(QUtils.objectsQsc);
            }
            else
                QUtils.ShowError("Ai scripts hasn't been added yet");
        }
    }

    public class HumanAi
    {
        public int aiCount { get; set; }
        public string aiType { get; set; }
        public int graphId { get; set; }
        public string weapon { get; set; }
        public string model { get; set; }
        public bool guardGenerator { get; set; }
        public int maxSpawns { get; set; }
        public bool friendly { get; set; }
        public bool invulnerability { get; set; }
        public bool advanceView { get; set; }
    }
}
