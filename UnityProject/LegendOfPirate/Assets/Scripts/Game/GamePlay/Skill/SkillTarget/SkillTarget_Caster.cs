using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class SkillTarget_Caster : SkillTarget
    {
        public SkillTarget_Caster(Skill owner) : base(owner) { }


        public override BattleRoleController PicketTarget()
        {
            return Owner.Owner;
        }
    }

}