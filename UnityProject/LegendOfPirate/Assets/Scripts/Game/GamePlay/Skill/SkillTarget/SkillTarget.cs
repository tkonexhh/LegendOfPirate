using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public abstract class SkillTarget
    {
        protected Skill Owner;
        public SkillTarget(Skill owner)
        {
            this.Owner = owner;
        }


        public abstract BattleRoleController PicketTarget();
    }

}