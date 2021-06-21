using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    [Serializable]
    public struct RoleSkillData
    {
        public int id;
        public int level;
        public bool isLocked;

        private RoleGroupData m_RoleGroupData;

        public RoleSkillData(int id, int level)
        {
            this.id = id;
            this.level = level;
            isLocked = false;

            m_RoleGroupData = null;
        }

        public void Upgrade(int deltaLevel)
        {
            level += deltaLevel;
            SetDataDirty();
        }

        public void SetSkillUnlocked()
        {
            isLocked = true;
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