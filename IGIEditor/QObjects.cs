using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static IGIEditor.QUtils;

namespace IGIEditor
{
    class QObjects
    {
        internal static string AddRigidObj(string model, Real64 position, bool checkModel = false, string taskNote = "")
        {
            if (checkModel)
            {
                bool modelExist = QUtils.CheckModelExist(model);

                if (!modelExist)
                {
                    QUtils.ShowError("Model " + model + " does not exist in current level");
                    QUtils.AddLog("Model " + model + " does not exist in current level");
                    return null;
                }
            }
            return RigidObj(-1, taskNote, position.x, position.y, position.z, 0, 0, 0, model);
        }

        internal static string AddRigidObj(string model, Real64 position, Real32 orientation, bool checkModel, string taskNote = "")
        {
            if (checkModel)
            {
                bool modelExist = QUtils.CheckModelExist(model);

                if (!modelExist)
                {
                    QUtils.AddLog("Model " + model + " does not exist in current level");
                    QUtils.ShowError("Model " + model + " does not exist in current level");
                }
            }
            return RigidObj(-1, taskNote, position.x, position.y, position.z, orientation.alpha, orientation.beta, orientation.gamma, model);
        }

        internal static string RigidObj(int taskId = -1, string taskNote = "", double x = 0.0f, double y = 0.0f, double z = 0.0f, float alpha = 0.0f, float beta = 0.0f, float gamma = 0.0f, string modelId = "")
        {
            QUtils.qtaskObjId = -1;
            if (modelId.Contains("\""))
                modelId = modelId.Replace("\"", String.Empty);

            string qtaskRigidObj = "Task_New(" + QUtils.qtaskObjId + ",\"EditRigidObj\",\"" + taskNote + "\"," + x + "," + y + "," + z + "," + alpha + "," + beta + "," + gamma + ",\"" + modelId + "\"" + ",1" + ",1" + ",1" + ",0" + ",0" + ",0" + ");" + "\n";
            QUtils.AddLog("RigidObj called with ID : " + QUtils.qtaskObjId + "  RigidObj : " + FindModelName(modelId) + "\"\tX : " + x + " Y : " + y + " Z : " + z + "\tAlpha : " + alpha + " Beta : " + beta + ",Gamma : " + gamma + " Model : " + modelId + "\n");
            return qtaskRigidObj;
        }

        internal static string ComputerMapHilight(int targetTaskId = -1, string taskNote = "", string taskTitle = "", string taskInfo = "", string markerType = "MARKER_NONE", string markerColor = "MARKER_COLOR_NONE", int taskId = -1)
        {
            string qtaskHilight = "Task_New(" + taskId + ",\"ComputerHilight\",\"" + taskNote + "\",24958512.0, -56097400.0, 174370800.0, \"1\"," + "\"" + targetTaskId + "\",\"\",\"" + markerType + "\"," + "\"" + markerColor + "\",\"" + taskTitle + "\"" + ",\"" + taskInfo + "\");" + "\n";
            QUtils.AddLog("ComputerHilight called with ID : " + taskId + "  ComputerHilight : ");
            return qtaskHilight;
        }


        internal static string UpdatePositionInMeter(int id, ref Real64 position, bool checkModel = false)
        {
            QUtils.QTask qtask = null;
            qtask = QUtils.GetQTask(id);

            double xPos = (position.x == 0.0f) ? qtask.position.x : position.x;
            double yPos = (position.y == 0.0f) ? qtask.position.y : position.y;
            double zPos = (position.z == 0.0f) ? qtask.position.z : position.z;

            bool xVal = (xPos == 0.0f) ? false : true;
            bool yVal = (yPos == 0.0f) ? false : true;
            bool zVal = (zPos == 0.0f) ? false : true;

            int xLen = xVal ? xPos.ToString().Length : 0;
            int yLen = yVal ? yPos.ToString().Length : 0;
            int zLen = zVal ? zPos.ToString().Length : 0;

            QUtils.AddLog("UpdatePositionInMeter()  length : X:" + xLen + " Y: " + yLen + " Z: " + zLen);
            QUtils.AddLog("UpdatePositionInMeter() called with offset : X:" + position.x + " Y: " + position.y + " Z: " + position.z);


            //Check for length error.
            if (xLen > 3 || yLen > 3 || zLen > 3)
                throw new ArgumentOutOfRangeException("Offsets are out of range");

            int[] meterOffsets = { 100000, 1000000, 10000000 };

            //Add meter offset to distance. (M/S) .
            if (xVal) xPos = xPos + meterOffsets[xLen - 1];
            if (yVal) yPos = yPos + meterOffsets[yLen - 1];
            if (zVal) zPos = zPos + meterOffsets[zLen - 1];

            return UpdatePosition(id, ref position, checkModel);
        }

