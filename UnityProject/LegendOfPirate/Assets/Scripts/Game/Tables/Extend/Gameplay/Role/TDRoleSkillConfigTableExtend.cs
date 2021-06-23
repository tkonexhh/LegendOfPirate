using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDRoleSkillConfigTable
    {
        public static RoleSkillUnitConfig[] roleSkillUnitConfigs = null;
        private static int m_RoleSkillUnitIndex = 0;
        static void CompleteRowAdd(TDRoleSkillConfig tdData, int rowCount)
        {
            try
            {
                if (roleSkillUnitConfigs == null)
                    roleSkillUnitConfigs = new RoleSkillUnitConfig[rowCount];

                if (m_RoleSkillUnitIndex > roleSkillUnitConfigs.Length)
                    throw new ArgumentOutOfRangeException("RoleSkill Data Out Of Range");

                roleSkillUnitConfigs[m_RoleSkillUnitIndex] = new RoleSkillUnitConfig(tdData);
                m_RoleSkillUnitIndex++;
            }
            catch (Exception e)
            {
                Log.e("e =" + e);
            }
        }

        #region Public
        public static RoleSkillUnitConfig GetRoleSkillUnitConfigByID(int skillID)
        {
            RoleSkillUnitConfig roleSkillConfig;
            foreach (var item in roleSkillUnitConfigs)
            {
                if (item.skillID == skillID)
                {
                    roleSkillConfig = item;
                    return roleSkillConfig;
                }
            }
            return default(RoleSkillUnitConfig);
        }
        #endregion
    }
    #region Struct
    public struct SkillUpCost
    {
        public int skillLevel;
        public int upCost;
        public SkillUpCost(int level, string str)
        {
            this.skillLevel = level;
            this.upCost = int.Parse(str);
        }
    }

    public struct SkillUpRoleLevel
    {
        //到某一级所需要的角色等级
        public int skillLevel;
        public int requireRoleLevel;
        public SkillUpRoleLevel(int level, string str)
        {
            this.skillLevel = level;
            this.requireRoleLevel = int.Parse(str);
        }
    }

    public struct RoleSkillUnitConfig
    {
        public int skillID;
        public string skillName;
        public string skillParams;
        public string skillDesc;
        public SkillUpCost[] skillUpCost;
        public SkillUpRoleLevel[] skillUpRoleLevel;

        public RoleSkillUnitConfig(TDRoleSkillConfig tdData)
        {
            this.skillID = tdData.skillId;
            this.skillName = tdData.skillName;
            this.skillParams = tdData.skillParams;
            this.skillDesc = tdData.skillDsc;

            #region Analysis SkillUpCost
            if (!string.IsNullOrEmpty(tdData.skillUpCost))
            {
                string[] strs = tdData.skillUpCost.Split(';');
                skillUpCost = new SkillUpCost[strs.Length];
                for (int i = 0; i < strs.Length; i++)
                {
                    SkillUpCost cost = new SkillUpCost(i+1, strs[i]);
                    skillUpCost[i] = cost;
                }
            }
            else
                skillUpCost = new SkillUpCost[0];
            #endregion
            #region Analysis SkillUpCost
            if (!string.IsNullOrEmpty(tdData.skillUpCost))
            {
                string[] strs = tdData.skillUpCost.Split(';');
                skillUpRoleLevel = new SkillUpRoleLevel[strs.Length];
                for (int i = 0; i < strs.Length; i++)
                {
                    SkillUpRoleLevel roleLevel = new SkillUpRoleLevel(i+1, strs[i]);
                    skillUpRoleLevel[i] = roleLevel;
                }
            }
            else
                skillUpRoleLevel = new SkillUpRoleLevel[0];
            #endregion
        }
    }
    #endregion
}