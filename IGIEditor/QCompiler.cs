using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace IGIEditor
{
    internal class QCompiler
    {
        internal static bool Compile(string qscFile, string gamePath, int _ignore)
        {
            bool status = false;
            QUtils.currGameLevel = QMemory.GetCurrentLevel();
            string currLevelPath = "level" + QUtils.currGameLevel;
            QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "file: '" + qscFile + "' Game Path: '" + gamePath + "'" + " CurrLevel path: " + currLevelPath);

            if (File.Exists(qscFile))
            {
                string scriptFile = "MISSION:objects.qsc";
                string outScriptPath = gamePath + "\\" + qscFile;
                QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "QscFile : Output path '" + outScriptPath + "'");

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
                        QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "QscFile : File exist '" + outScriptPath + "' deleting file.");
                        File.Delete(outScriptPath);
                    }

                    //Copy file to OutPath and Compile with Internal Compiler.
                    File.Copy(qscFile, outScriptPath);

                    QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "QscFile : Starting Compiling of file '" + scriptFile + "'");
                    QInternals.ScriptCompile(scriptFile);
                    status = true;
                    QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "QscFile : Compiling of file '" + scriptFile + "' done\tOutput Script Path: '" + outScriptPath + "'");


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
                QUtils.currGameLevel = QMemory.GetCurrentLevel();
                string currLevelPath = "level" + QUtils.currGameLevel;
                if (!String.IsNullOrEmpty(qscData))
                {
                    string scriptFile = "MISSION:objects.qsc";
                    QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "QscData : Script File :'" + scriptFile + "' Game Path: '" + gamePath + ",CurrLevel path: " + currLevelPath + "',Append Data: " + appendData + ",Restart Level: " + restartLevel + ",Save Position: " + savePos);

                    if (!gamePath.Contains(currLevelPath))
                    {
                        QUtils.ShowLogError(MethodBase.GetCurrentMethod().Name, "Compile error in game path for level #" + QUtils.currGameLevel);
                        return false;
                    }

                    //Compile for Objets.
                    QUtils.SaveFile(qscData, appendData);
                    QUtils.gamePath = QUtils.cfgGamePath + QMemory.GetCurrentLevel();
                    string outScriptPath = QUtils.gamePath + "\\" + QUtils.objectsQsc;
                    QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "QscData : Output Path: '" + outScriptPath + "'");

                    if (File.Exists(outScriptPath))
                    {
                        QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "QscData : File exist '" + outScriptPath + "' deleting file.");
                        File.Delete(outScriptPath);
                    }

                    //Copy file to OutPath and Compile with Internal Compiler.
                    QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "QscData : Starting Compiling of file '" + QUtils.objectsQsc + "'");
                    File.Copy(QUtils.objectsQsc, outScriptPath);
                    QInternals.ScriptCompile(scriptFile);
                    QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "QscData : Compiling of file '" + scriptFile + "' done\tOutput Path: '" + outScriptPath + "'");

                    QUtils.Sleep(1.5f);
                    //Delete script file after compiling.
                    File.Delete(outScriptPath);

                    QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "QscData : Output Path: '" + outScriptPath + "' removed");

                    if (restartLevel) QMemory.RestartLevel(savePos);
                    status = true;
                }
            }
            catch (Exception ex)
            {
                QUtils.ShowLogException(MethodBase.GetCurrentMethod().Name, ex);
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
