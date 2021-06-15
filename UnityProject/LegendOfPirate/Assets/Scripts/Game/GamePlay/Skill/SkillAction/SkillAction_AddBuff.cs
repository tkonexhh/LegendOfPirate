using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class SkillAction_AddBuff : SkillAction
    {
        private BuffConfigSO m_BuffConfig;
        private SkillTargetType m_Target;

        public SkillAction_AddBuff(BuffConfigSO buffConfig, SkillTargetType target) //: base(owner)
        {
            m_BuffConfig = buffConfig;
            m_Target = target;
        }

        public override void ExcuteAction(Skill skill)
        {
            var buff = BuffFactory.CreateBuff(m_BuffConfig);
            switch (m_Target)
            {
                case SkillTargetType.Caster:
                    skill.TargetInfo.Caster.Buff.AddBuff(buff);
                    break;
                case SkillTargetType.Target:
                    skill.TargetInfo.Target.Buff.AddBuff(buff);
                    break;
            }
        }
    }

}