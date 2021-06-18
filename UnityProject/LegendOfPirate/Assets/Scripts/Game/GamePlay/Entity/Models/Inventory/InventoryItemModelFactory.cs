using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public class InventoryItemModelFactory
	{
        public static IInventoryItemModel CreateItemModel(InventoryItemData dbItem, InventoryItemType itemType, int id, int count)
        {
            IInventoryItemModel itemModel = null;

            switch (itemType)
            {
                case InventoryItemType.HeroChip:
                    itemModel = new HeroChipModel(dbItem, itemType, id, count);
                    break;
                case InventoryItemType.Material:
                    break;
                case InventoryItemType.Equip:
                    break;
                case InventoryItemType.Food:
                    break;
            }

            return itemModel;
        }
	}
	
}