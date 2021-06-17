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

        #region Public Set

        public void OnAddRoleItem(int id,bool isUnlock)
        {
            if (roleList.Any(i => i.id == id))
            {
                Log.e("Role already exists, id: " + id);
            }
            else
            {
                roleList.Add(new RoleData(id, isUnlock));

                SetDataDirty();
            }
        }

        public void OnRoleSkillUnlocked(int id)
        { }

        public void OnRollSkillUpgraded(int id, int deltaLevel)
        {
        }

        public RoleData GetRoleItem(int id)
        {
            return roleList.FirstOrDefault(i => i.id == id);
        }

        #endregion

        #region Private



        #endregion

    }

}