using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace IGIEditor
{
    internal class QCompiler
    {
        enum QTYPE
        {
            COMPILE,
            DECOMPILE,
        };
        private static string qpath;

        private string compile_start = "compile.bat";
        private string decompile_start = "decompile.bat";
        internal static string compile_path = QUtils.appdataPath + @"\" + QUtils.igiEditor + @"\" + QUtils.qconv + @"\Compile";
        private string compile_input_path = QUtils.appdataPath + @"\" + QUtils.igiEditor + @"\" + QUtils.qconv + @"\Compile\input";
        internal static string decompile_path = QUtils.appdataPath + @"\" + QUtils.igiEditor + @"\" + QUtils.qconv + @"\Decompile";
        private string decompile_input_path = QUtils.appdataPath + @"\" + QUtils.igiEditor + @"\" + QUtils.qconv + @"\Decompile\input";
        private string copy_none_err = "0 File(s) copied";
        private string move_none_err = "0 File(s) moved";
        private string qapp_path;

        internal QCompiler()
        {
            qapp_path = Directory.GetCurrentDirectory();
            qpath = QUtils.appdataPath;
        }

        internal static void CheckQConvExist()
        {
            var qconv_path = QUtils.appdataPath + Path.DirectorySeparatorChar + QUtils.igiEditor + Path.DirectorySeparatorChar + QUtils.qconv;
            bool exist = Directory.Exists(qconv_path);
            if (!exist)
            {
                QUtils.ShowError("QConvertor tool not found in system", QUtils.CAPTION_FATAL_SYS_ERR);
                System.Environment.Exit(1);
            }
        }


        private void QSetPath(string path)
        {
            //path = qpath + path;
            Directory.SetCurrentDirectory(path);
        }

        private string QGetAbsPath(string dir_name)
        {
            return qpath + dir_name;
        }

        private bool QCopy(List<string> files, QTYPE type)
        {
            bool status = true;
            string copy_path = (type == (int)QTYPE.COMPILE) ? (compile_input_path) : (decompile_input_path);
            foreach (var file in files)
            {
                string copy_file = "copy \"" + file + "\" \"" + copy_path + "\"";
                var shell_out = QUtils.ShellExec(copy_file);

                //Check for error in copy.
                if (shell_out.Contains(copy_none_err))
                {
                    status = false;
                    break;
                }
            }
            return status;
        }

        private bool XCopy(string src, string dest)
        {
            bool status = true;
            string xcopy_cmd = "xcopy " + src + dest + " /s /e /h /D";

            var shell_out = QUtils.ShellExec(xcopy_cmd);

            //Check for error in copy.
            if (shell_out.Contains(copy_none_err))
                status = false;
            return status;
        }

        private bool XMove(string src, string dest, QTYPE qtype)
        {
            bool status = true;
            string filter = "*.";

            if (qtype == QTYPE.COMPILE) filter = "*qvm";
            else if (qtype == QTYPE.DECOMPILE) filter = "*qsc";

            string xmove_cmd = "for /r \"" + src + "\" %x in (" + filter + ") do move \"%x\" \"" + dest + "\"";

            var shell_out = QUtils.ShellExec(xmove_cmd);

            //Check for error in move.
            if (shell_out.Contains(move_none_err))
                status = false;
            return status;
        }



        internal static bool Compile(string qscFile, string qscPath, int __ignore)
        {
            bool status = false;
            if (!String.IsNullOrEmpty(qscFile))
            {
                var qcompiler = new QCompiler();
                status = qcompiler.QCompile(new List<string>() { qscFile }, qscPath);

                if (status)
                {
                    IGIEditorUI.editorRef.SetStatusText("QCompile success");
                }
            }
            return status;
        }

        internal static bool Compile(string qsc_data, string game_path, bool append_data = false, bool restart_level = false, bool save_pos = true)
        {
            bool status = false;
            if (!String.IsNullOrEmpty(qsc_data))
            {
                QUtils.SaveFile(qsc_data, append_data);
                var qcompiler = new QCompiler();
                status = qcompiler.QCompile(new List<string>() { QUtils.objectsQsc }, game_path);

                if (status)
                {
                    IGIEditorUI.editorRef.SetStatusText("QCompile success");
                    if (restart_level)
                        QMemory.RestartLevel(save_pos);
                }
            }
            return status;
        }


        internal static void ShowCompileErrors()
        {
            {
                string qsc_data = QUtils.LoadFile();

                int start_token_count = qsc_data.Count(o => o == '(');
                int end_token_count = qsc_data.Count(o => o == ')');

                if (start_token_count != end_token_count)
                {
                    QUtils.ShowError("QError : Error while compiling Mismatch token '(' found in script");
                }
                else
                {
                    QUtils.AddLog("ShowCompileErrors() Start token : " + start_token_count);
                    QUtils.AddLog("ShowCompileErrors() End token : " + end_token_count);
                }

                var match_1 = Regex.Match(qsc_data, @",\s*\n\),");
                var match_2 = Regex.Match(qsc_data, @""",\s\){1,},");


                if (!match_1.Success || !match_2.Success)
                {
                    QUtils.ShowError("QError : Expected eof after token ')'");
                }

                else
                {
                    QUtils.ShellExec("QSuccess : Compiling finished");
                }
            }
        }

        public bool QCompile(List<string> qsc_files, string output_path)
        {
            bool status = true;
            try
            {
                status = QCopy(qsc_files, QTYPE.COMPILE);

                if (!status)
                    QUtils.ShowError("Error occurred while copying files");

                //Change directory to compile directory.
                QSetPath(compile_path);

                //Start compile command.
                string shell_out = QUtils.ShellExec(compile_start);
                if (shell_out.Contains("Error") || shell_out.Contains("import_module") || shell_out.Contains("ModuleNotFoundError") || shell_out.Contains("Converted: 0"))
                {
                    QUtils.ShowError("Error in compiling input files");
                    return false;
                }

                var curr_dir = Directory.GetCurrentDirectory();
                if (Directory.Exists(curr_dir))
                {
                    bool move_status = XMove("output", output_path, QTYPE.COMPILE);
                    if (!move_status)
                        QUtils.ShowError("Error while moving data to Output path");
                }
                else
                {
                    QUtils.ShowError("Path '" + curr_dir + "' does not exist!");
                }

            }
            catch (Exception ex)
            {
                QUtils.ShowError(ex.Message);
            }
            Directory.SetCurrentDirectory(qapp_path);
            return status;
        }

        public bool QDecompile(List<string> qvm_files, string output_path)
        {
            bool status = true;
            try
            {
                status = QCopy(qvm_files, QTYPE.DECOMPILE);

                if (!status)
                    QUtils.ShowError("Error occurred while copying files");

                //Change directory to compile directory.
                QSetPath(decompile_path);

                //Start decompile command.
                string shell_out = QUtils.ShellExec(decompile_start);
                if (shell_out.Contains("Error") || shell_out.Contains("ModuleNotFoundError") || shell_out.Contains("Converted: 0"))
                    QUtils.ShowError("Error in decompiling input files");

                var curr_dir = Directory.GetCurrentDirectory();

                if (Directory.Exists(curr_dir))
                {
                    bool move_status = XMove("output", output_path, QTYPE.DECOMPILE);
                    if (!move_status)
                        QUtils.ShowError("Error while moving data to Output path");
                }
                else
                {
                    QUtils.ShowError("Path '" + curr_dir + "' does not exist!");
                }

            }
            catch (Exception ex)
            {
                QUtils.ShowError(ex.Message);
            }
            Directory.SetCurrentDirectory(qapp_path);
            return status;
        }

        internal static bool CompileEx(string qsc_data)
        {
            return Compile(qsc_data, QUtils.gamePath, false, true, true);
        }
    }
}
