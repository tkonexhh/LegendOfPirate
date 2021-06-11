using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public abstract class SkillTrigger
    {
        public Run onSkillTrigger;

        public virtual void Start(Skill skill)
        {
            onSkillTrigger += skill.ExcuteSkill;
        }

        public virtual void Stop(Skill skill)
        {
            onSkillTrigger -= skill.ExcuteSkill;
        }

        protected void OnTrigger()
        {
            if (onSkillTrigger != null)
            {
                onSkillTrigger();
            }
        }
    }

}