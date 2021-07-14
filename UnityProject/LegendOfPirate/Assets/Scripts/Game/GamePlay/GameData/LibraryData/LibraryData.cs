using System;
using Qarth;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GameWish.Game
{
    public class LibraryData : IDataClass
    {
        public int level;
        public List<LibraryDBData> libraryItemList = new List<LibraryDBData>();

        public List<LibraryDBData> LibraryItemList { get { return libraryItemList; } }

        #region IDataClass
        /// <summary>
        /// 手动保存
        /// </summary>
        public void SaveManually()
        {
            SetDataDirty();

            GameDataMgr.S.SaveDataToLocal();
        }

        public override void InitWithEmptyData()
        {
        }

        public override void OnDataLoadFinish()
        {

        }
        #endregion

        #region Public

        public void OnUpgradeUnit(int level)
        {
            this.level = level;

            SaveManually();
        }

        public void AddTrainingSlotData(LibraryDBData slot)
        {
            if (!libraryItemList.Any(i => i.slotId == slot.slotId))
                libraryItemList.Add(slot);
            else
                Log.e("SortId is exit , SortId = " + slot.slotId);

            SaveManually();
        }

        /// <summary>
        /// 设置坑位的状态
        /// </summary>
        /// <param name="slotID"></param>
        /// <param name="librarySlotState"></param>
        public void SetLibrarySlotState(int slotID, LibrarySlotState librarySlotState)
        {
            LibraryDBData libraryDBData = libraryItemList.FirstOrDefault(i => i.slotId == slotID);
            if (libraryDBData != null)
            {
                if (librarySlotState == LibrarySlotState.Reading)
                    libraryDBData.ReadingStartTime = DateTime.Now;
                else if (librarySlotState == LibrarySlotState.Free)
                    libraryDBData.ReadingStartTime = default(DateTime);
                libraryDBData.libraryState = librarySlotState;
            }
            else
                Log.e("SortID is exit , SortID = " + slotID);

            SaveManually();
        }

        /// <summary>
        /// 设置阅读角色的ID
        /// </summary>
        /// <param name="slotID"></param>
        /// <param name="heroID"></param>
        public void SetReadHeroID(int slotID, int heroID)
        {
            LibraryDBData libraryDBData = libraryItemList.FirstOrDefault(i => i.slotId == slotID);
            if (libraryDBData != null)
            {
                libraryDBData.heroId = heroID;
            }
            else
                Log.e("SortID is exit , SortID = " + slotID);

            SaveManually();
        }

        #endregion

        private LibraryDBData GetLibraryDataItem(int slotId)
        {
            LibraryDBData item = libraryItemList.FirstOrDefault(i => i.slotId == slotId);
            if (item == null)
            {
                Log.e("LibraryDataItem Not Found: " + slotId);
            }
            return item;
        }
    }
}