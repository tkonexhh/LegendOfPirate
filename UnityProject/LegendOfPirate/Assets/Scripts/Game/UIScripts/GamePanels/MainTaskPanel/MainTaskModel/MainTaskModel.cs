using UniRx;
using System.Linq;
using UnityEngine;


namespace GameWish.Game
{
    public class MainTaskModel : Model
    {
        public enum MainTaskRewardType
        {
            UnFinished,
            Finished
        }
        public ReactiveCollection<TDMainTask> tdMainTaskList = new ReactiveCollection<TDMainTask>();
        public TaskData taskData;
        public IntReactiveProperty curTaskID;
        public IntReactiveProperty curCompleteTimes;
        public StringReactiveProperty titleTex;
        public StringReactiveProperty contentTex;
        public StringReactiveProperty rewardTex;
        public StringReactiveProperty claimTex;
        public MainTaskRewardType curMainTaskRewardType = MainTaskRewardType.UnFinished;
        private int taskCount;
        public MainTaskModel()
        {
            foreach (var item in TDMainTaskTable.dataList)
            {
                tdMainTaskList.Add(item);
            }

            taskData = GameDataMgr.S.GetData<TaskData>();
            curTaskID = new IntReactiveProperty(GetCurTaskID());
            curCompleteTimes = new IntReactiveProperty(GetCurCompleteTimes());
            titleTex = new StringReactiveProperty(GetCurMainTaskData().taskTitle);
            contentTex = new StringReactiveProperty(GetCurMainTaskData().type);
            rewardTex = new StringReactiveProperty(string.Format("*{0}", GetCurMainTaskData().reward.Split('|')[1]));
            claimTex = new StringReactiveProperty("Claim");
            taskCount = GetCurMainTaskData().count;
        }
        public TDMainTask GetCurMainTaskData()
        {
            return GetMainTaskData(curTaskID.Value);
        }

        public void RefreshData(int taskID)
        {
            if (taskID == curTaskID.Value)
            {
                curTaskID.Value = GetCurTaskID();
                curCompleteTimes.Value = GetCurCompleteTimes();
                titleTex.Value = GetCurMainTaskData().taskTitle;
                contentTex.Value = GetCurMainTaskData().type;
                rewardTex.Value = string.Format("*{0}", GetCurMainTaskData().reward.Split('|')[1]);
                bool isFinish = IsFinishTask();
                claimTex.Value = isFinish ? "Claim" : "GoTo";
                curMainTaskRewardType = isFinish ? MainTaskRewardType.Finished : MainTaskRewardType.UnFinished;
                Debug.LogError("curCompleteTimes.Value =" + curCompleteTimes.Value);
            }
            else
            {
                ResetCompleteTimes();
            }
        }

        public TDMainTask GetMainTaskData(int taskId)
        {
            return tdMainTaskList.FirstOrDefault(i => i.taskID == taskId);
        }

        public bool IsFinishTask()
        {
            return taskCount <= curCompleteTimes.Value;
        }
        public int GetCurTaskID()
        {
            return taskData.GetTaskMainData().GetTaskID();
        }

        public int GetCurCompleteTimes()
        {
            return taskData.GetTaskMainData().GetTaskTimes();
        }
        public void ResetCompleteTimes()
        {
            taskData.GetTaskMainData().ResetCompleteTimes();
        }
        public void ResetTaskState()
        {
            taskData.GetTaskMainData().ResetTaskState(curTaskID.Value + 1);
            curTaskID.Value = GetCurTaskID();
            RefreshData(curTaskID.Value);
        }

    }

}