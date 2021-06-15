using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class SkillTrigger_Cast : SkillTrigger
    {
        public override void Start(Skill skill)
        {
            base.Start(skill);
            skill.onCast += OnTrigger;
        }

        public override void Stop(Skill skill)
        {
            base.Stop(skill);
            skill.onCast -= OnTrigger;
        }
    }

}