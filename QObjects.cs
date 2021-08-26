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
        internal static string AddRigidObj(string model, Real64 position, bool check_model = false)
        {
            if (check_model)
            {
                bool model_exist = QUtils.CheckModelExist(model);

                if (!model_exist)
                {
                    QUtils.ShowError("Model " + model + " does not exist in current level");
                    QUtils.AddLog("Model " + model + " does not exist in current level");
                    return null;
                }
            }
            return RigidObj(-1, "", position.x, position.y, position.z, 0, 0, 0, model);
        }

        internal static string AddRigidObj(string model, Real64 position, Real32 orientation, bool check_model)
        {
            if (check_model)
            {
                bool model_exist = QUtils.CheckModelExist(model);

                if (!model_exist)
                {
                    QUtils.AddLog("Model " + model + " does not exist in current level");
                    QUtils.ShowError("Model " + model + " does not exist in current level");
                }
            }
            return RigidObj(-1, "", position.x, position.y, position.z, orientation.alpha, orientation.beta, orientation.gamma, model);
        }

        internal static string RigidObj(int task_id = -1, string task_note = "", double x = 0.0f, double y = 0.0f, double z = 0.0f, float alpha = 0.0f, float beta = 0.0f, float gamma = 0.0f, string model = "")
        {
            QUtils.qtaskObjId = -1;
            string qtask_building = "Task_New(" + QUtils.qtaskObjId + ",\"EditRigidObj\",\"" + task_note + "\"," + x + "," + y + "," + z + "," + alpha + "," + beta + "," + gamma + ",\"" + model + "\"" + ",1" + ",1" + ",1" + ",0" + ",0" + ",0" + ");" + "\n";
            QUtils.AddLog("RigidObj called with ID : " + QUtils.qtaskObjId + "  RigidObj : " + GetModelName(model) + "\"\tX : " + x + " Y : " + y + " Z : " + z + "\tAlpha : " + alpha + " Beta : " + beta + ",Gamma : " + gamma + " Model : " + model + "\n");
            return qtask_building;
        }

        internal static string UpdatePositionNoOffset(int id, ref Real64 position, bool check_model = false)
        {
            QUtils.QTask qtask = null;
            qtask = QUtils.GetQTask(id);

            double x_pos = (position.x == 0.0f) ? qtask.position.x : position.x;
            double y_pos = (position.y == 0.0f) ? qtask.position.y : position.y;
            double z_pos = (position.z == 0.0f) ? qtask.position.z : position.z;

            bool x_val = (x_pos == 0.0f) ? false : true;
            bool y_val = (y_pos == 0.0f) ? false : true;
            bool z_val = (z_pos == 0.0f) ? false : true;

            int x_len = x_val ? x_pos.ToString().Length : 0;
            int y_len = y_val ? y_pos.ToString().Length : 0;
            int z_len = z_val ? z_pos.ToString().Length : 0;

            QUtils.AddLog("UpdatePositionNoOffset()  length : X:" + x_len + " Y: " + y_len + " Z: " + z_len);
            QUtils.AddLog("UpdatePositionNoOffset() called with offset : X:" + position.x + " Y: " + position.y + " Z: " + position.z);


            //Check for length error.
            if (x_len > 3 || y_len > 3 || z_len > 3)
                throw new ArgumentOutOfRangeException("Offsets are out of range");

            int[] meter_offsets = { 100000, 1000000, 10000000 };

            //Add meter offset to distance. (M/S) .
            if (x_val) x_pos = x_pos + meter_offsets[x_len - 1];
            if (y_val) y_pos = y_pos + meter_offsets[y_len - 1];
            if (z_val) z_pos = z_pos + meter_offsets[z_len - 1];

            return UpdatePosition(id, ref position, check_model);
        }

        internal static string UpdatePositionNoOffset(string model, ref Real64 offsets, bool check_model = false)
        {
            QUtils.QTask qtask = null;
            qtask = QUtils.GetQTask(model);

            double x_pos = (offsets.x == 0.0f) ? 0 : offsets.x;
            double y_pos = (offsets.y == 0.0f) ? 0 : offsets.y;
            double z_pos = (offsets.z == 0.0f) ? 0 : offsets.z;

            bool x_val = (x_pos == 0.0f) ? false : true;
            bool y_val = (y_pos == 0.0f) ? false : true;
            bool z_val = (z_pos == 0.0f) ? false : true;

            int x_len = x_val ? x_pos.ToString().Length : 0;
            int y_len = y_val ? y_pos.ToString().Length : 0;
            int z_len = z_val ? z_pos.ToString().Length : 0;

            QUtils.AddLog("UpdatePositionNoOffset()  length : X:" + x_len + " Y: " + y_len + " Z: " + z_len);
            QUtils.AddLog("UpdatePositionNoOffset() called with offset : X:" + offsets.x + " Y: " + offsets.y + " Z: " + offsets.z);


            //Check for length error.
            if (x_len > 3 || y_len > 3 || z_len > 3)
                throw new ArgumentOutOfRangeException("Offsets are out of range");

            int[] meter_offsets = { 100000, 1000000, 10000000 };

            //Add meter offset to distance. (M/S) .
            if (x_val) x_pos = qtask.position.x + meter_offsets[x_len - 1];
            if (y_val) y_pos = qtask.position.y + meter_offsets[y_len - 1];
            if (z_val) z_pos = qtask.position.z + meter_offsets[z_len - 1];

            Real64 position = new Real64(x_pos, y_pos, z_pos);
            return UpdatePosition(model, ref position, check_model);
        }

        internal static string UpdatePosition(int task_id, ref Real64 position, bool check_model)
        {
            var r32 = new Real32();
            return Update(task_id, null, ref position, ref r32, (int)QTASKINFO.QTASK_ID, check_model);
        }

        internal static string UpdatePosition(string model, ref Real64 position, bool check_model = false)
        {
            var r32 = new Real32();
            return Update(-999, model, ref position, ref r32, (int)QTASKINFO.QTASK_MODEL, check_model);
        }

        internal static string UpdateOrientation(int task_id, ref Real32 orientation, bool check_model = false)
        {
            var r64 = new Real64();
            return Update(task_id, null, ref r64, ref orientation, (int)QTASKINFO.QTASK_ID, check_model);
        }


        internal static string UpdateOrientation(string model, ref Real32 orientation, bool check_model = false)
        {
            var r64 = new Real64();
            return Update(-999, model, ref r64, ref orientation, (int)QTASKINFO.QTASK_MODEL, check_model);
        }


        internal static string UpdateOrientationAll(int update_type, ref Real32 orientation)
        {
            string qsc_data = null;
            var qlist = QUtils.GetQTaskList(true);
            foreach (var qtask in qlist)
            {
                if (qtask.name == "\"Building\"" && update_type == (int)QTASKINFO.QTASK_MODEL)
                {
                    QUtils.AddLog("Building " + GetModelName(qtask.id) + " compatible for orientation");
                    qsc_data = UpdateOrientation(qtask.model, ref orientation);
                }
                else if (qtask.name == "\"EditRigidObj\"" && update_type != (int)QTASKINFO.QTASK_MODEL)
                {
                    QUtils.AddLog("3D Object " + GetModelName(qtask.id) + " compatible for orientation");
                    qsc_data = UpdateOrientation(qtask.model, ref orientation);
                }
                if (!String.IsNullOrEmpty(qsc_data))
                    QUtils.SaveFile(qsc_data);
            }
            return qsc_data;
        }



        //Update the object.
        internal static string Update(int id, string model, ref Real64 position, ref Real32 orientation, int update_type, bool check_model = false)
        {
            string qsc_data = QUtils.LoadFile();

            //Remove all whitespaces.
            qsc_data = qsc_data.Replace("\t", String.Empty);
            string[] qsc_data_split = qsc_data.Split('\n');

            if (check_model)
            {
                bool model_exist = QUtils.CheckModelExist(model);
                if (!model_exist)
                {
                    QUtils.ShowError("Model " + model + " does not exist in current level");
                    QUtils.AddLog("Model " + model + " does not exist in current level");
                    return null;
                }
            }

            qsc_data = Update(qsc_data, id, model, ref position, ref orientation, update_type);
            return qsc_data;
        }

        //Update the object via general method.
        private static string Update(string qsc_data, int id, string model, ref Real64 position, ref Real32 orientation, int update_type)
        {
            const string angular_ambient_effect = ",1, 1, 1, 0, 0, 0";
            bool isRigidObj = false;

            QUtils.QTask qtask = null;
            if (update_type == (int)QTASKINFO.QTASK_ID)
                qtask = QUtils.GetQTask(id);
            else if (update_type == (int)QTASKINFO.QTASK_MODEL)
                qtask = QUtils.GetQTask(model);

            if (qtask.model.Contains(";")) qtask.model = qtask.model.Replace(";", String.Empty);

            if (qtask == null)
            {
                //QUtils.ShowError("QTask in empty");
                QUtils.AddLog("Update() : QTask is empty for model : " + model);
                return qsc_data;
            }

            double x_pos = (position.x == 0.0f) ? qtask.position.x : position.x;
            double y_pos = (position.y == 0.0f) ? qtask.position.y : position.y;
            double z_pos = (position.z == 0.0f) ? qtask.position.z : position.z;
            float alpha = (orientation.alpha == 0.0f) ? qtask.orientation.alpha : orientation.alpha;
            float beta = (orientation.beta == 0.0f) ? qtask.orientation.beta : orientation.beta;
            float gamma = (orientation.gamma == 0.0f) ? qtask.orientation.gamma : orientation.gamma;

            QUtils.AddLog("Update() called with update_type  : " + (update_type == (int)QTASKINFO.QTASK_MODEL ? "MODEL" : "ID") + " position : X:" + position.x + " Y: " + position.y + " Z: " + position.z + "\t Alpha : " + orientation.alpha + ",Beta : " + orientation.beta + ",Gamma : " + orientation.gamma);
            QUtils.AddLog("Update() called changed update_type  : " + (update_type == (int)QTASKINFO.QTASK_MODEL ? "MODEL" : "ID") + " position : X:" + x_pos + " Y: " + y_pos + " Z: " + z_pos + "\t Alpha : " + alpha + ",Beta : " + beta + ",Gamma : " + gamma);
            string task_id_str = "Task_New(" + Convert.ToString(qtask.id);
            QUtils.AddLog("Update() finding task id : " + task_id_str);

            int qtask_index = -1;

            //Find task by name if id is not found.
            if (qtask.id == -1)
            {
                QUtils.AddLog("Update() Finding task by Name for RigidObj");
                var qsc_lines = qsc_data.Split('\n');
                foreach (var qsc_line in qsc_lines)
                {
                    if (qsc_line.Contains(QUtils.taskNew))
                    {
                        if (qsc_line.Contains(qtask.model) && qsc_line.Contains("EditRigidObj"))
                        {
                            QUtils.AddLog("Update() TaskByName " + model + " : qsc_lines : " + qsc_line);
                            qtask_index = qsc_data.IndexOf(qsc_line);
                            isRigidObj = true;
                            break;
                        }
                    }
                }
                if (qtask_index == -1)
                    return qsc_data;
            }

            if (qtask.id != -1)
            {
                QUtils.AddLog("Update() Finding task by Name for Building");
                qtask_index = qsc_data.IndexOf(task_id_str);
            }

            int newline_index = qsc_data.IndexOf("\n", qtask_index);

            string endline_terminator = "";
            string qsc_sub = qsc_data.Slice(qtask_index, newline_index);
            int terminator_count = qsc_sub.Count(o => o == ')');

            //Get the endline terminator.
            if (terminator_count > 0)
            {
                QUtils.AddLog("Update() terminator count = " + terminator_count + " for model : " + model);
                for (int i = 1; i <= terminator_count; ++i)
                    endline_terminator += ")";

                if (qsc_sub.Contains(";"))
                    endline_terminator += ";";
                else
                    endline_terminator += ",";
            }
            else
            {
                if (qsc_sub.Contains(";"))
                    endline_terminator = ";";
                else
                    endline_terminator = ",";
            }

            QUtils.AddLog("Update() Found index : " + qtask_index);

            var model_name = qtask.note.Length < 3 ? FindModelName(qtask.model) : qtask.note;
            model_name = model_name.Replace("\"", String.Empty);

            string object_task = "Task_New(" + qtask.id + "," + qtask.name + ", \"" + model_name + "\"," + x_pos + "," + y_pos + "," + z_pos + ", " + alpha + "," + beta + "," + gamma + "," + qtask.model + (isRigidObj ? angular_ambient_effect : "") + endline_terminator;
            qsc_data = qsc_data.Remove(qtask_index, newline_index - qtask_index).Insert(qtask_index, object_task);

            return qsc_data;
        }



        internal static bool HasMultiObjects(string qsc_data, string model)
        {
            int start_token_count = 0, end_token_count = 0;
            bool start_run = true, has_multi_objs = false;

            //Remove all whitespaces.
            qsc_data = qsc_data.Replace("\t", String.Empty);
            if (!String.IsNullOrEmpty(model))
            {
                if (!model.Contains("\""))
                    model = "\"" + model + "\"";
            }

            string[] qsc_data_split = qsc_data.Split('\n');

            foreach (var data in qsc_data_split)
            {
                if (data.Contains(QUtils.taskNew))
                {
                    if (data.Contains(model))
                    {
                        if (data.Contains('(') && !data.Contains(')') && start_run)
                        {
                            start_token_count++;
                            start_run = false;
                        }
                    }
                    else if (!start_run)
                    {
                        if (data.Contains('('))
                        {
                            start_token_count += data.Count(o => o == '(');
                        }

                        if (data.Contains(')'))
                        {
                            end_token_count += data.Count(o => o == ')');
                        }

                        if (start_token_count == end_token_count)
                        {
                            break;
                        }

                    }
                }
            }
            QUtils.AddLog("HasMultiObjects For object : " + model + " Start : " + start_token_count + " End : " + end_token_count);
            has_multi_objs = (start_token_count == end_token_count) && start_token_count >= 2;
            return has_multi_objs;
        }

        private static string FindModelName(string modelId)
        {
            string modelName = "UNKNOWN_OBJECT";

            if (modelId.Contains("\""))
                modelId = modelId.Replace("\"", String.Empty);

            if (File.Exists(QUtils.objectsMasterList))
            {
                var masterobjList = QCryptor.Decrypt(QUtils.objectsMasterList);
                var objList = masterobjList.Split('\n');
                QUtils.AddLog("FindModelName() called with id : \"" + modelId + "\"");

                foreach (var obj in objList)
                {
                    if (obj.Contains(modelId))
                    {
                        modelName = obj.Split('=')[0];
                        if (modelName.Length < 3 || String.IsNullOrEmpty(modelName))
                        {
                            QUtils.AddLog("FindModelName() couldn't find model name for id : " + modelId);
                            return modelName;
                        }
                    }
                }

                if (modelName.Length > 3 && !String.IsNullOrEmpty(modelName))
                    QUtils.AddLog("FindModelName() Found model name " + modelName + " for id : " + modelId);
            }
            return modelName;
        }

        internal static string GetModelName(string model, bool full_qtask_list = false, bool master_model = false)
        {
            string model_name = String.Empty;
            var qtask_list = QUtils.GetQTaskList(full_qtask_list, false, true);

            foreach (var qtask in qtask_list)
            {
                if (qtask.model.Contains(model))
                {
                    if (master_model) model_name = FindModelName(model);
                    else model_name = (qtask.note.Length < 3) ? FindModelName(model) : qtask.note;
                    QUtils.AddLog("GetModelName() found model name : " + model_name + " for model : " + model);
                    break;
                }
            }
            if (!String.IsNullOrEmpty(model_name))
                model_name = model_name.Replace("\"", String.Empty).ToUpperInvariant();
            return model_name;
        }

        internal static string FixErrors(string qsc_data, string model = "", bool multi_obj = false)
        {
            string static_regex = @"[A-T]{1}[a-s]{3}_[A-N]{1}[a-w]{2}\(-{1,}\d*,\s*" + "x" + "[A-S]{1}[a-t]*" + "x" + @"\s*,\s*" + "xx" + @"\s*,\s{1,}\n";
            static_regex = static_regex.Replace('x', '"');
            var regex_sub = static_regex.Substring(0, 82);
            string comma_token_regex = @",\s*\n\),";

            qsc_data = Regex.Replace(qsc_data, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
            qsc_data = Regex.Replace(qsc_data, @""",\s\){1,},", @"""),", RegexOptions.Multiline);
            qsc_data = Regex.Replace(qsc_data, @""",\s\){1,},", @"""),", RegexOptions.Multiline);

            string elevator = "\n" + "\"" + "elvdoor_close" + "\"," + " \"" + "elvdoor_move" + "\"),\n";

            if (qsc_data.Contains(elevator))
            {
                //qsc_data = qsc_data.Replace(elevator, String.Empty);
                QUtils.AddLog("FixErrors()  Token elevator fixed");
            }

            if (qsc_data.Contains(",)") || qsc_data.Contains(",) "))
            {
                qsc_data = qsc_data.Replace(",)", ")");
                QUtils.AddLog("FixErrors() Token Comma fixed");
            }

            if (qsc_data.Contains(",,"))
            {
                qsc_data = qsc_data.Replace(",,", ",");
                QUtils.AddLog("FixErrors() Token Quote fixed");
            }

            if (Regex.Match(qsc_data, regex_sub).Success && multi_obj)
            {
                //qsc_data = Regex.Replace(qsc_data, static_regex, string.Empty, RegexOptions.Multiline);
                QUtils.AddLog("FixErrors() Static block found with error expected");
            }

            if (Regex.Match(qsc_data, comma_token_regex).Success && multi_obj)
            {
                qsc_data = Regex.Replace(qsc_data, comma_token_regex, @"),", RegexOptions.Multiline);
                QUtils.AddLog("FixErrors() Comma block found with error expected");
            }

            if (Regex.Match(qsc_data, "^,\\s", RegexOptions.Multiline).Success)
            {
                qsc_data = Regex.Replace(qsc_data, "^,\\s\\r\\n", String.Empty, RegexOptions.Multiline);
                QUtils.AddLog("FixErrors() Multi commas fixed");
            }

            if (!String.IsNullOrEmpty(model))
            {
                string static_task = "Task_New(-1, \"Static\", \"\",";
                int model_index = qsc_data.IndexOf(model);
                int nextline_index = qsc_data.IndexOf('\n', model_index + model.Length + 0x3);

                if (qsc_data.Contains(model))
                {
                    var qsc_temp = qsc_data.Substring(nextline_index, static_task.Length);
                    QUtils.AddLog("Qsc_temp object : " + qsc_temp);

                    if (qsc_temp.Contains("Static"))
                    {
                        QUtils.AddLog(model + " Contains static objects");
                        qsc_data = qsc_data.Remove(nextline_index, static_task.Length).Insert(nextline_index - 1, "),");
                    }
                    else
                    {
                        int endline_index = qsc_data.IndexOf('\n', model_index);
                        qsc_data = qsc_data.Insert(model_index + model.Length + 1, ")");
                    }
                }

            }

            return qsc_data;
        }

        internal static string GetModelName(int id)
        {
            string model_name = null;
            var qtask_list = QUtils.GetQTaskList(false, true);

            foreach (var qtask in qtask_list)
            {
                if (qtask.id == id)
                {
                    model_name = String.IsNullOrEmpty(qtask.note) ? "UnknownModel" : qtask.note;
                    break;
                }
            }
            return model_name;
        }


        internal static string ReplaceAllObjects(string qsc_data, string new_model, string ligthmap = "")
        {
            var qtask_list = QUtils.GetQTaskList();
            foreach (var qtask in qtask_list)
            {
                if (qtask.name.Contains("Building") || qtask.name.Contains("EditRigidObj") ||
                qtask.name.Contains("Terminal") || qtask.name.Contains("Elevator")
                || qtask.name.Contains("Fence") || qtask.name.Contains("Door"))
                {
                    qsc_data = ReplaceObject(qsc_data, qtask.model, new_model, ligthmap);
                }
            }
            return qsc_data;
        }

        internal static string ReplaceObject(string qsc_data, string old_model, string new_model, string ligthmap = "")
        {
            if (!String.IsNullOrEmpty(old_model) && !String.IsNullOrEmpty(new_model))
            {
                if (!old_model.Contains("\""))
                    old_model = "\"" + old_model + "\"";
                if (!new_model.Contains("\""))
                    new_model = "\"" + new_model + "\"";
            }

            if (new_model.Length > old_model.Length)
            {
                Console.WriteLine("Input models length error");
                Console.WriteLine("Length : " + new_model.Length);
                return qsc_data;
            }

            //Replace all objects.
            qsc_data = qsc_data.Replace(old_model, new_model);

            if (!String.IsNullOrEmpty(ligthmap))
                qsc_data = qsc_data.Replace(ligthmap, ligthmap);
            return qsc_data;
        }

        internal static string RemoveObject(string qsc_data, string model, bool fix_errors = true, bool check_model = false)
        {
            if (check_model)
            {
                bool model_exist = QUtils.CheckModelExist(model);

                if (!model_exist)
                {
                    QUtils.ShowError("Model " + model + " does not exist in current level");
                    QUtils.AddLog("Model " + model + " does not exist in current level");
                }
            }

            bool has_multi_objects = HasMultiObjects(qsc_data, model);

            QUtils.AddLog(has_multi_objects ? "Model " + model + " has multi objects" : "Model " + model + " doesn't have multi objects");

            if (has_multi_objects)
                //qsc_data = RemoveMultiObjects(qsc_data, model, !fix_errors);
                qsc_data = RemoveWholeObject(qsc_data, model, true, check_model);
            else
                qsc_data = RemoveAllRigidObjects(qsc_data, model);

            //Remove syntax error if any , too lazy to fix them manually :|
            if (fix_errors && !has_multi_objects)
            {
                qsc_data = FixErrors(qsc_data, model, has_multi_objects);
            }

            if (has_multi_objects)
                qsc_data = RemoveAllRigidObjects(qsc_data, model);

            return qsc_data;
        }

        internal static string RemoveAllRigidObjects(string qsc_data, string model)
        {
            //Remove all whitespaces.
            qsc_data = qsc_data.Replace("\t", String.Empty);
            if (!String.IsNullOrEmpty(model))
            {
                if (!model.Contains("\""))
                    model = "\"" + model + "\"";
            }

            if (model.Length < 3)
            {
                //QCompiler.ShowError("Trying to remove empty model");
                QUtils.AddLog("RemoveSingleObject : Trying to remove empty model : '" + model + "'");
                return qsc_data;
            }

            string[] qsc_data_split = qsc_data.Split('\n');

            foreach (var data in qsc_data_split)
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
                                qsc_data = qsc_data.Replace(data, String.Empty);
                            }
                            else
                            {
                                var inner_data = data.Slice(0, data.IndexOf(')') + 1);
                                qsc_data = qsc_data.Replace(inner_data, String.Empty);
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
            return qsc_data;
        }

        internal static string RemoveWholeObject(string qsc_data, string model, bool fix_errors = true, bool check_model = false)
        {
            int start_index = 0, end_index = 0;
            bool start_run = true;

            if (check_model)
            {
                bool model_exist = QUtils.CheckModelExist(model);

                if (!model_exist)
                {
                    QUtils.ShowError("Model " + model + " does not exist in current level");
                    QUtils.AddLog("Model " + model + " does not exist in current level");
                }
            }

            //Remove all whitespaces.
            qsc_data = qsc_data.Replace("\t", String.Empty);
            string qsc_data_back = String.Copy(qsc_data);

            if (!String.IsNullOrEmpty(model))
            {
                if (!model.Contains("\""))
                    model = "\"" + model + "\"";
            }

            string[] qsc_data_split = qsc_data.Split('\n');

            foreach (var data in qsc_data_split)
            {
                if (data.Contains(QUtils.taskNew))
                {
                    if (data.Contains(model))
                    {
                        //For single objects.
                        if (data.Contains('(') && !data.Contains(')') && start_run)
                        {
                            start_index = qsc_data.IndexOf(data);
                            end_index += data.Length;
                            start_run = false;
                        }
                    }
                    else if (!start_run)
                    {
                        if (data.Contains(QUtils.taskNew) && data.Contains("Building"))
                        {
                            end_index = qsc_data.IndexOf("Building", start_index + end_index);
                            QUtils.AddLog("RemoveWholeObjects : start index : " + start_index + " end index : " + end_index);

                            var building_sub = qsc_data.Slice(start_index, end_index);
                            building_sub = building_sub.Remove(building_sub.LastIndexOf(Environment.NewLine));

                            qsc_data = qsc_data.Replace(building_sub, String.Empty);
                            QUtils.AddLog("RemoveWholeObjects : Removed " + GetModelName(model) + " with id : '" + model + "' for multiple object");
                            break;
                        }
                    }
                }
            }

            return qsc_data;
        }


        internal static string RemoveMultiObjects(string qsc_data, string model, bool fix_errors = true)
        {
            int start_token_count = 0, end_token_count = 0;
            bool start_run = true;

            //Remove all whitespaces.
            qsc_data = qsc_data.Replace("\t", String.Empty);
            string qsc_data_back = String.Copy(qsc_data);
            string unmatched_newline_regex = @",\s\r\n\),\s\r\n", match_newline_regex = ")," + "\n";

            if (!String.IsNullOrEmpty(model))
            {
                if (!model.Contains("\""))
                    model = "\"" + model + "\"";
            }

            string[] qsc_data_split = qsc_data.Split('\n');

            foreach (var data in qsc_data_split)
            {
                if (data.Contains(QUtils.taskNew))
                {
                    if (data.Contains(model))
                    {
                        //For single objects.
                        if (data.Contains('(') && !data.Contains(')') && start_run)
                        {
                            start_token_count++;
                            start_run = false;
                        }
                    }
                    else if (!start_run)
                    {
                        if (data.Contains('('))
                            start_token_count += data.Count(o => o == '(');

                        if (data.Contains(')'))
                            end_token_count += data.Count(o => o == ')');

                        //For nested objects.
                        if (data.Contains('(') || data.Contains(')'))
                        {
                            int count = data.Count(o => o == ')');

                            if (count == 0)
                            {
                                var inner_data = data.Slice(0, data.IndexOf('\n'));
                                int task_count = new Regex(Regex.Escape(data)).Matches(qsc_data).Count;

                                if (task_count == 1)
                                {
                                    qsc_data = qsc_data.Replace(inner_data, String.Empty);
                                }
                                else
                                {
                                    QUtils.AddLog("RemoveMultiObjects : Task count is greater than expected");
                                }

                            }

                            else if (count == 1)
                            {
                                qsc_data = qsc_data.Replace(data, String.Empty);
                            }
                            else
                            {
                                //var inner_data = data.Slice(0, data.IndexOf(')') + 1 + count);
                                var inner_data = data.Slice(0, data.LastIndexOf(')') + 1);
                                qsc_data = qsc_data.Replace(inner_data, String.Empty);
                                qsc_data = Regex.Replace(qsc_data, unmatched_newline_regex, match_newline_regex);
                            }
                        }

                        if (start_token_count == end_token_count)
                        {
                            QUtils.AddLog("RemoveMultiObjects : Model " + GetModelName(model) + " with id : '" + model + "'Removed with Objects : " + start_token_count);
                            break;
                        }
                    }
                }
            }

            if (start_token_count != end_token_count)
            {
                QUtils.AddLog("RemoveMultiObjects : Couldn't remove " + GetModelName(model) + " with id : '" + model + "' for multiple object");
            }

            if (fix_errors)
                qsc_data = FixErrors(qsc_data, model);
            return qsc_data;
        }

        internal static string RemoveAllObjects(string qsc_data, bool buildings = false, bool rigid_objects = false, int count = -1, bool hasRigid = false, bool whole_obj = true)
        {
            var qtask_list = QUtils.GetQTaskList(true);
            int buildings_count = qtask_list.Count(o => o.name.Contains("Building"));
            int rigid_obj_count = qtask_list.Count(o => o.name.Contains("EditRigidObj"));

            int b_count = 0, r_count = 0;

            if (count == -1 && buildings)
                count = buildings_count - 1;

            else if (count == -1 && rigid_objects)
                count = rigid_obj_count - 1;

            foreach (var qtask in qtask_list)
            {
                if (qtask.name == "\"Building\"" && buildings)
                {
                    if (b_count < count && count < buildings_count)
                    {
                        if (whole_obj)
                            qsc_data = RemoveWholeObject(qsc_data, qtask.model, true);
                        else
                            qsc_data = RemoveAllRigidObjects(qsc_data, qtask.model);
                    }
                    else
                    {
                        break;
                    }

                    b_count++;
                }

                if (qtask.name == "\"EditRigidObj\"" && rigid_objects)
                {
                    if (r_count < count && count < rigid_obj_count)
                    {
                        if (whole_obj)
                            qsc_data = RemoveWholeObject(qsc_data, qtask.model, true);
                        else
                            qsc_data = RemoveObject(qsc_data, qtask.model, true);
                    }
                    else
                    {
                        break;
                    }
                    r_count++;
                }

                if (!buildings && !rigid_objects)
                {
                    qsc_data = RemoveObject(qsc_data, qtask.model, false);
                }
            }

            if (hasRigid)
                qsc_data = QObjects.RemoveRigidObjects(qsc_data, -1);
            return qsc_data;
        }

        internal static string RemoveRigidObjects(string qsc_data, int count = -1)
        {
            var qtask_list = QUtils.GetQTaskList(true);
            int obj_count = 0;
            int rigid_obj_count = qtask_list.Count(o => o.name.Contains("EditRigidObj"));
            if (count == -1) count = rigid_obj_count - 1;

            foreach (var model in qtask_list)
            {

                if (model.name.Contains("EditRigidObj"))
                {
                    if (obj_count < count && count < rigid_obj_count)
                        qsc_data = RemoveAllRigidObjects(qsc_data, model.model);
                    obj_count++;
                }
            }
            qsc_data = FixErrors(qsc_data);
            return qsc_data;
        }

        internal static string SetAllAreaActivated(AreaDim area_dim, string area_type, int area_count, float status_duration = 8.0f, bool is_cutscene = false)
        {
            var qtask_list = QUtils.GetQTaskList(false, false, true);
            var qtask_graph_list = QGraphs.GetQTaskGraphList(true, true);

            var qsc_data = QUtils.LoadFile();
            QUtils.qtaskId = QUtils.GenerateTaskID(true);
            if (area_type != QUtils.aiGraphTask)
                area_type = "\"" + area_type + "\"";

            QUtils.AddLog("SetAllAreaActivated called with dim : " + area_dim.x + " area type : " + area_type + " area count : " + area_count + " duration : " + status_duration + " is cutscene : " + is_cutscene);

            if (area_count > (area_type == QUtils.aiGraphTask ? qtask_graph_list.Count : qtask_list.Count))
            {
                QUtils.ShowError("Area count should be less than total objects");
                return null;
            }

            if (area_count == -1) area_count = (area_type == QUtils.aiGraphTask ? qtask_graph_list.Count : qtask_list.Count) - 1;
            int count = 0;

            if (area_type == QUtils.aiGraphTask)
            {
                foreach (var qtask in qtask_graph_list)
                {
                    if (count > area_count) break;
                    var graph_name = qtask.name.Replace("\"", String.Empty) + "_" + qtask.id;
                    qsc_data += AddAreaActivate(qtask.id, null, graph_name, qtask.note, ref qtask.position, ref area_dim, status_duration, is_cutscene);
                    QUtils.qtaskId++;
                    count++;
                }
            }
            else
            {
                foreach (var qtask in qtask_list)
                {
                    if (count > area_count) break;

                    if (qtask.name == area_type)
                    {
                        qsc_data += AddAreaActivate(qtask.id, qtask.model, qtask.note, qtask.note, ref qtask.position, ref area_dim, status_duration, is_cutscene);
                        QUtils.qtaskId++;
                        count++;
                    }
                }
            }
            return qsc_data;
        }

        internal static string AddAreaActivate(int task_id, string model, string model_name, string task_note, ref Real64 position, ref AreaDim area_dim, float status_duration = 5.0f, bool is_cutscene = false)
        {
            var area_id = QUtils.qtaskId;
            var status_id = area_id + 1;
            var task_note_str = task_note.Replace("\"", String.Empty).ToUpperInvariant();

            QUtils.AddLog("AddAreaActivate task_id : " + task_id + " model : " + model + " model_name : " + model_name + " task_note : " + task_note + " X : " + position.x + " Y : " + position.y + " Z : " + position.z + " dim : " + area_dim.x + " duration : " + status_duration + " is cutscene : " + is_cutscene);

            if (!String.IsNullOrEmpty(model))
                model_name = model.Replace("\"", String.Empty);

            if (task_note_str.Length < 3 || model_name.Length < 3)
                task_note_str = GetModelName(model);

            string task_comment = "\n//" + task_note_str + " Area" + "\n";
            string area_task = "Task_New(" + QUtils.qtaskId++ + ",\"AreaActivate\"," + task_note + "," + position.x + "," + position.y + "," + position.z + ",0,0,0," + area_dim.x + "," + area_dim.y + "," + area_dim.z + ",\"CRITERIA_HUMAN0\");" + "\n";

            string status_msg_task = "Task_New(" + -1 + ",\"StatusMessage\"," + task_note + ",0,0,0,0,0,0,\"AreaActivate_" + area_id + ".nActive\",\"" + task_note_str + " " + model_name + " ID : " + task_id + " Pos : X : " + position.x + " Y: " + position.y + " Z: " + position.z + "\"," + "\"\", \"message\",FALSE," + is_cutscene.ToString().ToUpperInvariant() + "," + status_duration + ");" + "\n";

            var qsc_data = task_comment + area_task + status_msg_task;
            return qsc_data;
        }

        internal static Real64 GetObjectPosition(string model)
        {
            Real64 object_pos = new Real64();

            if (String.IsNullOrEmpty(model))
                throw new ArgumentNullException();

            var qtask_list = QUtils.GetQTaskList();
            model = "\"" + model + "\"";

            foreach (var qtask in qtask_list)
            {
                if (qtask.model == model)
                {
                    QUtils.AddLog("GetObjectPosition() : Model found :  " + model + " with position : X :" + qtask.position.x + " Y: " + qtask.position.y + " Z: " + qtask.position.z);
                    object_pos = qtask.position;
                    break;
                }
            }
            return object_pos;
        }

        internal static Real32 GetObjectOrientation(string model)
        {
            Real32 object_orientation = new Real32();

            if (String.IsNullOrEmpty(model))
                throw new ArgumentNullException();

            var qtask_list = QUtils.GetQTaskList();
            model = "\"" + model + "\"";

            foreach (var qtask in qtask_list)
            {
                if (qtask.model == model)
                {
                    QUtils.AddLog("GetObjectOrientation() : Model found :  " + model + " with position : X :" + qtask.position.x + " Y: " + qtask.position.y + " Z: " + qtask.position.z);
                    object_orientation = qtask.orientation;
                    break;
                }
            }
            return object_orientation;
        }

        internal static List<Dictionary<string, string>> GetObjectList(int level, QTYPES objType, bool distinct = false, bool fromBackup = false)
        {
            QUtils.AddLog("GetObjectList() called with level : " + level + " With type : " + ((objType == QTYPES.BUILDING) ? "Buildings" : "3D Rigid objects" + " from_backup : " + fromBackup));
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
                        modelName = GetModelName(qtask.model, false, true);

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
                        modelName = GetModelName(qtask.model);

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
