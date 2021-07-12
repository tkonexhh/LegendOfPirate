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
        public ReactiveCollection<RoleModel> m_RoleItemList = new ReactiveCollection<RoleModel>();
        public ReactiveCollection<RoleModel> m_RoleUnlockedItemList = new ReactiveCollection<RoleModel>();
        RoleGroupData roleGroupData;

        public ReactiveCollection<RoleModel> RoleUnlockedItemList { get { return m_RoleUnlockedItemList; } }

        protected override void LoadDataFromDb()
        {
            roleGroupData = GameDataMgr.S.GetData<RoleGroupData>();
            foreach (var item in roleGroupData.roleList)
            {
                RoleModel itemModel = new RoleModel(item.id);
                m_RoleItemList.Add(itemModel);

                if (itemModel.isLocked.Value)
                {
                    if (m_RoleUnlockedItemList.Any(i => i.id == itemModel.id))
                        return;
                    m_RoleUnlockedItemList.Add(itemModel);
                }
            }
        }

        /// <summary>
        /// 根据经营状态获取角色
        /// </summary>
        /// <param name="managementRoleState"></param>
        /// <returns></returns>
        public List<RoleModel> GetRolesByManagementState(ManagementRoleState managementRoleState = ManagementRoleState.None)
        {
            List<RoleModel> roleModels = new List<RoleModel>();
            foreach (var item in m_RoleUnlockedItemList)
            {
                if (item.managementState == managementRoleState)
                {
                    roleModels.Add(item);
                }
            }
            return roleModels;
        }

        public List<RoleModel> GetRoleModelsByStarlevel(int startLevel)
        {
            var ret = m_RoleItemList.Where(role => role.starLevel.Value == startLevel).ToList();
            return ret;
        }

        public RoleModel GetRoleModel(int id)
        {
            RoleModel role = m_RoleItemList.FirstOrDefault(i => i.id == id);
            return role;
        }

        public void SetRoleManagementState(int id, ManagementRoleState managementRoleState)
        {
            RoleModel roleModel = GetUnlockedRoleModel(id);
            if (roleModel != null)
            {
                roleModel.managementState = managementRoleState;
                roleGroupData.SetRoleManagementState(id, managementRoleState);
            }
            else
                Log.e("Not find role , id = " + id);
        }

        public RoleModel GetUnlockedRoleModel(int id)
        {
            RoleModel role = m_RoleUnlockedItemList.FirstOrDefault(i => i.id == id);
            if (role != null)
            {
                return role;
            }
            else
            {
                Log.e("Not find role , id = " + id);
                return null;
            }
        }

        public int GetRoleIndexById(int id)
        {
            int ret = 0;
            var roleList = m_RoleItemList.Concat(roleUnlockedList);
            foreach (var item in roleList)
            {
                if (item.id == id)
                {
                    return ret;
                }
                ret++;
            }
            return -1;
        }

        public RoleModel GetRoleModelByIndex(int index)
        {
            return m_RoleItemList.Concat(roleUnlockedList).ToList()[index];
        }

        public List<RoleModel> GetRoleModelsByType(RoleType roleType)
        {
            var rolesList = m_RoleItemList.Concat(roleUnlockedList);
            return roleUnlockedList.Where(r => r.GetRoleType() == roleType).ToList();
        }

        /// <summary>
        /// 获取碎片，添加到对应的role
        /// </summary>
        /// <param name="spiritId"></param>
        /// <param name="count"></param>
        public void AddSpiritRoleModel(int spiritId, int count)
        {
            int roleId = TDRoleConfigTable.GetSpiritIdToRoleId(spiritId);
            if (m_RoleItemList.Any(item => item.id == roleId))
            {
                m_RoleItemList.FirstOrDefault(i => i.id == roleId).spiritCount.Value += count;
            }
            else
            {
                roleGroupData.OnAddRoleItem(roleId);
                RoleModel itemModel = new RoleModel(roleId);
                m_RoleItemList.Add(itemModel);
                itemModel.AddSpiritCount(count);
            }
        }

        public void SetRoleUnlockedModel(int id)
        {
            RoleModel role = m_RoleItemList.FirstOrDefault(item => item.id == id);
            if (role.isLocked.Value) return;
            role.isLocked.Value = false;
            m_RoleUnlockedItemList.Add(role);
        }


        List<RoleModel> roleModelList = new List<RoleModel>();
        List<RoleModel> roleUnlockedList = new List<RoleModel>();
        /// <summary>
        /// role根据星级从高到低、等级从高到低、已拥有高于已有碎片，三级顺序进行排序
        /// </summary>
        /// <returns></returns>
        public List<RoleModel> GetSortRoleItemList()
        {
            roleUnlockedList = m_RoleUnlockedItemList.ToList();
            if (m_RoleUnlockedItemList.Count > 1)
            {
                roleUnlockedList.OrderBy(item => item.GetStarLevel())
                   .ThenBy(item => item.level);
            }

            roleModelList = m_RoleItemList.Where(i => i.isLocked.Value == false && i.spiritCount.Value != 0).ToList();
            //foreach (var item in roleItemList)
            //{
            //    Log.e(string.Format("id,{0}  islocked:{1}    spiritcout{2}", item.id, item.isLocked.Value, item.spiritCount.Value));
            //}
            if (roleModelList.Count > 1)
            {
                roleModelList.OrderBy(i => i.spiritCount);
                Log.e(roleUnlockedList.Concat(roleModelList).ToList().Count);
            }
            return (roleUnlockedList.Concat(roleModelList).ToList());
        }

        public List<RoleModel> GetSortRoleItemList(int type)
        {

            roleUnlockedList = m_RoleUnlockedItemList.ToList();
            if (m_RoleUnlockedItemList.Count > 1)
            {
                roleUnlockedList.OrderBy(item => item.GetStarLevel())
                   .ThenBy(item => item.level);
            }
            if (type <= 0)
            {
                roleModelList = m_RoleItemList.Where(i => i.isLocked.Value = true && i.spiritCount.Value != 0).ToList();
            }
            else
            {
                roleModelList = m_RoleItemList.Where(i => i.isLocked.Value = true && i.spiritCount.Value != 0 && (int)i.GetRoleType() == type).ToList();
            }
            //foreach (var item in roleItemList)
            //{
            //    Log.e(string.Format("id,{0}  islocked:{1}    spiritcout{2}", item.id, item.isLocked.Value, item.spiritCount.Value));
            //}
            if (roleModelList.Count > 1)
            {
                roleModelList.OrderBy(i => i.spiritCount);
                Log.e(roleUnlockedList.Concat(roleModelList).ToList().Count);
            }
            return (roleUnlockedList.Concat(roleModelList).ToList());
        }
    }
}