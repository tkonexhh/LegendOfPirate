//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDLevelConfig
    {
        
       
        private EInt m_Level = 0;   
        private EInt m_Time = 0;   
        private EInt m_ModelCount = 0;   
        private EInt m_UnlockNewModel = 0;  
        
        //private Dictionary<string, TDUniversally.FieldData> m_DataCacheNoGenerate = new Dictionary<string, TDUniversally.FieldData>();
      
        /// <summary>
        /// ID
        /// </summary>
        public  int  level {get { return m_Level; } }
       
        /// <summary>
        /// Key
        /// </summary>
        public  int  time {get { return m_Time; } }
       
        /// <summary>
        /// Value
        /// </summary>
        public  int  modelCount {get { return m_ModelCount; } }
       
        /// <summary>
        /// Value
        /// </summary>
        public  int  unlockNewModel {get { return m_UnlockNewModel; } }
       

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
                    m_Time = dataR.ReadInt();
                    break;
                case 2:
                    m_ModelCount = dataR.ReadInt();
                    break;
                case 3:
                    m_UnlockNewModel = dataR.ReadInt();
                    break;
                default:
                    //TableHelper.CacheNewField(dataR, schemeNames[col], m_DataCacheNoGenerate);
                    break;
            }
          }

        }
        
        public static Dictionary<string, int> GetFieldHeadIndex()
        {
          Dictionary<string, int> ret = new Dictionary<string, int>(4);
          
          ret.Add("Level", 0);
          ret.Add("Time", 1);
          ret.Add("ModelCount", 2);
          ret.Add("UnlockNewModel", 3);
          return ret;
        }
    } 
}//namespace LR