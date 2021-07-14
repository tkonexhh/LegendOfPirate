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
        public DateTime readingStartTime;
        public LibrarySlotState libraryState;

        public LibraryDBData() { }

        public LibraryDBData(int slot)
        {
            slotId = slot;
            heroId = -1;
            readingStartTime = default(DateTime);
            libraryState = LibrarySlotState.Locked;
        }
    }
}