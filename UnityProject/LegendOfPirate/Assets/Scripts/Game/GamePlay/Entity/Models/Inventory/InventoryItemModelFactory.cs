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
                    itemModel = new RawMatModel(dbItem, itemType, id, count);
                    break;
                case InventoryItemType.Equip:
                    itemModel = new HeroEquipModel(dbItem, itemType, id, count);
                    break;
                case InventoryItemType.Food:
                    itemModel = new FoodModel(dbItem, itemType, id, count);
                    break;
            }
            return itemModel;
        }
	}
	
}