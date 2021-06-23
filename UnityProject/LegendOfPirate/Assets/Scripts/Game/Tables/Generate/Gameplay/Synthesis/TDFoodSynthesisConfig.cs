//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDFoodSynthesisConfig
    {
        
       
        private EInt m_Id = 0;   
        private string m_Name;   
        private string m_SpriteName;   
        private string m_Quality;   
        private string m_Desc;   
        private string m_MakeRes;   
        private string m_BuffType;   
        private EInt m_BuffRate = 0;   
        private EInt m_BuffTime = 0;   
        private EInt m_MakeTime = 0;   
        private EInt m_MakeCost = 0;  
        
        //private Dictionary<string, TDUniversally.FieldData> m_DataCacheNoGenerate = new Dictionary<string, TDUniversally.FieldData>();
      
        /// <summary>
        /// ID
        /// </summary>
        public  int  id {get { return m_Id; } }
       
        /// <summary>
        /// 名称
        /// </summary>
        public  string  name {get { return m_Name; } }
       
        /// <summary>
        /// 贴图
        /// </summary>
        public  string  spriteName {get { return m_SpriteName; } }
       
        /// <summary>
        /// 品质(白色-普通-Normal；绿色-进阶-Advanced；蓝色-稀有-Rare；紫色-史诗-Epic；红色-传说-Legendary；金色-不朽-Immortal)
        /// </summary>
        public  string  quality {get { return m_Quality; } }
       
        /// <summary>
        /// 说明
        /// </summary>
        public  string  desc {get { return m_Desc; } }
       
        /// <summary>
        /// 制作材料（id|数量）
        /// </summary>
        public  string  makeRes {get { return m_MakeRes; } }
       
        /// <summary>
        /// 增益效果
        /// </summary>
        public  string  buffType {get { return m_BuffType; } }
       
        /// <summary>
        /// 增益效率（%增加岛资源产出效率）
        /// </summary>
        public  int  buffRate {get { return m_BuffRate; } }
       
        /// <summary>
        /// 维持时长（s）
        /// </summary>
        public  int  buffTime {get { return m_BuffTime; } }
       
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
                    m_Name = dataR.ReadString();
                    break;
                case 2:
                    m_SpriteName = dataR.ReadString();
                    break;
                case 3:
                    m_Quality = dataR.ReadString();
                    break;
                case 4:
                    m_Desc = dataR.ReadString();
                    break;
                case 5:
                    m_MakeRes = dataR.ReadString();
                    break;
                case 6:
                    m_BuffType = dataR.ReadString();
                    break;
                case 7:
                    m_BuffRate = dataR.ReadInt();
                    break;
                case 8:
                    m_BuffTime = dataR.ReadInt();
                    break;
                case 9:
                    m_MakeTime = dataR.ReadInt();
                    break;
                case 10:
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
          Dictionary<string, int> ret = new Dictionary<string, int>(11);
          
          ret.Add("Id", 0);
          ret.Add("Name", 1);
          ret.Add("SpriteName", 2);
          ret.Add("Quality", 3);
          ret.Add("Desc", 4);
          ret.Add("MakeRes", 5);
          ret.Add("BuffType", 6);
          ret.Add("BuffRate", 7);
          ret.Add("BuffTime", 8);
          ret.Add("MakeTime", 9);
          ret.Add("MakeCost", 10);
          return ret;
        }
    } 
}//namespace LR