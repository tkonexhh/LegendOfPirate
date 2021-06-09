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
        static void CompleteRowAdd(TDFacilityKitchen tdData)
        {
            if (kitchenUnitProperties == null)
                kitchenUnitProperties = new KitchenUnitProperty[dataList.Count];

            int level = tdData.level;
            if (level > kitchenUnitProperties.Length)
            {
                throw new ArgumentOutOfRangeException("Kitchen Data Out Of Range");
            }

            kitchenUnitProperties[level - 1] = new KitchenUnitProperty(tdData.level, tdData.upgradeRes, tdData.upgradeCost,
                tdData.upgradePreconditions, tdData.upgradeTime, tdData.modelResources, tdData.unlockSpaceCost);
        }

        public static KitchenUnitProperty[] kitchenUnitProperties = null;

        public static KitchenUnitProperty GetKitchenUnitProperty(int level)
        {
            if (level > kitchenUnitProperties.Length)
            {
                Log.e("GetKitchenUnitProperty Level Out Of Range: " + level + "  Data Count: " + kitchenUnitProperties.Length);
                return default(KitchenUnitProperty);
            }

            return kitchenUnitProperties[level - 1];
        }
    }

    public struct KitchenUnitProperty
    {
        public ShipUnitBaseProperty baseProperty;
        public int unlockSpaceCost;

        public KitchenUnitProperty(int level, string upgradeRes, int upgradeCoinCost, int upgradePrecondition,
            int upgradeTime, string modelRes, int unlockSpaceCost
            )
        {
            baseProperty = new ShipUnitBaseProperty(level, upgradeCoinCost, upgradeRes, upgradePrecondition, upgradeTime, modelRes);
            this.unlockSpaceCost = unlockSpaceCost;
        }
    }
}