using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Qarth;

namespace GameWish.Game
{
    [Serializable]
    public class RoleData 
    {
        public int id;
        public bool isLocked;
        public int spiritCount;
        public int level;

        public int curExp;
        public int starLevel;
        public List<RoleEquipData> equipList;
        public List<RoleSkillData> skillList;

        private RoleGroupData m_RoleGroupData;

        public RoleData() { }

        public RoleData(int id)
        {
            this.id = id;
            this.isLocked = false;
            this.level = 1;
            this.spiritCount = 0;

            this.curExp = 1;
            this.starLevel = 1;
            equipList = new List<RoleEquipData>();
            skillList = new List<RoleSkillData>();

            m_RoleGroupData = null;
        }

        #region Public

        public void SetRoleUnlocked()
        {
            isLocked = true;
            SetDataDirty();
        }

        public void SetRoleSpiritCount(int count)
        {
            spiritCount = count;
            SetDataDirty();
        }

        public void SetRoleLevel(int deltaLevel)
        {
            level = deltaLevel;
            SetDataDirty();
        }

        public bool AddRoleSkill(int id)
        {
            RoleSkillData? skill = GetRoleSkillData(id);

            if (skill == null)
            {
                skillList.Add(new RoleSkillData());
                SetDataDirty();
                return true;
            }

            return false;
        }

        public bool UpgradeRoleSkill(int id, int deltaLevel)
        {
            RoleSkillData? skill = GetRoleSkillData(id);

            if (skill != null)
            {
                skill.Value.Upgrade(deltaLevel);
                SetDataDirty();
                return true;
            }

            return false;
        }

        public bool AddRoleEquip(int id)
        {
            RoleEquipData? equip = GetRoleEquipData(id);

            if (equip == null)
            {
                equipList.Add(new RoleEquipData());
                SetDataDirty();
                return true;
            }

            return false;
        }

        public bool UpgradeRoleEquip(int id, int deltaLevel)
        {
            RoleEquipData? equip = GetRoleEquipData(id);

            if (equip != null)
            {
                equip.Value.Upgrade(deltaLevel);
                SetDataDirty();
                return true;
            }

            return false;
        }

        #endregion

        #region Private

        private RoleSkillData? GetRoleSkillData(int id)
        {
            RoleSkillData? skill = skillList.FirstOrDefault(i => i.id == id);

            if (skill == null)
            {
                Log.e("Skill not found: " + id);
            }

            return skill;
        }

        private RoleEquipData? GetRoleEquipData(int id)
        {
            RoleEquipData? equip = equipList.FirstOrDefault(i => i.id == id);

            if (equip == null)
            {
                Log.e("Equip not found: " + id);
            }

            return equip;
        }

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