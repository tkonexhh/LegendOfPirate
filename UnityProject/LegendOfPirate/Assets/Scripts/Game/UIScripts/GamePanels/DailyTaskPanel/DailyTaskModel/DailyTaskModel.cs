using UniRx;
using System.Linq;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections;
using Qarth;

namespace GameWish.Game
{
    public class DailyTaskModel : Model
    {
        public ReactiveCollection<TDDailyTask> tdDailyTaskList = new ReactiveCollection<TDDailyTask>();
        public List<DailyTasklItem> dailyItems;
        public TaskData taskData;
        public StringReactiveProperty leftTime;
        public Coroutine refreshTimeCoroutine;
        private int m_RefreshTime = 6;
        public DailyTaskModel()
        {
            taskData = GameDataMgr.S.GetData<TaskData>();
            dailyItems = new List<DailyTasklItem>();
            leftTime = new StringReactiveProperty();
            foreach (var item in TDDailyTaskTable.dataList)
            {
                tdDailyTaskList.Add(item);
            }
            // Debug.Log("tDMarinLevelConfig=" + tDMarinLevelConfig);

        }

        public TDDailyTask GetDailyTaskData(int taskId)
        {
            return tdDailyTaskList.FirstOrDefault(i => i.taskID == taskId);
        }
        public int GetDailyTaskCount()
        {
            return TDDailyTaskTable.count;
        }

        public int GetActiveNum()
        {
            return taskData.GetTaskDailyData().activeNum;
        }
        public IEnumerator GetRefreshTime()
        {
            while (true)
            {
                int day = DateTime.Now.DayOfYear;
                int hour = DateTime.Now.Hour;
                int minute = DateTime.Now.Minute;
                int second = DateTime.Now.Second;
                DateTime tomorrow = DateTime.Now.AddDays(1);
                TimeSpan time = new TimeSpan(hour < 6 ? day : tomorrow.DayOfYear, m_RefreshTime, 0, 0) - new TimeSpan(day, hour, minute, second);
                //Debug.LogError("time = " + time);
                leftTime.Value = time.ToString();
                if (time.TotalSeconds == 0)
                {
                    taskData.NewDay();
                    EventSystem.S.Send(EventID.DailyTaskRefresh, 0);
                }
                yield return new WaitForSeconds(1f);
            }
        }

    }

}