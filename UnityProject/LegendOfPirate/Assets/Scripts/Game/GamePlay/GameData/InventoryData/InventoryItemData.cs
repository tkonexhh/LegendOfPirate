using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    [System.Serializable]
	public class InventoryItemData
	{
        public InventoryItemType itemType;
        public int id;
        public int count;

        private InventoryData m_InventoryData;

        public InventoryItemData() { }

        public InventoryItemData(InventoryItemType itemType, int id, int count)
        {
            m_InventoryData = null;

            this.itemType = itemType;
            this.id = id;
            this.count = count;
        }

        public void OnValueChanged(int newValue)
        {
            count = newValue;

            SetDateDirty();
        }

        private void SetDateDirty()
        {
            if (m_InventoryData == null)
            {
                m_InventoryData = GameDataMgr.S.GetData<InventoryData>();
            }

            m_InventoryData.SetDataDirty();
        }
    }
}