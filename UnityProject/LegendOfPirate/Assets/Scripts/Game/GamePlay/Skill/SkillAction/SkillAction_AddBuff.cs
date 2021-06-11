using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class SkillAction_AddBuff : SkillAction
    {
        private Buff m_Buff;
        private SkillTarget m_Target;

        public SkillAction_AddBuff(Buff buff, SkillTarget target) //: base(owner)
        {
            m_Buff = buff;
            m_Target = target;
        }

        public override void ExcuteAction(Skill skill)
        {
            m_Target.PicketTarget().Buff.AddBuff(m_Buff);
        }
    }

}