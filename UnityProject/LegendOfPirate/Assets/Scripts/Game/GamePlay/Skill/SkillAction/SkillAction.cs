using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public abstract class SkillAction
    {
        // protected Skill owner;
        // public SkillAction(Skill owner)
        // {
        //     this.owner = owner;
        // }
        public abstract void ExcuteAction(Skill skill);
    }

}