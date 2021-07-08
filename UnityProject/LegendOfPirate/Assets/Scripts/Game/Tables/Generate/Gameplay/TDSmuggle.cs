//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDSmuggle
    {
        
       
        private EInt m_Id = 0;   
        private string m_AddName;   
        private string m_BannerName;   
        private EInt m_Time = 0;   
        private EInt m_Award = 0;   
        private string m_Coefficient;  
        
        //private Dictionary<string, TDUniversally.FieldData> m_DataCacheNoGenerate = new Dictionary<string, TDUniversally.FieldData>();
      
        /// <summary>
        /// ID
        /// </summary>
        public  int  id {get { return m_Id; } }
       
        /// <summary>
        /// 地点名称
        /// </summary>
        public  string  addName {get { return m_AddName; } }
       
        /// <summary>
        /// 插图名
        /// </summary>
        public  string  bannerName {get { return m_BannerName; } }
       
        /// <summary>
        /// 所需时间
        /// </summary>
        public  int  time {get { return m_Time; } }
       
        /// <summary>
        /// 奖励内容
        /// </summary>
        public  int  award {get { return m_Award; } }
       
        /// <summary>
        /// 系数
        /// </summary>
        public  string  coefficient {get { return m_Coefficient; } }
       

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
                    m_AddName = dataR.ReadString();
                    break;
                case 2:
                    m_BannerName = dataR.ReadString();
                    break;
                case 3:
                    m_Time = dataR.ReadInt();
                    break;
                case 4:
                    m_Award = dataR.ReadInt();
                    break;
                case 5:
                    m_Coefficient = dataR.ReadString();
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
          
          ret.Add("Id", 0);
          ret.Add("AddName", 1);
          ret.Add("BannerName", 2);
          ret.Add("Time", 3);
          ret.Add("Award", 4);
          ret.Add("Coefficient", 5);
          return ret;
        }
    } 
}//namespace LR