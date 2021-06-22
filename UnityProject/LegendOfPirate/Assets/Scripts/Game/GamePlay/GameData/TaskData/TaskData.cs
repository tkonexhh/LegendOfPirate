using Qarth;
using System;
namespace GameWish.Game
{
    public class TaskData : IDataClass
    {
        public int CurDayInYear = -1;
        public TaskData()
        {

        }

        public TaskDailyData m_TaskDailyData;
        public TaskAchievementData m_TaskAchievementData;

        public override void InitWithEmptyData()
        {
            CurDayInYear = -1;
            m_TaskDailyData = new TaskDailyData();
            m_TaskDailyData.InitWithEmptyData();
            m_TaskAchievementData = new TaskAchievementData();
            m_TaskAchievementData.InitWithEmptyData();
        }

        public override void OnDataLoadFinish()
        {

            m_TaskDailyData.OnDataLoadFinish();
            m_TaskAchievementData.OnDataLoadFinish();
            if (CurDayInYear != System.DateTime.Now.DayOfYear)
            {
                CurDayInYear = System.DateTime.Now.DayOfYear;
                SetDataDirty();
                NewDay();
            }
        }

        public TaskDailyData GetTaskDailyData()
        {
            return m_TaskDailyData;
        }

        public TaskAchievementData GetTaskAchievementData()
        {
            return m_TaskAchievementData;
        }

        //此处需要初始化就每天
        public void NewDay()
        {
            m_TaskDailyData.NewDay();
        }

        public bool CheckExistReward()
        {
            //return m_TaskDailyData.CheckExistReward() || m_TaskAchievementData.CheckExistReward();
            return m_TaskDailyData.CheckExistReward();
        }

    }

    [Serializable]
    public class TaskItemData
    {
        public int m_IdKey;
        public int m_CurCompleteTimes;
        public bool m_IsGetRewardToday;
        private TaskData m_TaskData;
        public TaskItemData() { }

        public TaskItemData(int key)
        {
            m_IdKey = key;
            NewDay();
        }

        public void NewDay()
        {
            m_CurCompleteTimes = 0;
            m_IsGetRewardToday = false;
            SetDataDirty();
        }

        public void AddTaskTimes()
        {
            m_CurCompleteTimes++;
            SetDataDirty();
        }

        public void SetComplete(int completeSum)
        {
            m_IsGetRewardToday = true;
            m_CurCompleteTimes = completeSum;
            SetDataDirty();
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