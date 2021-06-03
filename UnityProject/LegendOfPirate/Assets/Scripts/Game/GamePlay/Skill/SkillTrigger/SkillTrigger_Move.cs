using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class SkillTrigger_Move : SkillTrigger
    {
        public override void Start(BattleRoleController controller)
        {
            controller.AI.onMove += OnTrigger;
        }

        public override void Stop(BattleRoleController controller)
        {
            controller.AI.onMove -= OnTrigger;
        }
    }

}