using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace GameWish.Game
{
    [ModelAutoRegister]
	public class RoleGroupModel : DbModel
	{
        public ReactiveCollection<RoleModel> roleItemList = new ReactiveCollection<RoleModel>();

        protected override void LoadDataFromDb()
        {
            RoleGroupData roleGroupData = GameDataMgr.S.GetRoleGroupData();
            for (int i = 0; i < roleGroupData.roleList.Count; i++)
            {
                RoleData roleData = roleGroupData.roleList[i];
                RoleModel itemModel = new RoleModel(roleData);
                roleItemList.Add(itemModel);
            }
        }
    }    
}