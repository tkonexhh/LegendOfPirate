//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDRoleStarUpConfig
    {
        
       
        private EInt m_StarLevel = 0;   
        private EInt m_StarUpCost = 0;  
        
        //private Dictionary<string, TDUniversally.FieldData> m_DataCacheNoGenerate = new Dictionary<string, TDUniversally.FieldData>();
      
        /// <summary>
        /// 角色星级
        /// </summary>
        public  int  starLevel {get { return m_StarLevel; } }
       
        /// <summary>
        /// 升星消耗碎片数量，1级为解锁需要数量
        /// </summary>
        public  int  starUpCost {get { return m_StarUpCost; } }
       

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
                    m_StarLevel = dataR.ReadInt();
                    break;
                case 1:
                    m_StarUpCost = dataR.ReadInt();
                    break;
                default:
                    //TableHelper.CacheNewField(dataR, schemeNames[col], m_DataCacheNoGenerate);
                    break;
            }
          }

        }
        
        public static Dictionary<string, int> GetFieldHeadIndex()
        {
          Dictionary<string, int> ret = new Dictionary<string, int>(2);
          
          ret.Add("StarLevel", 0);
          ret.Add("StarUpCost", 1);
          return ret;
        }
    } 
}//namespace LR