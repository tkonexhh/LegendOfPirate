using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    public class SkillAction_Damage : SkillAction
    {
        private int m_Damage;
        private BattleDamageType m_DamageType;
        private SkillTargetType m_Target;

        public SkillAction_Damage(int damage, BattleDamageType damageType, SkillTargetType target) //: base(owner)
        {
            this.m_Damage = damage;
            this.m_DamageType = damageType;
            this.m_Target = target;
        }

        public override void ExcuteAction(Skill skill)
        {
            skill.SkillActionStepEnd();
            RoleDamagePackage package = new RoleDamagePackage();
            package.damage = (int)(m_Damage * 0.01) * skill.Owner.Data.buffedData.ATK;
            package.damageType = m_DamageType;
            switch (m_Target)
            {
                case SkillTargetType.Caster:
                    BattleMgr.S.SendDamage(skill.TargetInfo.Caster, package);
                    break;
                case SkillTargetType.Target:
                    BattleMgr.S.SendDamage(skill.TargetInfo.Target, package);
                    break;
            }

        }
    }


}