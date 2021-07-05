using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class SkillAction_Heal : SkillAction
    {
        private int m_HealPercent;
        private int m_HeadDelta;
        private SkillTargetType m_Target;

        public SkillAction_Heal(int healPercent, int healDelta, SkillTargetType target) //: base(owner)
        {
            this.m_HealPercent = healPercent;
            this.m_HeadDelta = healDelta;
            this.m_Target = target;
        }

        public override void ExcuteAction(Skill skill)
        {
            Debug.LogError("加血啦!");
            skill.SkillActionStepEnd();
            int healAmount = 0;
            int healPercent = (m_HealPercent + skill.level * m_HeadDelta);
            switch (m_Target)
            {
                case SkillTargetType.Caster:
                    healAmount = (int)(skill.TargetInfo.Caster.Data.buffedData.BasicMaxHp * healPercent * 0.01f);
                    skill.TargetInfo.Caster.Data.Heal(healAmount);
                    break;
                case SkillTargetType.Target:
                    healAmount = (int)(skill.TargetInfo.Target.Data.buffedData.BasicMaxHp * healPercent * 0.01f);
                    skill.TargetInfo.Target.Data.Heal(healAmount);
                    break;
            }
        }
    }

}