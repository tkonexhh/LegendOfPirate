//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDFacilityGarden
    {
        
       
        private EInt m_Level = 0;   
        private string m_UpgradeRes;   
        private EInt m_UpgradeCost = 0;   
        private EInt m_UpgradePreconditions = 0;   
        private EInt m_UpgradeSpeed = 0;   
        private EInt m_SeedUnlock = 0;   
        private string m_ModelResources;   
        private EInt m_PlantingSpeed = 0;  
        
        //private Dictionary<string, TDUniversally.FieldData> m_DataCacheNoGenerate = new Dictionary<string, TDUniversally.FieldData>();
      
        /// <summary>
        /// ID
        /// </summary>
        public  int  level {get { return m_Level; } }
       
        /// <summary>
        /// 升级资源（id|数量）
        /// </summary>
        public  string  upgradeRes {get { return m_UpgradeRes; } }
       
        /// <summary>
        /// 升级花费
        /// </summary>
        public  int  upgradeCost {get { return m_UpgradeCost; } }
       
        /// <summary>
        /// 升级条件（主船等级）
        /// </summary>
        public  int  upgradePreconditions {get { return m_UpgradePreconditions; } }
       
        /// <summary>
        /// 升级时间（s）
        /// </summary>
        public  int  upgradeSpeed {get { return m_UpgradeSpeed; } }
       
        /// <summary>
        /// 解锁种子（id）
        /// </summary>
        public  int  seedUnlock {get { return m_SeedUnlock; } }
       
        /// <summary>
        /// 模型资源
        /// </summary>
        public  string  modelResources {get { return m_ModelResources; } }
       
        /// <summary>
        /// 种植时间（s）
        /// </summary>
        public  int  plantingSpeed {get { return m_PlantingSpeed; } }
       

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
                    m_UpgradeRes = dataR.ReadString();
                    break;
                case 2:
                    m_UpgradeCost = dataR.ReadInt();
                    break;
                case 3:
                    m_UpgradePreconditions = dataR.ReadInt();
                    break;
                case 4:
                    m_UpgradeSpeed = dataR.ReadInt();
                    break;
                case 5:
                    m_SeedUnlock = dataR.ReadInt();
                    break;
                case 6:
                    m_ModelResources = dataR.ReadString();
                    break;
                case 7:
                    m_PlantingSpeed = dataR.ReadInt();
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
          ret.Add("UpgradeRes", 1);
          ret.Add("UpgradeCost", 2);
          ret.Add("UpgradePreconditions", 3);
          ret.Add("UpgradeSpeed", 4);
          ret.Add("SeedUnlock", 5);
          ret.Add("ModelResources", 6);
          ret.Add("PlantingSpeed", 7);
          return ret;
        }
    } 
}//namespace LR