        internal static string UpdatePositionOffset(string model, ref Real64 offsets, bool checkModel = false)
        {
            QUtils.QTask qtask = null;
            qtask = QUtils.GetQTask(model);

            double xPos = (offsets.x == 0.0f) ? 0 : offsets.x;
            double yPos = (offsets.y == 0.0f) ? 0 : offsets.y;
            double zPos = (offsets.z == 0.0f) ? 0 : offsets.z;

            bool xVal = (xPos == 0.0f) ? false : true;
            bool yVal = (yPos == 0.0f) ? false : true;
            bool zVal = (zPos == 0.0f) ? false : true;

            int xLen = xVal ? xPos.ToString().Length : 0;
            int yLen = yVal ? yPos.ToString().Length : 0;
            int zLen = zVal ? zPos.ToString().Length : 0;

            QUtils.AddLog("UpdatePositionInMeter()  length : X:" + xLen + " Y: " + yLen + " Z: " + zLen);
            QUtils.AddLog("UpdatePositionInMeter() called with offset : X:" + offsets.x + " Y: " + offsets.y + " Z: " + offsets.z);


            //Check for length error.
            if (xLen > 3 || yLen > 3 || zLen > 3)
                throw new ArgumentOutOfRangeException("Offsets are out of range");

            int[] meterOffsets = { 100000, 1000000, 10000000 };

            //Add meter offset to distance. (M/S) .
            if (xVal) xPos = qtask.position.x + meterOffsets[xLen - 1];
            if (yVal) yPos = qtask.position.y + meterOffsets[yLen - 1];
            if (zVal) zPos = qtask.position.z + meterOffsets[zLen - 1];

            Real64 position = new Real64(xPos, yPos, zPos);
            return UpdatePositionMeter(model, ref position, checkModel);
        }

        internal static string UpdatePosition(int taskId, ref Real64 position, bool checkModel)
        {
            var r32 = new Real32();
            return Update(taskId, null, ref position, ref r32, (int)QTASKINFO.QTASK_ID, checkModel);
        }

        internal static string UpdatePositionMeter(string model, ref Real64 position, bool checkModel = false)
        {
            var r32 = new Real32();
            return Update(-999, model, ref position, ref r32, (int)QTASKINFO.QTASK_MODEL, checkModel);
        }

        internal static string UpdateOrientation(int taskId, ref Real32 orientation, bool checkModel = false)
        {
            var r64 = new Real64();
            return Update(taskId, null, ref r64, ref orientation, (int)QTASKINFO.QTASK_ID, checkModel);
        }


        internal static string UpdateOrientation(string model, ref Real32 orientation, bool checkModel = false)
        {
            var r64 = new Real64();
            return Update(-999, model, ref r64, ref orientation, (int)QTASKINFO.QTASK_MODEL, checkModel);
        }


        internal static string UpdateOrientationAll(int updateType, ref Real32 orientation)
        {
            string qscData = null;
            var qlist = QUtils.GetQTaskList(true);
            foreach (var qtask in qlist)
            {
                if (qtask.name == "\"Building\"" && updateType == (int)QTASKINFO.QTASK_MODEL)
                {
                    QUtils.AddLog("Building " + GetModelName(qtask.id) + " compatible for orientation");
                    qscData = UpdateOrientation(qtask.model, ref orientation);
                }
                else if (qtask.name == "\"EditRigidObj\"" && updateType != (int)QTASKINFO.QTASK_MODEL)
                {
                    QUtils.AddLog("3D Object " + GetModelName(qtask.id) + " compatible for orientation");
                    qscData = UpdateOrientation(qtask.model, ref orientation);
                }
                if (!String.IsNullOrEmpty(qscData))
                    QUtils.SaveFile(qscData);
            }
            return qscData;
        }



        //Update the object.
        internal static string Update(int id, string model, ref Real64 position, ref Real32 orientation, int updateType, bool checkModel = false)
        {
            string qscData = QUtils.LoadFile();

            //Remove all whitespaces.
            qscData = qscData.Replace("\t", String.Empty);
            string[] qscDataSplit = qscData.Split('\n');

            if (checkModel)
            {
                bool modelExist = QUtils.CheckModelExist(model);
                if (!modelExist)
                {
                    QUtils.ShowError("Model " + model + " does not exist in current level");
                    QUtils.AddLog("Model " + model + " does not exist in current level");
                    return null;
                }
            }

            qscData = Update(qscData, id, model, ref position, ref orientation, updateType);
            return qscData;
        }

