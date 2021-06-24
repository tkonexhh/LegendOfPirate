using System;
using Qarth;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GameWish.Game
{
	public class LaboratoryData : IDataClass
	{
        public List<LaboratorySlotDataItem> laboratorySlotItemLst = new List<LaboratorySlotDataItem>();
        public override void InitWithEmptyData()
        {
            for (int i = 0; i < Define.LABORATORY_MAX_SLOT; i++) 
            {
                laboratorySlotItemLst.Add(new LaboratorySlotDataItem(i));
            }
        }

        public override void OnDataLoadFinish()
        {
            base.OnDataLoadFinish();
        }
    }

    [Serializable]
    public struct LaboratorySlotDataItem 
    {
        public int slotId;
        public int potionId;
        public DateTime makingStartTime;
        public LaboratorySlotState laboratorySlotState;

        private LaboratoryData m_LaboratoryData;

        public LaboratorySlotDataItem(int slotId) 
        {
            m_LaboratoryData = null;
            this.slotId = slotId;
            potionId = TDPotionSynthesisConfigTable.dataList[0].id;
            makingStartTime = default(DateTime);
            laboratorySlotState = LaboratorySlotState.Locked;
        }

        public void OnStartProcessing(int potionId, DateTime time)
        {
            this.potionId = potionId;
            this.makingStartTime = time;
            laboratorySlotState = LaboratorySlotState.Making;

            SetDataDirty();
        }

        public void OnPartSelected(int potionId)
        {
            this.potionId = potionId;
            laboratorySlotState = LaboratorySlotState.Selected;

            SetDataDirty();
        }

        public void OnPartUnSelected()
        {
            laboratorySlotState = LaboratorySlotState.Free;

            SetDataDirty();
        }

        public void OnEndProcessing()
        {
            potionId = TDPartSynthesisConfigTable.dataList[0].id;
            this.makingStartTime = default(DateTime);
            laboratorySlotState = LaboratorySlotState.Free;

            SetDataDirty();
        }

        public void OnUnlocked()
        {
            laboratorySlotState = LaboratorySlotState.Free;

            SetDataDirty();
        }

        private void SetDataDirty()
        {
            if (m_LaboratoryData == null)
            {
                m_LaboratoryData = GameDataMgr.S.GetData<LaboratoryData>();
            }
            m_LaboratoryData.SetDataDirty();
        }
    }
	
}