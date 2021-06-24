using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDFacilityLaboratoryTable
    {
        static void CompleteRowAdd(TDFacilityLaboratory tdData, int rowCount)
        {
            if (laboratoryUnitProperties == null)
                laboratoryUnitProperties = new LaboratoryUnitConfig[rowCount];

            int level = tdData.level;
            if (level > laboratoryUnitProperties.Length)
            {
                throw new ArgumentOutOfRangeException("Laboratory Data Out Of Range");
            }

            laboratoryUnitProperties[level - 1] = new LaboratoryUnitConfig(tdData.level, tdData.upgradeRes, tdData.upgradeCost,
                tdData.upgradePreconditions, tdData.upgradeTime, tdData.modelResources, tdData.unlockSpaceCost,tdData.unlockPartSpace);
        }

        public static LaboratoryUnitConfig[] laboratoryUnitProperties = null;

        public static LaboratoryUnitConfig GetConfig(int level)
        {
            if (level > laboratoryUnitProperties.Length)
            {
                Log.e("GetLaboratoryUnitProperty Level Out Of Range: " + level + "  Data Count: " + laboratoryUnitProperties.Length);
                return default(LaboratoryUnitConfig);
            }

            return laboratoryUnitProperties[level - 1];
        }
    }

    public struct LaboratoryUnitConfig
    {
        public ShipUnitBaseConfig baseProperty;
        public int unlockSpaceCost;
        public int unlockSpaceCount;
        public LaboratoryUnitConfig(int level, string upgradeRes, int upgradeCoinCost, int upgradePrecondition,
            int upgradeTime, string modelRes, int unlockSpaceCost,int unlockSpaceCount
            )
        {
            baseProperty = new ShipUnitBaseConfig(level, upgradeCoinCost, upgradeRes, upgradePrecondition, upgradeTime, modelRes);
            this.unlockSpaceCost = unlockSpaceCost;
            this.unlockSpaceCount = unlockSpaceCount;
        }
    }
}
