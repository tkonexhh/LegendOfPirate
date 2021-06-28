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
        public MainSeaLevelModel()
        {
            foreach (var item in TDMarinLevelConfigTable.dataList)
            {
                tDMarinLevelList.Add(item);
            }
            allLevelItems = new List<GameObject>();
            // Debug.Log("tDMarinLevelConfig=" + tDMarinLevelConfig);
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