using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;
using System.Linq;

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
                tdData.upgradePreconditions, tdData.modelResources, tdData.unlockEquipmentID);
        }

        public static ForgeUnitConfig[] forgeUnitProperties = null;

        public static int GetMaxLevel()
        {
            return forgeUnitProperties[forgeUnitProperties.Length - 1].baseProperty.level;
        }

        /// <summary>
        ///  根据打造的装备ID获取锻造室某一级
        /// </summary>
        /// <param name="EquipID"></param>
        /// <returns></returns>
        public static ForgeUnitConfig GetConfigByEquipID(int EquipID)
        {
            if (forgeUnitProperties != null)
            {
                foreach (var item in forgeUnitProperties)
                {
                    if (item.unlockEuipIDs.Any(i => i == EquipID))
                    {
                        return item;
                    }
                }
                Log.e("Error : Not find id = " + EquipID);
                return default(ForgeUnitConfig);
            }
            else
            {
                Log.e("Error : Struct Array is null");
                return default(ForgeUnitConfig);
            }
        }

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
        public List<int> unlockEuipIDs;

        public ForgeUnitConfig(int level, string upgradeRes, int upgradeCoinCost, int upgradePrecondition,
            string modelRes, string unlockEquipId
            )
        {
            baseProperty = new ShipUnitBaseConfig(level, upgradeCoinCost, upgradeRes, upgradePrecondition, modelRes);

            #region unlockEuipIDs
            this.unlockEuipIDs = new List<int>();
            string[] strs = unlockEquipId.Split(';');
            for (int i = 0; i < strs.Length; i++)
                unlockEuipIDs.Add(int.Parse(strs[i]));
            #endregion
        }
    }
}