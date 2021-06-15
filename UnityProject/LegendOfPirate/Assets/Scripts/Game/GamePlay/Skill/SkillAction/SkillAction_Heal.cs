using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class SkillAction_Heal : SkillAction
    {
        private int m_HealAmount;
        private SkillTargetType m_Target;

        public SkillAction_Heal(int healAmount, SkillTargetType target) //: base(owner)
        {
            this.m_HealAmount = healAmount;
            this.m_Target = target;
        }

        public override void ExcuteAction(Skill skill)
        {
            Debug.LogError("加血啦!");
            switch (m_Target)
            {
                case SkillTargetType.Caster:
                    skill.TargetInfo.Caster.Data.buffedData.Hp += m_HealAmount;
                    break;
                case SkillTargetType.Target:
                    skill.TargetInfo.Target.Data.buffedData.Hp += m_HealAmount;
                    break;
            }
        }
    }

}