using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class SkillAction_RangeHeal : SkillAction
    {
        private int m_HealAmount;
        private RangeDamage m_RangeDamage;

        public SkillAction_RangeHeal(RangeDamage rangeDamage, int healAmount) //: base(owner)
        {
            this.m_RangeDamage = rangeDamage;
            this.m_HealAmount = healAmount;
        }

        public override void ExcuteAction(Skill skill)
        {
            var oppoCamp = BattleHelper.GetOppositeCamp(skill.Owner.camp);
            var roles = BattleMgr.S.Role.GetControllersByCamp(oppoCamp);
            Transform transform = skill.TargetInfo.Target.transform;

            m_RangeDamage.DealWithRange(roles, transform, (r) => { r.Data.Heal(m_HealAmount); });

            skill.SkillActionStepEnd();
        }
    }

}