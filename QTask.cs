using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static IGIEditor.QUtils;

namespace IGIEditor
{
    class QTask
    {
        internal static List<QScriptTask> GetQTaskList(bool fullQtaskList = false, bool distinct = false, bool fromBackup = false)
        {
            int level = QMemory.GetCurrentLevel();
            string inputQscPath = cfgQscPath + level + "\\" + objectsQsc;
            QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "called with level : " + level + " fullList : " + fullQtaskList.ToString() + " distinct : " + distinct.ToString() + " backup : " + fromBackup);
            string qscData = fromBackup ? LoadFile(inputQscPath) : LoadFile();

            var qtaskList = fullQtaskList ? QObjects.ParseAllOjects(qscData) : QObjects.ParseObjects(qscData);
            if (distinct)
                qtaskList = qtaskList.GroupBy(p => p.model).Select(g => g.First()).ToList();
            QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "returned list count: " + qtaskList.Count);
            return qtaskList;
        }

        internal static List<QScriptTask> GetQTaskList(int level, bool fullQtaskList = false, bool distinct = false, bool fromBackup = false)
        {
            string inputQscPath = cfgQscPath + level + "\\" + objectsQsc;
            QUtils.AddLog(MethodBase.GetCurrentMethod().Name, " Qsc Path: '" + inputQscPath + "' level : " + level + " full List : " + fullQtaskList.ToString() + " distinct : " + distinct.ToString() + " backup : " + fromBackup);
            string qscData = fromBackup ? LoadFile(inputQscPath) : LoadFile();

            bool isBinary = qscData.HasBinaryContent();
            QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "isBinary: " + isBinary);

            var qtaskList = fullQtaskList ? QObjects.ParseAllOjects(qscData) : QObjects.ParseObjects(qscData);
            if (qtaskList.Count > 0)
                if (distinct) qtaskList = qtaskList.GroupBy(p => p.model).Select(g => g.First()).ToList();
            QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "task List count " + qtaskList.Count);
            return qtaskList;
        }

        internal static string GetUniqueQTaskId(string taskId)
        {
            int uniqueTaskId = 0;
            try
            {
                uniqueTaskId = Convert.ToInt32(taskId);
                uniqueTaskId = GetUniqueQTaskId(uniqueTaskId);

            }
            catch (Exception ex)
            {
                QUtils.ShowLogException(MethodBase.GetCurrentMethod().Name, ex);
            }
            return uniqueTaskId.ToString();
        }

        internal static int GetUniqueQTaskId(int taskId)
        {
            //Generating New Id for Duplicate QTaskId.
            if (QUtils.qIdsList.Contains(taskId))
            {
                QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "Duplicate TaskId: " + taskId + " Generating new TaskId");
                while (QUtils.qIdsList.Contains(taskId++)) ;
                QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "Generated New TaskId: " + taskId);
            }
            QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "Returned New TaskId: " + taskId);
            return taskId;
        }

        internal static int GenerateTaskID(bool minimalId = false, bool fromBackup = false)
        {
            int taskId = 0;
            try
            {
                QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "called minimal Id : " + minimalId + " fromBackup: " + fromBackup + " Level: " + QUtils.gGameLevel);

                string inputQscPath = cfgQscPath + QUtils.gGameLevel + "\\" + objectsQsc;
                string qscData = fromBackup ? LoadFile(inputQscPath) : LoadFile();

                qscData = qscData.Replace("\t", String.Empty);
                string[] qscDataSplit = qscData.Split('\n');

                foreach (var data in qscDataSplit)
                {
                    if (data.Contains(taskNew))
                    {
                        var startIndex = data.IndexOf(',', 14) + 1;
                        var taskName = (data.Slice(13, startIndex));

                        string[] taskNew = data.Split(',');
                        int taskIndex = 0;

                        foreach (var task in taskNew)
                        {
                            if (taskIndex == (int)QTASKINFO.QTASK_ID)
                            {
                                var taskIdx = task.IndexOf('(');
                                if (taskIdx == -1) break;
                                var qidSub = task.Substring(taskIdx + 1);
                                int qid = Convert.ToInt32(qidSub);
                                if (qid == -1) break;
                                if (qid > QUtils.LEVEL_FLOW_TASK_ID)
                                    qIdsList.Add(qid);
                                break;
                            }
                        }
                    }
                }

                qIdsList.Sort();
                QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "sorting done. count: " + qIdsList.Count);

                taskId = qIdsList[0] + 1;
                int maxVal = qIdsList[0], minVal = qIdsList[1];

                if (minimalId)
                {
                    int diffVal = 0;
                    for (int index = 0; index < qIdsList.Count; index++)
                    {
                        minVal = qIdsList[index];
                        maxVal = qIdsList[index + 1];
                        diffVal = Math.Abs(maxVal - minVal);
                        //QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "maxVal : " + maxVal + "\tminVal : " + minVal + "\tdiffVal : " + diffVal);

                        if (diffVal >= QUtils.MAX_MINIMAL_ID_DIFF)
                        {
                            taskId = minVal + 1;
                            break;
                        }
                    }
                }
                else
                {
                    qIdsList.Reverse();
                    maxVal = qIdsList[0];
                    taskId = maxVal + 1;
                }
                taskId = GetUniqueQTaskId(taskId);
                QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "Returned Task Id: " + taskId);
            }
            catch (Exception ex)
            {
                QUtils.ShowLogException(MethodBase.GetCurrentMethod().Name, ex);
            }
            return taskId;
        }



        internal static QScriptTask GetQTask(string taskName)
        {
            AddLog(MethodBase.GetCurrentMethod().Name, " taskName called");
            var qtaskList = GetQTaskList();

            foreach (var qtask in qtaskList)
            {
                if (qtask.model.Contains(taskName))
                {
                    AddLog(MethodBase.GetCurrentMethod().Name, "returned value for Model " + taskName);
                    return qtask;
                }
            }
            AddLog(MethodBase.GetCurrentMethod().Name, "returned : null");
            return null;
        }

        internal static QScriptTask GetQTask(int taskId)
        {
            AddLog(MethodBase.GetCurrentMethod().Name, " taskId called");
            var qtaskList = GetQTaskList();
            foreach (var qtask in qtaskList)
            {
                if (qtask.id == taskId)
                {
                    AddLog(MethodBase.GetCurrentMethod().Name, "returned value for Task_Id " + taskId);
                    return qtask;
                }
            }
            AddLog(MethodBase.GetCurrentMethod().Name, "returned : null");
            return null;
        }

    }
}
