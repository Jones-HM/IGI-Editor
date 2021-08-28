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

        private string compileStart = "compile.bat";
        private string decompileStart = "decompile.bat";
        internal static string compilePath = QUtils.appdataPath + @"\" + QUtils.qEditor + @"\" + QUtils.qconv + @"\Compile";
        private string compileInputPath = QUtils.appdataPath + @"\" + QUtils.qEditor + @"\" + QUtils.qconv + @"\Compile\input";
        internal static string decompilePath = QUtils.appdataPath + @"\" + QUtils.qEditor + @"\" + QUtils.qconv + @"\Decompile";
        private string decompileInputPath = QUtils.appdataPath + @"\" + QUtils.qEditor + @"\" + QUtils.qconv + @"\Decompile\input";
        private string copyNoneErr = "0 File(s) copied";
        private string moveNoneErr = "0 File(s) moved";
        private string qappPath;

        internal QCompiler()
        {
            qappPath = Directory.GetCurrentDirectory();
            qpath = QUtils.appdataPath;
        }

        internal static void CheckQConvExist()
        {
            var qconvPath = QUtils.appdataPath + Path.DirectorySeparatorChar + QUtils.qEditor + Path.DirectorySeparatorChar + QUtils.qconv;
            bool exist = Directory.Exists(qconvPath);
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

        private string QGetAbsPath(string dirName)
        {
            return qpath + dirName;
        }

        private bool QCopy(List<string> files, QTYPE type)
        {
            bool status = true;
            string copyPath = (type == (int)QTYPE.COMPILE) ? (compileInputPath) : (decompileInputPath);
            foreach (var file in files)
            {
                string copyFile = "copy \"" + file + "\" \"" + copyPath + "\"";
                var shellOut = QUtils.ShellExec(copyFile);

                //Check for error in copy.
                if (shellOut.Contains(copyNoneErr))
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
            string xcopyCmd = "xcopy " + src + dest + " /s /e /h /D";

            var shellOut = QUtils.ShellExec(xcopyCmd);

            //Check for error in copy.
            if (shellOut.Contains(copyNoneErr))
                status = false;
            return status;
        }

        private bool XMove(string src, string dest, QTYPE qtype)
        {
            bool status = true;
            string filter = "*.";

            if (qtype == QTYPE.COMPILE) filter = "*qvm";
            else if (qtype == QTYPE.DECOMPILE) filter = "*qsc";

            string xmoveCmd = "for /r \"" + src + "\" %x in (" + filter + ") do move \"%x\" \"" + dest + "\"";

            var shellOut = QUtils.ShellExec(xmoveCmd);

            //Check for error in move.
            if (shellOut.Contains(moveNoneErr))
                status = false;
            return status;
        }



        internal static bool Compile(string qscFile, string qscPath, int _Ignore)
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

        internal static bool Compile(string qscData, string gamePath, bool appendData = false, bool restartLevel = false, bool savePos = true)
        {
            bool status = false;
            if (!String.IsNullOrEmpty(qscData))
            {
                QUtils.SaveFile(qscData, appendData);
                var qcompiler = new QCompiler();
                status = qcompiler.QCompile(new List<string>() { QUtils.objectsQsc }, gamePath);

                if (status)
                {
                    IGIEditorUI.editorRef.SetStatusText("QCompile success");
                    if (restartLevel)
                        QMemory.RestartLevel(savePos);
                }
            }
            return status;
        }


        internal static void ShowCompileErrors()
        {
            {
                string qscData = QUtils.LoadFile();

                int startTokenCount = qscData.Count(o => o == '(');
                int endTokenCount = qscData.Count(o => o == ')');

                if (startTokenCount != endTokenCount)
                {
                    QUtils.ShowError("QError : Error while compiling Mismatch token '(' found in script");
                }
                else
                {
                    QUtils.AddLog("ShowCompileErrors() Start token : " + startTokenCount);
                    QUtils.AddLog("ShowCompileErrors() End token : " + endTokenCount);
                }

                var match_1 = Regex.Match(qscData, @",\s*\n\),");
                var match_2 = Regex.Match(qscData, @""",\s\){1,},");


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

        public bool QCompile(List<string> qscFiles, string outputPath)
        {
            bool status = true;
            try
            {
                status = QCopy(qscFiles, QTYPE.COMPILE);

                if (!status)
                    QUtils.ShowError("Error occurred while copying files");

                //Change directory to compile directory.
                QSetPath(compilePath);

                //Start compile command.
                string shellOut = QUtils.ShellExec(compileStart);
                if (shellOut.Contains("Error") || shellOut.Contains("importModule") || shellOut.Contains("ModuleNotFoundError") || shellOut.Contains("Converted: 0"))
                {
                    QUtils.ShowError("Error in compiling input files");
                    return false;
                }

                var currDir = Directory.GetCurrentDirectory();
                if (Directory.Exists(currDir))
                {
                    bool moveStatus = XMove("output", outputPath, QTYPE.COMPILE);
                    if (!moveStatus)
                        QUtils.ShowError("Error while moving data to Output path");
                }
                else
                {
                    QUtils.ShowError("Path '" + currDir + "' does not exist!");
                }

            }
            catch (Exception ex)
            {
                QUtils.ShowError(ex.Message);
            }
            Directory.SetCurrentDirectory(qappPath);
            return status;
        }

        public bool QDecompile(List<string> qvmFiles, string outputPath)
        {
            bool status = true;
            try
            {
                status = QCopy(qvmFiles, QTYPE.DECOMPILE);

                if (!status)
                    QUtils.ShowError("Error occurred while copying files");

                //Change directory to compile directory.
                QSetPath(decompilePath);

                //Start decompile command.
                string shellOut = QUtils.ShellExec(decompileStart);
                if (shellOut.Contains("Error") || shellOut.Contains("ModuleNotFoundError") || shellOut.Contains("Converted: 0"))
                    QUtils.ShowError("Error in decompiling input files");

                var currDir = Directory.GetCurrentDirectory();

                if (Directory.Exists(currDir))
                {
                    bool moveStatus = XMove("output", outputPath, QTYPE.DECOMPILE);
                    if (!moveStatus)
                        QUtils.ShowError("Error while moving data to Output path");
                }
                else
                {
                    QUtils.ShowError("Path '" + currDir + "' does not exist!");
                }

            }
            catch (Exception ex)
            {
                QUtils.ShowError(ex.Message);
            }
            Directory.SetCurrentDirectory(qappPath);
            return status;
        }

        internal static bool CompileEx(string qscData)
        {
            return Compile(qscData, QUtils.gamePath, false, true, true);
        }
    }
}
