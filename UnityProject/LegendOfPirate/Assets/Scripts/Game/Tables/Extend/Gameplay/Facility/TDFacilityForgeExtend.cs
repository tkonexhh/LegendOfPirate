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
    }
}