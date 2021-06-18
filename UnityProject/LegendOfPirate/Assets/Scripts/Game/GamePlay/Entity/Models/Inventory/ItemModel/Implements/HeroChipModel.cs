using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;


namespace GameWish.Game
{
    public class HeroChipModel : InventoryItemModel
    {
        public HeroChipModel(InventoryItemData dbItem, InventoryItemType itemType, int id, int count) : base(dbItem, itemType, id, count)
        {
        }

        public override string GetDesc()
        {
            return string.Empty;
        }

        public override string GetName()
        {
            return string.Empty;
        }
    }

}