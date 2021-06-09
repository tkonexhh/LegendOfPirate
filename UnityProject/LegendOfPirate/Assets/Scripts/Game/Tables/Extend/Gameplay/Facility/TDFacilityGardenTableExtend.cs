using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDFacilityGardenTable
    {
        static void CompleteRowAdd(TDFacilityGarden tdData)
        {

            if (gardenUnitProperties == null)
                gardenUnitProperties = new GardenUnitProperty[dataList.Count];

            int level = tdData.level;
            if (level > gardenUnitProperties.Length)
            {
                throw new ArgumentOutOfRangeException("Garden Data Out Of Range");
            }

            gardenUnitProperties[level - 1] = new GardenUnitProperty(tdData.level, tdData.upgradeRes, tdData.upgradeCost,
                tdData.upgradePreconditions, tdData.upgradeSpeed, tdData.modelResources, tdData.plantingSpeed);
        }

        public static GardenUnitProperty[] gardenUnitProperties = null;

        public static GardenUnitProperty GetGardenUnitProperty(int level)
        {
            if (level > gardenUnitProperties.Length)
            {
                Log.e("GetGardenUnitProperty Level Out Of Range: " + level + "  Data Count: " + gardenUnitProperties.Length);
                return default(GardenUnitProperty);
            }

            return gardenUnitProperties[level - 1];
        }
    }

    public struct GardenUnitProperty
    {
        public ShipUnitBaseProperty baseProperty;
        public int plantingSped;

        public GardenUnitProperty(int level, string upgradeRes, int upgradeCoinCost, int upgradePrecondition,
            int upgradeTime, string modelRes, int plantingSped
            )
        {
            baseProperty = new ShipUnitBaseProperty(level, upgradeCoinCost, upgradeRes, upgradePrecondition, upgradeTime, modelRes);
            this.plantingSped = plantingSped;
        }
    }
}