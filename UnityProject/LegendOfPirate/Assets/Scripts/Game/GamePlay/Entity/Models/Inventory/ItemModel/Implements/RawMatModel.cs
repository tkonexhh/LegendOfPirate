using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;


namespace GameWish.Game
{
    public class RawMatModel : InventoryItemModel
    {
        public RawMatModel(InventoryItemData dbItem, InventoryItemType itemType, int id, int count) : base(dbItem, itemType, id, count)
        {
        }

        public override string GetDesc()
        {
            return "原材料";
        }

        public override string GetName()
        {
            return "原材料" + m_Id;
        }
    }

}