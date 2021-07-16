using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    [Serializable]
    public class ForgeDBData
    {
        public int equipmentId;
        public DateTime forgeEndTime;
        public ForgeStage forgeState;

        public ForgeDBData()
        {
            equipmentId = 0;
            forgeEndTime = default(DateTime);
            forgeState = ForgeStage.Free;
        }
    }
}