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

            private LibraryData m_LibraryData;

            public LibraryDataItem(int slot)
            {
                m_LibraryData = null;
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

                SetDataDirty();
            }

            public void OnHeroSelected(int heroId)
            {
                this.heroId = heroId;
                libraryState = LibrarySlotState.HeroSelected;

                SetDataDirty();
            }

            public void OnHeroUnselected()
            {
                libraryState = LibrarySlotState.Free;

                SetDataDirty();
            }

            public void OnEndTraining()
            {
                this.heroId = -1;
                this.ReadingStartTime = default(DateTime);
                libraryState = LibrarySlotState.Free;

                SetDataDirty();
            }

            public void OnUnlocked()
            {
                libraryState = LibrarySlotState.Free;

                //m_TrainingData.SetDataDirty();
                SetDataDirty();
            }

            private void SetDataDirty()
            {
                if (m_LibraryData == null)
                {
                    m_LibraryData = GameDataMgr.S.GetData<LibraryData>();
                }
                m_LibraryData.SetDataDirty();
            }
        }
    }


}