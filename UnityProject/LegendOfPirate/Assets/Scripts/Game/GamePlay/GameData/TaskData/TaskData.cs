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

        public TaskDailyData taskDailyData;
        public TaskAchievementData taskAchievementData;
        public TaskMainData taskMainData;

        public override void InitWithEmptyData()
        {
            CurDayInYear = -1;
            taskDailyData = new TaskDailyData();
            taskDailyData.InitWithEmptyData();
            taskAchievementData = new TaskAchievementData();
            taskAchievementData.InitWithEmptyData();
            taskMainData = new TaskMainData();
            taskMainData.InitWithEmptyData();
        }

        public override void OnDataLoadFinish()
        {
            taskDailyData.OnDataLoadFinish();
            taskAchievementData.OnDataLoadFinish();
            taskMainData.OnDataLoadFinish();
            if (CurDayInYear != System.DateTime.Now.DayOfYear)
            {
                CurDayInYear = System.DateTime.Now.DayOfYear;
                // SetDataDirty();
                // NewDay();
            }
        }

        public TaskDailyData GetTaskDailyData()
        {
            return taskDailyData;
        }

        public TaskAchievementData GetTaskAchievementData()
        {
            return taskAchievementData;
        }

        public TaskMainData GetTaskMainData()
        {
            return taskMainData;
        }

        //此处需要初始化就每天
        public void NewDay()
        {
            taskDailyData.NewDay();
        }

        public bool CheckExistReward()
        {
            //return m_TaskDailyData.CheckExistReward() || m_TaskAchievementData.CheckExistReward();
            return taskDailyData.CheckExistReward();
        }

    }

    [Serializable]
    public class TaskItemData
    {
        public int idKey;
        public int curCompleteTimes;
        public bool isGetRewardToday;
        public int curTargetIndex = 0;
        public int curAchievementTimes = 0;
        private TaskData m_TaskData;
        public TaskItemData() { }

        public TaskItemData(int key)
        {
            idKey = key;
            NewDay();
        }

        public void NewDay()
        {
            curCompleteTimes = 0;
            isGetRewardToday = false;
            SetDataDirty();
        }

        public void AddTaskTimes()
        {
            curCompleteTimes++;
            SetDataDirty();
        }

        public void AddAchievementTimes()
        {
            curAchievementTimes++;
            SetDataDirty();
        }
        public void AddTargetIndex(bool canAdd)
        {
            if (!canAdd)
            {
                curTargetIndex++;
                SetDataDirty();
            }

        }
        public void SetComplete(int completeSum)
        {
            isGetRewardToday = true;
            curCompleteTimes = completeSum;
            SetDataDirty();
        }
        public void SetAchievementComplete(int completeSum)
        {
            isGetRewardToday = true;
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