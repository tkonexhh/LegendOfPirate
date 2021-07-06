//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDFacilityBattleship
    {
        
       
        private EInt m_WarshipId = 0;   
        private EInt m_NextWarship = 0;   
        private string m_Name;   
        private string m_ModelResources;   
        private EInt m_UnlockAccountLevel = 0;   
        private string m_StrengthenCost;   
        private string m_AttributeValues;  
        
        //private Dictionary<string, TDUniversally.FieldData> m_DataCacheNoGenerate = new Dictionary<string, TDUniversally.FieldData>();
      
        /// <summary>
        /// 战船ID
        /// </summary>
        public  int  warshipId {get { return m_WarshipId; } }
       
        /// <summary>
        /// 下一级战船
        /// </summary>
        public  int  nextWarship {get { return m_NextWarship; } }
       
        /// <summary>
        /// 战船名称
        /// </summary>
        public  string  name {get { return m_Name; } }
       
        /// <summary>
        /// 模型资源
        /// </summary>
        public  string  modelResources {get { return m_ModelResources; } }
       
        /// <summary>
        /// 解锁等级条件（主船等级）
        /// </summary>
        public  int  unlockAccountLevel {get { return m_UnlockAccountLevel; } }
       
        /// <summary>
        /// 强化消耗（id|数量）
        /// </summary>
        public  string  strengthenCost {get { return m_StrengthenCost; } }
       
        /// <summary>
        /// 属性数值(参数|百分比%，参数：HP,ATK,Armor-护甲值)
        /// </summary>
        public  string  attributeValues {get { return m_AttributeValues; } }
       

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
                    m_WarshipId = dataR.ReadInt();
                    break;
                case 1:
                    m_NextWarship = dataR.ReadInt();
                    break;
                case 2:
                    m_Name = dataR.ReadString();
                    break;
                case 3:
                    m_ModelResources = dataR.ReadString();
                    break;
                case 4:
                    m_UnlockAccountLevel = dataR.ReadInt();
                    break;
                case 5:
                    m_StrengthenCost = dataR.ReadString();
                    break;
                case 6:
                    m_AttributeValues = dataR.ReadString();
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
          
          ret.Add("WarshipId", 0);
          ret.Add("NextWarship", 1);
          ret.Add("Name", 2);
          ret.Add("ModelResources", 3);
          ret.Add("UnlockAccountLevel", 4);
          ret.Add("StrengthenCost", 5);
          ret.Add("AttributeValues", 6);
          return ret;
        }
    } 
}//namespace LR