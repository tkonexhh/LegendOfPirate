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
        
       
        private EInt m_RoleId = 0;   
        private string m_MaterialName;   
        private string m_MaterialDsc;   
        private string m_MaterialType;   
        private EInt m_MaterialPrice = 0;  
        
        //private Dictionary<string, TDUniversally.FieldData> m_DataCacheNoGenerate = new Dictionary<string, TDUniversally.FieldData>();
      
        /// <summary>
        /// 材料ID
        /// </summary>
        public  int  roleId {get { return m_RoleId; } }
       
        /// <summary>
        /// 材料名字
        /// </summary>
        public  string  materialName {get { return m_MaterialName; } }
       
        /// <summary>
        /// 角色描述
        /// </summary>
        public  string  materialDsc {get { return m_MaterialDsc; } }
       
        /// <summary>
        /// 材料类型
        /// </summary>
        public  string  materialType {get { return m_MaterialType; } }
       
        /// <summary>
        /// 出售价格
        /// </summary>
        public  int  materialPrice {get { return m_MaterialPrice; } }
       

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
                    m_RoleId = dataR.ReadInt();
                    break;
                case 1:
                    m_MaterialName = dataR.ReadString();
                    break;
                case 2:
                    m_MaterialDsc = dataR.ReadString();
                    break;
                case 3:
                    m_MaterialType = dataR.ReadString();
                    break;
                case 4:
                    m_MaterialPrice = dataR.ReadInt();
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
          
          ret.Add("RoleId", 0);
          ret.Add("MaterialName", 1);
          ret.Add("MaterialDsc", 2);
          ret.Add("MaterialType", 3);
          ret.Add("MaterialPrice", 4);
          return ret;
        }
    } 
}//namespace LR