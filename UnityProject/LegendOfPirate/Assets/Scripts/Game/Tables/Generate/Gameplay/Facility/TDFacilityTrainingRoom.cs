//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDFacilityTrainingRoom
    {
        
       
        private EInt m_Level = 0;   
        private string m_UpgradeRes;   
        private EInt m_UpgradeCost = 0;   
        private EInt m_UpgradePreconditions = 0;   
        private EInt m_UpgradeSpeed = 0;   
        private string m_ModelResources;   
        private EInt m_Capacity = 0;   
        private EInt m_Experience = 0;   
        private EInt m_TrainingSpeed = 0;  
        
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
        /// 模型资源
        /// </summary>
        public  string  modelResources {get { return m_ModelResources; } }
       
        /// <summary>
        /// 训练人数
        /// </summary>
        public  int  capacity {get { return m_Capacity; } }
       
        /// <summary>
        /// 经验值
        /// </summary>
        public  int  experience {get { return m_Experience; } }
       
        /// <summary>
        /// 训练时间（s）
        /// </summary>
        public  int  trainingSpeed {get { return m_TrainingSpeed; } }
       

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
                    m_ModelResources = dataR.ReadString();
                    break;
                case 6:
                    m_Capacity = dataR.ReadInt();
                    break;
                case 7:
                    m_Experience = dataR.ReadInt();
                    break;
                case 8:
                    m_TrainingSpeed = dataR.ReadInt();
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
          ret.Add("UpgradeSpeed", 4);
          ret.Add("ModelResources", 5);
          ret.Add("Capacity", 6);
          ret.Add("Experience", 7);
          ret.Add("TrainingSpeed", 8);
          return ret;
        }
    } 
}//namespace LR