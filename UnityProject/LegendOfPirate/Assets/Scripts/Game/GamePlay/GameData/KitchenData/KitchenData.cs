using System;
using Qarth;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{   
    public class KitchenData : IDataClass
    {
        public List<KitchenSlotData> kitchenSlotDataLst = new List<KitchenSlotData>();

        public override void InitWithEmptyData()
        {
            for (int i = 0; i < Define.KITCHEN_MAX_SLOT; i++) 
            {
                kitchenSlotDataLst.Add(new KitchenSlotData(i));
            }
        }

        public override void OnDataLoadFinish()
        {

        }

    }
    [Serializable]
    public struct KitchenSlotData 
    {
        public int slotId;
        public int foodId;

        public DateTime processingStartTime;
        public KitchenSlotState  kitchenSlotState;

        private KitchenData m_KitchenData;

        public KitchenSlotData(int slot)
        {
            m_KitchenData = null;
            slotId = slot;
            foodId = TDFoodSynthesisConfigTable.dataList[0].id;
            processingStartTime = default(DateTime);
            kitchenSlotState = KitchenSlotState.Locked;
        }

        public void OnStartCooking(int partId, DateTime time)
        {
            this.foodId = partId;
            this.processingStartTime = time;
            kitchenSlotState = KitchenSlotState.Cooking;

            SetDataDirty();
        }

        public void OnPartSelected(int partId)
        {
            this.foodId = partId;
            kitchenSlotState = KitchenSlotState.Selected;

            SetDataDirty();
        }

        public void OnFoodUnSelected()
        {
            kitchenSlotState = KitchenSlotState.Free;

            SetDataDirty();
        }

        public void OnEndCooking()
        {
            foodId = TDPartSynthesisConfigTable.dataList[0].id;
            this.processingStartTime = default(DateTime);
            kitchenSlotState = KitchenSlotState.Free;

            SetDataDirty();
        }

        public void OnUnlocked()
        {
            kitchenSlotState = KitchenSlotState.Free;

            SetDataDirty();
        }

        private void SetDataDirty()
        {
            if (m_KitchenData == null)
            {
                m_KitchenData = GameDataMgr.S.GetData<KitchenData>();
            }
            m_KitchenData.SetDataDirty();
        }

    }
}