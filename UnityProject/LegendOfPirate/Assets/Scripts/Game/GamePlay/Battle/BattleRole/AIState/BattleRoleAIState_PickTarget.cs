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
            ai.Target = ai.controller.Data.Sensor.PickTarget(ai.controller);
            if (ai.Target == null)
            {
                // Debug.LogError("Pick Target is Null");
                ai.controller.Renderer.PlayAnim(BattleDefine.ROLEANIM_IDLE);
                return;
            }
            ai.FSM.SetCurrentStateByID(BattleRoleAIStateEnum.MoveToTarget);
        }
    }

}