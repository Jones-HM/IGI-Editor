using System;
using System.Linq;

namespace IGIEditor
{
    class QBuildings
    {

        internal static string AddBuilding(string model, Real64 position, bool check_model = false)
        {
            string model_name = QObjects.GetModelName(model);
            QUtils.AddLog("AddBuilding() called check_model : " + check_model.ToString() + "  Building : " + model_name + "\"\tX : " + position.x + " Y : " + position.y + " Z : " + position.z + " Model : " + model + "\n");

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

            //int task_id = QUtils.GenerateTaskID(true);
            return Building(-1, model_name, position.x, position.y, position.z, 0, 0, 0, model);
        }

        internal static string AddBuilding(string model, Real64 position, Real32 orientation, bool check_model = false)
        {
            string model_name = QObjects.GetModelName(model);
            QUtils.AddLog("AddBuilding() called check_model : " + check_model.ToString() + "  Building : " + QObjects.GetModelName(model) + "\"\tX : " + position.x + " Y : " + position.y + " Z : " + position.z + "\tBeta : " + orientation.beta + " Gamma : " + orientation.gamma + ",Alpha : " + orientation.alpha + " Model : " + model + "\n");

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
            //int task_id = QUtils.GenerateTaskID(true);
            return Building(-1, model_name, position.x, position.y, position.z, orientation.alpha, orientation.beta, orientation.gamma, model);
        }

        internal static string Building(int task_id = -1, string task_note = "", double x = 0.0f, double y = 0.0f, double z = 0.0f, float alpha = 0.0f, float beta = 0.0f, float gamma = 0.0f, string model = "")
        {
            string qtask_building = "Task_New(" + task_id + ",\"Building\",\"" + task_note + "\"," + x + "," + y + "," + z + "," + alpha + "," + beta + "," + gamma + ",\"" + model + "\");" + "\n";
            QUtils.AddLog("Building() called with ID : " + QUtils.qtaskObjId + "  Building : " + QObjects.GetModelName(model) + "\"\tX : " + x + " Y : " + y + " Z : " + z + "\t Alpha : " + alpha + " Beta : " + beta + ",Gamma : " + gamma + " Model : " + model + "\n");
            return qtask_building;
        }


        string RemoveBuilding(int qid)
        {
            string qsc_data = QUtils.LoadFile();
            //Remove all whitespaces.
            qsc_data = qsc_data.Replace("\t", String.Empty);
            bool is_recursive = false;//Because Task_ID is unique.
            return RemoveBuilding(qsc_data, qid, null, (int)QTASKINFO.QTASK_ID, is_recursive);
        }

        string RemoveBuilding(string model)
        {
            string qsc_data = QUtils.LoadFile();
            //Remove all whitespaces.
            qsc_data = qsc_data.Replace("\t", String.Empty);
            if (!String.IsNullOrEmpty(model))
                model = "\"" + model + "\"";
            bool is_recursive = QUtils.GetModelCount(model) > 1;

            return RemoveBuilding(qsc_data, -9999, model, (int)QTASKINFO.QTASK_MODEL, is_recursive);
        }



        string RemoveBuilding(string qsc_data, int qid, string model, int update_type, bool is_recursive)
        {

            string[] qsc_data_split = qsc_data.Split('\n');

            foreach (var data in qsc_data_split)
            {
                if (data.Contains(QUtils.taskNew))
                {
                    var start_index = data.IndexOf(',', 14) + 1;
                    var task_name = (data.Slice(13, start_index));

                    string[] task_new = data.Split(',');
                    int task_index = 0;
                    if (QUtils.objTypeList.Any(o => o.Contains(task_name)))
                    {
                        foreach (var task in task_new)
                        {
                            if (task_index == (int)QTASKINFO.QTASK_ID || task_index == (int)QTASKINFO.QTASK_MODEL)
                            {
                                int task_id = -3;
                                string qmodel = null;
                                if (update_type == (int)QTASKINFO.QTASK_ID)
                                {
                                    string task_id_str = task_new[(int)QTASKINFO.QTASK_ID].Remove(0, QUtils.taskNew.Length + 1).Replace("\"", String.Empty);
                                    task_id = Convert.ToInt32(task_id_str);
                                }

                                if (update_type == (int)QTASKINFO.QTASK_MODEL)
                                {
                                    string qmodel_str = task_new[(int)QTASKINFO.QTASK_MODEL];
                                    qmodel = task_new[(int)QTASKINFO.QTASK_MODEL].Slice(0, qmodel_str.IndexOf('"', 3) + 1).Trim();
                                }

                                if (qid == task_id || model == qmodel && (!(String.IsNullOrEmpty(model))))
                                {
                                    var qid_str = task_new[(int)QTASKINFO.QTASK_ID].Substring(task.IndexOf('(') + 1);
                                    var id = Convert.ToInt32(qid_str);
                                    string id_model_index = "";

                                    if (qid == task_id)
                                        id_model_index = "Task_New(" + id;
                                    else
                                        id_model_index = model;

                                    int qtask_start_index = qsc_data.IndexOf(id_model_index);
                                    int new_line_index = qsc_data.IndexOf('\n', qtask_start_index) + 1;
                                    int new_line_index_len = new_line_index - qtask_start_index;

                                    int qtask_end_index = qsc_data.IndexOf(')', qtask_start_index, new_line_index_len);
                                    int nested_count = 0;

                                    if (qtask_end_index != -1)
                                    {
                                        qtask_end_index = new_line_index;
                                    }

                                    else
                                    {
                                        //Remove nested objects.
                                        while (qtask_end_index == -1)
                                        {
                                            new_line_index = qsc_data.IndexOf('\n', new_line_index + 2);
                                            qtask_start_index = qsc_data.IndexOf("Task_New(", new_line_index);
                                            new_line_index_len = new_line_index - qtask_start_index;

                                            var nested_start = qsc_data.IndexOf('(', new_line_index, new_line_index_len);
                                            var nested_end = qsc_data.IndexOf(')', new_line_index, new_line_index_len);

                                            if (nested_start != -1 && nested_end != -1 && qsc_data[nested_end + 1] != ',')
                                                nested_count++;
                                            else
                                            {
                                                qtask_end_index = new_line_index;
                                            }

                                        }
                                    }

                                    qsc_data = qsc_data.Remove(qtask_start_index, qtask_end_index - qtask_start_index).Trim();

                                    if (is_recursive)
                                    {
                                        QUtils.SaveFile(qsc_data);
                                        return RemoveBuilding(qsc_data, qid, model, update_type, is_recursive);
                                    }
                                    else
                                        return qsc_data;
                                }
                            }
                            task_index++;
                        }
                    }
                }
            }
            return qsc_data;
        }

    }
}
