using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDFacilityKitchenTable
    {
        static void CompleteRowAdd(TDFacilityKitchen tdData, int rowCount)
        {
            if (kitchenUnitProperties == null)
                kitchenUnitProperties = new KitchenUnitConfig[rowCount];

            int level = tdData.level;
            if (level > kitchenUnitProperties.Length)
            {
                throw new ArgumentOutOfRangeException("Kitchen Data Out Of Range");
            }

            kitchenUnitProperties[level - 1] = new KitchenUnitConfig(tdData.level, tdData.upgradeRes, tdData.upgradeCost,
                tdData.upgradePreconditions, tdData.upgradeTime, tdData.modelResources, tdData.unlockSpaceCost,tdData.unlockCookSpace);
        }

        public static KitchenUnitConfig[] kitchenUnitProperties = null;

        public static KitchenUnitConfig GetConfig(int level)
        {
            if (level > kitchenUnitProperties.Length)
            {
                Log.e("GetKitchenUnitProperty Level Out Of Range: " + level + "  Data Count: " + kitchenUnitProperties.Length);
                return default(KitchenUnitConfig);
            }

            return kitchenUnitProperties[level - 1];
        }
    }

    public struct KitchenUnitConfig
    {
        public ShipUnitBaseConfig baseProperty;
        public int unlockSpaceCost;
        public int unlockSpaceCount;
        public KitchenUnitConfig(int level, string upgradeRes, int upgradeCoinCost, int upgradePrecondition,
            int upgradeTime, string modelRes, int unlockSpaceCost, int unlockSpaceCount
            )
        {
            baseProperty = new ShipUnitBaseConfig(level, upgradeCoinCost, upgradeRes, upgradePrecondition, upgradeTime, modelRes);
            this.unlockSpaceCost = unlockSpaceCost;
            this.unlockSpaceCount = unlockSpaceCount;
        }
    }
}