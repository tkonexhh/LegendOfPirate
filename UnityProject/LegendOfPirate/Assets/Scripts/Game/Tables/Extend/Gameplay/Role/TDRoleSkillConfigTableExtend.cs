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

        static void CompleteRowAdd(TDRoleSkillConfig tdData, int rowCount)
        {

        }

        #region Struct

        public struct SkillUpCost
        {
            public int level;
            public int upCost;
            public SkillUpCost(int level,string str) 
            {
                this.level = level;
                this.upCost = int.Parse(str);
            }
        }

        public struct RoleSkillUnitConfig
        {
            public int skillID;
            public string skillName;
            public string skillParams;
            public string skillDesc;
            public SkillUpCost[] skillUpCost;

            public RoleSkillUnitConfig(TDRoleSkillConfig tdData)
            {
                this.skillID = tdData.skillId;
                this.skillName = tdData.skillName;
                this.skillParams = tdData.skillParams;
                this.skillDesc = tdData.skillDsc;
                if (!string.IsNullOrEmpty(tdData.skillUpCost))
                {
                    string[] strs = tdData.skillUpCost.Split(';');
                    skillUpCost = new SkillUpCost[strs.Length];
                    for (int i = 0; i < strs.Length; i++)
                    {
                        SkillUpCost cost = new SkillUpCost(i,strs[i]);
                        skillUpCost[i] = cost;
                    }
                }
                else
                    skillUpCost = new SkillUpCost[0];
            }
        }
        #endregion
    }
}