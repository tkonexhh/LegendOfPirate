using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Qarth;

namespace GameWish.Game
{
    [Serializable]
    public struct RoleData
    {
        public int id;
        public int level;
        public string name;
        public List<RoleEquipData> equipList;
        public List<RoleSkillData> skillList;

        public RoleData(int id, int level, string name)
        {
            this.id = id;
            this.level = level;
            this.name = name;

            equipList = new List<RoleEquipData>();
            skillList = new List<RoleSkillData>();
        }

        #region Public

        public void AddLevel(int deltaLevel)
        {
            level += deltaLevel;
        }

        public bool AddSkill(int id)
        {
            RoleSkillData? skill = GetRoleSkillData(id);

            if (skill == null)
            {
                skillList.Add(new RoleSkillData());
                return true;
            }

            return false;
        }

        public bool UpgradeSkill(int id, int deltaLevel)
        {
            RoleSkillData? skill = GetRoleSkillData(id);

            if (skill != null)
            {
                skill.Value.Upgrade(deltaLevel);
                return true;
            }

            return false;
        }

        public bool AddEquip(int id)
        {
            RoleEquipData? equip = GetRoleEquipData(id);

            if (equip == null)
            {
                equipList.Add(new RoleEquipData());
                return true;
            }

            return false;
        }

        public bool UpgradeEquip(int id, int deltaLevel)
        {
            RoleEquipData? equip = GetRoleEquipData(id);

            if (equip != null)
            {
                equip.Value.Upgrade(deltaLevel);
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

        #endregion
    }

}