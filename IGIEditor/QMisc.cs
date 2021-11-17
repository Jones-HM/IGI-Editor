using System;
using System.Text.RegularExpressions;

namespace IGIEditor
{
    class QMisc
    {
        static string cutscenceRegex = @"!CutScene_\d{3,}.isFinished";

        internal static string RemoveCutscene()
        {
            string qscData = QUtils.LoadFile();

            qscData = Regex.Replace(qscData, cutscenceRegex, String.Empty, RegexOptions.Multiline);
            return qscData;
        }

        internal static string RemoveCutscene(string inputPath)
        {
            string qscData = QUtils.LoadFile(inputPath);

            qscData = Regex.Replace(qscData, cutscenceRegex, String.Empty, RegexOptions.Multiline);
            return qscData;
        }

        internal static bool RemoveCutscenes(string input_path)
        {
            bool status = false;
            for (int level = 1; level <= QUtils.GAME_MAX_LEVEL; ++level)
            {
                input_path = input_path + level;//Not tested.
                var qsc_data = RemoveCutscene(input_path);
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
