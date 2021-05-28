using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class InventroyFactory : AbsInventroyFactory
    {
        public override ItemBase CreateItem(ItemType itemType, ItemID id,long capacity)
        {
            switch (itemType)
            {
                case ItemType.Consumable:
                    return new Consumable(itemType, id, capacity);
                case ItemType.Equipment:
                    return new Equipment(itemType, id, capacity);
                case ItemType.Weapon:
                    return new Weapon(itemType, id, capacity);
                case ItemType.Material:
                    return new ItemMaterial(itemType, id, capacity);
            }
            Debug.LogError("ItemType is not find");
            return null;
        }
    }

}