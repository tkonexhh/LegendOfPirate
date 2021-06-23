using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDEquipmentConfigTable
    {
        public static EquipmentUnitConfig[] equipmentUnitProperites = null;
        private static int m_EquipUnitIndex = 0;

        static void CompleteRowAdd(TDEquipmentConfig tdData, int rowCount)
        {
            //Try Catch 防止表内数据有问题
            try
            {
                if (equipmentUnitProperites==null)
                    equipmentUnitProperites = new EquipmentUnitConfig[rowCount];
            
                if (m_EquipUnitIndex > equipmentUnitProperites.Length)
                    throw new ArgumentOutOfRangeException("Equipment Data Out Of Range");
            
                equipmentUnitProperites[m_EquipUnitIndex] = new EquipmentUnitConfig(tdData);
                m_EquipUnitIndex++;
            }
            catch (Exception e)
            {
                Log.e("e =" + e);
            }
        }
        #region Public
        public static string GetEquipmentNameById(int id)
        {
            var fitId = id * 10 + 1;
            foreach (var item in dataList)
            {
                if (item.equipmentId == fitId) return item.roleName.Replace("1", "");
            }
            return null;
        }
        public static EquipmentUnitConfig GetEquipmentConfigByID(int equipID)
        {
            EquipmentUnitConfig equipConfig;
            foreach (var item in equipmentUnitProperites)
            {
                if (item.equipmentID == equipID)
                {
                    equipConfig = item;
                    return equipConfig;
                }
            }
            return default(EquipmentUnitConfig);
        }
        #endregion
    }
    #region Struct
    /// <summary>
    /// 装备强化消耗
    /// </summary>
    public struct EquipStrengthenCost
    {
        public int materialID;
        public int materialCostNumber;
        public EquipStrengthenCost(string strs)
        {
            string[] subStrs = strs.Split('|');
            this.materialID = int.Parse(subStrs[0]);
            this.materialCostNumber = int.Parse(subStrs[1]);
        }
    }

    /// <summary>
    /// 装备属性数值
    /// </summary>
    public struct EquipAttributeValue
    {
        public EquipAttributeType equipAttrType;
        public float percentage;
        public EquipAttributeValue(string strs)
        {
            string[] subStrs = strs.Split('|');
            this.equipAttrType = EnumUtil.ConvertStringToEnum<EquipAttributeType>(subStrs[0]);
            this.percentage = float.Parse(subStrs[1]);
        }
    }

    public struct EquipmentUnitConfig
    {
        public int equipmentID;
        public int startLevel;
        public int nextEquipmentID;
        public string equipName;
        public EquipmentType equipmentType;
        public EquipAttributeValue[] equipAttributeValues;
        public EquipQualityType equipQualityType;
        public EquipStrengthenCost[] equipStrengthenCosts;
        public int coinCostNumber;
        public string Desc;

        public EquipmentUnitConfig(TDEquipmentConfig tdData)
        {
            this.equipmentID = tdData.equipmentId;
            this.startLevel = tdData.starLevel;
            this.nextEquipmentID = tdData.nextEquipment;
            this.equipName = tdData.roleName;
            this.equipmentType = EnumUtil.ConvertStringToEnum<EquipmentType>(tdData.equipmentType);
            this.equipQualityType = EnumUtil.ConvertStringToEnum<EquipQualityType>(tdData.quality);
            this.coinCostNumber = tdData.intensifyCost;
            this.Desc = tdData.desc;
            #region Analysis EquipAttributeValue
            if (!string.IsNullOrEmpty(tdData.paramValue))
            {
                string[] strs = tdData.paramValue.Split(';');
                equipAttributeValues = new EquipAttributeValue[strs.Length];
                for (int i = 0; i < strs.Length; i++)
                {
                    EquipAttributeValue attr = new EquipAttributeValue(strs[i]);
                    equipAttributeValues[i] = attr;
                }
            }
            else
            {
                equipAttributeValues = new EquipAttributeValue[0];
            }
            #endregion
            #region Analysis EquipStrengthenCost
            if (!string.IsNullOrEmpty(tdData.strengthenCost))
            {
                string[] strs = tdData.strengthenCost.Split(';');
                equipStrengthenCosts = new EquipStrengthenCost[strs.Length];
                for (int i = 0; i < strs.Length; i++)
                {
                    EquipStrengthenCost cost = new EquipStrengthenCost(strs[i]);
                    equipStrengthenCosts[i] = cost;
                }
            }
            else
            {
                equipStrengthenCosts = new EquipStrengthenCost[0];
            }
            #endregion
        }   
    }
    #endregion
}