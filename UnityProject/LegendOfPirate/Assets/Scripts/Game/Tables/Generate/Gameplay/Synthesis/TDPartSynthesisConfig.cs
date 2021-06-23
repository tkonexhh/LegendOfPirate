//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDPartSynthesisConfig
    {
        
       
        private EInt m_Id = 0;   
        private string m_Desc;   
        private string m_MakeRes;   
        private EInt m_MakeTime = 0;   
        private EInt m_MakeCost = 0;  
        
        //private Dictionary<string, TDUniversally.FieldData> m_DataCacheNoGenerate = new Dictionary<string, TDUniversally.FieldData>();
      
        /// <summary>
        /// ID
        /// </summary>
        public  int  id {get { return m_Id; } }
       
        /// <summary>
        /// 说明
        /// </summary>
        public  string  desc {get { return m_Desc; } }
       
        /// <summary>
        /// 制作材料（id|数量）
        /// </summary>
        public  string  makeRes {get { return m_MakeRes; } }
       
        /// <summary>
        /// 制作时间（s）
        /// </summary>
        public  int  makeTime {get { return m_MakeTime; } }
       
        /// <summary>
        /// 制作花费
        /// </summary>
        public  int  makeCost {get { return m_MakeCost; } }
       

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
                    m_Desc = dataR.ReadString();
                    break;
                case 2:
                    m_MakeRes = dataR.ReadString();
                    break;
                case 3:
                    m_MakeTime = dataR.ReadInt();
                    break;
                case 4:
                    m_MakeCost = dataR.ReadInt();
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
          ret.Add("Desc", 1);
          ret.Add("MakeRes", 2);
          ret.Add("MakeTime", 3);
          ret.Add("MakeCost", 4);
          return ret;
        }
    } 
}//namespace LR