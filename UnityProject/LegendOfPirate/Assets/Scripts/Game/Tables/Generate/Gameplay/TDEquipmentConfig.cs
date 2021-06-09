//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDEquipmentConfig
    {
        
       
        private EInt m_EquipmentId = 0;   
        private EInt m_StarLevel = 0;   
        private EInt m_NextEquipment = 0;   
        private string m_RoleName;   
        private string m_EquipmentType;   
        private string m_ParamType;   
        private string m_StrengthenCost;  
        
        //private Dictionary<string, TDUniversally.FieldData> m_DataCacheNoGenerate = new Dictionary<string, TDUniversally.FieldData>();
      
        /// <summary>
        /// 装备ID
        /// </summary>
        public  int  equipmentId {get { return m_EquipmentId; } }
       
        /// <summary>
        /// 星级
        /// </summary>
        public  int  starLevel {get { return m_StarLevel; } }
       
        /// <summary>
        /// 下一星级装备
        /// </summary>
        public  int  nextEquipment {get { return m_NextEquipment; } }
       
        /// <summary>
        /// 装备名字
        /// </summary>
        public  string  roleName {get { return m_RoleName; } }
       
        /// <summary>
        /// 装备类型
        /// </summary>
        public  string  equipmentType {get { return m_EquipmentType; } }
       
        /// <summary>
        /// 装备属性类型
        /// </summary>
        public  string  paramType {get { return m_ParamType; } }
       
        /// <summary>
        /// 强化消耗
        /// </summary>
        public  string  strengthenCost {get { return m_StrengthenCost; } }
       

        public void ReadRow(DataStreamReader dataR, int[] filedIndex)
        {
          //var schemeNames = dataR.GetSchemeName();
          int col = 0;
          while(true)
          {
            col = dataR.MoreFieldOnRow();
            if (col == -1)
            {
              break;
            }
            switch (filedIndex[col])
            { 
            
                case 0:
                    m_EquipmentId = dataR.ReadInt();
                    break;
                case 1:
                    m_StarLevel = dataR.ReadInt();
                    break;
                case 2:
                    m_NextEquipment = dataR.ReadInt();
                    break;
                case 3:
                    m_RoleName = dataR.ReadString();
                    break;
                case 4:
                    m_EquipmentType = dataR.ReadString();
                    break;
                case 5:
                    m_ParamType = dataR.ReadString();
                    break;
                case 6:
                    m_StrengthenCost = dataR.ReadString();
                    break;
                default:
                    //TableHelper.CacheNewField(dataR, schemeNames[col], m_DataCacheNoGenerate);
                    break;
            }
          }

        }
        
        public static Dictionary<string, int> GetFieldHeadIndex()
        {
          Dictionary<string, int> ret = new Dictionary<string, int>(7);
          
          ret.Add("EquipmentId", 0);
          ret.Add("StarLevel", 1);
          ret.Add("NextEquipment", 2);
          ret.Add("RoleName", 3);
          ret.Add("EquipmentType", 4);
          ret.Add("ParamType", 5);
          ret.Add("StrengthenCost", 6);
          return ret;
        }
    } 
}//namespace LR