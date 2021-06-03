using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class SkillTrigger_Time : SkillTrigger
    {
        public override void Start(BattleRoleController controller)
        {
            controller.AI.onUpdate += OnTrigger;
        }

        public override void Stop(BattleRoleController controller)
        {
            controller.AI.onUpdate -= OnTrigger;
        }
    }

}