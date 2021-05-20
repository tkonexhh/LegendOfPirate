//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDGuideSpeed
    {
        
       
        private EInt m_Id = 0;   
        private string m_Trigger;   
        private string m_CommonParam;   
        private EInt m_RequireGuideId = 0;   
        private string m_JumpTrigger;  
        
        //private Dictionary<string, TDUniversally.FieldData> m_DataCacheNoGenerate = new Dictionary<string, TDUniversally.FieldData>();
      
        /// <summary>
        /// ID
        /// </summary>
        public  int  id {get { return m_Id; } }
       
        /// <summary>
        /// Trigger
        /// </summary>
        public  string  trigger {get { return m_Trigger; } }
       
        /// <summary>
        /// CommonParam
        /// </summary>
        public  string  commonParam {get { return m_CommonParam; } }
       
        /// <summary>
        /// RequireGuideId
        /// </summary>
        public  int  requireGuideId {get { return m_RequireGuideId; } }
       
        /// <summary>
        /// JumpTrigger
        /// </summary>
        public  string  jumpTrigger {get { return m_JumpTrigger; } }
       

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
                    m_Trigger = dataR.ReadString();
                    break;
                case 2:
                    m_CommonParam = dataR.ReadString();
                    break;
                case 3:
                    m_RequireGuideId = dataR.ReadInt();
                    break;
                case 4:
                    m_JumpTrigger = dataR.ReadString();
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
          
          ret.Add("Id", 0);
          ret.Add("Trigger", 1);
          ret.Add("CommonParam", 2);
          ret.Add("RequireGuideId", 3);
          ret.Add("JumpTrigger", 4);
          return ret;
        }
    } 
}//namespace LR