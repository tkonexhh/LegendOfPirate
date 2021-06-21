using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    [Serializable]
    public class RoleSkillData
    {
        public int id;
        public int level;

        private RoleGroupData m_RoleGroupData;

        public RoleSkillData(int id)
        {
            this.id = id;
            this.level = 1;

            m_RoleGroupData = null;
        }

        public void Upgrade(int deltaLevel)
        {
            level += deltaLevel;
            SetDataDirty();
        }

        private void SetDataDirty()
        {
            if (m_RoleGroupData == null)
            {
                m_RoleGroupData = GameDataMgr.S.GetData<RoleGroupData>();
            }

            m_RoleGroupData.SetDataDirty();
        }


    }

}