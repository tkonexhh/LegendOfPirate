using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class SkillAction_RangeDamage : SkillAction
    {
        private Picker m_RangeDamage;
        private SkillTargetType m_TargetType;
        private int m_Damage;

        public SkillAction_RangeDamage(Picker rangeDamage, SkillTargetType targetType, int damage) //: base(owner)
        {
            this.m_RangeDamage = rangeDamage;
            this.m_TargetType = targetType;
        }

        public override void ExcuteAction(Skill skill)
        {
            var oppoCamp = BattleHelper.GetOppositeCamp(skill.Owner.camp);
            var roles = BattleMgr.S.Role.GetControllersByCamp(oppoCamp);
            Transform transform = skill.TargetInfo.Target.transform;
            switch (m_TargetType)
            {
                case SkillTargetType.Caster: transform = skill.TargetInfo.Caster.transform; break;
                case SkillTargetType.Target: transform = skill.TargetInfo.Target.transform; break;

            }

            RoleDamagePackage damagePackage = new RoleDamagePackage(skill.Owner);
            damagePackage.damageType = BattleDamageType.Skill;
            damagePackage.damage = m_Damage;

            m_RangeDamage.DealWithRange(roles, transform, (r) => { BattleMgr.S.SendDamage(r, damagePackage); });

            skill.SkillActionStepEnd();
        }
    }

}