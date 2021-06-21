using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;
using System;

namespace GameWish.Game
{
	public class WareHouseItemModel
	{
		public IInventoryItemModel inventoryItemModel;
		public WareHouseItem wareHouseItem ;

		public WareHouseItemModel(IInventoryItemModel inventoryItemModel)
		{
			this.inventoryItemModel = inventoryItemModel;
		}
		public void SetWareHouseItemData(WareHouseItem wareHouseItem)
        {
			this.wareHouseItem = wareHouseItem;
		}
    }
}