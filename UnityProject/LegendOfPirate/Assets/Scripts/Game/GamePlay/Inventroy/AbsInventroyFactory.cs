using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public abstract class AbsInventroyFactory
	{ 
		public abstract ItemBase CreateItem(ItemType itemType, ItemID id,long capacity);
		
	}
	
}