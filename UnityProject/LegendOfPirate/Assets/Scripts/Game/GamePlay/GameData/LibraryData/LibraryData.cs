using System;
using Qarth;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GameWish.Game
{
    public class LibraryData : IDataClass
    {
        public List<LibraryDataItem> libraryItemList = new List<LibraryDataItem>();

        public override void InitWithEmptyData()
        {
            for (int i = 1; i <= Define.TRAINING_ROOM_MAX_SLOT; i++)
            {
                LibraryDataItem item = new LibraryDataItem(i);
                libraryItemList.Add(item);
            }
        }

        public override void OnDataLoadFinish()
        {

        }

        private LibraryDataItem? GetLibraryDataItem(int slotId)
        {
            LibraryDataItem? item = libraryItemList.FirstOrDefault(i => i.slotId == slotId);
            if (item == null)
            {
                Log.e("LibraryDataItem Not Found: " + slotId);
            }

            return item;
        }

        [Serializable]
        public struct LibraryDataItem
        {
            public int slotId;
            public int heroId;
            public DateTime ReadingStartTime;
            public LibrarySlotState libraryState;

            public LibraryDataItem(int slot)
            {
                slotId = slot;
                heroId = -1;
                ReadingStartTime = default(DateTime);
                libraryState = LibrarySlotState.Locked;
            }


            public void OnStartReading(int heroId, DateTime time)
            {
                this.heroId = heroId;
                this.ReadingStartTime = time;
                libraryState = LibrarySlotState.Reading;

                GameDataMgr.S.GetData<LibraryData>().SetDataDirty();
            }

            public void OnHeroSelected(int heroId)
            {
                this.heroId = heroId;
                libraryState = LibrarySlotState.HeroSelected;

                GameDataMgr.S.GetData<LibraryData>().SetDataDirty();
            }

            public void OnHeroUnselected()
            {
                libraryState = LibrarySlotState.Free;

                GameDataMgr.S.GetData<LibraryData>().SetDataDirty();
            }

            public void OnEndTraining()
            {
                this.heroId = -1;
                this.ReadingStartTime = default(DateTime);
                libraryState = LibrarySlotState.Free;

                GameDataMgr.S.GetData<LibraryData>().SetDataDirty();
            }

            public void OnUnlocked()
            {
                libraryState = LibrarySlotState.Free;

                //m_TrainingData.SetDataDirty();
                GameDataMgr.S.GetData<LibraryData>().SetDataDirty();
            }
        }
    }


}