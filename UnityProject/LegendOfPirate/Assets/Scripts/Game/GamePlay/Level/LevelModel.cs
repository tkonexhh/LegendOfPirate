using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace GameWish.Game
{
    [ModelAutoRegister]
	public class LevelModel : DbModel
	{
        public IntReactiveProperty curLevel;

        public ReactiveCollection<RoleItemModel> roleItemList = new ReactiveCollection<RoleItemModel>();

        public override void OnInit()
        {
            base.OnInit();

            curLevel.Subscribe((value) => 
            {
                RefreshEnemyRoleList();
            });
        }

        protected override void LoadDataFromDb()
        {
            base.LoadDataFromDb();
        }

        private void RefreshEnemyRoleList()
        {

        }
    }
}