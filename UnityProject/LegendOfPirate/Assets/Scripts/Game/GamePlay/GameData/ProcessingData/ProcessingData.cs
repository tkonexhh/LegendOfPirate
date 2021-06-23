using System;
using Qarth;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GameWish.Game
{
    public class ProcessingData : IDataClass
    {
        public List<ProcessingSlotDataItem> processingItemList = new List<ProcessingSlotDataItem>();
        public override void InitWithEmptyData()
        {
            for (int i = 0; i < Define.PROCESSING_ROOM_MAX_SLOT; i++)
            {
                var item = new ProcessingSlotDataItem(i);
                processingItemList.Add(item);
            }
        }

        public override void OnDataLoadFinish()
        {

        }

        private ProcessingSlotDataItem? GetProcessingDataItem(int slotId)
        {
            ProcessingSlotDataItem? item = processingItemList.FirstOrDefault(i => i.slotId == slotId);
            if (item == null)
            {
                Log.e("ProcessingSlotDataItem Not Found: " + slotId);
            }
            return item;
        }
    }

    [Serializable]
    public struct ProcessingSlotDataItem
    {
        public int slotId;
        public int partId;
        public DateTime processingStartTime;
        public ProcessSlotState progressSlotState;

        private ProcessingData m_ProcessingData;

        public ProcessingSlotDataItem(int slot)
        {
            m_ProcessingData = null;
            slotId = slot;
            partId = TDPartSynthesisConfigTable.dataList[0].id;
            processingStartTime = default(DateTime);
            progressSlotState = ProcessSlotState.Locked;
        }

        public void OnStartProcessing(int partId, DateTime time)
        {
            this.partId = partId;
            this.processingStartTime = time;
            progressSlotState = ProcessSlotState.Processing;

            SetDataDirty();
        }

        public void OnPartSelected(int partId)
        {
            this.partId = partId;
            progressSlotState = ProcessSlotState.Selected;

            SetDataDirty();
        }

        public void OnPartUnSelected()
        {
            progressSlotState = ProcessSlotState.Free;

            SetDataDirty();
        }

        public void OnEndProcessing()
        {
            partId = TDPartSynthesisConfigTable.dataList[0].id;
            this.processingStartTime = default(DateTime);
            progressSlotState = ProcessSlotState.Free;

            SetDataDirty();
        }

        public void OnUnlocked()
        {
            progressSlotState = ProcessSlotState.Free;

            SetDataDirty();
        }

        private void SetDataDirty()
        {
            if (m_ProcessingData == null)
            {
                m_ProcessingData = GameDataMgr.S.GetData<ProcessingData>();
            }
            m_ProcessingData.SetDataDirty();
        }
    }

}