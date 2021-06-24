//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDRoleSkillConfig
    {
        
       
        private EInt m_SkillId = 0;   
        private string m_SkillName;   
        private string m_SkillParams;   
        private string m_SkillDsc;   
        private string m_SkillUpCost;   
        private string m_SkillUpRequirement;  
        
        //private Dictionary<string, TDUniversally.FieldData> m_DataCacheNoGenerate = new Dictionary<string, TDUniversally.FieldData>();
      
        /// <summary>
        /// 技能ID
        /// </summary>
        public  int  skillId {get { return m_SkillId; } }
       
        /// <summary>
        /// 技能名称
        /// </summary>
        public  string  skillName {get { return m_SkillName; } }
       
        /// <summary>
        /// 技能属性
        /// </summary>
        public  string  skillParams {get { return m_SkillParams; } }
       
        /// <summary>
        /// 技能描述
        /// </summary>
        public  string  skillDsc {get { return m_SkillDsc; } }
       
        /// <summary>
        /// 技能解锁/升级消耗
        /// </summary>
        public  string  skillUpCost {get { return m_SkillUpCost; } }
       
        /// <summary>
        /// 技能解锁/升级条件（角色等级）
        /// </summary>
        public  string  skillUpRequirement {get { return m_SkillUpRequirement; } }
       

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
                    m_SkillId = dataR.ReadInt();
                    break;
                case 1:
                    m_SkillName = dataR.ReadString();
                    break;
                case 2:
                    m_SkillParams = dataR.ReadString();
                    break;
                case 3:
                    m_SkillDsc = dataR.ReadString();
                    break;
                case 4:
                    m_SkillUpCost = dataR.ReadString();
                    break;
                case 5:
                    m_SkillUpRequirement = dataR.ReadString();
                    break;
                default:
                    //TableHelper.CacheNewField(dataR, schemeNames[col], m_DataCacheNoGenerate);
                    break;
            }
          }

        }
        
        public static Dictionary<string, int> GetFieldHeadIndex()
        {
          Dictionary<string, int> ret = new Dictionary<string, int>(6);
          
          ret.Add("SkillId", 0);
          ret.Add("SkillName", 1);
          ret.Add("SkillParams", 2);
          ret.Add("SkillDsc", 3);
          ret.Add("SkillUpCost", 4);
          ret.Add("SkillUpRequirement", 5);
          return ret;
        }
    } 
}//namespace LR