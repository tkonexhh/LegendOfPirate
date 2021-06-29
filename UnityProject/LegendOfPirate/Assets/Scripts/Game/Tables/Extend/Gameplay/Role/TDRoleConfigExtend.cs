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
        public void Reset()
        {

        }
        public RoleType GetRoleType() 
        {
            if (m_Type == "Front")
            {
                return RoleType.Front;
            }
            else if (m_Type == "Mid")
            {
                return RoleType.Mid;
            }
            else 
            {
                return RoleType.Back;
            }
        }
        public Dictionary<EquipmentType, int> GetEquipLimit() 
        {
            var limitequip = Helper.String2IntArray(m_EquipId, ";");
            Dictionary<EquipmentType, int> ret = new Dictionary<EquipmentType, int>();
            for (int i = 0; i < limitequip.Length; i++) 
            {
                ret.Add((EquipmentType)i, limitequip[i]);
            }
            return ret;
        }
    }
}