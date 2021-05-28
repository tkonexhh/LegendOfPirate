using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class ItemMaterial : ItemBase
    {
        public ItemMaterial(ItemType itemType, ItemID id, long capacity) : base(itemType, id, capacity)
        { }
        public override void OnInitOtherInfo()
        {
        }
    }

}