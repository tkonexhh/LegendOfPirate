using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    [Serializable]
    public class LibraryDBData
    {
        public int slotId;
        public int heroId;
        public DateTime ReadingStartTime;
        public LibrarySlotState libraryState;

        public LibraryDBData() { }

        public LibraryDBData(int slot)
        {
            slotId = slot;
            heroId = -1;
            ReadingStartTime = default(DateTime);
            libraryState = LibrarySlotState.Locked;
        }
    }
}