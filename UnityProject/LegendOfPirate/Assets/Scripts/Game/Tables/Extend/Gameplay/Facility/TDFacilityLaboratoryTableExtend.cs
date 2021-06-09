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
        static void CompleteRowAdd(TDFacilityLaboratory tdData)
        {
            if (laboratoryUnitProperties == null)
                laboratoryUnitProperties = new LaboratoryUnitProperty[dataList.Count];

            int level = tdData.level;
            if (level > laboratoryUnitProperties.Length)
            {
                throw new ArgumentOutOfRangeException("Laboratory Data Out Of Range");
            }

            laboratoryUnitProperties[level - 1] = new LaboratoryUnitProperty(tdData.level, tdData.upgradeRes, tdData.upgradeCost,
                tdData.upgradePreconditions, tdData.upgradeTime, tdData.modelResources, tdData.unlockSpaceCost);
        }

        public static LaboratoryUnitProperty[] laboratoryUnitProperties = null;

        public static LaboratoryUnitProperty GetLaboratoryUnitProperty(int level)
        {
            if (level > laboratoryUnitProperties.Length)
            {
                Log.e("GetLaboratoryUnitProperty Level Out Of Range: " + level + "  Data Count: " + laboratoryUnitProperties.Length);
                return default(LaboratoryUnitProperty);
            }

            return laboratoryUnitProperties[level - 1];
        }
    }

    public struct LaboratoryUnitProperty
    {
        public ShipUnitBaseProperty baseProperty;
        public int unlockSpaceCost;

        public LaboratoryUnitProperty(int level, string upgradeRes, int upgradeCoinCost, int upgradePrecondition,
            int upgradeTime, string modelRes, int unlockSpaceCost
            )
        {
            baseProperty = new ShipUnitBaseProperty(level, upgradeCoinCost, upgradeRes, upgradePrecondition, upgradeTime, modelRes);
            this.unlockSpaceCost = unlockSpaceCost;
        }
    }
}
