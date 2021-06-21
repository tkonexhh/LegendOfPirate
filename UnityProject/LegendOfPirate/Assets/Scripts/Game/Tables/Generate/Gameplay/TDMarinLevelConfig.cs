//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDMarinLevelConfig
    {
        
       
        private EInt m_Level = 0;   
        private EInt m_Chapter = 0;   
        private string m_Type;   
        private string m_ResOutput;   
        private string m_EnemyHeadIcon;   
        private string m_Reward;   
        private string m_RecommendAtkValue;   
        private string m_BattleName;  
        
        //private Dictionary<string, TDUniversally.FieldData> m_DataCacheNoGenerate = new Dictionary<string, TDUniversally.FieldData>();
      
        /// <summary>
        /// ID
        /// </summary>
        public  int  level {get { return m_Level; } }
       
        /// <summary>
        /// 所属章节
        /// </summary>
        public  int  chapter {get { return m_Chapter; } }
       
        /// <summary>
        /// 关卡类型
        /// </summary>
        public  string  type {get { return m_Type; } }
       
        /// <summary>
        /// 产出资源
        /// </summary>
        public  string  resOutput {get { return m_ResOutput; } }
       
        /// <summary>
        /// 敌人头像
        /// </summary>
        public  string  enemyHeadIcon {get { return m_EnemyHeadIcon; } }
       
        /// <summary>
        /// 奖励
        /// </summary>
        public  string  reward {get { return m_Reward; } }
       
        /// <summary>
        /// 推荐功力
        /// </summary>
        public  string  recommendAtkValue {get { return m_RecommendAtkValue; } }
       
        /// <summary>
        /// 战斗名称
        /// </summary>
        public  string  battleName {get { return m_BattleName; } }
       

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
                    m_Chapter = dataR.ReadInt();
                    break;
                case 2:
                    m_Type = dataR.ReadString();
                    break;
                case 3:
                    m_ResOutput = dataR.ReadString();
                    break;
                case 4:
                    m_EnemyHeadIcon = dataR.ReadString();
                    break;
                case 5:
                    m_Reward = dataR.ReadString();
                    break;
                case 6:
                    m_RecommendAtkValue = dataR.ReadString();
                    break;
                case 7:
                    m_BattleName = dataR.ReadString();
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
          ret.Add("Chapter", 1);
          ret.Add("Type", 2);
          ret.Add("ResOutput", 3);
          ret.Add("EnemyHeadIcon", 4);
          ret.Add("Reward", 5);
          ret.Add("RecommendAtkValue", 6);
          ret.Add("BattleName", 7);
          return ret;
        }
    } 
}//namespace LR