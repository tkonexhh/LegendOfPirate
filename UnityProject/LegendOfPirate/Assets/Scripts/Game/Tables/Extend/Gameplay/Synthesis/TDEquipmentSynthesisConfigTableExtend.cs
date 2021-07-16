using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDEquipmentSynthesisConfigTable
    {
        public static EquipmentSynthesisConfig[] EquipmentSynthesisConfigs = null;
        private static int m_Index = 0;
        static void CompleteRowAdd(TDEquipmentSynthesisConfig tdData, int rowCount)
        {
            try
            {
                if (EquipmentSynthesisConfigs == null)
                    EquipmentSynthesisConfigs = new EquipmentSynthesisConfig[rowCount];
                EquipmentSynthesisConfigs[m_Index] = new EquipmentSynthesisConfig(tdData);
                m_Index++;
            }
            catch (Exception e)
            {
                Log.e("error : id = " + tdData.id);
                Log.e("e : " + e);
            }
        }

        public static EquipmentSynthesisConfig GetEquipmentSynthesisConfigByID(int id)
        {
            EquipmentSynthesisConfig equipmentSynthesisConfig ;
            foreach (var item in EquipmentSynthesisConfigs)
            {
                if (item.id == id)
                {
                    equipmentSynthesisConfig = item;
                    return equipmentSynthesisConfig;
                }
            }
            Log.e("Error : Not Find ID = " + id);
            return EquipmentSynthesisConfigs[0];
        }
    }

    #region Struct
    public struct MakeRes
    {
        public int id;
        public int number;

        public MakeRes(string makeRes)
        {
            string[] strs = makeRes.Split('|');
            this.id = int.Parse(strs[0]);
            this.number = int.Parse(strs[1]);
        }
    }

    public struct EquipmentSynthesisConfig
    {
        public int id;
        public string Desc;
        public MakeRes makeRes;
        public int makeTime;
        public int makeCost;
        
        public EquipmentSynthesisConfig(TDEquipmentSynthesisConfig tdData)
        {
            this.id = tdData.id;
            this.Desc = tdData.desc;
            this.makeRes = new MakeRes(tdData.makeRes);
            this.makeTime = tdData.makeTime;
            this.makeCost = tdData.makeCost;
        }
    }
    #endregion
}