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
        }

        public static int GetSpiritIdToRoleId(int spiritId)
        {
            int roleId;
            SpiritIdToRoleIdDic.TryGetValue(spiritId, out roleId);
            return roleId;
        }


    }
}
