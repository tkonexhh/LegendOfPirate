using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public partial class TDFacilityProcessingRoom
    {
        public void Reset()
        {

        }
        public  int[] GetUnlockPartIDBySlot()
        {
            return Helper.String2IntArray(unlockPartID, "|");
        }
    }
}