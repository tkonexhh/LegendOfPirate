using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


namespace GameWish.Game
{
    public class BattleRoleAIState_Global : BattleRoleAIState
    {
        private IDisposable m_IsDeadChange;

        public override void Enter(BattleRoleAI ai)
        {
            m_IsDeadChange = ai.controller.Data.buffedData.IsDead.Subscribe(isDead =>
            {
                if (isDead)
                {
                    ai.FSM.SetCurrentStateByID(BattleRoleAIStateEnum.Dead);
                }
            });

        }

        public override void Execute(BattleRoleAI ai, float dt)
        {
            if (ai.Target != null)
            {
                if (ai.Target.Data.buffedData.IsDead.Value)
                {
                    ai.Target = null;
                }
            }

            if (ai.Target == null
                && !ai.controller.Data.buffedData.IsDead.Value
                && BattleMgr.S.Started)
            {
                // Debug.LogError("Pick another target");
                //目标消失了，需要换一个目标
                ai.FSM.SetCurrentStateByID(BattleRoleAIStateEnum.PickTarget);
            }
        }

        public override void Exit(BattleRoleAI ai)
        {
            m_IsDeadChange.Dispose();
        }
    }

}