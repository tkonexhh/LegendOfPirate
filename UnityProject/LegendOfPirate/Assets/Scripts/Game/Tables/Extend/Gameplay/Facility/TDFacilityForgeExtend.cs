using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDFacilityForge
    {
        public void Reset()
        {

        }

        public int[] GetUnlockEquipment() 
        {
            return Helper.String2IntArray(m_UnlockEquipmentID, ";");
        }

        public List<ResPair> GetUpGradeCostRes()
        {
            List<ResPair> ret = new List<ResPair>();
            var resPairString = m_UpgradeRes.Split(';');
            foreach (var Str in resPairString)
            {
                var resMsg = Helper.String2IntArray(Str, "|");
                ret.Add(new ResPair(resMsg[0], resMsg[1]));
            }

            return ret;
        }
    }
}