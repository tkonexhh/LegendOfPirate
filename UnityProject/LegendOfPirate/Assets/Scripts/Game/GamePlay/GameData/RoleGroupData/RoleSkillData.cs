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
        public int lastLevel;

        private RoleGroupData m_RoleGroupData;

        #region Public
        public RoleSkillData() { }
        public RoleSkillData(int id)
        {
            this.id = id;
            this.level = 0;
            this.lastLevel = 0;

            m_RoleGroupData = null;
        }

        public void Upgrade(int deltaLevel)
        {
            level += deltaLevel;
            SetDataDirty();
        }

        public void SetLastLevel(int lastLevel)
        {
            this.lastLevel = lastLevel;
            SetDataDirty();
        }
        #endregion
        #region Private
        private void SetDataDirty()
        {
            if (m_RoleGroupData == null)
            {
                m_RoleGroupData = GameDataMgr.S.GetData<RoleGroupData>();
            }

            m_RoleGroupData.SetDataDirty();
        }
        #endregion
    }
}