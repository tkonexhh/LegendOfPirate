//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDFacilityFishingPlatform
    {
        
       
        private EInt m_Level = 0;   
        private string m_UpgradeRes;   
        private EInt m_UpgradeCost = 0;   
        private EInt m_UpgradePreconditions = 0;   
        private string m_FishingRod;   
        private string m_ModelResources;   
        private EInt m_FishingSpeed = 0;   
        private string m_UnlockRecipe;   
        private EInt m_Capability = 0;  
        
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
        /// 鱼竿名称
        /// </summary>
        public  string  fishingRod {get { return m_FishingRod; } }
       
        /// <summary>
        /// 模型资源
        /// </summary>
        public  string  modelResources {get { return m_ModelResources; } }
       
        /// <summary>
        /// 钓鱼时间（s）
        /// </summary>
        public  int  fishingSpeed {get { return m_FishingSpeed; } }
       
        /// <summary>
        /// 各种鱼的概率%（id|概率%）
        /// </summary>
        public  string  unlockRecipe {get { return m_UnlockRecipe; } }
       
        /// <summary>
        /// 钓鱼人数
        /// </summary>
        public  int  capability {get { return m_Capability; } }
       

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
                    m_FishingRod = dataR.ReadString();
                    break;
                case 5:
                    m_ModelResources = dataR.ReadString();
                    break;
                case 6:
                    m_FishingSpeed = dataR.ReadInt();
                    break;
                case 7:
                    m_UnlockRecipe = dataR.ReadString();
                    break;
                case 8:
                    m_Capability = dataR.ReadInt();
                    break;
                default:
                    //TableHelper.CacheNewField(dataR, schemeNames[col], m_DataCacheNoGenerate);
                    break;
            }
          }

        }
        
        public static Dictionary<string, int> GetFieldHeadIndex()
        {
          Dictionary<string, int> ret = new Dictionary<string, int>(9);
          
          ret.Add("Level", 0);
          ret.Add("UpgradeRes", 1);
          ret.Add("UpgradeCost", 2);
          ret.Add("UpgradePreconditions", 3);
          ret.Add("FishingRod", 4);
          ret.Add("ModelResources", 5);
          ret.Add("FishingSpeed", 6);
          ret.Add("UnlockRecipe", 7);
          ret.Add("Capability", 8);
          return ret;
        }
    } 
}//namespace LR