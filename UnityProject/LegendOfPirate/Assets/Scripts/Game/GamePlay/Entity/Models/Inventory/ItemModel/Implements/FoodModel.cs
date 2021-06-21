using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;


namespace GameWish.Game
{
    public class FoodModel : InventoryItemModel
    {
        public FoodModel(InventoryItemData dbItem, InventoryItemType itemType, int id, int count) : base(dbItem, itemType, id, count)
        {
        }

        public override string GetDesc()
        {
            return "食物";
        }

        public override string GetName()
        {
            return "食物" + m_Id;
        }
    }

}