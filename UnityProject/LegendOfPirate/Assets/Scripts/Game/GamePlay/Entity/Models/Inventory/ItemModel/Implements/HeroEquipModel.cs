using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;


namespace GameWish.Game
{
    public class HeroEquipModel : InventoryItemModel
    {
        public HeroEquipModel(InventoryItemData dbItem, InventoryItemType itemType, int id, int count) : base(dbItem, itemType, id, count)
        {
        }

        public override string GetDesc()
        {
            return "装备";
        }

        public override string GetName()
        {
            return "装备" + m_Id;
        }
    }

}