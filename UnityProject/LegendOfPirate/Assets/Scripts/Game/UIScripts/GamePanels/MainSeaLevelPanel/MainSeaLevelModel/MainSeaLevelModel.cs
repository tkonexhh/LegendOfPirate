using UniRx;
using System.Linq;
namespace GameWish.Game
{
    public class MainSeaLevelModel : Model
    {
        public ReactiveCollection<TDMarinLevelConfig> tDMarinLevelList = new ReactiveCollection<TDMarinLevelConfig>();

        public MainSeaLevelModel()
        {
            foreach (var item in TDMarinLevelConfigTable.dataList)
            {
                tDMarinLevelList.Add(item);
            }
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