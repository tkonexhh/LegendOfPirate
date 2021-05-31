using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public interface IInventoryItem
	{
        string GetName();
        string GetDesc();
        int GetCount();
        int GetId();
        void AddCount(int deltaCount);
        void SubCount(int deltaCount);
	}
	
}