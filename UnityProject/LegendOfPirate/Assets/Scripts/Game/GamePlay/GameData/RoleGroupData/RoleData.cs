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
        public Dictionary<EquipmentType, RoleEquipData> equipDic;
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
            equipDic = new Dictionary<EquipmentType, RoleEquipData>();
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

        public RoleSkillData AddRoleSkill(int id)
        {
            RoleSkillData skill = GetRoleSkillData(id);

            if (skill == null)
            {
                skillList.Add(new RoleSkillData(id));
                SetDataDirty();
                return GetRoleSkillData(id);
            }
            return null;
        }

        public bool UpgradeRoleSkill(int id, int deltaLevel)
        {
            RoleSkillData skill = GetRoleSkillData(id);

            if (skill != null)
            {
                skill.Upgrade(deltaLevel);
                SetDataDirty();
                return true;
            }

            return false;
        }

        public bool UpgradeRoleEquip(int id,EquipmentType equipType)
        {
            RoleEquipData equip = GetRoleEquipData(id);

            if (equip == null)
            {
                //equipDic.Add(new RoleEquipData());
                SetDataDirty();
                return true;
            }

            return false;
        }
        /// <summary>
        /// 添加新装备
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool AddRoleEquip(EquipmentType type, int id)
        {
            var equipConfig = TDEquipmentConfigTable.GetEquipmentConfigByID(id);

            if (!equipDic.ContainsKey(type)) 
            {
                equipDic.Add(type, new RoleEquipData(
                        equipConfig.equipmentID,
                        equipConfig.startLevel,
                        equipConfig.equipmentType,
                        1,
                        equipConfig.equipQualityType
                        ));
                return true;
            }
            return false;
        }

        public RoleEquipData GetRoleEquipData(int id)
        {
            RoleEquipData equip = equipDic.FirstOrDefault(i => i.Value.id == id).Value;

            if (equip == null)
            {
                Log.e("Equip not found: " + id);
            }

            return equip;
        }
        #endregion

        #region Private

        private RoleSkillData GetRoleSkillData(int id)
        {
            RoleSkillData skill = skillList.FirstOrDefault(i => i.id == id);

            return skill;
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