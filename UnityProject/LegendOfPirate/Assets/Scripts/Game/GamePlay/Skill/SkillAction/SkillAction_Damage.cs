using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    public class SkillAction_Damage : SkillAction
    {
        private int m_Damage;
        private int m_DamageDelta;
        private SkillTargetType m_Target;

        public SkillAction_Damage(int damage, int damageDelta, SkillTargetType target) //: base(owner)
        {
            this.m_Damage = damage;
            this.m_DamageDelta = damageDelta;
            this.m_Target = target;
        }

        public override void ExcuteAction(Skill skill)
        {
            skill.SkillActionStepEnd();

            if (skill.TargetInfo == null) return;

            RoleDamagePackage package = new RoleDamagePackage(skill.Owner);
            int damagePercent = m_Damage + skill.level * m_DamageDelta;
            package.damage = (int)((damagePercent * 0.01f) * skill.Owner.Data.buffedData.ATK * skill.Owner.Data.buffedData.SkillATKRate);
            package.damageType = BattleDamageType.Skill;
            switch (m_Target)
            {
                case SkillTargetType.Caster:
                    BattleMgr.S.SendDamage(skill.TargetInfo.Caster, package);
                    break;
                case SkillTargetType.Target:
                    if (skill.TargetInfo.Target != null)
                    {
                        BattleMgr.S.SendDamage(skill.TargetInfo.Target, package);
                    }
                    break;
            }

        }
    }


}