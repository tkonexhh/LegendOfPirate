using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;
using System.Linq;

namespace GameWish.Game
{
    public partial class TDFacilityBattleshipTable
    {
        static void CompleteRowAdd(TDFacilityBattleship tdData, int rowCount)
        {

        }

        public static BattleShipUnitConfig GetConfigById(int id)
        {
            var item = dataList.FirstOrDefault(data => data.warshipId == id);
            return new BattleShipUnitConfig(item.warshipId,
                                         item.nextWarship,
                                         item.name,
                                         item.modelResources,
                                         item.unlockAccountLevel,
                                         item.strengthenCost,
                                         item.attributeValues);
        }

        public static List<BattleShipUnitConfig> GetDefaultConfig()
        {
            List<BattleShipUnitConfig> ret = new List<BattleShipUnitConfig>();
            int index = 0;
            foreach (var item in dataList)
            {
                int idOfWarShip = item.warshipId / 100;
                if ((idOfWarShip) != index)
                {
                    index = idOfWarShip;
                    ret.Add(new BattleShipUnitConfig(item.warshipId,
                                                  item.nextWarship,
                                                  item.name,
                                                  item.modelResources,
                                                  item.unlockAccountLevel,
                                                  item.strengthenCost,
                                                  item.attributeValues));
                }
            }

            return ret;
        }


    }
    public class BattleShipUnitConfig
    {
        public int warShipId;
        public int nextWarShipId;
        public string battleShipName;
        public string modelRes;
        public int unlockAccountLevel;
        public List<ResPair> strengthenCost;
        public float ATKCount;
        public float HPCount;
        public float ArmorCount;

        public BattleShipUnitConfig(int warShipId, int nextWarShipId, string warShipName, string modelRes, int unlockAccountLevel, string strengthenCostStrings, string attributeValues)
        {
            this.warShipId = warShipId;
            this.nextWarShipId = nextWarShipId;
            this.battleShipName = warShipName;
            this.modelRes = modelRes;
            this.unlockAccountLevel = unlockAccountLevel;
            strengthenCost = ResPair.StringToResPairLst(strengthenCostStrings);
            var attValuesStrings = attributeValues.Split(';');
            ATKCount = Helper.String2Float(attValuesStrings[0].Split('|')[1]);
            HPCount = Helper.String2Float(attValuesStrings[1].Split('|')[1]);
            ArmorCount = Helper.String2Float(attValuesStrings[2].Split('|')[1]);
        }
    }
}