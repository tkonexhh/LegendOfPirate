using System;
using Qarth;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;

namespace GameWish.Game
{   
    public class RoleGroupData : IDataClass
    {
        public List<RoleData> roleList = new List<RoleData>();

        #region IDataClass

        public void SetDefaultValue()
        {
            SetDataDirty();
        }

        public override void InitWithEmptyData()
        {
            SetDataDirty();
        }

        public override void OnDataLoadFinish()
        {

        }

        #endregion

        #region Public

        public void OnAddRoleItem(int id)
        {
            if (roleList.Any(i => i.id == id))
            {
                Log.e("Role already exists, id: " + id);
            }
            else
            {
                roleList.Add(new RoleData(id));

                SetDataDirty();
            }
        }

        public RoleData GetUnlockedRoleModel(int id)
        {
            RoleData role = roleList.FirstOrDefault(i => i.id == id);
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

        public void SetRoleManagementState(int id, ManagementRoleState managementRoleState)
        {
            RoleData roleModel = GetUnlockedRoleModel(id);
            if (roleModel != null)
            {
                roleModel.managementState = managementRoleState;
            }
            else
                Log.e("Not find role , id = " + id);
        }

        public void OnRoleSkillUnlocked(int id)
        { }

        public void OnRollSkillUpgraded(int id, int deltaLevel)
        {
        }

        public RoleData GetRoleItem(int id)
        {
            RoleData roleData = roleList.FirstOrDefault(i => i.id == id);
            if (roleData == null)
            {
                Log.e("RoleData Not Found: " + id);
            }
            return (RoleData)roleData;
        }

        #endregion

        #region Private



        #endregion
    }
}