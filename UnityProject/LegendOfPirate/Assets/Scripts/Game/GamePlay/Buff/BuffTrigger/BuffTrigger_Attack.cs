using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BuffTrigger_Attack : BuffTrigger
    {
        // public SkillTrigger_Attack(Skill skill) : base(skill){}


        public override void Start(BattleRoleController controller)
        {
            controller.AI.onAttack += OnTrigger;
        }

        public override void Stop(BattleRoleController controller)
        {
            controller.AI.onAttack -= OnTrigger;
        }

    }

}