using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;

namespace GameWish.Game
{
    [ModelAutoRegister]
	public class RoleGroupModel : DbModel
	{
        public ReactiveCollection<RoleModel> roleItemList = new ReactiveCollection<RoleModel>();
        public ReactiveCollection<RoleModel> roleUnlockedItemList = new ReactiveCollection<RoleModel>();
        RoleGroupData roleGroupData;

        protected override void LoadDataFromDb()
        {
            roleGroupData = GameDataMgr.S.GetData<RoleGroupData>();
            for (int i = 0; i < roleGroupData.roleList.Count; i++)
            {
                RoleData roleData = roleGroupData.roleList[i];
                RoleModel itemModel = new RoleModel(roleData);
                roleItemList.Add(itemModel);

                if (itemModel.isUnlcok.Value)
                {
                    if (roleUnlockedItemList.Any(item => item.id == itemModel.id))
                        return;
                    roleUnlockedItemList.Add(itemModel);
                }
            }
        }

        public RoleModel GetRoleModel(int id)
        {
            RoleModel role = roleItemList.FirstOrDefault(i => i.id == id);
            return role;
        }

        public void AddRoleModel(int id,bool isUnlock)
        {
            if (!roleItemList.Any(item => item.id == id))
                return;
           
            roleGroupData.OnAddRoleItem(id,false);
            RoleData roleData = (RoleData)roleGroupData.GetRoleItem(id);
            RoleModel itemModel = new RoleModel(roleData);
            roleItemList.Add(itemModel);

            if (isUnlock)
            {
                roleUnlockedItemList.Add(itemModel);
            }
        }

        public void AddRoleUnlockedModel(int id)
        {
            RoleModel role = roleItemList.FirstOrDefault(item => item.id == id);
            role.isUnlcok.Value = true;
            roleUnlockedItemList.Add(role);
        }
    }    
}