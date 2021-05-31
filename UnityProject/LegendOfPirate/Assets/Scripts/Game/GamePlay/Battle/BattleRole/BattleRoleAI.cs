using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattleRoleAI : IRoleAI
    {
        public BattleRoleController controller { get; private set; }
        public BattleRoleController Target { get; set; }

        public FSMStateMachine<BattleRoleAI> FSM { get; private set; }

        public BattleRoleAI(BattleRoleController controller)
        {
            this.controller = controller;
            // this.controller.fSM.SetState(BattleRoleStateEnum.Idle);
            this.controller.renderer.PlayAnim("Idle", true);

            FSM = new FSMStateMachine<BattleRoleAI>(this);
            FSM.stateFactory = new FSMStateFactory<BattleRoleAI>(false);
            FSM.stateFactory.RegisterState(BattleRoleAIStateEnum.PickTarget, new BattleRoleAIState_PickTarget());
            FSM.stateFactory.RegisterState(BattleRoleAIStateEnum.MoveToTarget, new BattleRoleAIState_MoveToTarget());
            FSM.stateFactory.RegisterState(BattleRoleAIStateEnum.Attack, new BattleRoleAIState_Attack());
            FSM.stateFactory.RegisterState(BattleRoleAIStateEnum.Dead, new BattleRoleAIState_Dead());
            FSM.stateFactory.RegisterState(BattleRoleAIStateEnum.Global, new BattleRoleAIState_Global());

        }


        public void OnBattleStart()
        {
            FSM.SetCurrentStateByID(BattleRoleAIStateEnum.PickTarget);
            FSM.SetGlobalStateByID(BattleRoleAIStateEnum.Global);
        }

        public void OnUpdate()
        {
            FSM.UpdateState(Time.deltaTime);
        }

    }

}