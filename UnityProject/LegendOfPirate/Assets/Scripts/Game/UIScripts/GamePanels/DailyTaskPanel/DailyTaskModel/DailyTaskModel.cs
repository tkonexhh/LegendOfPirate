using UniRx;
using System.Linq;
using System.Collections.Generic;
namespace GameWish.Game
{
    public class DailyTaskModel : Model
    {
        public ReactiveCollection<TDDailyTask> tdDailyTaskList = new ReactiveCollection<TDDailyTask>();
        public List<DailyTasklItem> dailyItems;
        public TaskData taskData;
        public DailyTaskModel()
        {
            taskData = GameDataMgr.S.GetData<TaskData>();
            dailyItems = new List<DailyTasklItem>();
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

    }

}