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
        public bool m_CanStop = false;
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
        public IEnumerator GetRefreshTime(DailyTaskPanel myPanel)
        {
            m_CanStop = true;
            while (myPanel.gameObject.layer == LayerDefine.LAYER_UI && m_CanStop)
            {
                int day = DateTime.Now.DayOfYear;
                int hour = DateTime.Now.Hour;
                int minute = DateTime.Now.Minute;
                int second = DateTime.Now.Second;
                DateTime tomorrow = DateTime.Now.AddDays(1);
                TimeSpan time = new TimeSpan(hour < m_RefreshTime ? day : tomorrow.DayOfYear, m_RefreshTime, 0, 0) - new TimeSpan(day, hour, minute, second);
                //Debug.LogError("time.TotalSeconds = " + time.TotalSeconds);
                leftTime.Value = time.ToString();
                yield return new WaitForSeconds(1f);
                if ((int)time.TotalSeconds == 86400)
                {
                    m_CanStop = false;
                    taskData.NewDay();
                    myPanel.NewDay();
                }
            }
        }

    }

}