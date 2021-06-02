using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace GameWish.Game
{
    [ModelAutoRegister]
	public class RoleGroupModel : DbModel
	{
        public ReactiveCollection<RoleItemModel> roleItemList = new ReactiveCollection<RoleItemModel>();

        protected override void LoadDataFromDb()
        {
            RoleGroupData roleGroupData = GameDataMgr.S.GetRoleGroupData();
            for (int i = 0; i < roleGroupData.roleList.Count; i++)
            {
                RoleData roleData = roleGroupData.roleList[i];
                RoleItemModel itemModel = new RoleItemModel(roleData);
                roleItemList.Add(itemModel);
            }
        }
    }

    public class RoleItemModel : Model
    {
        public int id;
        public IntReactiveProperty level;
        public string name;
        public string resName;
        public ReactiveCollection<int> equipList;
        public ReactiveCollection<int> skillList;

        public RoleItemModel(RoleData roleData)
        {
            id = roleData.id;
            level = new IntReactiveProperty(roleData.level);
            name = roleData.name;
            //TODO: equp and skill
        }
    }
}