using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class SkillTrigger_Create : SkillTrigger
    {
        public override void Start(Skill skill)
        {
            base.Start(skill);
            skill.onCreate += OnTrigger;
        }

        public override void Stop(Skill skill)
        {
            base.Stop(skill);
            skill.onCreate -= OnTrigger;
        }
    }

}