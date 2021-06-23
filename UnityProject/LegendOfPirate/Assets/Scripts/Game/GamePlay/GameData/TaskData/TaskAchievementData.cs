
using System.Collections.Generic;
using Qarth;
using QuickEngine.Extensions;
using System;
namespace GameWish.Game
{
    [Serializable]
    public class TaskAchievementData
    {
        public TaskAchievementData()
        {

        }

        public List<TaskItemData> listTaskItemData;
        Dictionary<int, TaskItemData> dicTaskItemData;
        private TaskData m_TaskData;
        public void InitWithEmptyData()
        {
            listTaskItemData = new List<TaskItemData>();
            SetDataDirty();
        }

        public void OnDataLoadFinish()
        {
            //SetDirtyRecorder(TaskDataMgr.dataDirtyRecorder);
            EventSystem.S.Register(EventID.AchievementTimesAdd, AddTaskTimes);
        }

        public TaskItemData GetTaskItemData(int keyId)
        {
            if (!GetDicItemData().ContainsKey(keyId))
            {
                TaskItemData record = new TaskItemData(keyId);
                //record.SetDirtyRecorder(TaskDataMgr.dataDirtyRecorder);
                GetDicItemData().TryAddKey(keyId, record);
                listTaskItemData.Add(record);
                SetDataDirty();
            }
            return GetDicItemData()[keyId];
        }

        Dictionary<int, TaskItemData> GetDicItemData()
        {
            if (dicTaskItemData == null)
            {
                dicTaskItemData = new Dictionary<int, TaskItemData>();
                if (listTaskItemData == null)
                {
                    listTaskItemData = new List<TaskItemData>();
                }

                for (int i = 0; i < listTaskItemData.Count; i++)
                {
                    //listTaskItemData[i].SetDirtyRecorder(m_Recorder);
                    dicTaskItemData.Add(listTaskItemData[i].m_IdKey, listTaskItemData[i]);
                }
            }
            return dicTaskItemData;
        }

        void AddTaskTimes(int key, params object[] parm)
        {
            EventSystem.S.Send(EventID.AchievementTaskRefresh, null);
        }

        // public void SetTaskComplete(int key)
        // {
        //     int sum = TDTaskAchievementTable.GetData(key).targetTimes;
        //     GetTaskItemData(key).SetComplete(sum);
        //     EventSystem.S.Send(TaskEventId.TaskRefreshOnce, key);
        // }

        // public bool CheckExistReward()
        // {
        //     bool exist = false;
        //     int starSum = PuzzleMatchManager.instance.starSystem.GetAllStar();
        //     for (int i = 0; i < TDTaskAchievementTable.count; i++)
        //     {
        //         if (!GetTaskItemData(TDTaskAchievementTable.dataList[i].id).m_IsGetRewardToday)
        //         {
        //             if (starSum >= TDTaskAchievementTable.dataList[i].targetTimes)
        //             {
        //                 exist = true;
        //             }
        //         }
        //     }
        //     return exist;
        // }

        private void SetDataDirty()
        {
            if (m_TaskData == null)
            {
                m_TaskData = GameDataMgr.S.GetData<TaskData>();
            }
            m_TaskData.SetDataDirty();
        }

    }

}
