using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    // 注意动画事件
    public class SkillTrigger_Attack : SkillTrigger
    {
        private Skill m_Skill;
        public override void Start(Skill skill)
        {
            base.Start(skill);
            m_Skill = skill;
            skill.Owner.AI.onAttack += OnAttack;//OnTrigger;
        }

        public override void Stop(Skill skill)
        {
            base.Stop(skill);
            skill.Owner.AI.onAttack -= OnAttack;//OnTrigger;
        }

        private void OnAttack()
        {
            if (m_Skill.triggerCDTimer >= m_Skill.TriggerCD)
            {
                // Debug.LogError("SkillTrigger_Attack OnAttack");
                m_Skill.triggerCDTimer = 0;
                OnTrigger();
            }
        }
    }

}