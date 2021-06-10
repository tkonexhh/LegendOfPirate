using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDFacilityForgeTable
    {
        static void CompleteRowAdd(TDFacilityForge tdData, int rowCount)
        {

            if (forgeUnitProperties == null)
                forgeUnitProperties = new ForgeUnitConfig[rowCount];

            int level = tdData.level;
            if (level > forgeUnitProperties.Length)
            {
                throw new ArgumentOutOfRangeException("Forge Data Out Of Range");
            }

            forgeUnitProperties[level - 1] = new ForgeUnitConfig(tdData.level, tdData.upgradeRes, tdData.upgradeCost,
                tdData.upgradePreconditions, tdData.upgradeTime, tdData.modelResources, tdData.unlockEquipmentID);
        }

        public static ForgeUnitConfig[] forgeUnitProperties = null;

        public static ForgeUnitConfig GetConfig(int level)
        {
            if (level > forgeUnitProperties.Length)
            {
                Log.e("GetForgeUnitProperty Level Out Of Range: " + level + "  Data Count: " + forgeUnitProperties.Length);
                return default(ForgeUnitConfig);
            }

            return forgeUnitProperties[level - 1];
        }
    }

    public struct ForgeUnitConfig
    {
        public ShipUnitBaseConfig baseProperty;
        public int unlockEuipId;

        public ForgeUnitConfig(int level, string upgradeRes, int upgradeCoinCost, int upgradePrecondition,
            int upgradeTime, string modelRes, int unlockEquipId
            )
        {
            baseProperty = new ShipUnitBaseConfig(level, upgradeCoinCost, upgradeRes, upgradePrecondition, upgradeTime, modelRes);
            this.unlockEuipId = unlockEquipId;
        }
    }
}