//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDGlobalConfig
    {
        
       
        private EInt m_Id = 0;   
        private EFloat m_RoleAttributesGrowRateOfLevel = 0.0f;   
        private EFloat m_RoleAttributesGrowRateOfStar = 0.0f;  
        
        //private Dictionary<string, TDUniversally.FieldData> m_DataCacheNoGenerate = new Dictionary<string, TDUniversally.FieldData>();
      
        /// <summary>
        /// 序号
        /// </summary>
        public  int  id {get { return m_Id; } }
       
        /// <summary>
        /// 角色属性等级成长系数
        /// </summary>
        public  float  roleAttributesGrowRateOfLevel {get { return m_RoleAttributesGrowRateOfLevel; } }
       
        /// <summary>
        /// 角色属性星级成长系数
        /// </summary>
        public  float  roleAttributesGrowRateOfStar {get { return m_RoleAttributesGrowRateOfStar; } }
       

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
                    m_Id = dataR.ReadInt();
                    break;
                case 1:
                    m_RoleAttributesGrowRateOfLevel = dataR.ReadFloat();
                    break;
                case 2:
                    m_RoleAttributesGrowRateOfStar = dataR.ReadFloat();
                    break;
                default:
                    //TableHelper.CacheNewField(dataR, schemeNames[col], m_DataCacheNoGenerate);
                    break;
            }
          }

        }
        
        public static Dictionary<string, int> GetFieldHeadIndex()
        {
          Dictionary<string, int> ret = new Dictionary<string, int>(3);
          
          ret.Add("Id", 0);
          ret.Add("RoleAttributesGrowRateOfLevel", 1);
          ret.Add("RoleAttributesGrowRateOfStar", 2);
          return ret;
        }
    } 
}//namespace LR