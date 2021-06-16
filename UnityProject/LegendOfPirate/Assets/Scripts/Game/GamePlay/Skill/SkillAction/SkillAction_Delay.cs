using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class SkillAction_Delay : SkillAction
    {
        private float m_Delay;

        private float m_Timer;
        private Skill m_Skill;

        public SkillAction_Delay(float delay) //: base(owner)
        {
            this.m_Delay = delay;
        }

        public override void ExcuteAction(Skill skill)
        {
            m_Skill = skill;
            m_Timer = 0;
            skill.Owner.onUpdate += OnUpdate;

        }

        private void OnUpdate()
        {
            m_Timer += Time.deltaTime;
            if (m_Timer >= m_Delay)
            {
                OnDelayEnd();
            }
        }

        private void OnDelayEnd()
        {
            Debug.LogError("延时结束");
            m_Skill.SkillActionStepEnd();
            m_Skill.Owner.onUpdate -= OnUpdate;
        }
    }

}