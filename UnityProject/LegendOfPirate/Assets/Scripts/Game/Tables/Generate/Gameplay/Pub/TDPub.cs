//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDPub
    {
        
       
        private EInt m_ID = 0;   
        private string m_UpgradeRes;   
        private string m_ItemDetails;   
        private string m_IconResources;   
        private string m_ModelResources;   
        private string m_AnimationResources;  
        
        //private Dictionary<string, TDUniversally.FieldData> m_DataCacheNoGenerate = new Dictionary<string, TDUniversally.FieldData>();
      
        /// <summary>
        /// ID
        /// </summary>
        public  int  iD {get { return m_ID; } }
       
        /// <summary>
        /// 物品名称
        /// </summary>
        public  string  upgradeRes {get { return m_UpgradeRes; } }
       
        /// <summary>
        /// 物品详情：编号/类型/数量/概率
        /// </summary>
        public  string  itemDetails {get { return m_ItemDetails; } }
       
        /// <summary>
        /// icon资源
        /// </summary>
        public  string  iconResources {get { return m_IconResources; } }
       
        /// <summary>
        /// 模型资源
        /// </summary>
        public  string  modelResources {get { return m_ModelResources; } }
       
        /// <summary>
        /// 动画资源
        /// </summary>
        public  string  animationResources {get { return m_AnimationResources; } }
       

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
                    m_ID = dataR.ReadInt();
                    break;
                case 1:
                    m_UpgradeRes = dataR.ReadString();
                    break;
                case 2:
                    m_ItemDetails = dataR.ReadString();
                    break;
                case 3:
                    m_IconResources = dataR.ReadString();
                    break;
                case 4:
                    m_ModelResources = dataR.ReadString();
                    break;
                case 5:
                    m_AnimationResources = dataR.ReadString();
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
          
          ret.Add("ID", 0);
          ret.Add("UpgradeRes", 1);
          ret.Add("ItemDetails", 2);
          ret.Add("IconResources", 3);
          ret.Add("ModelResources", 4);
          ret.Add("AnimationResources", 5);
          return ret;
        }
    } 
}//namespace LR