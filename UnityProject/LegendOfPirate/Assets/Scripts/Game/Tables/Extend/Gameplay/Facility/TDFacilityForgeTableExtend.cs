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
        static void CompleteRowAdd(TDFacilityForge tdData)
        {

            if (forgeUnitProperties == null)
                forgeUnitProperties = new ForgeUnitProperty[dataList.Count];

            int level = tdData.level;
            if (level > forgeUnitProperties.Length)
            {
                throw new ArgumentOutOfRangeException("Forge Data Out Of Range");
            }

            forgeUnitProperties[level - 1] = new ForgeUnitProperty(tdData.level, tdData.upgradeRes, tdData.upgradeCost,
                tdData.upgradePreconditions, tdData.upgradeTime, tdData.modelResources, tdData.unlockEquipmentID);
        }

        public static ForgeUnitProperty[] forgeUnitProperties = null;

        public static ForgeUnitProperty GetForgeUnitProperty(int level)
        {
            if (level > forgeUnitProperties.Length)
            {
                Log.e("GetForgeUnitProperty Level Out Of Range: " + level + "  Data Count: " + forgeUnitProperties.Length);
                return default(ForgeUnitProperty);
            }

            return forgeUnitProperties[level - 1];
        }
    }

    public struct ForgeUnitProperty
    {
        public ShipUnitBaseProperty baseProperty;
        public int unlockEuipId;

        public ForgeUnitProperty(int level, string upgradeRes, int upgradeCoinCost, int upgradePrecondition,
            int upgradeTime, string modelRes, int unlockEquipId
            )
        {
            baseProperty = new ShipUnitBaseProperty(level, upgradeCoinCost, upgradeRes, upgradePrecondition, upgradeTime, modelRes);
            this.unlockEuipId = unlockEquipId;
        }
    }
}