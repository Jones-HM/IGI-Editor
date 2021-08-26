using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace IGIEditor
{
    class QGraphs
    {

        public class Graph
        {
            public int x, y, z;

            public Graph() { x = y = z = 0; }
            public Graph(int x)
            {
                this.x = x;
                this.y = this.z = 0;
            }

            public Graph(int x, int y)
            {
                this.x = x;
                this.y = y;
                this.z = 0;
            }

            public Graph(int x, int y, int z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }
        };

        public class QTaskGraph
        {
            public Int32 id;
            public string name;
            public string note;
            public Real64 position;
            public Graph graph;
            public bool update;
            public bool use_precise;
            public Int32 graph_data;
            public Double mid_offsets;
            public Double top_offsets;
            public Double height_diff;
            public Double node_width;
            public Double ground_dist;
            public Double precise_step_val;
        };

        enum QTASKINFO
        {
            QTASK_ID,
            QTASK_NAME,
            QTASK_NOTE,
            //Object pos in Real64x3.
            QTASK_POSX,
            QTASK_POSY,
            QTASK_POSZ,

            //Graph data.
            QTASK_UPDATE,
            QTASK_GRAPH_DATA_X,
            QTASK_GRAPH_DATA_Y,
            QTASK_GRAPH_DATA_Z,

            //Graph properties in Real64.
            QTASK_MID_OFFSET,
            QTASK_TOP_OFFSET,
            QTASK_HEIGH_DIFF,
            QTASK_NODE_WIDTH,
            QTASK_GROUND_DIST,
            QTASK_USE_PRECISE,
            QTASK_PRECISE_STEP_VAL
        }

        internal static List<int> GetGraphIds(int game_level = -1)
        {
            return GetGraphNodeIds(game_level, 1);
        }

        internal static List<int> GetNodesIds(int game_level = -1)
        {
            return GetGraphNodeIds(game_level, 2);
        }

        internal static Real64 GetGraphPosition(string graphId)
        {
            var qGraphList = GetQTaskGraphList(true, true);
            Real64 qGraphPos = new Real64();

            foreach (var qGraph in qGraphList)
            {
                if (qGraph.id.ToString() == graphId)
                {
                    qGraphPos = qGraph.position;
                    break;
                }
            }
            return qGraphPos;
        }


        private static List<int> GetGraphNodeIds(int level = -1, int id_type = 1)
        {
            List<int> graph_node_ids = new List<int>();
            string node_regex = "node id [0-9]*";
            string graph_regex = "[0-9]{0,4}, \"AIGraph\"";
            string selected_regex = (id_type == 1) ? graph_regex : node_regex;

            //For current level.
            if (level == -1)
                level = QMemory.GetCurrentLevel();


            QUtils.AddLog("GetGraphNodeIds() called with level " + level + "id_type : " + id_type);

            string input_qsc_path = QUtils.cfgInputQscPath + level + "\\" + QUtils.objectsQsc;
            string qsc_data = QCryptor.Decrypt(input_qsc_path);

            QUtils.AddLog("GetGraphNodeIds() inputQscPath : " + input_qsc_path);

            var matches_result = Regex.Matches(qsc_data, selected_regex).Cast<Match>().Select(m => m.Value).ToList();

            foreach (var match_result in matches_result)
            {
                var node_id = Int32.Parse(Regex.Match(match_result, @"\d+").Value);
                graph_node_ids.Add(node_id);
            }

            graph_node_ids.Sort();
            graph_node_ids = graph_node_ids.Distinct().ToList();
            QUtils.AddLog("GetGraphNodeIds() returned  list : " + graph_node_ids);
            return graph_node_ids;
        }

        internal static List<KeyValuePair<int, List<int>>> GetNodes4Graph(int level = -1, bool exportData = false, bool exportDetails = false, bool hasAI = true, bool hasGraph = false, bool hasPatrol = false)
        {
            List<KeyValuePair<int, List<int>>> graphNodesList = new List<KeyValuePair<int, List<int>>>();

            //For current level.
            if (level == -1) level = QMemory.GetCurrentLevel();
            string graphNodesDetails = "GraphNodesDetails_Level_" + level + ".txt";
            string graphNodesData = "GraphNodesData_Level_" + level + ".txt";

            string input_qsc_path = QUtils.cfgInputQscPath + level + "\\" + QUtils.objectsQsc;
            QUtils.AddLog("GetNodes4Graph() : called with level : " + level);
            string data = QCryptor.Decrypt(input_qsc_path);

            var qscDataLines = data.Split('\n');
            string idData = null;

            foreach (var qscData in qscDataLines)
            {
                if (qscData.Contains("Task_New") && qscData.Contains("HumanAI") && qscData.Contains("AITYPE"))
                {
                    var qscLine = qscData.Split(',');
                    int qscLineLen = qscLine.Length;

                    var aiStr = qscLine[0];
                    var aiId = aiStr.Substring(aiStr.IndexOf("Task_New("));
                    var graphId = qscLine[4];
                    aiId = Regex.Match(aiId, @"\d+").Value;
                    graphId = Regex.Match(graphId, @"\d+").Value;
                    QUtils.AddLog("GetNodes4Graph() : Regex aiId : " + aiId + "\tgraphId : " + graphId);


                    string aiFileName = aiId + ".qsc";
                    var inputAiPath = QUtils.cfgInputQscPath + level + "\\ai\\" + aiFileName;

                    var aiFileData = QCryptor.Decrypt(inputAiPath);
                    var aiFileLines = aiFileData.Split('\n');
                    foreach (var aiLine in aiFileLines)
                    {
                        if (aiLine.Contains("AIAction_Patrol"))
                        {
                            var aiPatrolLine = aiLine.Split(',')[0];
                            string patrolId = Regex.Match(aiPatrolLine, @"\d+").Value;
                            if (hasAI) idData += "AI : " + aiId;
                            if (hasGraph) idData += "\tGraphId : " + graphId;
                            if (hasPatrol) idData += "\tPatrolId : " + patrolId;
                            idData += "\n";
                            QUtils.AddLog("GetNodes4Graph() patrolId : " + patrolId.Trim());
                            var nodeId = GetNodes4mPatrolId(patrolId, level);
                            var iGraphId = Convert.ToInt32(graphId);
                            graphNodesList.Add(new KeyValuePair<int, List<int>>(iGraphId, nodeId));
                        }
                    }
                }
            }
            if (exportDetails)
            {
                QUtils.SaveFile(graphNodesDetails, idData);
            }
            if (exportData)
            {
                idData = null;
                foreach (var graphNode in graphNodesList)
                {
                    idData += "Graph : " + graphNode.Key + "\tNodes : ";
                    foreach (var key in graphNode.Value)
                    {
                        idData += key + ",";
                    }
                    idData += "\n";
                }
                QUtils.SaveFile(graphNodesData, idData);
            }
            return graphNodesList;
        }

        internal static List<int> GetNodes4mPatrolId(string patrolId, int level)
        {
            List<int> nodesId = new List<int>();
            string input_qsc_path = QUtils.cfgInputQscPath + level + "\\" + QUtils.objectsQsc;
            QUtils.AddLog("GetNodes4mPatrolId() : called with level : " + level);
            string data = QCryptor.Decrypt(input_qsc_path);

            var dataLines = data.Split('\n');
            bool patrolLine = false;
            string nodeRegex = "node id [0-9]*";

            QUtils.AddLog("GetNodes4mPatrolId() : called with patrolId : " + patrolId);


            foreach (var dataLine in dataLines)
            {
                if (dataLine.Contains("PatrolPath") && dataLine.Contains(patrolId))
                {
                    QUtils.AddLog("GetNodes4mPatrolId() patrolLine : " + patrolLine);
                    patrolLine = true;
                }

                else if (patrolLine && dataLine.Contains("PatrolPathCommand"))
                {
                    if (dataLine.Contains("node id"))
                    {
                        QUtils.AddLog("GetNodes4mPatrolId() dataLine : " + dataLine);
                        var matchesResult = Regex.Matches(dataLine, nodeRegex).Cast<Match>().Select(m => m.Value).ToList();

                        foreach (var matchResult in matchesResult)
                        {
                            var nodeId = Int32.Parse(Regex.Match(matchResult, @"\d+").Value);
                            QUtils.AddLog("GetNodes4mPatrolId() node id : " + nodeId);
                            nodesId.Add(nodeId);
                        }
                    }
                }
                else
                {
                    patrolLine = false;
                }
            }
            QUtils.AddLog("GetNodes4mPatrolId() nodesId count : " + nodesId.Count);
            return nodesId;
        }

        internal static List<int> GetAllNodes4mGraph(int graphId)
        {
            var graphNodesList = GetNodes4Graph();
            List<int> nodeIds = new List<int>();
            foreach (var graphNode in graphNodesList)
            {
                if (graphNode.Key == graphId)
                {
                    foreach (var key in graphNode.Value)
                    {
                        nodeIds.Add(key);
                    }
                }
            }

            if (nodeIds != null)
            {
                if (nodeIds.Count < 1)
                {
                    return null;
                }
                nodeIds.Sort();
                nodeIds = nodeIds.Distinct().ToList();
            }
            return nodeIds;
        }

        internal static List<QTaskGraph> GetQTaskGraphList(bool sorted = false, bool from_backup = false, int level = -1)
        {
            //For current level.
            if (level == -1) level = QMemory.GetCurrentLevel();

            string input_qsc_path = QUtils.cfgInputQscPath + level + "\\" + QUtils.objectsQsc;
            QUtils.AddLog("GetQTaskGraphList() : called with level : " + level + " backup : " + from_backup);
            string qsc_data = from_backup ? QCryptor.Decrypt(input_qsc_path) : QUtils.LoadFile();

            var qtask_list = ParseGraphData(qsc_data);

            if (sorted) qtask_list = qtask_list.OrderBy(q => q.id).ToList();
            return qtask_list;
        }

        //Parse the Objects.
        private static List<QTaskGraph> ParseGraphData(string qsc_data)
        {
            //Remove all whitespaces.
            qsc_data = qsc_data.Replace("\t", String.Empty);
            string[] qsc_data_split = qsc_data.Split('\n');

            var qtask_list = new List<QTaskGraph>();
            foreach (var data in qsc_data_split)
            {
                if (data.Contains(QUtils.taskNew))
                {
                    var start_index = data.IndexOf(',') + 1;
                    var end_index = data.IndexOf(',', start_index);
                    var task_name = data.Slice(start_index, end_index).Trim().Replace("\"", String.Empty);

                    if (task_name.Contains(QUtils.aiGraphTask))
                    {
                        var qtask = new QTaskGraph();
                        Real64 position = new Real64();
                        Graph graph = new Graph();

                        string[] task_new = data.Split(',');
                        int task_index = 0;

                        foreach (var task in task_new)
                        {
                            if (task_index == (int)QTASKINFO.QTASK_ID)
                            {
                                var task_id = task.Substring(task.IndexOf('(') + 1);
                                qtask.id = Convert.ToInt32(task_id);
                            }
                            else if (task_index == (int)QTASKINFO.QTASK_NAME)
                                qtask.name = task.Trim();

                            else if (task_index == (int)QTASKINFO.QTASK_NOTE)
                                qtask.note = task.Trim();

                            else if (task_index == (int)QTASKINFO.QTASK_POSX)
                                position.x = Double.Parse(task);

                            else if (task_index == (int)QTASKINFO.QTASK_POSY)
                                position.y = Double.Parse(task);

                            else if (task_index == (int)QTASKINFO.QTASK_POSZ)
                                position.z = Double.Parse(task);

                            else if (task_index == (int)QTASKINFO.QTASK_UPDATE)
                                qtask.update = Boolean.Parse(task.Trim());

                            else if (task_index == (int)QTASKINFO.QTASK_GRAPH_DATA_X)
                                graph.x = Int32.Parse(task.Trim());

                            else if (task_index == (int)QTASKINFO.QTASK_GRAPH_DATA_Y)
                                graph.y = Int32.Parse(task.Trim());

                            else if (task_index == (int)QTASKINFO.QTASK_GRAPH_DATA_Z)
                                graph.z = Int32.Parse(task.Trim());

                            else if (task_index == (int)QTASKINFO.QTASK_MID_OFFSET)
                                qtask.mid_offsets = Double.Parse(task);

                            else if (task_index == (int)QTASKINFO.QTASK_TOP_OFFSET)
                                qtask.top_offsets = Double.Parse(task);

                            else if (task_index == (int)QTASKINFO.QTASK_HEIGH_DIFF)
                                qtask.height_diff = Double.Parse(task);

                            else if (task_index == (int)QTASKINFO.QTASK_NODE_WIDTH)
                                qtask.node_width = Double.Parse(task);

                            else if (task_index == (int)QTASKINFO.QTASK_GROUND_DIST)
                                qtask.ground_dist = Double.Parse(task);

                            else if (task_index == (int)QTASKINFO.QTASK_USE_PRECISE)
                                qtask.use_precise = Boolean.Parse(task.Trim());

                            else if (task_index == (int)QTASKINFO.QTASK_PRECISE_STEP_VAL)
                                qtask.precise_step_val = Double.Parse(task.Trim().Replace(")", String.Empty));

                            qtask.position = position;
                            qtask.graph = graph;
                            task_index++;
                        }
                        qtask_list.Add(qtask);
                    }
                }
            }
            return qtask_list;
        }

        internal static string ShowGraphVisual(string qsc_data, int nGraphs = -1)
        {
            int total_graphs = GetGraphIds().Count, graph_count = 0;
            if (nGraphs == -1) nGraphs = total_graphs - 1;
            var qtask_list = GetQTaskGraphList(true, true, QUtils.gGameLevel);
            int width = 50000;
            QUtils.AddLog("ShowGraphVisual() called with nGraphs : " + nGraphs);

            if (nGraphs > total_graphs)
            {
                QUtils.ShowError("Graph cannot be greater than max graphs");
                return null;
            }

            foreach (var qtask in qtask_list)
            {
                if (graph_count > nGraphs) break;

                qsc_data += "Task_New(-1, \"Wire\",\"Graph Wire\"," + qtask.position.x + "," + qtask.position.y + "," + qtask.position.z + "," + qtask.position.x + width + "," + qtask.position.y + width + "," + qtask.position.z + width + "," + "\"320_01_1\");" + "\n";
                graph_count++;
            }
            return qsc_data;
        }

        internal static bool CheckIdExist(string id, string idType, int game_level, string errMsg = "")
        {
            bool status = false;
            if (idType == "AI")
                GetNodes4Graph(game_level, false, true, true, false, false);

            else if (idType == "Graph")
                GetNodes4Graph(game_level, false, true, false, true, false);

            else if (idType == "Patrol")
                GetNodes4Graph(game_level, false, true, false, false, true);

            string nodesFile = "GraphNodesDetails_Level_" + game_level + ".txt";
            string fileData = QUtils.LoadFile(nodesFile);

            if (fileData.Contains(id) && idType != "Graph")
            {
                QUtils.ShowError(errMsg);
                status = true;
            }
            else if (!fileData.Contains(id) && idType == "Graph")
            {
                QUtils.ShowError(errMsg);
                status = false;
            }
            else if (fileData.Contains(id) && idType == "Graph")
                status = true;

            System.IO.File.Delete(nodesFile);
            return status;
        }

        internal static List<KeyValuePair<int, string>> GetGraphAreaList(int level)
        {
            var graphList = GetQTaskGraphList(true, true, level);
            List<KeyValuePair<int, string>> graphNames = new List<KeyValuePair<int, string>>();

            foreach (var graph in graphList)
            {
                if (!String.IsNullOrEmpty(graph.note))
                    graphNames.Add(new KeyValuePair<int, string>(graph.id, graph.note));
            }
            return graphNames;
        }

        internal static string GetGraphArea(int graphId, int level)
        {
            var graphNamesList = GetGraphAreaList(level);
            bool graphIdExist = false;
            string graphName = null;

            foreach (var graph in graphNamesList)
            {
                if (graph.Key == graphId)
                {
                    graphName = graph.Value;
                    graphIdExist = true;
                    break;
                }
            }

            if (!graphIdExist)
            {
                QUtils.ShowError("GraphId + " + graphId + " doesn't exist for current level");
                return graphName;
            }
            return graphName;
        }

    }
}
