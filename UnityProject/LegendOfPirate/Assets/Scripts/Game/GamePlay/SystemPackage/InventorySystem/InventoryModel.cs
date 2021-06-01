using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;


namespace GameWish.Game
{
    [ModelAutoRegister]
	public class InventoryModel : DbModel
	{
        public IntReactiveProperty level;

        protected override void LoadDataFromDb()
        {
            base.LoadDataFromDb();
        }
    }
	
}