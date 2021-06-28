//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDAchievementTask
    {
        
       
        private EInt m_AchievementID = 0;   
        private string m_AchievementType;   
        private string m_AchievementDescription;   
        private string m_TaskCount;   
        private string m_Reward;  
        
        //private Dictionary<string, TDUniversally.FieldData> m_DataCacheNoGenerate = new Dictionary<string, TDUniversally.FieldData>();
      
        /// <summary>
        /// 成就ID
        /// </summary>
        public  int  achievementID {get { return m_AchievementID; } }
       
        /// <summary>
        /// 成就类型
        /// </summary>
        public  string  achievementType {get { return m_AchievementType; } }
       
        /// <summary>
        /// 成就描述
        /// </summary>
        public  string  achievementDescription {get { return m_AchievementDescription; } }
       
        /// <summary>
        /// 数量参数
        /// </summary>
        public  string  taskCount {get { return m_TaskCount; } }
       
        /// <summary>
        /// 奖励
        /// </summary>
        public  string  reward {get { return m_Reward; } }
       

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
                    m_AchievementID = dataR.ReadInt();
                    break;
                case 1:
                    m_AchievementType = dataR.ReadString();
                    break;
                case 2:
                    m_AchievementDescription = dataR.ReadString();
                    break;
                case 3:
                    m_TaskCount = dataR.ReadString();
                    break;
                case 4:
                    m_Reward = dataR.ReadString();
                    break;
                default:
                    //TableHelper.CacheNewField(dataR, schemeNames[col], m_DataCacheNoGenerate);
                    break;
            }
          }

        }
        
        public static Dictionary<string, int> GetFieldHeadIndex()
        {
          Dictionary<string, int> ret = new Dictionary<string, int>(5);
          
          ret.Add("AchievementID", 0);
          ret.Add("AchievementType", 1);
          ret.Add("AchievementDescription", 2);
          ret.Add("TaskCount", 3);
          ret.Add("Reward", 4);
          return ret;
        }
    } 
}//namespace LR