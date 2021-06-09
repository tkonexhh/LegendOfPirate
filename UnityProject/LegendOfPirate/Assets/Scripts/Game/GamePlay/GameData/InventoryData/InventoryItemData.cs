using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    [System.Serializable]
	public struct InventoryItemData
	{
        public int id;
        public int count;

        public InventoryItemData(int id, int count)
        {
            this.id = id;
            this.count = count;
        }

        public void AddCount(int deltaCount)
        {
            count += deltaCount;
            count = Mathf.Max(0, count);
        }
    }
	
}