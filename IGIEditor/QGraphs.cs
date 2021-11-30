using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            public bool usePrecise;
            public Int32 graphData;
            public Double midOffsets;
            public Double topOffsets;
            public Double heightDiff;
            public Double nodeWidth;
            public Double groundDist;
            public Double preciseStepVal;
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

        internal static List<int> GetGraphIds(int gameLevel = -1)
        {
            return GetGraphNodeIds(gameLevel);
        }

        internal static Real64 GetGraphPosition(int graphId)
        {
            return GetGraphPosition(graphId.ToString());
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


        private static List<int> GetGraphNodeIds(int level = -1)
        {
            List<int> graphNodeIds = new List<int>();
            string graphRegex = "[0-9]{0,4}, \"AIGraph\"";
            string selectedRegex = graphRegex;

            //For current level.
            if (level == -1) level = QMemory.GetCurrentLevel();

            QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "called with level " + level);

            string inputQscPath = QUtils.cfgQscPath + level + "\\" + QUtils.objectsQsc;
            string qscData = QUtils.LoadFile(inputQscPath);

            QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "inputQscPath : '" + inputQscPath + "'");

            var matchesResult = Regex.Matches(qscData, selectedRegex).Cast<Match>().Select(m => m.Value).ToList();

            foreach (var matchResult in matchesResult)
            {
                var nodeId = Int32.Parse(Regex.Match(matchResult, @"\d+").Value);
                graphNodeIds.Add(nodeId);
            }

            graphNodeIds.Sort();
            graphNodeIds = graphNodeIds.Distinct().ToList();
            QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "returned  list count : " + graphNodeIds.Count);
            return graphNodeIds;
        }

        internal static List<KeyValuePair<int, List<int>>> GetNodes4Graph(int level = -1, bool exportData = false, bool exportDetails = false, bool hasAI = true, bool hasGraph = false, bool hasPatrol = false)
        {
            List<KeyValuePair<int, List<int>>> graphNodesList = new List<KeyValuePair<int, List<int>>>();

            //For current level.
            if (level == -1) level = QMemory.GetCurrentLevel();
            string graphNodesDetails = "GraphNodesDetails_Level_" + level + ".txt";
            string graphNodesData = "GraphNodesData_Level_" + level + ".txt";

            string inputQscPath = QUtils.cfgQscPath + level + "\\" + QUtils.objectsQsc;
            QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "called with level : " + level);
            string data = QUtils.LoadFile(inputQscPath);

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
                    QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "Regex aiId : " + aiId + "\tgraphId : " + graphId);


                    string aiFileName = aiId + ".qsc";
                    var inputAiPath = QUtils.cfgQscPath + level + "\\ai\\" + aiFileName;

                    var aiFileData = QUtils.LoadFile(inputAiPath);
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
                            QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "Patrol Id : " + patrolId.Trim());
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
            string inputQscPath = QUtils.cfgQscPath + level + "\\" + QUtils.objectsQsc;
            QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "called with level : " + level);
            string data = QUtils.LoadFile(inputQscPath);

            var dataLines = data.Split('\n');
            bool patrolLine = false;
            string nodeRegex = "node id [0-9]*";

            QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "called with Patrol Id : " + patrolId);


            foreach (var dataLine in dataLines)
            {
                if (dataLine.Contains("PatrolPath") && dataLine.Contains(patrolId))
                {
                    QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "Patrol Line : " + patrolLine);
                    patrolLine = true;
                }

                else if (patrolLine && dataLine.Contains("PatrolPathCommand"))
                {
                    if (dataLine.Contains("node id"))
                    {
                        QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "Data Line : " + dataLine);
                        var matchesResult = Regex.Matches(dataLine, nodeRegex).Cast<Match>().Select(m => m.Value).ToList();

                        foreach (var matchResult in matchesResult)
                        {
                            var nodeId = Int32.Parse(Regex.Match(matchResult, @"\d+").Value);
                            QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "Node id : " + nodeId);
                            nodesId.Add(nodeId);
                        }
                    }
                }
                else
                {
                    patrolLine = false;
                }
            }
            QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "Nodes Id count : " + nodesId.Count);
            return nodesId;
        }

        internal static List<int> GetNodesForGraph(int graphId)
        {
            List<int> graphNodeIds = new List<int>();
            string graphFile = QUtils.graphsPath + "\\" + "graph" + graphId + QUtils.datExt;

            QUtils.graphNodesList = QGraphs.ReadGraphNodeData(graphFile);
            int totalNodes = QUtils.graphNodesList.Count;
            int graphWorkCount = 1, graphWorkPercent = 1;

            foreach (var node in QUtils.graphNodesList)
            {
                if (node.NodeId > 0 && node.NodeId < 5000)
                {
                    graphNodeIds.Add(node.NodeId);
                    graphWorkPercent = (int)Math.Round((double)(100 * graphWorkCount) / totalNodes);
                    IGIEditorUI.editorRef.SetStatusText("Graph#" + graphId + " Node#" + node.NodeId + " updating, Completed " + graphWorkPercent + "%");
                    graphWorkCount++;
                }
            }

            QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "GraphFile: '" + graphFile + "'" + " NodeId Count: " + graphNodeIds.Count);
            return graphNodeIds;
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

        internal static List<QTaskGraph> GetQTaskGraphList(bool sorted = false, bool fromBackup = false, int level = -1)
        {
            //For current level.
            if (level == -1) level = QMemory.GetCurrentLevel();

            string inputQscPath = QUtils.cfgQscPath + level + "\\" + QUtils.objectsQsc;
            QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "Called with level : " + level + " backup : " + fromBackup);
            string qscData = fromBackup ? QUtils.LoadFile(inputQscPath) : QUtils.LoadFile();

            var qtaskList = ParseGraphData(qscData);

            if (sorted) qtaskList = qtaskList.OrderBy(q => q.id).ToList();
            return qtaskList;
        }

        //Parse the Objects.
        private static List<QTaskGraph> ParseGraphData(string qscData)
        {
            if (String.IsNullOrEmpty(qscData)) return null;
            //Remove all whitespaces.
            qscData = qscData.Replace("\t", String.Empty);
            string[] qscDataSplit = qscData.Split('\n');

            var qtaskList = new List<QTaskGraph>();
            foreach (var data in qscDataSplit)
            {
                if (data.Contains(QUtils.taskNew))
                {
                    var startIndex = data.IndexOf(',') + 1;
                    var endIndex = data.IndexOf(',', startIndex);
                    var taskName = data.Slice(startIndex, endIndex).Trim().Replace("\"", String.Empty);

                    if (taskName.Contains(QUtils.aiGraphTask))
                    {
                        var qtask = new QTaskGraph();
                        Real64 position = new Real64();
                        Graph graph = new Graph();

                        string[] taskNew = data.Split(',');
                        int taskIndex = 0;

                        foreach (var task in taskNew)
                        {
                            if (taskIndex == (int)QTASKINFO.QTASK_ID)
                            {
                                var taskId = task.Substring(task.IndexOf('(') + 1);
                                qtask.id = Convert.ToInt32(taskId);
                            }
                            else if (taskIndex == (int)QTASKINFO.QTASK_NAME)
                                qtask.name = task.Trim();

                            else if (taskIndex == (int)QTASKINFO.QTASK_NOTE)
                                qtask.note = task.Trim();

                            else if (taskIndex == (int)QTASKINFO.QTASK_POSX)
                                position.x = Double.Parse(task);

                            else if (taskIndex == (int)QTASKINFO.QTASK_POSY)
                                position.y = Double.Parse(task);

                            else if (taskIndex == (int)QTASKINFO.QTASK_POSZ)
                                position.z = Double.Parse(task);

                            else if (taskIndex == (int)QTASKINFO.QTASK_UPDATE)
                                qtask.update = Boolean.Parse(task.Trim());

                            else if (taskIndex == (int)QTASKINFO.QTASK_GRAPH_DATA_X)
                                graph.x = Int32.Parse(task.Trim());

                            else if (taskIndex == (int)QTASKINFO.QTASK_GRAPH_DATA_Y)
                                graph.y = Int32.Parse(task.Trim());

                            else if (taskIndex == (int)QTASKINFO.QTASK_GRAPH_DATA_Z)
                                graph.z = Int32.Parse(task.Trim());

                            else if (taskIndex == (int)QTASKINFO.QTASK_MID_OFFSET)
                                qtask.midOffsets = Double.Parse(task);

                            else if (taskIndex == (int)QTASKINFO.QTASK_TOP_OFFSET)
                                qtask.topOffsets = Double.Parse(task);

                            else if (taskIndex == (int)QTASKINFO.QTASK_HEIGH_DIFF)
                                qtask.heightDiff = Double.Parse(task);

                            else if (taskIndex == (int)QTASKINFO.QTASK_NODE_WIDTH)
                                qtask.nodeWidth = Double.Parse(task);

                            else if (taskIndex == (int)QTASKINFO.QTASK_GROUND_DIST)
                                qtask.groundDist = Double.Parse(task);

                            else if (taskIndex == (int)QTASKINFO.QTASK_USE_PRECISE)
                                qtask.usePrecise = Boolean.Parse(task.Trim());

                            else if (taskIndex == (int)QTASKINFO.QTASK_PRECISE_STEP_VAL)
                                qtask.preciseStepVal = Double.Parse(task.Trim().Replace(")", String.Empty));

                            qtask.position = position;
                            qtask.graph = graph;
                            taskIndex++;
                        }
                        qtaskList.Add(qtask);
                    }
                }
            }
            return qtaskList;
        }

        internal static string ShowGraphVisual(string qscData, int nGraphs = -1)
        {
            int totalGraphs = GetGraphIds().Count, graphCount = 0;
            if (nGraphs == -1) nGraphs = totalGraphs - 1;
            var qtaskList = GetQTaskGraphList(true, true, QUtils.gGameLevel);
            int width = 50000;
            QUtils.AddLog(MethodBase.GetCurrentMethod().Name,"called with nGraphs : " + nGraphs);

            if (nGraphs > totalGraphs)
            {
                QUtils.ShowLogError(MethodBase.GetCurrentMethod().Name, "Graph cannot be greater than max graphs");
                return null;
            }

            foreach (var qtask in qtaskList)
            {
                if (graphCount > nGraphs) break;

                qscData += "Task_New(-1, \"Wire\",\"Graph Wire\"," + qtask.position.x + "," + qtask.position.y + "," + qtask.position.z + "," + qtask.position.x + width + "," + qtask.position.y + width + "," + qtask.position.z + width + "," + "\"320_01_1\");" + "\n";
                graphCount++;
            }
            return qscData;
        }

        internal static string ShowGraphNodesVisual(int graphId, int visualType = 1, string nodeObject = "000_00_0", string markerColor = "MARKER_COLOR_YELLOW")
        {
            string qscData = null;
            string graphFile = QUtils.graphsPath + "\\" + "graph" + graphId + QUtils.datExt;
            QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "Graph File: '" + graphFile + "'");
            var nodeData = ReadGraphNodeData(graphFile);
            var graphPos = GetGraphPosition(graphId.ToString());
            QUtils.qtaskId = QTask.GenerateTaskID(true);
            var graphArea = GetGraphArea(graphId);

            int graphWorkTotal = nodeData.Count, graphWorkCount = 1, graphWorkPercent = 1;

            foreach (var node in nodeData)
            {
                if (node.NodeId < 0 || node.NodeId > 5000) continue;

                QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "Node_" + node.NodeId + " X: " + node.NodePos.x + " Y: " + node.NodePos.y + " Z: " + node.NodePos.z + " Criteria: " + node.NodeCriteria);

                var nodeRealPos = new Real64();
                nodeRealPos.x = graphPos.x + node.NodePos.x;
                nodeRealPos.y = graphPos.y + node.NodePos.y;
                nodeRealPos.z = graphPos.z + node.NodePos.z;

                string taskNote = "Graph#" + graphId + "Node#" + node.NodeId + " - G#" + graphId + "N#1";
                string taskInfo = taskNote.Slice(0, taskNote.IndexOf("-")).Trim();
                //Visualisation Object - StatusMsg.
                if (visualType == 1)
                {
                    IGIEditorUI.editorRef.AddRigidObject(nodeObject, true, nodeRealPos, false, -1, taskNote, false);

                    //var areaDim = new AreaDim(8000);
                    //qscData += QObjects.AddAreaActivate(QUtils.qtaskId, nodeObject, null, "\"" + taskNote + "\"", ref nodeRealPos, ref areaDim);
                }

                //Visualisation Hilight - ComputerMap Hilight.
                else if (visualType == 2)
                {
                    IGIEditorUI.editorRef.AddRigidObject(nodeObject, false, nodeRealPos, false, QUtils.qtaskId, taskNote, false);
                    qscData += QObjects.ComputerMapHilight(QUtils.qtaskId, taskNote, "Graph#" + graphId, taskInfo, "MARKER_BOX", markerColor);
                }
                graphWorkPercent = (int)Math.Round((double)(100 * graphWorkCount) / graphWorkTotal);
                IGIEditorUI.editorRef.SetStatusText("Graph#" + graphId + " Node#" + node.NodeId + " added, Completed " + graphWorkPercent + "%");
                QUtils.qtaskId++;
                graphWorkCount++;
            }
            return qscData;
        }

        internal static string ShowNodeLinksVisual(int graphId, ref Real64 graphPos)
        {
            QInternals.GraphNodeLinks(graphId.ToString());
            var dataLines = QInternals.InternalDataReadLines();
            string graphLinkTag;

            if (dataLines.Length < 4 || dataLines.Length >= 50) { return null; }
            int graphWorkTotal = dataLines.Length, graphWorkCount = 1, graphWorkPercent = 1;

            string qscData = null;
            foreach (var data in dataLines)
            {
                try
                {
                    var lineData = data.Split('=');
                    int nodeId1 = Convert.ToInt32(lineData[0].Trim());
                    int nodeId2 = Convert.ToInt32(lineData[1].Trim());
                    var nodeId1Pos = RealGrapNodePos(nodeId1, graphId, ref graphPos);
                    var nodeId2Pos = RealGrapNodePos(nodeId2, graphId, ref graphPos);
                    if (nodeId1Pos.Empty() || nodeId2Pos.Empty()) continue; //Skip On empty positions.

                    graphLinkTag = "Graph#" + graphId + "Link#" + nodeId1 + "-Link#" + nodeId2 + " - G#" + graphId + "L#1";
                    qscData += QObjects.AddWire(nodeId1Pos, nodeId2Pos, graphLinkTag);
                    graphWorkPercent = (int)Math.Round((double)(100 * graphWorkCount) / graphWorkTotal);
                    IGIEditorUI.editorRef.SetStatusText("Graph#" + graphId + " Link#" + graphWorkCount + " added, Completed " + graphWorkPercent + "%");
                    graphWorkCount++;
                }
                catch (Exception ex)
                {
                    QUtils.ShowLogException(MethodBase.GetCurrentMethod().Name, ex);
                }
            }
            return qscData;
        }

        private static Real64 RealGrapNodePos(int nodeId, int graphId, ref Real64 graphPos)
        {
            Real64 nodeRealPos = null;
            try
            {
                var node = QGraphs.GetGraphNodeData(graphId, nodeId);
                //Get Node Real Position.
                //nodeRealPos = new Real64().Real64Operator(graphPos, node.NodePos, "+");
                nodeRealPos = new Real64();
                nodeRealPos.x = graphPos.x + node.NodePos.x;
                nodeRealPos.y = graphPos.y + node.NodePos.y;
                nodeRealPos.z = graphPos.z + node.NodePos.z + 4500.0f;
            }
            catch (Exception ex)
            {
                QUtils.ShowLogException(MethodBase.GetCurrentMethod().Name, ex);
            }
            return nodeRealPos;
        }

        internal static bool RemoveNodeLinks(int graphId = 1, int nodeId = 1, string nodeLinkNote = "")
        {
            var qscData = QUtils.LoadFile();
            int linkCount = 0;
            string nodeLinkObj = (nodeLinkNote.Contains("N#")) ? "EditRigidObj" : "Wire";

            string levelFlowTaskId = "Task_New(10,";
            int qtaskLevelFlowIndex = qscData.IndexOf(levelFlowTaskId);

            var qscLevelFlowSub = qscData.Substring(qtaskLevelFlowIndex);
            var qscDataLines = qscLevelFlowSub.Split('\n');

            foreach (var qscDataLine in qscDataLines)
            {
                if (qscDataLine.Contains(nodeLinkObj) && qscDataLine.Contains(nodeLinkNote))
                {
                    int qtaskIndex = qscData.IndexOf(qscDataLine);
                    qscData = qscData.Remove(qtaskIndex, qscDataLine.Length);
                    linkCount++;
                }
            }
            qscData = Regex.Replace(qscData, @"^\s*$", "", RegexOptions.Multiline);
            if (!String.IsNullOrEmpty(qscData)) QCompiler.CompileEx(qscData);
            return (linkCount > 0) ? true : false;
        }


        internal static bool CheckIdExist(string id, string idType, int gameLevel, string errMsg = "")
        {
            bool status = false;
            if (idType == "AI")
                GetNodes4Graph(gameLevel, false, true, true, false, false);

            else if (idType == "Graph")
                GetNodes4Graph(gameLevel, false, true, false, true, false);

            else if (idType == "Patrol")
                GetNodes4Graph(gameLevel, false, true, false, false, true);

            string nodesFile = "GraphNodesDetails_Level_" + gameLevel + ".txt";
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

        internal static string GetGraphArea(int graphId)
        {
            string graphFile = QUtils.qGraphsPath + "\\Areas\\" + "graph_area_level" + QUtils.gGameLevel + ".txt";
            QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "Level: " + QUtils.gGameLevel + " graphId: " + graphId + " graphFile: " + graphFile);
            if (!System.IO.File.Exists(graphFile)) { return "Area Not Available."; }

            if (QUtils.graphAreas.Count == 0) QUtils.graphAreas = GetGraphAreasList(graphFile);

            foreach (var graph in QUtils.graphAreas)
            {
                if (graph.Key == graphId) return graph.Value;
            }
            return null;
        }

        internal static Dictionary<int, string> GetGraphAreasList(string graphFile)
        {
            var fileData = System.IO.File.ReadAllLines(graphFile);
            var graphAreas = new Dictionary<int, string>();
            string areaSub = "Area : ";

            foreach (var data in fileData)
            {
                var graphIdStr = data.Slice(0, data.IndexOf(areaSub)).Trim();
                var graphArea = data.Substring(data.IndexOf(areaSub) + areaSub.Length + 1).Replace("\"", String.Empty).Trim();
                int graphId = Int32.Parse(Regex.Match(graphIdStr, @"\d+").Value);
                graphAreas.Add(graphId, graphArea);
            }
            QUtils.AddLog(MethodBase.GetCurrentMethod().Name, " Level: " + QUtils.gGameLevel + " graphFile: " + graphFile + " retured Area count: " + graphAreas.Count);
            return graphAreas;
        }

        internal static List<GraphNode> ReadGraphNodeData(string graphFile)
        {
            var graphFileData = System.IO.File.ReadAllBytes(graphFile);
            List<GraphNode> graphNodeData = new List<GraphNode>();

            for (int index = 0; index < graphFileData.Length; index++)
            {
                //Find Node signature - 04CE
                if (graphFileData[index] == 0x04 && graphFileData[index + 1] == 0xCE)
                {
                    var graphData = new GraphNode();
                    double x, y, z;
                    var nodeIdData = graphFileData.Skip(index + 8).Take(4).ToArray();
                    int nodeId = BitConverter.ToInt32(nodeIdData, 0);

                    //Read Node X,Y,Z Position offset.

                    int nodePosXIndex = index + 20;
                    int nodePosYIndex = nodePosXIndex + 8;
                    int nodePosZIndex = nodePosYIndex + 8;

                    var nodePosXData = graphFileData.Skip(nodePosXIndex).Take(8).ToArray();
                    x = BitConverter.ToDouble(nodePosXData, 0);

                    var nodePosYData = graphFileData.Skip(nodePosYIndex).Take(8).ToArray();
                    y = BitConverter.ToDouble(nodePosYData, 0);

                    var nodePosZData = graphFileData.Skip(nodePosZIndex).Take(8).ToArray();
                    z = BitConverter.ToDouble(nodePosZData, 0);

                    //Read node criteria.
                    int nodeCriteriaIndex = index + 88 + 1;
                    var nodeCriteriaData = graphFileData.Skip(nodeCriteriaIndex).Take(18).ToArray();
                    string nodeCriteria = System.Text.Encoding.UTF8.GetString(nodeCriteriaData);
                    nodeCriteria = nodeCriteria.IsNonASCII() ? String.Empty : nodeCriteria;

                    //Adding Graph Node data.
                    graphData.NodeId = nodeId;
                    graphData.NodePos = new Real64(x, y, z);
                    graphData.NodeCriteria = nodeCriteria;
                    graphNodeData.Add(graphData);
                }
            }
            return graphNodeData;
        }

        internal static GraphNode GetGraphNodeData(int graphId, int nodeId)
        {
            string graphFile = QUtils.graphsPath + "\\" + "graph" + graphId + QUtils.datExt;
            QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "GraphFile: '" + graphFile + "'");

            if (QUtils.graphNodesList.Count == 0) QUtils.graphNodesList = QGraphs.ReadGraphNodeData(graphFile);

            foreach (var node in QUtils.graphNodesList)
            {
                if (node.NodeId == nodeId)
                    return node;
            }
            return null;
        }

    }
}
