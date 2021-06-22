//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDRoleConfig
    {
        
       
        private EInt m_RoleId = 0;   
        private string m_RoleName;   
        private string m_RoleDsc;   
        private EInt m_InitHp = 0;   
        private EInt m_InitAtk = 0;   
        private EInt m_Equip1Id = 0;   
        private EInt m_Equip2Id = 0;   
        private EInt m_Equip3Id = 0;   
        private EInt m_Equip4Id = 0;   
        private EInt m_SpiritId = 0;   
        private string m_SkillId;  
        
        //private Dictionary<string, TDUniversally.FieldData> m_DataCacheNoGenerate = new Dictionary<string, TDUniversally.FieldData>();
      
        /// <summary>
        /// 角色ID
        /// </summary>
        public  int  roleId {get { return m_RoleId; } }
       
        /// <summary>
        /// 角色名字
        /// </summary>
        public  string  roleName {get { return m_RoleName; } }
       
        /// <summary>
        /// 角色描述
        /// </summary>
        public  string  roleDsc {get { return m_RoleDsc; } }
       
        /// <summary>
        /// 初始血量
        /// </summary>
        public  int  initHp {get { return m_InitHp; } }
       
        /// <summary>
        /// 初始攻击
        /// </summary>
        public  int  initAtk {get { return m_InitAtk; } }
       
        /// <summary>
        /// 装备1ID
        /// </summary>
        public  int  equip1Id {get { return m_Equip1Id; } }
       
        /// <summary>
        /// 装备2ID
        /// </summary>
        public  int  equip2Id {get { return m_Equip2Id; } }
       
        /// <summary>
        /// 装备3ID
        /// </summary>
        public  int  equip3Id {get { return m_Equip3Id; } }
       
        /// <summary>
        /// 装备4ID
        /// </summary>
        public  int  equip4Id {get { return m_Equip4Id; } }
       
        /// <summary>
        /// 升星材料
        /// </summary>
        public  int  spiritId {get { return m_SpiritId; } }
       
        /// <summary>
        /// 技能
        /// </summary>
        public  string  skillId {get { return m_SkillId; } }
       

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
                    m_RoleName = dataR.ReadString();
                    break;
                case 2:
                    m_RoleDsc = dataR.ReadString();
                    break;
                case 3:
                    m_InitHp = dataR.ReadInt();
                    break;
                case 4:
                    m_InitAtk = dataR.ReadInt();
                    break;
                case 5:
                    m_Equip1Id = dataR.ReadInt();
                    break;
                case 6:
                    m_Equip2Id = dataR.ReadInt();
                    break;
                case 7:
                    m_Equip3Id = dataR.ReadInt();
                    break;
                case 8:
                    m_Equip4Id = dataR.ReadInt();
                    break;
                case 9:
                    m_SpiritId = dataR.ReadInt();
                    break;
                case 10:
                    m_SkillId = dataR.ReadString();
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
          
          ret.Add("RoleId", 0);
          ret.Add("RoleName", 1);
          ret.Add("RoleDsc", 2);
          ret.Add("InitHp", 3);
          ret.Add("InitAtk", 4);
          ret.Add("Equip1Id", 5);
          ret.Add("Equip2Id", 6);
          ret.Add("Equip3Id", 7);
          ret.Add("Equip4Id", 8);
          ret.Add("SpiritId", 9);
          ret.Add("SkillId", 10);
          return ret;
        }
    } 
}//namespace LR