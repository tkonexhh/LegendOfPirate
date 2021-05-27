using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace GameWish.Game
{
    [ModelAutoRegister]
	public class LevelModel : Model
	{
        public IntReactiveProperty curLevel;

        public ReactiveCollection<RoleDataItem> roleItemList = new ReactiveCollection<RoleDataItem>();

        protected override void LoadDataFromDb()
        {
            base.LoadDataFromDb();
        }
    }
}