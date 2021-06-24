using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using QuickEngine.Extensions;
using System;
namespace GameWish.Game
{
    [Serializable]
    public class TaskDailyData
    {
        public TaskDailyData()
        {

        }

        public List<TaskItemData> listTaskItemData;
        Dictionary<int, TaskItemData> dicTaskItemData;
        public int activeNum = 0;
        public int[] getRewardList;
        private TaskData m_TaskData;
        public void InitWithEmptyData()
        {
            listTaskItemData = new List<TaskItemData>();
            getRewardList = new int[] { 0, 0, 0, 0, 0 };
            SetDataDirty();
        }

        public void OnDataLoadFinish()
        {
            EventSystem.S.Register(EventID.DailyTimesAdd, AddTaskTimes);
        }


        public void AddActiveNum(int count)
        {
            activeNum += count;
            SetDataDirty();
        }
        public TaskItemData GetTaskItemData(int keyId)
        {
            if (!GetDicItemData().ContainsKey(keyId))
            {
                TaskItemData record = new TaskItemData(keyId);
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
                    dicTaskItemData.Add(listTaskItemData[i].idKey, listTaskItemData[i]);
                }
            }
            return dicTaskItemData;
        }

        public void NewDay()
        {
            activeNum = 0;
            getRewardList = new int[] { 0, 0, 0, 0, 0 };
            for (int i = 0; i < TDDailyTaskTable.count; i++)
            {
                GetTaskItemData(TDDailyTaskTable.dataList[i].taskID).NewDay();
            }
            SetDataDirty();
        }
        //state = 1 代表已领取 0 代表未领取
        public void SetGetRewardState(int index, int state)
        {
            getRewardList[index] = state;
            SetDataDirty();
        }

        void AddTaskTimes(int key, params object[] parm)
        {
            int idKey = (int)(parm[0]);
            GetTaskItemData(idKey).AddTaskTimes();
            EventSystem.S.Send(EventID.DailyTaskRefresh, idKey);
        }

        public void SetTaskComplete(int key)
        {
            int sum = TDDailyTaskTable.GetData(key).taskCount;
            GetTaskItemData(key).SetComplete(sum);
            EventSystem.S.Send(EventID.DailyTaskRefresh, key);
        }

        public bool CheckExistReward()
        {
            bool exist = false;
            for (int i = 0; i < TDDailyTaskTable.count; i++)
            {
                if (!GetTaskItemData(TDDailyTaskTable.dataList[i].taskID).isGetRewardToday)
                {
                    if (GetTaskItemData(TDDailyTaskTable.dataList[i].taskID).curCompleteTimes >= TDDailyTaskTable.dataList[i].taskCount)
                    {
                        exist = true;
                    }
                }
            }
            return exist;
        }

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
