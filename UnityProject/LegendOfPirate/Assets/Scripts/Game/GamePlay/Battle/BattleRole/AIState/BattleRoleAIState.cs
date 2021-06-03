using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public enum BattleRoleAIStateEnum
    {
        PickTarget,
        MoveToTarget,
        Attack,
        Skill,
        Global,
        Dead,
        Victory,
    }

    public class BattleRoleAIState : FSMState<BattleRoleAI>
    {
        protected BattleRoleAI m_AI;

        public override void Enter(BattleRoleAI ai)
        {
            m_AI = ai;
        }

        public override void Execute(BattleRoleAI ai, float dt)
        {

        }

        public override void Exit(BattleRoleAI ai)
        {

        }

        public override void OnMsg(BattleRoleAI ai, int key, params object[] args)
        {

        }
    }

}