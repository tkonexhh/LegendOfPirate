using UniRx;
using System.Linq;
using System.Collections.Generic;
namespace GameWish.Game
{
    public class DailyTaskAwardModel : Model
    {
        public ReactiveCollection<TDDailyTaskReward> tdDailyTaskAwardList = new ReactiveCollection<TDDailyTaskReward>();
        public List<RewardBoxItem> rewardBoxItems;
        public int targetActive;
        public RewardBoxItem curRewardBoxItem;
        public DailyTaskAwardModel()
        {
            rewardBoxItems = new List<RewardBoxItem>();
            foreach (var item in TDDailyTaskRewardTable.dataList)
            {
                tdDailyTaskAwardList.Add(item);
            }
            targetActive = GetTargetActive();
            // Debug.Log("tDMarinLevelConfig=" + tDMarinLevelConfig);
        }

        public TDDailyTaskReward GetDailyTaskData(int rewardId)
        {
            return tdDailyTaskAwardList.FirstOrDefault(i => i.iD == rewardId);
        }
        public int GetDailyTaskCount()
        {
            return TDDailyTaskRewardTable.count;
        }

        private int GetTargetActive()
        {
            if (GetDailyTaskCount() > 1)
                return tdDailyTaskAwardList[GetDailyTaskCount() - 1].activity;
            return 0;
        }

    }

}