using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class SkillAction_RangeHeal : SkillAction
    {
        private int m_HealPercent;
        private int m_HealDelta;
        private Picker m_RangeDamage;

        public SkillAction_RangeHeal(Picker rangeDamage, int healPercent, int healDelta) //: base(owner)
        {
            this.m_RangeDamage = rangeDamage;
            this.m_HealPercent = healPercent;
            this.m_HealDelta = healDelta;
        }

        public override void ExcuteAction(Skill skill)
        {
            var oppoCamp = BattleHelper.GetOppositeCamp(skill.Owner.camp);
            var roles = BattleMgr.S.Role.GetControllersByCamp(oppoCamp);
            Transform transform = skill.TargetInfo.Target.transform;

            int healPercent = m_HealPercent + skill.level * m_HealDelta;
            m_RangeDamage.DealWithRange(roles, transform, (r) =>
            {
                int healAmount = (int)(r.Data.buffedData.BasicMaxHp * healPercent * 0.01f);
                r.Data.Heal(healAmount);
            });

            skill.SkillActionStepEnd();
        }
    }

}