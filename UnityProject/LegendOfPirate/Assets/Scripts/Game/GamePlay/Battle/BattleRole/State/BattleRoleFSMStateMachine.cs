using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public enum BattleRoleStateEnum
    {
        Idle,
        Move,
        Attack,
        Dead,
    }
    public class BattleRoleFSMStateMachine : FSMStateMachine<BattleRoleController>
    {
        public BattleRoleFSMStateMachine(BattleRoleController entity) : base(entity)
        {
            stateFactory = new FSMStateFactory<BattleRoleController>(false);
            stateFactory.RegisterState(BattleRoleStateEnum.Idle, new BattleRoleState_Idle());
            stateFactory.RegisterState(BattleRoleStateEnum.Move, new BattleRoleState_Move());
            stateFactory.RegisterState(BattleRoleStateEnum.Attack, new BattleRoleState_Attack());
            stateFactory.RegisterState(BattleRoleStateEnum.Dead, new BattleRoleState_Dead());
        }
    }
}

