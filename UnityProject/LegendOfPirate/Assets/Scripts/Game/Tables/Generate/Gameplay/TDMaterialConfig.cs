//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDMaterialConfig
    {
        
       
        private EInt m_MaterialId = 0;   
        private string m_MaterialName;   
        private string m_MaterialIcon;   
        private EInt m_Quality = 0;   
        private string m_MaterialDesc;   
        private string m_MaterialType;   
        private EInt m_MaterialPrice = 0;   
        private EInt m_OutputWay = 0;  
        
        //private Dictionary<string, TDUniversally.FieldData> m_DataCacheNoGenerate = new Dictionary<string, TDUniversally.FieldData>();
      
        /// <summary>
        /// 材料ID
        /// </summary>
        public  int  materialId {get { return m_MaterialId; } }
       
        /// <summary>
        /// 材料名字
        /// </summary>
        public  string  materialName {get { return m_MaterialName; } }
       
        /// <summary>
        /// 材料icon
        /// </summary>
        public  string  materialIcon {get { return m_MaterialIcon; } }
       
        /// <summary>
        /// 品质(白色-普通-Normal；绿色-进阶-Advanced；蓝色-稀有-Rare；紫色-史诗-Epic；红色-传说-Legendary；金色-不朽-Immortal)
        /// </summary>
        public  int  quality {get { return m_Quality; } }
       
        /// <summary>
        /// 材料描述
        /// </summary>
        public  string  materialDesc {get { return m_MaterialDesc; } }
       
        /// <summary>
        /// 材料类型（HeroesPiece-英雄碎片，Material-材料）
        /// </summary>
        public  string  materialType {get { return m_MaterialType; } }
       
        /// <summary>
        /// 出售价格
        /// </summary>
        public  int  materialPrice {get { return m_MaterialPrice; } }
       
        /// <summary>
        /// 产出途径(1-资源岛，2-战斗奖励，3-菜园，4-钓鱼台，5-加工室,6-商城，7-活动)
        /// </summary>
        public  int  outputWay {get { return m_OutputWay; } }
       

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
                    m_MaterialId = dataR.ReadInt();
                    break;
                case 1:
                    m_MaterialName = dataR.ReadString();
                    break;
                case 2:
                    m_MaterialIcon = dataR.ReadString();
                    break;
                case 3:
                    m_Quality = dataR.ReadInt();
                    break;
                case 4:
                    m_MaterialDesc = dataR.ReadString();
                    break;
                case 5:
                    m_MaterialType = dataR.ReadString();
                    break;
                case 6:
                    m_MaterialPrice = dataR.ReadInt();
                    break;
                case 7:
                    m_OutputWay = dataR.ReadInt();
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
          
          ret.Add("MaterialId", 0);
          ret.Add("MaterialName", 1);
          ret.Add("MaterialIcon", 2);
          ret.Add("Quality", 3);
          ret.Add("MaterialDesc", 4);
          ret.Add("MaterialType", 5);
          ret.Add("MaterialPrice", 6);
          ret.Add("OutputWay", 7);
          return ret;
        }
    } 
}//namespace LR