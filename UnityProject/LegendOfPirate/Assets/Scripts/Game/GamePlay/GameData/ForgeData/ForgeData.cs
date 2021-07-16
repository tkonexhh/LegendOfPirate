using System;
using Qarth;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    public class ForgeData : IDataClass
    {
        public ForgeDBData forgeDataItem = new ForgeDBData();

        public ForgeDBData ForgeDataItem { get { return forgeDataItem; } }

        #region IDataClass
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

        #region public
        /// <summary>
        /// 设置锻造数据
        /// </summary>
        /// <param name="forgeEquipModel"></param>
        public void SetForgeEquip(ForgeEquipModel forgeEquipModel = null)
        {
            if (forgeEquipModel != null)
            {
                forgeDataItem.equipmentId = forgeEquipModel.ID;
                forgeDataItem.forgeState = ForgeStage.Forging;
                forgeDataItem.forgeEndTime = DateTime.Now + TimeSpan.FromSeconds(forgeEquipModel.EquipmentSynthesisConfig.makeTime);
            }
            else
                ClearForgeDBData();
        }

        #endregion

        #region Private
        private void ClearForgeDBData()
        {
            forgeDataItem.equipmentId = 0;
            forgeDataItem.forgeState = ForgeStage.Free;
            forgeDataItem.forgeEndTime = default(DateTime);
        }

        #endregion

    }
}