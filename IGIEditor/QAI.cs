using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace IGIEditor
{
    class QAI
    {

        internal static string AddHumanSoldier(string ai_type, int ai_script_id, int graph_id, Real64 position, float angle, string model, int team, bool add_weapon, string weapon, int ammo, bool guardGenerator)
        {
            if (QUtils.qtaskId == 0)
            {
                QUtils.qtaskId = QUtils.GenerateTaskID(true);
            }
            else
            {
                QUtils.qtaskId++;
            }

            if (position == null)
            {
                position = QHuman.GetHumanTaskList().qtask.position;
            }
            int boneHeirarchy = 1;
            if (model == "015_01_1" || model == "012_01_1")
                boneHeirarchy = GetBoneHeirarchy(model);

            return AddHumanSoldier(QUtils.qtaskId, "A.I", ai_type, ai_script_id, graph_id, position, angle, model, team, boneHeirarchy, -1, add_weapon, weapon, ammo, guardGenerator);
        }

        internal static string AddHumanSoldier(int taskId, string task_note, string ai_type, int ai_script_id, int graph_id, Real64 position, float angle, string model, int team, int bone_heirachy, int stand_animation, bool add_weapon, string weapon, int ammo, bool guardGenerator)
        {

            //Add the A.I (Human soldier)
            string humanSoldierType = (ai_type == "AITYPE_ANYA" || ai_type == "AITYPE_EKK") ? "HumanSoldierFemale" : "HumanSoldier";
            string qtask_soldier = "\nTask_New(" + taskId + ",\"" + humanSoldierType + "\",\"" + task_note + "\"," + position.x + "," + position.y + "," + position.z + "," + angle + ",\"" + model + "\"," + team + "," + bone_heirachy + "," + stand_animation + ",\n";
            QUtils.AddLog("AddHumanSoldier() called with ID : " + taskId + "  HumanSoldier : " + task_note + "\"," + position.x + "," + position.y + "," + position.z + "," + angle + ",\"" + model + "\"," + team + "," + bone_heirachy + "," + stand_animation + ",\n");

            //Add A.I type to status message.
            if (team == 0)
                QUtils.aiFriendTask += humanSoldierType + "_" + taskId + ".isDead && ";

            else
                QUtils.aiEnenmyTask += humanSoldierType + "_" + taskId + ".isDead && ";


            //Add the weapon.
            if (add_weapon)
                qtask_soldier += QHuman.Weapon(weapon, ammo);

            //Add AI's script and graph data.
            qtask_soldier += "Task_New(" + ai_script_id + ",\"HumanAI\",\"" + task_note + "\",\"" + ai_type + "\"," + graph_id;
            qtask_soldier += (!guardGenerator) ? "));" : ")));";
            return qtask_soldier;
        }

        internal static string GuardGenerator(string task_note = "AI Troops", int maxSpawn = 10)
        {
            string qTaskGuardGen = "Task_New(-1, \"GuardGenerator\",\"" + task_note + "\"," + "\"!HumanPlayer_0.isDead\"," + maxSpawn + ",";
            return qTaskGuardGen;
        }

        internal static string AddHumanSoldierCfg(IGIEditor.HumanAi humanAi, bool guardGenerator = false, int maxSpawns = 10, bool invulnerability = false, bool advanceView = false)
        {
            string aiType = null, aiWeapon = null, graphId = null, aiId = null, patrolId = null, modelId = null;
            int aiCount = 1, teamId = 0, aiAmmo = 999;
            string qscData = null;

            if (humanAi != null) aiCount = humanAi.aiCount;

            for (int i = 1; i <= aiCount; i++)
            {
                if (humanAi != null)
                {
                    aiType = humanAi.aiType;
                    graphId = humanAi.graphId.ToString();
                }

                aiId = QUtils.randGenScriptId.ToString();
                bool aiIdExist = QGraphs.CheckIdExist(aiId, "AI", QUtils.gGameLevel, "AI Id " + aiId + " already exist for current level");

                patrolId = (QUtils.randGenScriptId + 1).ToString();
                bool patrolIdExist = QGraphs.CheckIdExist(patrolId, "Patrol", QUtils.gGameLevel, "PatrolId " + patrolId + " already exist for current level");
                bool graphIdExist = true;//QGraphs.CheckIdExist(graphId, "Graph", QUtils.gGameLevel, "GraphId " + graphId + " doesn't exist for current level");

                if (!patrolIdExist && !aiIdExist && graphIdExist)
                {
                    int aiId_i = Convert.ToInt32(aiId);
                    int patrolId_i = Convert.ToInt32(patrolId);
                    int graphId_i = Convert.ToInt32(graphId);

                    Real64 aiPos = QGraphs.GetGraphPosition(graphId);
                    float aiAngle = QMemory.GetRealAngle();
                    aiPos.x += new Random().Next(1000, 100000);
                    aiPos.y += new Random().Next(1000, 100000);

                    if (humanAi != null)
                    {
                        modelId = humanAi.model;
                        aiWeapon = humanAi.weapon;
                        teamId = humanAi.friendly == true ? 0 : 1;
                        aiAmmo = 999;
                        bool modelExist = QUtils.CheckModelExist(modelId);

                        if (!modelExist)
                        {
                            QUtils.ShowError("Model '" + modelId + "' doesnt exist for current level");
                            return qscData;
                        }
                    }

                    //Add GuardGenerator .
                    if (guardGenerator)
                        qscData += QAI.GuardGenerator("AI Army", maxSpawns);

                    //Add A.I HumanSoldier.
                    qscData += AddHumanSoldier(aiType, aiId_i, graphId_i, aiPos, aiAngle, modelId, teamId, true, aiWeapon, aiAmmo, guardGenerator);

                    //Add A.I Script to HumanSoldier.
                    var aiScriptData = AddAIScript(aiType, graphId, aiId, patrolId, QUtils.gGameLevel, invulnerability, advanceView);
                    if (!String.IsNullOrEmpty(aiScriptData))
                        qscData += aiScriptData;
                }
                QUtils.randGenScriptId += 2;
            }

            return qscData;
        }

        internal static string AddAIScript(string aiType, string graphId, string aiId, string patrolId, int level, bool invulnerability = false, bool advanceView = false)
        {
            var inputAiPath = QUtils.cfgInputAiPath;
            string result = null;
            string patrolAlarmId = null;

            string[] fileArray = System.IO.Directory.GetFiles(inputAiPath);
            var aiTypeSplit = aiType.Split('_')[1].ToLower();

            foreach (var file in fileArray)
            {
                if (file.Contains(aiTypeSplit))
                {
                    if (file.Contains("script"))
                    {
                        string aiScriptData = QUtils.LoadFile(file);

                        //Add Idle patrol.
                        if (aiScriptData.Contains(QUtils.patroIdleMask))
                        {
                            aiScriptData = aiScriptData.ReplaceFirst(QUtils.patroIdleMask, patrolId);
                        }

                        //Add Alarm patrol.
                        if (aiScriptData.Contains(QUtils.patroAlarmMask))
                        {
                            aiScriptData = aiScriptData.ReplaceFirst(QUtils.patroAlarmMask, patrolAlarmId);
                        }

                        //Add Alarm control Id.
                        if (aiScriptData.Contains(QUtils.alarmControlMask))
                        {
                            var aiPos = QGraphs.GetGraphPosition(graphId);
                            int alarmControlId = 0;
                            alarmControlId = QAI.GetNearestDynamicId(aiPos, QUtils.alarmControl);
                            if (alarmControlId == 0)
                                QUtils.ShowWarning("Couldn't find nearest alarm Id for AI : " + aiId + " on Graph : " + graphId);
                            aiScriptData = aiScriptData.ReplaceFirst(QUtils.alarmControlMask, alarmControlId.ToString());
                        }


                        //Add Gunner Id.
                        if (aiScriptData.Contains(QUtils.gunnerIdMask))
                        {
                            var aiPos = QGraphs.GetGraphPosition(graphId);
                            int gunnerId = 0;
                            gunnerId = QAI.GetNearestDynamicId(aiPos, QUtils.stationaryGun);
                            if (gunnerId == 0)
                                QUtils.ShowWarning("Couldn't find nearest Gunner Id for AI : " + aiId + " on Graph : " + graphId);
                            aiScriptData = aiScriptData.ReplaceFirst(QUtils.gunnerIdMask, gunnerId.ToString()).ReplaceFirst(QUtils.viewGammaMask, "180");//Set View Gamma to 180.
                        }

                        //Add invulnerability if opted for.
                        if (invulnerability)
                        {
                            string invulnerabilityMode = "AIFunction_DefaultHandler();\n" +
                                "AIFunction_SetEventPriority(AIEVENT_COMBAT);\n" +
                                "AIFunction_SetInstantDeath(FALSE);\n" +
                                "AIFunction_SetInvulnerability(TRUE);\n";
                            aiScriptData = aiScriptData.ReplaceFirst("AIFunction_DefaultHandler();", invulnerabilityMode);
                        }

                        //Add advance View if opted for.
                        if (advanceView)
                        {
                            string advanceViewLengthMode = "AIFunction_DefaultHandler();\n" +
                                "AIFunction_SetViewGamma(90000);\n" +
                                " AIFunction_SetSecondaryViewGamma(90000);\n" +
                                "AIFunction_SetSecondaryViewAlpha(90000);\n" +
                                "AIFunction_SetViewLength(90000);\n" +
                                "AIFunction_SetSecondaryViewLength(90000);\n";
                            aiScriptData = aiScriptData.ReplaceFirst("AIFunction_DefaultHandler();", advanceViewLengthMode);
                        }


                        string aiFileName = aiId + ".qsc";
                        QUtils.aiScriptFiles.Add(aiId + ".qvm");
                        var outputAiPath = QUtils.cfgGamePath + level + "\\ai\\";

                        QUtils.SaveFile(aiFileName, aiScriptData);
                        QCompiler.Compile(aiFileName, outputAiPath, 0x0);
                        System.IO.File.Delete(aiFileName);
                    }

                    else if (file.Contains("path"))
                    {
                        string aiPathData = QUtils.LoadFile(file);
                        bool graphExist = false;
                        var nodesList = QGraphs.GetAllNodes4mGraph(Convert.ToInt32(graphId));

                        if (nodesList == null)
                        {
                            var qTaskGraphList = QGraphs.GetQTaskGraphList(true, true, level);

                            foreach (var qTaskGraph in qTaskGraphList)
                            {
                                if (qTaskGraph.id.ToString() == graphId)
                                {
                                    graphExist = true;
                                    break;
                                }
                            }

                            if (graphExist)
                            {
                                QUtils.AddLog("AddAIScript() AI Patrol Updated to static for aiId : " + aiId + "\tgraphId : " + graphId);
                                return AddAIScript("AITYPE_STATIC", graphId, aiId, patrolId, level);
                            }
                            else
                            {
                                QUtils.ShowError("Invalid GraphId " + graphId + "provided.");
                                return null;
                            }
                        }

                        if (aiPathData.Contains("xxxx"))
                        {
                            aiPathData = aiPathData.Replace("xxxx", patrolId);
                            aiPathData = aiPathData.Replace(")),", "));");
                            result = aiPathData;

                            //Add Alarm path to selected A.I.
                            if (file.Contains("idle"))
                            {
                                QUtils.randGenScriptId++;
                                patrolAlarmId = (QUtils.randGenScriptId + 1).ToString();
                                string alarmPathFile = file.Replace("idle", "alarm");

                                string aiAlarmPathData = QUtils.LoadFile(alarmPathFile);

                                aiAlarmPathData = aiAlarmPathData.Replace("xxxx", patrolAlarmId);
                                aiAlarmPathData = aiAlarmPathData.Replace(")),", "));");
                                result = aiPathData + "\n" + aiAlarmPathData;
                            }
                        }

                        int index = 0;
                        if (nodesList.Count <= 2 && !aiType.Contains("AITYPE_SECURITY_PATROL_SPAS"))
                        {
                            QUtils.AddLog("AddAIScript() AI Patrol Updated to security for aiId : " + aiId + "\tgraphId : " + graphId);
                            result = AddAIScript("AITYPE_SECURITY_PATROL_SPAS", graphId, aiId, patrolId, level);
                        }
                        else if (aiType == "AITYPE_SECURITY_PATROL_SPAS")
                        {
                            for (char c = 'a'; c <= 'b'; c++)
                            {
                                int randIndex = new Random().Next(0, nodesList.Count - 1);
                                string pattern = @"\b" + c + @"\b";
                                string replace = nodesList[index++].ToString();
                                result = Regex.Replace(result, pattern, replace);
                            }
                        }

                        else
                        {
                            var nIdsList = new List<char>() { 'a', 'c', 'b', 'd' };
                            foreach (var nId in nIdsList)
                            {
                                int randIndex = new Random().Next(0, nodesList.Count - 1);
                                if (nodesList.Count >= 10) index = randIndex;
                                string pattern = @"\b" + nId + @"\b";
                                string replace = nodesList[index].ToString();
                                result = Regex.Replace(result, pattern, replace);
                                index++;
                            }
                        }
                    }
                }
            }

            return "\n" + result + "\n";
        }


        internal static string AddAiTaskDetection(string qscData)
        {
            //Add A.I detection.
            string statusMsg = null;
            statusMsg += "\nTask_New(-1,\"Container\", \"StatusMessages\"," + "\n";

            if (!String.IsNullOrEmpty(QUtils.aiFriendTask))
            {
                var varStringFriendly = QUtils.aiFriendTask.ReplaceLast("&&", string.Empty);

                var varStringSplit = QUtils.aiFriendTask.Replace("&&", "#").Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var varString in varStringSplit)
                {
                    statusMsg += QUtils.AddStatusMsg(-1, "Friendly man down! Watch out", varString);
                }

                QUtils.anyaTeamTaskId = QUtils.GenerateTaskID();
                statusMsg += QUtils.AddStatusMsg(QUtils.anyaTeamTaskId, "Anya team down. Mission Failed", varStringFriendly);
                QUtils.aiFriendTask = null;
            }

            if (!String.IsNullOrEmpty(QUtils.aiEnenmyTask))
            {
                var varStringEnemy = QUtils.aiEnenmyTask.ReplaceLast("&&", string.Empty);

                var varStringSplit = QUtils.aiEnenmyTask.Replace("&&", "#").Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var varString in varStringSplit)
                {
                    statusMsg += QUtils.AddStatusMsg(-1, "Enemy man down! Great", varString);
                }

                QUtils.ekkTeamTaskId = QUtils.GenerateTaskID();
                statusMsg += QUtils.AddStatusMsg(QUtils.ekkTeamTaskId, "Ekk team down. Mission Completed", varStringEnemy);
                QUtils.aiEnenmyTask = null;
            }

            qscData += statusMsg.ReplaceLast("),", "));");
            return qscData;
        }

        internal static string AddPatrolTask(int patrolId, string task_note = "Patrol Path")
        {
            string qtask_patrol_path = "Task_New(" + patrolId + ",\"PatrolPath\",\"" + task_note + "\"," + "\n";
            return qtask_patrol_path;
        }

        internal static string AddPatrolCommand(string patrol_task, string task_note, PATROLACTIONS path_cmd, int path_param, bool last_cmd = false)
        {
            patrol_task += "Task_New(-1,\"PatrolPathCommand\"," + task_note + path_cmd + "," + path_param + ")";
            patrol_task += (last_cmd) ? ")," : ",";
            return patrol_task;
        }

        internal static string RemoveHumanSoldier(string qsc_data, string model)
        {
            int start_index = 0, end_index = 0, lcount = 0, rcount = 0;
            bool start_run = false;
            QUtils.AddLog("RemoveHumanSoldier() called with model : " + model + "\n");

            if (String.IsNullOrEmpty(qsc_data) || String.IsNullOrEmpty(model))
            {
                QUtils.ShowError("RemoveHumanSoldier : Input is empty");
                return null;
            }

            qsc_data = qsc_data.Trim();
            var qsc_data_split = qsc_data.Split('\n');
            string qscTmp = String.Copy(qsc_data);

            foreach (var data in qsc_data_split)
            {
                if (data.Contains(QUtils.taskNew))
                {
                    if (data.Contains(model) && data.Contains("HumanSoldier"))
                    {
                        if (data.Contains("Task_New(-1,"))
                        {
                            start_run = false;
                        }
                        else
                        {
                            if (data.Contains('('))
                                lcount += data.Count(o => o == '(');

                            start_index = qsc_data.IndexOf(data);
                            if (start_index == -1)
                            {
                                QUtils.ShowError("RemoveHumanSoldier : Data couldn't be found in QData file");
                                QUtils.AddLog("RemoveHumanSoldier() : Data couldn't be found in Substring\n");
                                QUtils.AddLog(data);
                                QUtils.SaveFile("objects_tmp.txt", qsc_data);
                                return qscTmp;
                            }
                            end_index += data.Length + 1;
                            start_run = true;
                            continue;
                        }
                    }
                    if (start_run)
                    {
                        if (lcount >= 1)
                            end_index += data.Length + 1;

                        if (data.Contains('('))
                            lcount += data.Count(o => o == '(');

                        if (data.Contains(')'))
                            rcount += data.Count(o => o == ')');

                        if (lcount == rcount)
                        {
                            start_run = false;
                            var ai_sub = qsc_data.Substring(start_index, end_index);
                            qsc_data = qsc_data.Replace(ai_sub, String.Empty);
                            start_index = end_index = lcount = rcount = 0;
                        }
                    }
                }
            }

            QUtils.AddLog("RemoveHumanSoldier() start index : " + start_index + "  end index : " + end_index + "\n");
            return qsc_data;
        }

        internal static List<string> GetAiModels(int level)
        {
            string input_qsc_path = QUtils.cfgInputQscPath + level + "\\" + QUtils.objectsQsc;
            QUtils.AddLog("GetAiModels() level : called with level : " + level);
            string qsc_data = QCryptor.Decrypt(input_qsc_path);
            List<string> aiModelsList = new List<string>();
            var model_regex = @"\d{3}_\d{2}_\d{1}";
            var dataLines = qsc_data.Split('\n');

            foreach (var data in dataLines)
            {
                if (data.Contains("HumanSoldier"))
                {
                    string model = Regex.Match(data, model_regex).Value;
                    //Friendly A.I exclude.
                    if (model == "015_01_1" || model == "020_01_1" || model == "021_01_1"
                        || model == "022_01_1" || model == "009_01_1" || model == "000_01_1")
                        continue;
                    else
                        aiModelsList.Add(model);
                }
            }

            aiModelsList = aiModelsList.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
            return aiModelsList;
        }

        internal static List<KeyValuePair<int, Real64>> GetDynamicIds4AI(string dynamicType, bool from_backup = true)
        {
            string qscData = null;
            if (from_backup)
            {
                string qscBackupPath = QUtils.cfgInputQscPath + QUtils.gGameLevel + "\\" + QUtils.objectsQsc;
                qscData = QCryptor.Decrypt(qscBackupPath);
            }

            if (!from_backup)
                qscData = QUtils.LoadFile();

            var qscDataLines = qscData.Split('\n');
            var dynamiclIdsList = new List<KeyValuePair<int, Real64>>();

            QUtils.AddLog("GetDynamicIds4AI() caled with level : " + QUtils.gGameLevel + " with type : " + dynamicType);

            foreach (var dataLine in qscDataLines)
            {
                if (dataLine.Contains(QUtils.taskNew) && dataLine.Contains(dynamicType))
                {
                    var dynamicTypeData = dataLine.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    QUtils.AddLog("GetDynamicIds4AI() dynamicType[1] : " + dynamicTypeData[1]);

                    if (dynamicTypeData[1].Trim() == "\"" + dynamicType + "\"")
                    {
                        QUtils.AddLog("GetDynamicIds4AI() dynamicType Data : " + dynamicTypeData);

                        int dynamicTypeId = Int32.Parse(Regex.Match(dynamicTypeData[0], @"\d+").Value);
                        QUtils.AddLog("GetDynamicIds4AI() dynamicTypeId : " + dynamicTypeId);

                        Double xPos = Double.Parse(dynamicTypeData[3].Trim());
                        Double yPos = Double.Parse(dynamicTypeData[4].Trim());
                        Double zPos = Double.Parse(dynamicTypeData[5].Trim());

                        Real64 alarmControlPos = new Real64(xPos, yPos, zPos);
                        dynamiclIdsList.Add(new KeyValuePair<int, Real64>(dynamicTypeId, alarmControlPos));
                    }
                }
            }
            QUtils.AddLog("GetDynamicIds4AI() returned list count : " + dynamiclIdsList.Count);
            return dynamiclIdsList;
        }

        internal static int GetNearestDynamicId(Real64 aiPos, string dynamicType)
        {
            QUtils.AddLog("GetNearestDynamicId() called with pos X : " + aiPos.x + " Y : " + aiPos.y + " Z : " + aiPos.z + " dynamicType : " + dynamicType);

            var dynamicIds = GetDynamicIds4AI(dynamicType, false);
            var diffPosList = new List<KeyValuePair<int, Real64>>();
            int nearestDynamicId = 0;

            if (dynamicIds.Count > 1)
            {
                foreach (var alarmControlId in dynamicIds)
                {
                    var alarmPos = alarmControlId.Value;
                    var diffPos = GetPosDiff(aiPos, alarmPos);
                    diffPosList.Add(new KeyValuePair<int, Real64>(alarmControlId.Key, diffPos));
                }
                diffPosList = diffPosList.OrderBy(o => o.Value.x).ToList();
                nearestDynamicId = diffPosList[0].Key;
            }
            else if (dynamicIds.Count == 1)
            {
                nearestDynamicId = dynamicIds[0].Key;
            }
            QUtils.AddLog("GetNearestDynamicId() returned : " + nearestDynamicId);
            return nearestDynamicId;
        }

        internal static Real64 GetPosDiff(Real64 pos1, Real64 pos2)
        {
            QUtils.AddLog("GetNearestAlarmId() called");
            Real64 diffPos = new Real64();

            diffPos.x = Math.Abs(pos1.x - pos2.x);
            diffPos.y = Math.Abs(pos1.y - pos2.y);
            diffPos.z = Math.Abs(pos1.z - pos2.z);

            QUtils.AddLog("GetNearestAlarmId() returned");
            return diffPos;
        }

        private static int GetBoneHeirarchy(string model)
        {
            int boneHeirarchy = 1;
            int level = QUtils.gGameLevel;
            string input_qsc_path = QUtils.cfgInputQscPath + level + "\\" + QUtils.objectsQsc;
            QUtils.AddLog("GetBoneHeirarchy() level : called with level : " + level + " model : " + model);
            string qsc_data = QCryptor.Decrypt(input_qsc_path);
            List<string> aiModelsList = new List<string>();
            var model_regex = @"\d{3}_\d{2}_\d{1}";
            var dataLines = qsc_data.Split('\n');

            foreach (var data in dataLines)
            {
                if (data.Contains("HumanSoldier") || data.Contains("HumanSoldierFemale"))
                {
                    string modelData = Regex.Match(data, model_regex, RegexOptions.RightToLeft).Value;
                    if (model == modelData)
                    {
                        var dataSplit = data.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        string heirarchy = dataSplit[(dataSplit.Length - 1) - 2];
                        boneHeirarchy = Convert.ToInt32(heirarchy);
                        break;
                    }
                }
            }
            QUtils.AddLog("GetBoneHeirarchy() returned boneHeirarchy : " + boneHeirarchy);
            return boneHeirarchy;
        }

    }
}
