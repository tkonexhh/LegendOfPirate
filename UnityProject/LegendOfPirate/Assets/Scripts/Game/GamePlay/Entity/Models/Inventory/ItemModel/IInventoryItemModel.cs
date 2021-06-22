using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;


namespace GameWish.Game
{
	public interface IInventoryItemModel
	{
        string GetName();
        string GetDesc();
        int GetCount();
        IntReactiveProperty GetReactiveCount();
        InventoryItemType GetItemType();
        int GetId();
        void AddCount(int deltaCount);
	}
	
}