        //Update the object via general method.
        private static string Update(string qscData, int id, string model, ref Real64 position, ref Real32 orientation, int updateType)
        {
            const string angularAmbientEffect = ",1, 1, 1, 0, 0, 0";
            bool isRigidObj = false;

            QUtils.QTask qtask = null;
            if (updateType == (int)QTASKINFO.QTASK_ID)
                qtask = QUtils.GetQTask(id);
            else if (updateType == (int)QTASKINFO.QTASK_MODEL)
                qtask = QUtils.GetQTask(model);

            if (qtask.model.Contains(";")) qtask.model = qtask.model.Replace(";", String.Empty);

            if (qtask == null)
            {
                //QUtils.ShowError("QTask in empty");
                QUtils.AddLog("Update() : QTask is empty for model : " + model);
                return qscData;
            }

            double xPos = (position.x == 0.0f) ? qtask.position.x : position.x;
            double yPos = (position.y == 0.0f) ? qtask.position.y : position.y;
            double zPos = (position.z == 0.0f) ? qtask.position.z : position.z;
            float alpha = (orientation.alpha == 0.0f) ? qtask.orientation.alpha : orientation.alpha;
            float beta = (orientation.beta == 0.0f) ? qtask.orientation.beta : orientation.beta;
            float gamma = (orientation.gamma == 0.0f) ? qtask.orientation.gamma : orientation.gamma;

            QUtils.AddLog("Update() called with updateType  : " + (updateType == (int)QTASKINFO.QTASK_MODEL ? "MODEL" : "ID") + " position : X:" + position.x + " Y: " + position.y + " Z: " + position.z + "\t Alpha : " + orientation.alpha + ",Beta : " + orientation.beta + ",Gamma : " + orientation.gamma);
            QUtils.AddLog("Update() called changed updateType  : " + (updateType == (int)QTASKINFO.QTASK_MODEL ? "MODEL" : "ID") + " position : X:" + xPos + " Y: " + yPos + " Z: " + zPos + "\t Alpha : " + alpha + ",Beta : " + beta + ",Gamma : " + gamma);
            string taskIdStr = "Task_New(" + Convert.ToString(qtask.id);
            QUtils.AddLog("Update() finding task id : " + taskIdStr);

            int qtaskIndex = -1;

            //Find task by name if id is not found.
            if (qtask.id == -1)
            {
                QUtils.AddLog("Update() Finding task by Name for RigidObj");
                var qscLines = qscData.Split('\n');
                foreach (var qscLine in qscLines)
                {
                    if (qscLine.Contains(QUtils.taskNew))
                    {
                        if (qscLine.Contains(qtask.model) && qscLine.Contains("EditRigidObj"))
                        {
                            QUtils.AddLog("Update() TaskByName " + model + " : qscLines : " + qscLine);
                            qtaskIndex = qscData.IndexOf(qscLine);
                            isRigidObj = true;
                            break;
                        }
                    }
                }
                if (qtaskIndex == -1)
                    return qscData;
            }

            if (qtask.id != -1)
            {
                QUtils.AddLog("Update() Finding task by Name for Building");
                qtaskIndex = qscData.IndexOf(taskIdStr);
            }

            int newlineIndex = qscData.IndexOf("\n", qtaskIndex);

            string endlineTerminator = "";
            string qscSub = qscData.Slice(qtaskIndex, newlineIndex);
            int terminatorCount = qscSub.Count(o => o == ')');

            //Get the endline terminator.
            if (terminatorCount > 0)
            {
                QUtils.AddLog("Update() terminator count = " + terminatorCount + " for model : " + model);
                for (int i = 1; i <= terminatorCount; ++i)
                    endlineTerminator += ")";

                if (qscSub.Contains(";"))
                    endlineTerminator += ";";
                else
                    endlineTerminator += ",";
            }
            else
            {
                if (qscSub.Contains(";"))
                    endlineTerminator = ";";
                else
                    endlineTerminator = ",";
            }

            QUtils.AddLog("Update() Found index : " + qtaskIndex);

            var modelName = qtask.note.Length < 3 ? FindModelName(qtask.model) : qtask.note;
            modelName = modelName.Replace("\"", String.Empty);

            string objectTask = "Task_New(" + qtask.id + "," + qtask.name + ", \"" + modelName + "\"," + xPos + "," + yPos + "," + zPos + ", " + alpha + "," + beta + "," + gamma + "," + qtask.model + (isRigidObj ? angularAmbientEffect : "") + endlineTerminator;
            qscData = qscData.Remove(qtaskIndex, newlineIndex - qtaskIndex).Insert(qtaskIndex, objectTask);

            return qscData;
        }



        internal static bool HasMultiObjects(string qscData, string model)
        {
            int startTokenCount = 0, endTokenCount = 0;
            bool startRun = true, hasMultiObjs = false;

            //Remove all whitespaces.
            qscData = qscData.Replace("\t", String.Empty);
            if (!String.IsNullOrEmpty(model))
            {
                if (!model.Contains("\""))
                    model = "\"" + model + "\"";
            }

            string[] qscDataSplit = qscData.Split('\n');

            foreach (var data in qscDataSplit)
            {
                if (data.Contains(QUtils.taskNew))
                {
                    if (data.Contains(model))
                    {
                        if (data.Contains('(') && !data.Contains(')') && startRun)
                        {
                            startTokenCount++;
                            startRun = false;
                        }
                    }
                    else if (!startRun)
                    {
                        if (data.Contains('('))
                        {
                            startTokenCount += data.Count(o => o == '(');
                        }

                        if (data.Contains(')'))
                        {
                            endTokenCount += data.Count(o => o == ')');
                        }

                        if (startTokenCount == endTokenCount)
                        {
                            break;
                        }

                    }
                }
            }
            QUtils.AddLog("HasMultiObjects For object : " + model + " Start : " + startTokenCount + " End : " + endTokenCount);
            hasMultiObjs = (startTokenCount == endTokenCount) && startTokenCount >= 2;
            return hasMultiObjs;
        }

