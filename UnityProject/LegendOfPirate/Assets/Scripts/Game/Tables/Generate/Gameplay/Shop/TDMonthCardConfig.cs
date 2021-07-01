//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDMonthCardConfig
    {
        
       
        private EInt m_Id = 0;   
        private string m_Name;   
        private EFloat m_Price = 0.0f;   
        private string m_DailyDiamond;   
        private string m_FirstDiamond;   
        private string m_RenewPrice;  
        
        //private Dictionary<string, TDUniversally.FieldData> m_DataCacheNoGenerate = new Dictionary<string, TDUniversally.FieldData>();
      
        /// <summary>
        /// ID
        /// </summary>
        public  int  id {get { return m_Id; } }
       
        /// <summary>
        /// 物品名称
        /// </summary>
        public  string  name {get { return m_Name; } }
       
        /// <summary>
        /// 价格
        /// </summary>
        public  float  price {get { return m_Price; } }
       
        /// <summary>
        /// 每日钻石
        /// </summary>
        public  string  dailyDiamond {get { return m_DailyDiamond; } }
       
        /// <summary>
        /// 首次钻石
        /// </summary>
        public  string  firstDiamond {get { return m_FirstDiamond; } }
       
        /// <summary>
        /// 续费价格
        /// </summary>
        public  string  renewPrice {get { return m_RenewPrice; } }
       

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
                    m_Name = dataR.ReadString();
                    break;
                case 2:
                    m_Price = dataR.ReadFloat();
                    break;
                case 3:
                    m_DailyDiamond = dataR.ReadString();
                    break;
                case 4:
                    m_FirstDiamond = dataR.ReadString();
                    break;
                case 5:
                    m_RenewPrice = dataR.ReadString();
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
          ret.Add("Name", 1);
          ret.Add("Price", 2);
          ret.Add("DailyDiamond", 3);
          ret.Add("FirstDiamond", 4);
          ret.Add("RenewPrice", 5);
          return ret;
        }
    } 
}//namespace LR