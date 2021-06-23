using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Qarth;

namespace GameWish.Game
{
	public class RoleSkillModel : Model
	{
        public int skillId;
        public IntReactiveProperty skillLevel;
        public IntReactiveProperty upgradeCost;
        public StringReactiveProperty skillParams;

        private RoleSkillData m_DbData;
        private RoleSkillUnitConfig m_RoleSkillUnitConfig;
        private int m_LastSkillLevel;

        public RoleSkillUnitConfig roleSkillUnitConfig { get { return m_RoleSkillUnitConfig; } }

        public RoleSkillModel(RoleSkillData roleSkillData)
        {
            m_DbData = roleSkillData;

            skillId = roleSkillData.id;
            skillLevel = new IntReactiveProperty(roleSkillData.level);
            m_RoleSkillUnitConfig = TDRoleSkillConfigTable.GetRoleSkillUnitConfigByID(skillId);

            skillParams = new StringReactiveProperty(m_RoleSkillUnitConfig.skillParams);

            ModelSubscribe();
        }
        #region Public
        /// <summary>
        /// 设置升级前的等级
        /// </summary>
        /// <param name="delta"></param>
        public void SetLastSkillLevel(int delta)
        {
            m_LastSkillLevel = Mathf.Max(skillLevel.Value - delta,0);
            m_DbData.SetLastLevel(m_LastSkillLevel);
        }

        public int GetLastSkillLevel()
        {
            return m_LastSkillLevel;
        }

        /// <summary>
        /// 获取升到某一级所需要的角色等级
        /// </summary>
        /// <returns></returns>
        public int GetSkillUnlockingRoleLevel()
        {
            foreach (var item in m_RoleSkillUnitConfig.skillUpRoleLevel)
            {
                //未解锁为0级，解析表索引等级是从1开始
                if (item.skillLevel == skillLevel.Value+1)
                {
                    return item.requireRoleLevel;
                }
            }
            Log.e("Not find skill level , skillLvel = " + skillLevel.Value);
            return 1;
        }
        
        /// <summary>
        /// 获取升级的技能碎片
        /// </summary>
        /// <returns></returns>
        public int GetSkillUpgradeClip()
        {
            foreach (var item in m_RoleSkillUnitConfig.skillUpCost)
            {
                if (item.skillLevel == skillLevel.Value+1)
                {
                    return item.upCost;
                }
            }
            Log.e("Not find skill level , skillLvel = " + skillLevel.Value);
            return 1;
        }

        /// <summary>
        /// 技能升级
        /// </summary>
        /// <param name="delta"></param>
        public void UpgradeSkill(int delta)
        {
            if (delta == 0)
                return;
            if (skillLevel.Value + delta <= Define.TRAINING_ROOM_MAX_LEVEL)
            {
                skillLevel.Value += delta;
            }
            else
            {
                delta = Define.TRAINING_ROOM_MAX_LEVEL - skillLevel.Value;
                UpgradeSkill(delta);
            }
        }
        #endregion

        #region Private
        private void ModelSubscribe()
        {
         
        }
        #endregion
    }
}