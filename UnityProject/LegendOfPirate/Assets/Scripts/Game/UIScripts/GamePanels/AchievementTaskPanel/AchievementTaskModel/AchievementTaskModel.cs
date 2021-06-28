using UniRx;
using System.Linq;
using System.Collections.Generic;
namespace GameWish.Game
{
    public class AchievementTaskModel : Model
    {
        public ReactiveCollection<TDAchievementTask> tdAchievementTaskList = new ReactiveCollection<TDAchievementTask>();
        public List<AchievementTasklItem> achievementItems;
        public TaskData taskData;
        public AchievementTaskModel()
        {
            taskData = GameDataMgr.S.GetData<TaskData>();
            achievementItems = new List<AchievementTasklItem>();
            foreach (var item in TDAchievementTaskTable.dataList)
            {
                tdAchievementTaskList.Add(item);
            }
            // Debug.Log("tDMarinLevelConfig=" + tDMarinLevelConfig);
        }

        public TDAchievementTask GetAchievementTaskData(int achievementID)
        {
            return tdAchievementTaskList.FirstOrDefault(i => i.achievementID == achievementID);
        }
        public int GetAchievementTaskCount()
        {
            return TDAchievementTaskTable.count;
        }

    }

}