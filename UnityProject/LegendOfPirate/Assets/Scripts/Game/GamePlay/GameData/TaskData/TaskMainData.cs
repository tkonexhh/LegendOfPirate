using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using QuickEngine.Extensions;
using System;
namespace GameWish.Game
{
    [Serializable]
    public class TaskMainData
    {
        public TaskMainData()
        {

        }

        public int taskID;
        public int curCompleteTimes;
        private TaskData m_TaskData;

        public void InitWithEmptyData()
        {
            ResetTaskState(0);
        }

        public void OnDataLoadFinish()
        {
            EventSystem.S.Register(EventID.MainTaskTimesAdd, AddTaskTimes);
        }

        public int GetTaskID()
        {
            return taskID;
        }
        public void ResetCompleteTimes()
        {
            curCompleteTimes = 0;
            SetDataDirty();
        }
        //当前任务次数自增
        public void AddTaskTimes(int key, params object[] parm)
        {
            if (parm != null && parm.Length > 0)
            {
                int idKey = (int)(parm[0]);
                curCompleteTimes++;
                EventSystem.S.Send(EventID.MainTaskRefresh, idKey);
                SetDataDirty();
            }
        }

        //获取当前任务完成数量
        public int GetTaskTimes()
        {
            return curCompleteTimes;
        }

        public void ResetTaskState(int curTaskID)
        {
            curCompleteTimes = 0;
            taskID = curTaskID;
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
