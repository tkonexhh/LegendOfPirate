using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDRoleConfigTable
    {
        public static Dictionary<int, int> SpiritIdToRoleIdDic;
        public static Dictionary<int, List<int>> SkillIdDic;
        static void CompleteRowAdd(TDRoleConfig tdData, int rowCount)
        {
            if (SpiritIdToRoleIdDic == null)
            {
                SpiritIdToRoleIdDic = new Dictionary<int, int>();
            }

            if (!SpiritIdToRoleIdDic.ContainsKey(tdData.spiritId))
            {
                SpiritIdToRoleIdDic.Add(tdData.spiritId, tdData.roleId);
            }

            if (SkillIdDic == null)
            {
                SkillIdDic = new Dictionary<int, List<int>>();
            }
            if (!SkillIdDic.ContainsKey(tdData.spiritId))
            {
                List<int> skilllist = new List<int>();
                skilllist = Helper.String2ListInt(tdData.skillId, "|");
                SkillIdDic.Add(tdData.spiritId, skilllist);
            }
        }

        public static int GetSpiritIdToRoleId(int spiritId)
        {
            int roleId;
            SpiritIdToRoleIdDic.TryGetValue(spiritId, out roleId);
            return roleId;
        }


    }
}
