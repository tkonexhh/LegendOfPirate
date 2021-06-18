using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public interface IInventoryItemModel
	{
        string GetName();
        string GetDesc();
        int GetCount();
        InventoryItemType GetItemType();
        int GetId();
        void AddCount(int deltaCount);
	}
	
}