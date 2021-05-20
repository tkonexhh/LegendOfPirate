using System;
using Qarth;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    public class MainTaskData : DataDirtyHandler
    {
        [System.Serializable]
        public class MainTaskItemData
        {
            public int taskId = 1;
            public bool isRewardClaimed = false;

            public MainTaskItemData()
            {
            }

            public MainTaskItemData(int taskId)
            {
                this.taskId = taskId;
            }
        }

        public List<MainTaskItemData> finishedTaskList = new List<MainTaskItemData>();

        public void SetDefaultValue()
        {

        }

        public void Init()
        {

        }

        public void OnTaskFinished(int taskId)
        {
            MainTaskItemData mainTaskItem = GetMainTaskItemData(taskId);
            if (mainTaskItem == null)
            {
                mainTaskItem = new MainTaskItemData(taskId);
                finishedTaskList.Add(mainTaskItem);
            }

            SetDataDirty();
        }

        public void OnTaskRewardClaimed(int taskId)
        {
            MainTaskItemData mainTaskItem = GetMainTaskItemData(taskId);
            if (mainTaskItem != null)
            {
                mainTaskItem.isRewardClaimed = true;
            }
            else
            {
                Log.e("OnTaskRewardClaimed, task not found in list : " + taskId);
            }

            SetDataDirty();
        }

        private MainTaskItemData GetMainTaskItemData(int taskId)
        {
            foreach (MainTaskItemData item in finishedTaskList)
            {
                if (item.taskId == taskId)
                {
                    return item;
                }
            }

            return null;
        }
    }
}