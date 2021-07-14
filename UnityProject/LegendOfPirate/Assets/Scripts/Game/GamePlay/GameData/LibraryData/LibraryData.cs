using System;
using Qarth;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GameWish.Game
{
    public class LibraryData : IDataClass
    {
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
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="slot"></param>
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
                    libraryDBData.readingStartTime = DateTime.Now;
                else if (librarySlotState == LibrarySlotState.Free)
                    libraryDBData.readingStartTime = default(DateTime);
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
        /// <param name="roleID"></param>
        public void SetReadRoleID(int slotID, int roleID)
        {
            LibraryDBData libraryDBData = libraryItemList.FirstOrDefault(i => i.slotId == slotID);
            if (libraryDBData != null)
            {
                libraryDBData.heroId = roleID;
            }
            else
                Log.e("SortID is exit , SortID = " + slotID);

            SaveManually();
        }
        #endregion
    }
}