using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
using Qarth;

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
            foreach (var item in roleGroupData.roleList)
            {
                RoleModel itemModel = new RoleModel(item.id);
                roleItemList.Add(itemModel);

                if (itemModel.isLocked.Value)
                {
                    if (roleUnlockedItemList.Any(i => i.id == itemModel.id))
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

        /// <summary>
        /// 获取碎片，添加到对应的role
        /// </summary>
        /// <param name="spiritId"></param>
        /// <param name="count"></param>
        public void AddSpiritRoleModel(int spiritId,int count)
        {
            int roleId = TDRoleConfigTable.GetSpiritIdToRoleId(spiritId);
            if (roleItemList.Any(item => item.id == roleId))
            {
                roleItemList.FirstOrDefault(i => i.id == roleId).spiritCount.Value += count;
            }
            else
            {
                roleGroupData.OnAddRoleItem(roleId);
                RoleModel itemModel = new RoleModel(roleId);
                roleItemList.Add(itemModel);
                itemModel.AddSpiritCount(count);
            }
        }

        public void SetRoleUnlockedModel(int id)
        {
            RoleModel role = roleItemList.FirstOrDefault(item => item.id == id);
            role.isLocked.Value = true;
            roleUnlockedItemList.Add(role);
        }


        List<RoleModel> roleModelList = new List<RoleModel>();
        List<RoleModel> roleUnlockedList = new List<RoleModel>();
        /// <summary>
        /// role根据星级从高到低、等级从高到低、已拥有高于已有碎片，三级顺序进行排序
        /// </summary>
        /// <returns></returns>
        public List<RoleModel> GetSortRoleItemList()
        {
            roleUnlockedList = roleUnlockedItemList.ToList();
            if (roleUnlockedItemList.Count > 1)
            {
                roleUnlockedList.OrderBy(item => item.GetStarLevel())
                   .ThenBy(item => item.level);
            }
           
            roleModelList = roleItemList.Where(i => i.isLocked.Value == false && i.spiritCount.Value != 0).ToList();
            foreach (var item in roleItemList)
            {
                Log.e(string.Format("id,{0}  islocked:{1}    spiritcout{2}",item.id,item.isLocked.Value,item.spiritCount.Value));
            }
            Log.e(roleModelList.Count);
            if (roleModelList.Count > 1)
            {
                roleModelList.OrderBy(i => i.spiritCount);
                Log.e(roleUnlockedList.Concat(roleModelList).ToList().Count);
            }
           
            return (roleUnlockedList.Concat(roleModelList).ToList());
        }
    }    
}