
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
            EventSystem.S.Register(EventID.AchievementTimesAdd, AddTaskTimes);
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

        void AddTaskTimes(int key, params object[] parm)
        {
            int idKey = (int)(parm[0]);
            GetTaskItemData(idKey).AddAchievementTimes();
            EventSystem.S.Send(EventID.AchievementTaskRefresh, idKey);
        }

        public void SetTaskComplete(int key)
        {
            // string m_Target = TDAchievementTaskTable.GetData(key).taskCount;
            // string[] temp = m_Target.Split('|');
            // int[] m_TargetList = new int[temp.Length];
            // for (int i = 0; i < temp.Length; i++)
            // {
            //     m_TargetList[i] = int.Parse(temp[i]);
            // }
            int[] targetList = TDAchievementTaskTable.GetTargetCountList(key);
            int sum = GetTaskItemData(key).curTargetIndex == targetList.Length ? targetList[GetTaskItemData(key).curTargetIndex - 1] : targetList[GetTaskItemData(key).curTargetIndex];
            GetTaskItemData(key).SetAchievementComplete(sum);
            GetTaskItemData(key).AddTargetIndex(GetTaskItemData(key).curTargetIndex == targetList.Length);
            EventSystem.S.Send(EventID.AchievementTaskRefresh, key);
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
