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
        static void CompleteRowAdd(TDFacilityGarden tdData, int rowCount)
        {

            if (gardenUnitProperties == null)
                gardenUnitProperties = new GardenUnitConfig[rowCount];

            int level = tdData.level;
            if (level > gardenUnitProperties.Length)
            {
                throw new ArgumentOutOfRangeException("Garden Data Out Of Range");
            }

            gardenUnitProperties[level - 1] = new GardenUnitConfig(tdData.level, tdData.upgradeRes, tdData.upgradeCost,
                tdData.upgradePreconditions, tdData.upgradeSpeed, tdData.modelResources, tdData.plantingSpeed);
        }

        public static GardenUnitConfig[] gardenUnitProperties = null;

        public static GardenUnitConfig GetConfig(int level)
        {
            if (level > gardenUnitProperties.Length)
            {
                Log.e("GetGardenUnitProperty Level Out Of Range: " + level + "  Data Count: " + gardenUnitProperties.Length);
                return default(GardenUnitConfig);
            }

            return gardenUnitProperties[level - 1];
        }
    }

    public struct GardenUnitConfig
    {
        public ShipUnitBaseConfig baseProperty;
        public int plantingSped;

        public GardenUnitConfig(int level, string upgradeRes, int upgradeCoinCost, int upgradePrecondition,
            int upgradeTime, string modelRes, int plantingSped
            )
        {
            baseProperty = new ShipUnitBaseConfig(level, upgradeCoinCost, upgradeRes, upgradePrecondition, upgradeTime, modelRes);
            this.plantingSped = plantingSped;
        }
    }
}