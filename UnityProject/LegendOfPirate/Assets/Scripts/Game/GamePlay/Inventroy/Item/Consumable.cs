using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class Consumable : ItemBase
    {
        public int HP { get; set; }
        public int MP { get; set; }

        public Consumable(ItemType itemType, ItemID id,long number) : base(itemType, id, number)
        { }

        public override void OnInitOtherInfo()
        {
     
        }
    }
}