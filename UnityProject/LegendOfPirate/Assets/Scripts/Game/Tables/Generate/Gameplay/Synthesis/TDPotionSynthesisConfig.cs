//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDPotionSynthesisConfig
    {
        
       
        private EInt m_Id = 0;   
        private EInt m_Type = 0;   
        private string m_Name;   
        private string m_SpriteName;   
        private EInt m_Superscript = 0;   
        private string m_Desc;   
        private string m_MakeRes;   
        private string m_BuffType;   
        private EInt m_EffectObject = 0;   
        private EInt m_BuffRate = 0;   
        private EInt m_BuffTime = 0;   
        private EInt m_MakeTime = 0;  
        
        //private Dictionary<string, TDUniversally.FieldData> m_DataCacheNoGenerate = new Dictionary<string, TDUniversally.FieldData>();
      
        /// <summary>
        /// ID
        /// </summary>
        public  int  id {get { return m_Id; } }
       
        /// <summary>
        /// 类型（1-用于boss战，2-用于人战）
        /// </summary>
        public  int  type {get { return m_Type; } }
       
        /// <summary>
        /// 名称
        /// </summary>
        public  string  name {get { return m_Name; } }
       
        /// <summary>
        /// 贴图
        /// </summary>
        public  string  spriteName {get { return m_SpriteName; } }
       
        /// <summary>
        /// 角标
        /// </summary>
        public  int  superscript {get { return m_Superscript; } }
       
        /// <summary>
        /// 说明
        /// </summary>
        public  string  desc {get { return m_Desc; } }
       
        /// <summary>
        /// 制作材料
        /// </summary>
        public  string  makeRes {get { return m_MakeRes; } }
       
        /// <summary>
        /// 增益效果(Potion_AddDamage增伤，Potion_AddAttackSpeed攻速提升，Potion_AddCommand控制，Potion_AddBlood血量恢复，Potion_AddArmorBonus增加护甲值，Potion_MonomerDamage单体伤害，Potion_AOE群体伤害)
        /// </summary>
        public  string  buffType {get { return m_BuffType; } }
       
        /// <summary>
        /// 目标对象1-己方单位，2-敌方单位
        /// </summary>
        public  int  effectObject {get { return m_EffectObject; } }
       
        /// <summary>
        /// 增益值（增伤，攻速，恢复血量为百分比提升；控制效果为秒；单（群）体伤害为固定值）
        /// </summary>
        public  int  buffRate {get { return m_BuffRate; } }
       
        /// <summary>
        /// 维持时长分钟
        /// </summary>
        public  int  buffTime {get { return m_BuffTime; } }
       
        /// <summary>
        /// 制作时间（分钟）
        /// </summary>
        public  int  makeTime {get { return m_MakeTime; } }
       

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
                    m_Type = dataR.ReadInt();
                    break;
                case 2:
                    m_Name = dataR.ReadString();
                    break;
                case 3:
                    m_SpriteName = dataR.ReadString();
                    break;
                case 4:
                    m_Superscript = dataR.ReadInt();
                    break;
                case 5:
                    m_Desc = dataR.ReadString();
                    break;
                case 6:
                    m_MakeRes = dataR.ReadString();
                    break;
                case 7:
                    m_BuffType = dataR.ReadString();
                    break;
                case 8:
                    m_EffectObject = dataR.ReadInt();
                    break;
                case 9:
                    m_BuffRate = dataR.ReadInt();
                    break;
                case 10:
                    m_BuffTime = dataR.ReadInt();
                    break;
                case 11:
                    m_MakeTime = dataR.ReadInt();
                    break;
                default:
                    //TableHelper.CacheNewField(dataR, schemeNames[col], m_DataCacheNoGenerate);
                    break;
            }
          }

        }
        
        public static Dictionary<string, int> GetFieldHeadIndex()
        {
          Dictionary<string, int> ret = new Dictionary<string, int>(12);
          
          ret.Add("Id", 0);
          ret.Add("Type", 1);
          ret.Add("Name", 2);
          ret.Add("SpriteName", 3);
          ret.Add("Superscript", 4);
          ret.Add("Desc", 5);
          ret.Add("MakeRes", 6);
          ret.Add("BuffType", 7);
          ret.Add("EffectObject", 8);
          ret.Add("BuffRate", 9);
          ret.Add("BuffTime", 10);
          ret.Add("MakeTime", 11);
          return ret;
        }
    } 
}//namespace LR