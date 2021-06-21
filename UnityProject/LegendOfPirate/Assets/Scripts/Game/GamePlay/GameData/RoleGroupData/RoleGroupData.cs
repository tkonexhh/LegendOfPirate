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
            //For Test
            OnAddRoleItem(1001);

            SetDataDirty();
        }

        public override void OnDataLoadFinish()
        {

        }

        #endregion

        #region Public Set

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

        public void OnRoleSkillUnlocked(int id)
        { }

        public void OnRollSkillUpgraded(int id, int deltaLevel)
        {
        }

        public RoleData GetRoleItem(int id)
        {
            RoleData? roleData = roleList.FirstOrDefault(i => i.id == id);
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