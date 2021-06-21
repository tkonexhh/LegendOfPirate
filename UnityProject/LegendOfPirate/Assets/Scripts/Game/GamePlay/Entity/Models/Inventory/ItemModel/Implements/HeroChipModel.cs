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
            return "英雄碎片";
        }

        public override string GetName()
        {
            return "英雄碎片" + m_Id;
        }
    }

}