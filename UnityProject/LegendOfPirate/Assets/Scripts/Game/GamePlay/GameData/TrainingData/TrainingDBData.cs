using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    [Serializable]
    public class TrainingDBData
	{
        public int slotId;
        public int heroId;
        public DateTime trainingStartTime;
        public TrainingSlotState trainingState;

        public TrainingDBData() { }

        public TrainingDBData(int slot)
        {
            slotId = slot;
            heroId = -1;
            trainingStartTime = default(DateTime);
            trainingState = TrainingSlotState.Locked;
        }
    }
}