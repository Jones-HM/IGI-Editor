using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace IGIEditor
{
    internal class QCompiler
    {
        internal static bool Compile(string qscFile, string gamePath, int _ignore)
        {
            bool status = false;
            QUtils.AddLog("Compile() QscFile : file: '" + qscFile + "' Game Path: '" + gamePath+ "'");

            if (File.Exists(qscFile))
            {
                string scriptFile = "MISSION:objects.qsc";
                string outScriptPath = gamePath + "\\" + qscFile;
                QUtils.AddLog("Compile() QscFile : Output path '" + outScriptPath + "'");

                var qscData = QUtils.LoadFile(qscFile);

                if (!String.IsNullOrEmpty(qscData))
                {
                    //Compile for Humanplayer.
                    if (gamePath.Contains(QUtils.humanplayerPath))
                    {
                        scriptFile = "LOCAL:humanplayer/" + qscFile;
                    }

                    else if (gamePath.Contains("ai"))
                    {
                        scriptFile = "MISSION:AI/" + qscFile;
                    }

                    if (File.Exists(outScriptPath))
                    {
                        QUtils.AddLog("Compile() QscFile : File exist '" + outScriptPath + "' deleting file.");
                        File.Delete(outScriptPath);
                    }

                    //Copy file to OutPath and Compile with Internal Compiler.
                    File.Copy(qscFile, outScriptPath);

                    QUtils.AddLog("Compile() QscFile : Starting Compiling of file '" + scriptFile + "'");
                    QInternals.ScriptCompile(scriptFile);
                    status = true;
                    QUtils.AddLog("Compile() QscFile : Compiling of file '" + scriptFile + "' done\tOutput Script Path: '" + outScriptPath+ "'");


                    if (status) IGIEditorUI.editorRef.SetStatusText("Compile success");
                }
                else QUtils.ShowError("Compile error data is empty.", "COMPILE ERROR");
            }
            else QUtils.ShowError("Compile error file not found.", "COMPILE ERROR");
            return status;
        }

        internal static bool Compile(string qscData, string gamePath, bool appendData = false, bool restartLevel = false, bool savePos = true)
        {
            bool status = false;
            try
            {
                if (!String.IsNullOrEmpty(qscData))
                {
                    string scriptFile = "MISSION:objects.qsc";
                    QUtils.AddLog("Compile() QscData : Script File :'" + scriptFile + "' Game Path: '" + gamePath + "' Append Data: " + appendData + " Restart Level: " + restartLevel + " Save Position: " + savePos);
                    
                    //Compile for Objets.
                    QUtils.SaveFile(qscData, appendData);
                    QUtils.gamePath = QUtils.cfgGamePath + QMemory.GetCurrentLevel();
                    string outScriptPath = QUtils.gamePath + "\\" + QUtils.objectsQsc;
                    QUtils.AddLog("Compile() QscData : Output Path: '" + outScriptPath + "'");

                    if (File.Exists(outScriptPath))
                    {
                        QUtils.AddLog("Compile() QscData : File exist '" + outScriptPath + "' deleting file.");
                       File.Delete(outScriptPath);
                    }

                    //Copy file to OutPath and Compile with Internal Compiler.
                    QUtils.AddLog("Compile() QscData : Starting Compiling of file '" + QUtils.objectsQsc + "'");
                    File.Copy(QUtils.objectsQsc, outScriptPath);
                    QInternals.ScriptCompile(scriptFile);
                    QUtils.AddLog("Compile() QscData : Compiling of file '" + scriptFile + "' done\tOutput Path: '" + outScriptPath + "'");

                    QUtils.Sleep(1.5f);
                    //Delete script file after compiling.
                    File.Delete(outScriptPath);
                    QUtils.AddLog("Compile() QscData : Output Path: '" + outScriptPath + "' removed");

                    if (restartLevel) QMemory.RestartLevel(savePos);
                    status = true;
                }
            }
            catch (Exception ex)
            {
                QUtils.ShowError("Exception: " + ex.Message ?? ex.StackTrace);
                status = false;
            }
            return status;
        }

        internal static bool CompileEx(string qscData)
        {
            return Compile(qscData, QUtils.gamePath, false, true, true);
        }
    }
}
