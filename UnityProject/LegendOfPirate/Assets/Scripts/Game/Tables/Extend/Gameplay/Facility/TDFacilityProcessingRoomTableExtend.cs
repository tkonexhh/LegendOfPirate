using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDFacilityProcessingRoomTable
    {
        static void CompleteRowAdd(TDFacilityProcessingRoom tdData, int rowCount)
        {
            if (processingRoomUnitProperties == null)
                processingRoomUnitProperties = new ProcessingRoomUnitConfig[rowCount];

            int level = tdData.level;
            if (level > processingRoomUnitProperties.Length)
            {
                throw new ArgumentOutOfRangeException("ProcessingRoom Data Out Of Range");
            }

            List<int> unlockPartIdList = ParsePartId(tdData.unlockPartID);
            processingRoomUnitProperties[level - 1] = new ProcessingRoomUnitConfig(tdData.level, tdData.upgradeRes, tdData.upgradeCost,
                tdData.upgradePreconditions, tdData.upgradeTime, tdData.modelResources, unlockPartIdList, tdData.unlockPartSpace, tdData.unlockSpaceCost);
        }

        public static ProcessingRoomUnitConfig[] processingRoomUnitProperties = null;

        public static ProcessingRoomUnitConfig GetConfig(int level)
        {
            if (level > processingRoomUnitProperties.Length)
            {
                Log.e("GetProcessingRoomProperty Level Out Of Range: " + level + "  Data Count: " + processingRoomUnitProperties.Length);
                return default(ProcessingRoomUnitConfig);
            }

            return processingRoomUnitProperties[level - 1];
        }

        private static List<int> ParsePartId(string partId)
        {
            if (string.IsNullOrEmpty(partId))
                return null;

            List<int> list = new List<int>();

            string[] itemStrs = partId.Split('|');
            for (int i = 0; i < itemStrs.Length; i++)
            {
                if (!string.IsNullOrEmpty(itemStrs[i]))
                {
                    list.Add(int.Parse(itemStrs[i]));
                }
            }

            return list;
        }
    }

    public struct ProcessingRoomUnitConfig
    {
        public ShipUnitBaseConfig baseProperty;
        public List<int> unlockPartIdList;
        public int unlockPartSpace;
        public int unlockSpaceCost;

        public ProcessingRoomUnitConfig(int level, string upgradeRes, int upgradeCoinCost, int upgradePrecondition,
            int upgradeTime, string modelRes, List<int> unlockPartId, int unlockPartSpace, int unlockSpaceCost
            )
        {
            baseProperty = new ShipUnitBaseConfig(level, upgradeCoinCost, upgradeRes, upgradePrecondition, upgradeTime, modelRes);
            this.unlockPartIdList = unlockPartId;
            this.unlockPartSpace = unlockPartSpace;
            this.unlockSpaceCost = unlockSpaceCost;
        }

       
    }
}