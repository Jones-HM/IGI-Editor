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
            string inputQscPath =  cfgQscPath + level + "\\" + objectsQsc;
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

        internal static int GenerateTaskID(bool minimalId = false)
        {
            List<int> qidsList = new List<int>();
            QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "called minimal Id : " + minimalId);

            var qscData = LoadFile();
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
                            var taskId = task.Substring(task.IndexOf('(') + 1);
                            int qid = Convert.ToInt32(taskId);
                            if (qid > 10)//10 reserved for LevelFlow Id.
                                qidsList.Add(qid);
                            break;
                        }
                    }

                }
            }

            qidsList.Sort();
            QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "sorting done");

            int qtaskId = qidsList[0] + 1;
            int maxVal = qidsList[0], minVal = qidsList[1];

            if (minimalId)
            {
                int diffVal = 0;
                for (int index = 0; index < qidsList.Count; index++)
                {
                    minVal = qidsList[index];
                    maxVal = qidsList[index + 1];
                    diffVal = Math.Abs(maxVal - minVal);
                    QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "maxVal : " + maxVal + "\tminVal : " + minVal + "\tdiffVal : " + diffVal);

                    if (diffVal >= 50)
                    {
                        qtaskId = minVal + 1;
                        break;
                    }
                }
            }
            else
            {
                qidsList.Reverse();
                maxVal = qidsList[0];
                qtaskId = maxVal + 1;
            }
            QUtils.AddLog(MethodBase.GetCurrentMethod().Name, "Returned Task Id: " + qtaskId);
            return qtaskId;
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
