using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BattleRoleAIState_PickTarget : BattleRoleAIState
    {
        public override void Enter(BattleRoleAI ai)
        {
            base.Enter(ai);
            ai.Target = ai.Sensor.PickTarget();
            if (ai.Target == null)
            {
                // Debug.LogError("Pick Target is Null");
                ai.controller.renderer.PlayAnim("Idle", true);
                return;
            }
            ai.FSM.SetCurrentStateByID(BattleRoleAIStateEnum.MoveToTarget);
        }
    }

}