        internal static string FindModelName(string modelId)
        {
            string modelName = "UNKNOWN_OBJECT";

            if (modelId.Contains("\""))
                modelId = modelId.Replace("\"", String.Empty);

            if (File.Exists(QUtils.objectsModelsList))
            {
                var masterobjList = QCryptor.Decrypt(QUtils.objectsModelsList);
                var objList = masterobjList.Split('\n');
                QUtils.AddLog("FindModelName() called with id : \"" + modelId + "\"");

                foreach (var obj in objList)
                {
                    if (obj.Contains(modelId))
                    {
                        modelName = obj.Split('=')[0];
                        if (modelName.Length < 3 || String.IsNullOrEmpty(modelName))
                        {
                            QUtils.AddLog("FindModelName() couldn't find model name for Model id : " + modelId);
                            return modelName;
                        }
                    }
                }

                if (modelName.Length > 3 && !String.IsNullOrEmpty(modelName))
                    QUtils.AddLog("FindModelName() Found model name " + modelName + " for id : " + modelId);
            }
            return modelName;
        }

        internal static string GetModelName(string modelId, bool fullQtaskList = false, bool masterModel = false)
        {
            string modelName = String.Empty;
            var qtaskList = QUtils.GetQTaskList(fullQtaskList, false, true);

            foreach (var qtask in qtaskList)
            {
                if (qtask.model.Contains(modelId))
                {
                    if (masterModel) modelName = FindModelName(modelId);
                    else modelName = (qtask.note.Length < 3) ? FindModelName(modelId) : qtask.note;
                    QUtils.AddLog("GetModelName() found model name : " + modelName + " for model : " + modelId);
                    break;
                }
            }
            if (!String.IsNullOrEmpty(modelName))
                modelName = modelName.Replace("\"", String.Empty).ToUpperInvariant();
            return modelName;
        }

