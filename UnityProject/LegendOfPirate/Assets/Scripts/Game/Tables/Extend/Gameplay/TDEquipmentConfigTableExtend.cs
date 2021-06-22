﻿using UnityEngine;
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
                {
                    equipmentUnitProperites = new EquipmentUnitConfig[rowCount];
                }
                if (m_EquipUnitIndex > equipmentUnitProperites.Length)
                {
                    throw new ArgumentOutOfRangeException("Equipment Data Out Of Range");
                }
                equipmentUnitProperites[m_EquipUnitIndex] = new EquipmentUnitConfig(tdData);
                m_EquipUnitIndex++;
            }
            catch (Exception e)
            {
                Log.e("e =" + e);
            }
        }
    }

    #region struct
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

    }

    public struct EquipmentUnitConfig
    {
        public int equipmentID;
        public int startLevel;
        public int nextEquipmentID;
        public string roleName;
        public EquipmentType equipmentType;


        public EquipStrengthenCost[] equipStrengthenCosts;
        public int coinCostNumber;
        public EquipAttributeValue equipAttributeValue;

        public EquipmentUnitConfig(TDEquipmentConfig tdData)
        {
            this.equipmentID = tdData.equipmentId;
            this.startLevel = tdData.starLevel;
            this.nextEquipmentID = tdData.nextEquipment;
            this.roleName = tdData.roleName;
            this.equipmentType = EnumUtil.ConvertStringToEnum<EquipmentType>(tdData.equipmentType);

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

            this.coinCostNumber = tdData.intensifyCost;
            equipAttributeValue = new EquipAttributeValue();
        }
    }
    #endregion
}