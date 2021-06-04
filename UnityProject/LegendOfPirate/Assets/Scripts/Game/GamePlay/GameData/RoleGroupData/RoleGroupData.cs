using System;
using Qarth;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GameWish.Game
{   
    public class RoleGroupData : IDataClass
    {
        public List<RoleData> roleList = new List<RoleData>();

        public void SetDefaultValue()
        {
            SetDataDirty();
        }

        public override void InitWithEmptyData()
        {

        }

        public override void OnDataLoadFinish()
        {

        }

        public void AddRole(int id, string name)
        {
            if (roleList.Any(i => i.id == id))
            {
                Log.e("Role already exists, id: " + id);
            }
            else
            {
                roleList.Add(new RoleData(id, 1, name,0,1));

                SetDataDirty();
            }
        }

        public void AddRoleLevel(int id, int deltaLevel)
        {
            RoleData? item = GetRoleItem(id);

            if (item == null)
            {
                Log.e("Role not found: " + id);
            }
            else
            {
                item.Value.AddLevel(deltaLevel);

                SetDataDirty();
            }
        }

        public void AddRoleSkill(int id)
        { }

        public void UpgradeRollSkill(int id, int deltaLevel)
        {
        }

        private RoleData? GetRoleItem(int id)
        {
            return roleList.FirstOrDefault(i => i.id == id);
        }

    
    }

}