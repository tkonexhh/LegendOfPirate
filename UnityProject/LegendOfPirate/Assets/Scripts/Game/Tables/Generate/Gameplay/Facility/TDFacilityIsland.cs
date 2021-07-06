//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDFacilityIsland
    {
        
       
        private EInt m_Level = 0;   
        private string m_Name;   
        private string m_UpgradeRes;   
        private string m_UpgradeCost;   
        private string m_ResourceID;   
        private string m_OutputSpeed;   
        private string m_StorageLimits;  
        
        //private Dictionary<string, TDUniversally.FieldData> m_DataCacheNoGenerate = new Dictionary<string, TDUniversally.FieldData>();
      
        /// <summary>
        /// ID
        /// </summary>
        public  int  level {get { return m_Level; } }
       
        /// <summary>
        /// 名称
        /// </summary>
        public  string  name {get { return m_Name; } }
       
        /// <summary>
        /// 升级资源（id|数量）
        /// </summary>
        public  string  upgradeRes {get { return m_UpgradeRes; } }
       
        /// <summary>
        /// 升级花费
        /// </summary>
        public  string  upgradeCost {get { return m_UpgradeCost; } }
       
        /// <summary>
        /// 资源产出（id|概率%）
        /// </summary>
        public  string  resourceID {get { return m_ResourceID; } }
       
        /// <summary>
        /// 产出速率（每秒产出数量，资源岛等级|数量）
        /// </summary>
        public  string  outputSpeed {get { return m_OutputSpeed; } }
       
        /// <summary>
        /// 存储上限 资源岛等级|数量
        /// </summary>
        public  string  storageLimits {get { return m_StorageLimits; } }
       

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
                    m_Level = dataR.ReadInt();
                    break;
                case 1:
                    m_Name = dataR.ReadString();
                    break;
                case 2:
                    m_UpgradeRes = dataR.ReadString();
                    break;
                case 3:
                    m_UpgradeCost = dataR.ReadString();
                    break;
                case 4:
                    m_ResourceID = dataR.ReadString();
                    break;
                case 5:
                    m_OutputSpeed = dataR.ReadString();
                    break;
                case 6:
                    m_StorageLimits = dataR.ReadString();
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
          
          ret.Add("Level", 0);
          ret.Add("Name", 1);
          ret.Add("UpgradeRes", 2);
          ret.Add("UpgradeCost", 3);
          ret.Add("ResourceID", 4);
          ret.Add("OutputSpeed", 5);
          ret.Add("StorageLimits", 6);
          return ret;
        }
    } 
}//namespace LR