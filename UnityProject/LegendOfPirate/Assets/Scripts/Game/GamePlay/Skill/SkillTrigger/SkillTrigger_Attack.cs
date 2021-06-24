using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class SkillTrigger_Attack : SkillTrigger
    {
        private Skill m_Skill;
        public override void Start(Skill skill)
        {
            base.Start(skill);
            m_Skill = skill;
            Debug.LogError("Start Attack" + skill.Owner.AI.onAttack);
            skill.Owner.AI.onAttack += OnAttack;//OnTrigger;
        }

        public override void Stop(Skill skill)
        {
            base.Stop(skill);
            Debug.LogError("End Attack");
            skill.Owner.AI.onAttack -= OnAttack;//OnTrigger;
        }

        private void OnAttack()
        {
            Debug.LogError("Onattack");
            if (m_Skill.triggerCDTimer >= m_Skill.TriggerCD)
            {
                Debug.LogError("SkillTrigger_Attack OnAttack");
                m_Skill.triggerCDTimer = 0;
                OnTrigger();
            }
        }
    }

}