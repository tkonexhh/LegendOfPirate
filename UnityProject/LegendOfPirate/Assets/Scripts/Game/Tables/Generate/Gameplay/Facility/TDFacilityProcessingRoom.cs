//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDFacilityProcessingRoom
    {
        
       
        private EInt m_Level = 0;   
        private string m_UpgradeRes;   
        private EInt m_UpgradeCost = 0;   
        private EInt m_UpgradePreconditions = 0;   
        private EInt m_UpgradeTime = 0;   
        private string m_ModelResources;   
        private string m_UnlockPartID;   
        private string m_UnlockPartSpace;   
        private string m_UnlockSpaceCost;  
        
        //private Dictionary<string, TDUniversally.FieldData> m_DataCacheNoGenerate = new Dictionary<string, TDUniversally.FieldData>();
      
        /// <summary>
        /// ID
        /// </summary>
        public  int  level {get { return m_Level; } }
       
        /// <summary>
        /// 升级资源
        /// </summary>
        public  string  upgradeRes {get { return m_UpgradeRes; } }
       
        /// <summary>
        /// 升级花费（普通货币）
        /// </summary>
        public  int  upgradeCost {get { return m_UpgradeCost; } }
       
        /// <summary>
        /// 升级条件
        /// </summary>
        public  int  upgradePreconditions {get { return m_UpgradePreconditions; } }
       
        /// <summary>
        /// 升级时间（分钟）
        /// </summary>
        public  int  upgradeTime {get { return m_UpgradeTime; } }
       
        /// <summary>
        /// 模型资源
        /// </summary>
        public  string  modelResources {get { return m_ModelResources; } }
       
        /// <summary>
        /// 解锁组件制作图
        /// </summary>
        public  string  unlockPartID {get { return m_UnlockPartID; } }
       
        /// <summary>
        /// 解锁制作位数量
        /// </summary>
        public  string  unlockPartSpace {get { return m_UnlockPartSpace; } }
       
        /// <summary>
        /// 主动解锁制作位置消耗
        /// </summary>
        public  string  unlockSpaceCost {get { return m_UnlockSpaceCost; } }
       

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
                    m_UpgradeTime = dataR.ReadInt();
                    break;
                case 5:
                    m_ModelResources = dataR.ReadString();
                    break;
                case 6:
                    m_UnlockPartID = dataR.ReadString();
                    break;
                case 7:
                    m_UnlockPartSpace = dataR.ReadString();
                    break;
                case 8:
                    m_UnlockSpaceCost = dataR.ReadString();
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
          ret.Add("UpgradeTime", 4);
          ret.Add("ModelResources", 5);
          ret.Add("UnlockPartID", 6);
          ret.Add("UnlockPartSpace", 7);
          ret.Add("UnlockSpaceCost", 8);
          return ret;
        }
    } 
}//namespace LR