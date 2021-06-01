using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BattleRoleAIState_MoveToTarget : BattleRoleAIState
    {
        public override void Enter(BattleRoleAI ai)
        {
            base.Enter(ai);
            ai.controller.MonoReference.AstarAI.canMove = true;
            ai.controller.renderer.CrossFadeAnim("Run", 0.1f, true);
        }

        public override void Execute(BattleRoleAI ai, float dt)
        {
            base.Execute(ai, dt);
            if (ai.controller.MonoReference.AstarAI.reachedDestination)
            {
                ai.FSM.SetCurrentStateByID(BattleRoleAIStateEnum.Attack);
            }
            ai.controller.MonoReference.AstarAI.canMove = !ai.controller.Data.buffedData.StatusMask.HasStatus(StatusControlType.MoveForbid);
            ai.controller.MonoReference.AstarAI.maxSpeed = ai.controller.Data.buffedData.MoveSpeed;
            ai.controller.MonoReference.AstarAI.destination = ai.Target.transform.position;
        }

        public override void Exit(BattleRoleAI ai)
        {
            base.Exit(ai);
            ai.controller.MonoReference.AstarAI.canMove = false;
        }
    }

}