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
        private EInt m_ResourceID = 0;   
        private EInt m_OutputSpeed = 0;   
        private EInt m_StorageLimits = 0;   
        private string m_UpgradeRes;   
        private EInt m_UpgradeCost = 0;   
        private EInt m_UpgradeTime = 0;  
        
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
        /// 资源产出（id）
        /// </summary>
        public  int  resourceID {get { return m_ResourceID; } }
       
        /// <summary>
        /// 产出速率（每秒产出数量）
        /// </summary>
        public  int  outputSpeed {get { return m_OutputSpeed; } }
       
        /// <summary>
        /// 存储上限
        /// </summary>
        public  int  storageLimits {get { return m_StorageLimits; } }
       
        /// <summary>
        /// 升级资源（id|数量）
        /// </summary>
        public  string  upgradeRes {get { return m_UpgradeRes; } }
       
        /// <summary>
        /// 升级花费
        /// </summary>
        public  int  upgradeCost {get { return m_UpgradeCost; } }
       
        /// <summary>
        /// 升级时间（s）
        /// </summary>
        public  int  upgradeTime {get { return m_UpgradeTime; } }
       

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
                    m_ResourceID = dataR.ReadInt();
                    break;
                case 3:
                    m_OutputSpeed = dataR.ReadInt();
                    break;
                case 4:
                    m_StorageLimits = dataR.ReadInt();
                    break;
                case 5:
                    m_UpgradeRes = dataR.ReadString();
                    break;
                case 6:
                    m_UpgradeCost = dataR.ReadInt();
                    break;
                case 7:
                    m_UpgradeTime = dataR.ReadInt();
                    break;
                default:
                    //TableHelper.CacheNewField(dataR, schemeNames[col], m_DataCacheNoGenerate);
                    break;
            }
          }

        }
        
        public static Dictionary<string, int> GetFieldHeadIndex()
        {
          Dictionary<string, int> ret = new Dictionary<string, int>(8);
          
          ret.Add("Level", 0);
          ret.Add("Name", 1);
          ret.Add("ResourceID", 2);
          ret.Add("OutputSpeed", 3);
          ret.Add("StorageLimits", 4);
          ret.Add("UpgradeRes", 5);
          ret.Add("UpgradeCost", 6);
          ret.Add("UpgradeTime", 7);
          return ret;
        }
    } 
}//namespace LR