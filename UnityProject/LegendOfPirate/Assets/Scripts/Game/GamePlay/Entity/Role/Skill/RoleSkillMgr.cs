using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public enum RoleSkillType
    {
        None,
    }

	public class RoleSkillMgr : TSingleton<RoleSkillMgr>
	{
        private Dictionary<RoleSkillType, RoleSkill> m_RoleSkillDic = new Dictionary<RoleSkillType, RoleSkill>();

        public void AddRoleSkill(RoleSkillType roleSkillType, RoleSkill roleSkill)
        {
            if (!m_RoleSkillDic.ContainsKey(roleSkillType))
            {
                m_RoleSkillDic.Add(roleSkillType, roleSkill);
            }
        }

        public RoleSkill GetSkill(RoleSkillType roleSkill)
        {
            if (m_RoleSkillDic.ContainsKey(roleSkill))
            {
                return m_RoleSkillDic[roleSkill];
            }

            return null;
        }
    }
	
}