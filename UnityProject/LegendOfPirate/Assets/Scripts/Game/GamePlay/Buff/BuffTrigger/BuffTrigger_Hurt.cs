using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BuffTrigger_Hurt : BuffTrigger
    {
        // public SkillTrigger_Hurt(Skill skill) : base(skill) { }


        public override void Start(BattleRoleController controller)
        {
            controller.AI.onHurt += OnTrigger;
        }

        public override void Stop(BattleRoleController controller)
        {
            controller.AI.onHurt -= OnTrigger;
        }
    }

}