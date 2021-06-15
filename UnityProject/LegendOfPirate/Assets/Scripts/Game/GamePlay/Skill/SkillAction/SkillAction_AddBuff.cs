using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class SkillAction_AddBuff : SkillAction
    {
        private BuffConfigSO m_BuffConfig;
        private SkillTarget m_Target;

        public SkillAction_AddBuff(BuffConfigSO buffConfig, SkillTarget target) //: base(owner)
        {
            m_BuffConfig = buffConfig;
            m_Target = target;
        }

        public override void ExcuteAction(Skill skill)
        {
            var buff = BuffFactory.CreateBuff(m_BuffConfig);
            m_Target.PicketTarget().Buff.AddBuff(buff);
        }
    }

}