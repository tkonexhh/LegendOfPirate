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

        public List<IInventoryItemModel> itemModelList = new List<IInventoryItemModel>();

        protected override void LoadDataFromDb()
        {
        }
    }
	
}