        internal static string FixErrors(string qscData, string model = "", bool multiObj = false)
        {
            string staticRegex = @"[A-T]{1}[a-s]{3}_[A-N]{1}[a-w]{2}\(-{1,}\d*,\s*" + "x" + "[A-S]{1}[a-t]*" + "x" + @"\s*,\s*" + "xx" + @"\s*,\s{1,}\n";
            staticRegex = staticRegex.Replace('x', '"');
            var regexSub = staticRegex.Substring(0, 82);
            string commaTokenRegex = @",\s*\n\),";

            qscData = Regex.Replace(qscData, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
            qscData = Regex.Replace(qscData, @""",\s\){1,},", @"""),", RegexOptions.Multiline);
            qscData = Regex.Replace(qscData, @""",\s\){1,},", @"""),", RegexOptions.Multiline);

            string elevator = "\n" + "\"" + "elvdoorClose" + "\"," + " \"" + "elvdoorMove" + "\"),\n";

            if (qscData.Contains(elevator))
            {
                //qscData = qscData.Replace(elevator, String.Empty);
                QUtils.AddLog("FixErrors()  Token elevator fixed");
            }

            if (qscData.Contains(",)") || qscData.Contains(",) "))
            {
                qscData = qscData.Replace(",)", ")");
                QUtils.AddLog("FixErrors() Token Comma fixed");
            }

            if (qscData.Contains(",,"))
            {
                qscData = qscData.Replace(",,", ",");
                QUtils.AddLog("FixErrors() Token Quote fixed");
            }

            if (Regex.Match(qscData, regexSub).Success && multiObj)
            {
                //qscData = Regex.Replace(qscData, staticRegex, string.Empty, RegexOptions.Multiline);
                QUtils.AddLog("FixErrors() Static block found with error expected");
            }

            if (Regex.Match(qscData, commaTokenRegex).Success && multiObj)
            {
                qscData = Regex.Replace(qscData, commaTokenRegex, @"),", RegexOptions.Multiline);
                QUtils.AddLog("FixErrors() Comma block found with error expected");
            }

            if (Regex.Match(qscData, "^,\\s", RegexOptions.Multiline).Success)
            {
                qscData = Regex.Replace(qscData, "^,\\s\\r\\n", String.Empty, RegexOptions.Multiline);
                QUtils.AddLog("FixErrors() Multi commas fixed");
            }

            if (!String.IsNullOrEmpty(model))
            {
                string staticTask = "Task_New(-1, \"Static\", \"\",";
                int modelIndex = qscData.IndexOf(model);
                int nextlineIndex = qscData.IndexOf('\n', modelIndex + model.Length + 0x3);

                if (qscData.Contains(model))
                {
                    var qscTemp = qscData.Substring(nextlineIndex, staticTask.Length);
                    QUtils.AddLog("QscTemp object : " + qscTemp);

                    if (qscTemp.Contains("Static"))
                    {
                        QUtils.AddLog(model + " Contains static objects");
                        qscData = qscData.Remove(nextlineIndex, staticTask.Length).Insert(nextlineIndex - 1, "),");
                    }
                    else
                    {
                        int endlineIndex = qscData.IndexOf('\n', modelIndex);
                        qscData = qscData.Insert(modelIndex + model.Length + 1, ")");
                    }
                }

            }

            return qscData;
        }

        internal static string GetModelName(int id)
        {
            string modelName = null;
            var qtaskList = QUtils.GetQTaskList(false, true);

            foreach (var qtask in qtaskList)
            {
                if (qtask.id == id)
                {
                    modelName = String.IsNullOrEmpty(qtask.note) ? "UnknownModel" : qtask.note;
                    break;
                }
            }
            return modelName;
        }


        internal static string ReplaceAllObjects(string qscData, string newModel, string ligthmap = "")
        {
            var qtaskList = QUtils.GetQTaskList();
            foreach (var qtask in qtaskList)
            {
                if (qtask.name.Contains("Building") || qtask.name.Contains("EditRigidObj") ||
                qtask.name.Contains("Terminal") || qtask.name.Contains("Elevator")
                || qtask.name.Contains("Fence") || qtask.name.Contains("Door"))
                {
                    qscData = ReplaceObject(qscData, qtask.model, newModel, ligthmap);
                }
            }
            return qscData;
        }

        internal static string ReplaceObject(string qscData, string oldModel, string newModel, string ligthmap = "")
        {
            if (!String.IsNullOrEmpty(oldModel) && !String.IsNullOrEmpty(newModel))
            {
                if (!oldModel.Contains("\""))
                    oldModel = "\"" + oldModel + "\"";
                if (!newModel.Contains("\""))
                    newModel = "\"" + newModel + "\"";
            }

            if (newModel.Length > oldModel.Length)
            {
                Console.WriteLine("Input models length error");
                Console.WriteLine("Length : " + newModel.Length);
                return qscData;
            }

            //Replace all objects.
            qscData = qscData.Replace(oldModel, newModel);

            if (!String.IsNullOrEmpty(ligthmap))
                qscData = qscData.Replace(ligthmap, ligthmap);
            return qscData;
        }

        internal static string RemoveObject(string qscData, string model, bool fixErrors = true, bool checkModel = false)
        {
            if (checkModel)
            {
                bool modelExist = QUtils.CheckModelExist(model);

                if (!modelExist)
                {
                    QUtils.ShowError("Model " + model + " does not exist in current level");
                    QUtils.AddLog("Model " + model + " does not exist in current level");
                    return null;
                }
            }

            bool hasMultiObjects = HasMultiObjects(qscData, model);

            QUtils.AddLog(hasMultiObjects ? "Model " + model + " has multi objects" : "Model " + model + " doesn't have multi objects");

            if (hasMultiObjects)
                //qscData = RemoveMultiObjects(qscData, model, !fixErrors);
                qscData = RemoveWholeObject(qscData, model, true, checkModel);
            else
                qscData = RemoveAllRigidObjects(qscData, model);

            //Remove syntax error if any , too lazy to fix them manually :|
            if (fixErrors && !hasMultiObjects)
            {
                qscData = FixErrors(qscData, model, hasMultiObjects);
            }

            if (hasMultiObjects)
                qscData = RemoveAllRigidObjects(qscData, model);

            return qscData;
        }

        internal static string RemoveAllRigidObjects(string qscData, string model)
        {
            //Remove all whitespaces.
            qscData = qscData.Replace("\t", String.Empty);
            if (!String.IsNullOrEmpty(model))
            {
                if (!model.Contains("\""))
                    model = "\"" + model + "\"";
            }

            if (model.Length < 3)
            {
                //QCompiler.ShowError("Trying to remove empty model");
                QUtils.AddLog("RemoveSingleObject : Trying to remove empty model : '" + model + "'");
                return qscData;
            }

            string[] qscDataSplit = qscData.Split('\n');

            foreach (var data in qscDataSplit)
            {
                if (data.Contains(QUtils.taskNew))
                {
                    if (data.Contains(model))
                    {
                        //For single objects.
                        if (data.Contains('(') && data.Contains(')'))
                        {
                            int count = data.Count(o => o == ')');
                            if (count == 1)
                            {
                                qscData = qscData.Replace(data, String.Empty);
                            }
                            else
                            {
                                var innerData = data.Slice(0, data.IndexOf(')') + 1);
                                qscData = qscData.Replace(innerData, String.Empty);
                            }
                            QUtils.AddLog("RemoveAllRigidObjects : Removed " + GetModelName(model) + " with id : '" + model + "' for single object");

                        }
                        else
                        {
                            QUtils.AddLog("RemoveAllRigidObjects : Couldn't remove " + GetModelName(model) + " with id : '" + model + "' for single object");
                        }
                    }
                }
            }
            return qscData;
        }

        internal static string RemoveWholeObject(string qscData, string model, bool fixErrors = true, bool checkModel = false)
        {
            int startIndex = 0, endIndex = 0;
            bool startRun = true;

            if (checkModel)
            {
                bool modelExist = QUtils.CheckModelExist(model);

                if (!modelExist)
                {
                    QUtils.ShowError("Model " + model + " does not exist in current level");
                    QUtils.AddLog("Model " + model + " does not exist in current level");
                }
            }

            //Remove all whitespaces.
            qscData = qscData.Replace("\t", String.Empty);
            string qscDataBack = String.Copy(qscData);

            if (!String.IsNullOrEmpty(model))
            {
                if (!model.Contains("\""))
                    model = "\"" + model + "\"";
            }

            string[] qscDataSplit = qscData.Split('\n');

            foreach (var data in qscDataSplit)
            {
                if (data.Contains(QUtils.taskNew))
                {
                    if (data.Contains(model))
                    {
                        //For single objects.
                        if (data.Contains('(') && !data.Contains(')') && startRun)
                        {
                            startIndex = qscData.IndexOf(data);
                            endIndex += data.Length;
                            startRun = false;
                        }
                    }
                    else if (!startRun)
                    {
                        if (data.Contains(QUtils.taskNew) && data.Contains("Building"))
                        {
                            endIndex = qscData.IndexOf("Building", startIndex + endIndex);
                            QUtils.AddLog("RemoveWholeObjects : start index : " + startIndex + " end index : " + endIndex);

                            var buildingSub = qscData.Slice(startIndex, endIndex);
                            buildingSub = buildingSub.Remove(buildingSub.LastIndexOf(Environment.NewLine));

                            qscData = qscData.Replace(buildingSub, String.Empty);
                            QUtils.AddLog("RemoveWholeObjects : Removed " + GetModelName(model) + " with id : '" + model + "' for multiple object");
                            break;
                        }
                    }
                }
            }

            return qscData;
        }


        internal static string RemoveMultiObjects(string qscData, string model, bool fixErrors = true)
        {
            int startTokenCount = 0, endTokenCount = 0;
            bool startRun = true;

            //Remove all whitespaces.
            qscData = qscData.Replace("\t", String.Empty);
            string qscDataBack = String.Copy(qscData);
            string unmatchedNewlineRegex = @",\s\r\n\),\s\r\n", matchNewlineRegex = ")," + "\n";

            if (!String.IsNullOrEmpty(model))
            {
                if (!model.Contains("\""))
                    model = "\"" + model + "\"";
            }

            string[] qscDataSplit = qscData.Split('\n');

            foreach (var data in qscDataSplit)
            {
                if (data.Contains(QUtils.taskNew))
                {
                    if (data.Contains(model))
                    {
                        //For single objects.
                        if (data.Contains('(') && !data.Contains(')') && startRun)
                        {
                            startTokenCount++;
                            startRun = false;
                        }
                    }
                    else if (!startRun)
                    {
                        if (data.Contains('('))
                            startTokenCount += data.Count(o => o == '(');

                        if (data.Contains(')'))
                            endTokenCount += data.Count(o => o == ')');

                        //For nested objects.
                        if (data.Contains('(') || data.Contains(')'))
                        {
                            int count = data.Count(o => o == ')');

                            if (count == 0)
                            {
                                var innerData = data.Slice(0, data.IndexOf('\n'));
                                int taskCount = new Regex(Regex.Escape(data)).Matches(qscData).Count;

                                if (taskCount == 1)
                                {
                                    qscData = qscData.Replace(innerData, String.Empty);
                                }
                                else
                                {
                                    QUtils.AddLog("RemoveMultiObjects : Task count is greater than expected");
                                }

                            }

                            else if (count == 1)
                            {
                                qscData = qscData.Replace(data, String.Empty);
                            }
                            else
                            {
                                //var innerData = data.Slice(0, data.IndexOf(')') + 1 + count);
                                var innerData = data.Slice(0, data.LastIndexOf(')') + 1);
                                qscData = qscData.Replace(innerData, String.Empty);
                                qscData = Regex.Replace(qscData, unmatchedNewlineRegex, matchNewlineRegex);
                            }
                        }

                        if (startTokenCount == endTokenCount)
                        {
                            QUtils.AddLog("RemoveMultiObjects : Model " + FindModelName(model) + " with id : '" + model + "'Removed with Objects : " + startTokenCount);
                            break;
                        }
                    }
                }
            }

            if (startTokenCount != endTokenCount)
            {
                QUtils.AddLog("RemoveMultiObjects : Couldn't remove " + FindModelName(model) + " with id : '" + model + "' for multiple object");
            }

            if (fixErrors)
                qscData = FixErrors(qscData, model);
            return qscData;
        }

        internal static string RemoveAllObjects(string qscData, bool buildings = false, bool rigidObjects = false, int count = -1, bool hasRigid = false, bool wholeObj = true)
        {
            var qtaskList = QUtils.GetQTaskList(true);
            int buildingsCount = qtaskList.Count(o => o.name.Contains("Building"));
            int rigidObjCount = qtaskList.Count(o => o.name.Contains("EditRigidObj"));

            int bCount = 0, rCount = 0;

            if (count == -1 && buildings)
                count = buildingsCount - 1;

            else if (count == -1 && rigidObjects)
                count = rigidObjCount - 1;

            foreach (var qtask in qtaskList)
            {
                if (qtask.name == "\"Building\"" && buildings)
                {
                    if (bCount < count && count < buildingsCount)
                    {
                        if (wholeObj)
                            qscData = RemoveWholeObject(qscData, qtask.model, true);
                        else
                            qscData = RemoveAllRigidObjects(qscData, qtask.model);
                    }
                    else
                    {
                        break;
                    }

                    bCount++;
                }

                if (qtask.name == "\"EditRigidObj\"" && rigidObjects)
                {
                    if (rCount < count && count < rigidObjCount)
                    {
                        if (wholeObj)
                            qscData = RemoveWholeObject(qscData, qtask.model, true);
                        else
                            qscData = RemoveObject(qscData, qtask.model, true);
                    }
                    else
                    {
                        break;
                    }
                    rCount++;
                }

                if (!buildings && !rigidObjects)
                {
                    qscData = RemoveObject(qscData, qtask.model, false);
                }
            }

            if (hasRigid)
                qscData = QObjects.RemoveRigidObjects(qscData, -1);
            return qscData;
        }

        internal static string RemoveRigidObjects(string qscData, int count = -1)
        {
            var qtaskList = QUtils.GetQTaskList(true);
            int objCount = 0;
            int rigidObjCount = qtaskList.Count(o => o.name.Contains("EditRigidObj"));
            if (count == -1) count = rigidObjCount - 1;

            foreach (var model in qtaskList)
            {

                if (model.name.Contains("EditRigidObj"))
                {
                    if (objCount < count && count < rigidObjCount)
                        qscData = RemoveAllRigidObjects(qscData, model.model);
                    objCount++;
                }
            }
            qscData = FixErrors(qscData);
            return qscData;
        }

        internal static string SetAllAreaActivated(AreaDim areaDim, string areaType, int areaCount, float statusDuration = 8.0f, bool isCutscene = false)
        {
            var qtaskList = QUtils.GetQTaskList(false, false, true);
            var qtaskGraphList = QGraphs.GetQTaskGraphList(true, true);

            var qscData = QUtils.LoadFile();
            QUtils.qtaskId = QUtils.GenerateTaskID(true);
            if (areaType != QUtils.aiGraphTask)
                areaType = "\"" + areaType + "\"";

            QUtils.AddLog("SetAllAreaActivated called with dim : " + areaDim.x + " area type : " + areaType + " area count : " + areaCount + " duration : " + statusDuration + " is cutscene : " + isCutscene);

            if (areaCount > (areaType == QUtils.aiGraphTask ? qtaskGraphList.Count : qtaskList.Count))
            {
                QUtils.ShowError("Area count should be less than total objects");
                return null;
            }

            if (areaCount == -1) areaCount = (areaType == QUtils.aiGraphTask ? qtaskGraphList.Count : qtaskList.Count) - 1;
            int count = 0;

            if (areaType == QUtils.aiGraphTask)
            {
                foreach (var qtask in qtaskGraphList)
                {
                    if (count > areaCount) break;
                    var graphName = qtask.name.Replace("\"", String.Empty) + "_" + qtask.id;
                    qscData += AddAreaActivate(qtask.id, null, graphName, qtask.note, ref qtask.position, ref areaDim, statusDuration, isCutscene);
                    QUtils.qtaskId++;
                    count++;
                }
            }
            else
            {
                foreach (var qtask in qtaskList)
                {
                    if (count > areaCount) break;

                    if (qtask.name == areaType)
                    {
                        qscData += AddAreaActivate(qtask.id, qtask.model, qtask.note, qtask.note, ref qtask.position, ref areaDim, statusDuration, isCutscene);
                        QUtils.qtaskId++;
                        count++;
                    }
                }
            }
            return qscData;
        }

        internal static string AddAreaActivate(int taskId, string model, string modelName, string taskNote, ref Real64 position, ref AreaDim areaDim, float statusDuration = 5.0f, bool isCutscene = false)
        {
            var areaId = QUtils.qtaskId;
            var statusId = areaId + 1;
            var taskNoteStr = taskNote.Replace("\"", String.Empty).ToUpperInvariant();

            QUtils.AddLog("AddAreaActivate taskId : " + taskId + " model : " + model + " modelName : " + modelName + " taskNote : " + taskNote + " X : " + position.x + " Y : " + position.y + " Z : " + position.z + " dim : " + areaDim.x + " duration : " + statusDuration + " is cutscene : " + isCutscene);

            if (!String.IsNullOrEmpty(model))
                modelName = model.Replace("\"", String.Empty);

            if (taskNoteStr.Length < 3 || modelName.Length < 3)
                taskNoteStr = GetModelName(model);

            string taskComment = "\n//" + taskNoteStr + " Area" + "\n";
            string areaTask = "Task_New(" + QUtils.qtaskId++ + ",\"AreaActivate\"," + taskNote + "," + position.x + "," + position.y + "," + position.z + ",0,0,0," + areaDim.x + "," + areaDim.y + "," + areaDim.z + ",\"CRITERIA_HUMAN0\");" + "\n";

            //string statusMsgTask = "Task_New(" + -1 + ",\"StatusMessage\"," + taskNote + ",0,0,0,0,0,0,\"AreaActivate_" + areaId + ".nActive\",\"" + taskNoteStr + " " + modelName + " ID : " + taskId + " Pos : X : " + position.x + " Y: " + position.y + " Z: " + position.z + "\"," + "\"\", \"message\",FALSE," + isCutscene.ToString().ToUpperInvariant() + "," + statusDuration + ");" + "\n";
            string statusMsgTask = "Task_New(" + -1 + ",\"StatusMessage\"," + taskNote + ",0,0,0,0,0,0,\"AreaActivate_" + areaId + ".nActive\",\"" + taskNoteStr + "\"," + "\"\", \"message\",FALSE," + isCutscene.ToString().ToUpperInvariant() + "," + statusDuration + ");" + "\n";

            var qscData = taskComment + areaTask + statusMsgTask;
            return qscData;
        }

        internal static Real64 GetObjectPosition(string model)
        {
            Real64 objectPos = new Real64();

            if (String.IsNullOrEmpty(model))
                throw new ArgumentNullException();

            var qtaskList = QUtils.GetQTaskList();
            model = "\"" + model + "\"";

            foreach (var qtask in qtaskList)
            {
                if (qtask.model == model)
                {
                    QUtils.AddLog("GetObjectPosition() : Model found :  " + model + " with position : X :" + qtask.position.x + " Y: " + qtask.position.y + " Z: " + qtask.position.z);
                    objectPos = qtask.position;
                    break;
                }
            }
            return objectPos;
        }

        internal static Real32 GetObjectOrientation(string model)
        {
            Real32 objectOrientation = new Real32();

            if (String.IsNullOrEmpty(model))
                throw new ArgumentNullException();

            var qtaskList = QUtils.GetQTaskList();
            model = "\"" + model + "\"";

            foreach (var qtask in qtaskList)
            {
                if (qtask.model == model)
                {
                    QUtils.AddLog("GetObjectOrientation() : Model found :  " + model + " with position : X :" + qtask.position.x + " Y: " + qtask.position.y + " Z: " + qtask.position.z);
                    objectOrientation = qtask.orientation;
                    break;
                }
            }
            return objectOrientation;
        }

        internal static List<Dictionary<string, string>> GetObjectList(int level, QTYPES objType, bool distinct = false, bool fromBackup = false)
        {
            QUtils.AddLog("GetObjectList() called with level : " + level + " With type : " + ((objType == QTYPES.BUILDING) ? "Buildings" : "3D Rigid objects" + " fromBackup : " + fromBackup));
            var qtaskList = QUtils.GetQTaskList(level, false, distinct, fromBackup);
            QUtils.AddLog("GetObjectList() qtaskList count : " + qtaskList.Count);
            var objList = new List<Dictionary<string, string>>();
            string modelName = null;

            foreach (var qtask in qtaskList)
            {
                if (objType == QTYPES.BUILDING)
                {
                    if (qtask.name.Contains("Building"))
                    {
                        var obj = new Dictionary<string, string>();

                        //Find model name if not found.
                        modelName = FindModelName(qtask.model);

                        if (modelName.Length > 3)
                        {
                            obj.Add(modelName, qtask.model.Replace("\"", String.Empty).ToUpperInvariant());
                            objList.Add(obj);
                            QUtils.AddLog("Building : Model " + modelName + " ID : " + qtask.model);
                        }
                    }
                }
                else if (objType == QTYPES.RIGID_OBJ)
                {
                    if (qtask.name.Contains("EditRigidObj"))
                    {
                        var obj = new Dictionary<string, string>();
                        //Find model name if not found.
                        modelName = FindModelName(qtask.model);

                        if (modelName.Length > 3)
                        {
                            obj.Add(modelName, qtask.model.Replace("\"", String.Empty).ToUpperInvariant());
                            objList.Add(obj);
                            QUtils.AddLog("EditRigidObj : Model " + modelName + " ID : " + qtask.model);
                        }
                    }
                }
            }
            objList = objList.OrderBy(key => key.Keys.ElementAt(0)).ToList();

            QUtils.AddLog("GetObjectList() objList count : " + objList.Count);
            return objList;
        }

    }
}
