using System;
using System.Text.RegularExpressions;

namespace IGIEditor
{
    class QMisc
    {
        static string cutscence_regex = @"!CutScene_\d{3,}.isFinished";

        internal static string RemoveCutscene(string input_path, int game_level)
        {
            QUtils.ResetFile(game_level);
            string qsc_data = QUtils.LoadFile();

            qsc_data = Regex.Replace(qsc_data, cutscence_regex, String.Empty, RegexOptions.Multiline);
            return qsc_data;
        }

        internal static bool RemoveCutscenes(string input_path)
        {
            bool status = false;
            for (int level = 1; level <= 14; ++level)
            {
                var qsc_data = RemoveCutscene(input_path, level);
                if (!String.IsNullOrEmpty(qsc_data))
                {
                    string game_path = QUtils.cfgGamePath + level + "\\" + QUtils.objectsQvm;
                    status = QCompiler.Compile(qsc_data, game_path);
                }
            }
            return status;
        }
    }
}
