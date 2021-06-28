using UniRx;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
namespace GameWish.Game
{
    public class MainSeaLevelModel : Model
    {
        public ReactiveCollection<TDMarinLevelConfig> tDMarinLevelList = new ReactiveCollection<TDMarinLevelConfig>();

        public List<GameObject> allLevelItems;
        public IntReactiveProperty curLevelID;
        public StringReactiveProperty curLevelType;
        public StringReactiveProperty curEnemyHeadIcon;
        public StringReactiveProperty curResOutPut;
        public StringReactiveProperty curReward;
        public StringReactiveProperty curRecommendAtkValue;
        public StringReactiveProperty curBattleName;
        public MainSeaLevelModel()
        {
            foreach (var item in TDMarinLevelConfigTable.dataList)
            {
                tDMarinLevelList.Add(item);
            }
            allLevelItems = new List<GameObject>();
            curLevelID = new IntReactiveProperty();
            curLevelType = new StringReactiveProperty();
            curEnemyHeadIcon = new StringReactiveProperty();
            curResOutPut = new StringReactiveProperty();
            curReward = new StringReactiveProperty();
            curRecommendAtkValue = new StringReactiveProperty();
            curBattleName = new StringReactiveProperty();
            // Debug.Log("tDMarinLevelConfig=" + tDMarinLevelConfig);
        }
        public void InitContent()
        {
            TDMarinLevelConfig curLevelData = GetBattleLevelData(curLevelID.Value);
            curLevelType.Value = curLevelData.type;
            curEnemyHeadIcon.Value = curLevelData.enemyHeadIcon;
            curResOutPut.Value = curLevelData.resOutput;
            curReward.Value = curLevelData.reward;
            curRecommendAtkValue.Value = curLevelData.recommendAtkValue;
            curBattleName.Value = curLevelData.battleName;
        }
        public string[] GetReward()
        {
            string[] rewardList = curReward.Value.Split(';');
            return rewardList;
        }
        
        public TDMarinLevelConfig GetBattleLevelData(int levelId)
        {
            return tDMarinLevelList.FirstOrDefault(i => i.level == levelId);
        }
        public int GetBattleLevelCount()
        {
            return TDMarinLevelConfigTable.count;
        }


    }

}