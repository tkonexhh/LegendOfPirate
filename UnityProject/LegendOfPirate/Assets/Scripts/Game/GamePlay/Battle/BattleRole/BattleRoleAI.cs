using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattleRoleAI : BattleRoleComponent
    {

        public BattleRoleController Target { get; set; }
        public FSMStateMachine<BattleRoleAI> FSM { get; private set; }
        public IBattleSensor Sensor { get; private set; }

        public delegate void OnMove();
        public delegate void OnAttack();
        public delegate void OnHurt();

        //TODO 移动到其他component中
        public OnAttack onAttack;
        public OnMove onMove;
        public OnHurt onHurt;

        public BattleRoleAI(BattleRoleController controller) : base(controller)
        {
            Sensor = BattleSensorFactory.CreateBattleSensor(PickTargetType.Enemy, SensorTypeEnum.Nearest);
            this.controller.renderer.PlayAnim("Idle", true);

            FSM = new FSMStateMachine<BattleRoleAI>(this);
            FSM.stateFactory = new FSMStateFactory<BattleRoleAI>(false);
            FSM.stateFactory.RegisterState(BattleRoleAIStateEnum.PickTarget, new BattleRoleAIState_PickTarget());
            FSM.stateFactory.RegisterState(BattleRoleAIStateEnum.MoveToTarget, new BattleRoleAIState_MoveToTarget());
            FSM.stateFactory.RegisterState(BattleRoleAIStateEnum.Attack, new BattleRoleAIState_Attack());
            FSM.stateFactory.RegisterState(BattleRoleAIStateEnum.Dead, new BattleRoleAIState_Dead());
            FSM.stateFactory.RegisterState(BattleRoleAIStateEnum.Victory, new BattleRoleAIState_Victory());
            FSM.stateFactory.RegisterState(BattleRoleAIStateEnum.Global, new BattleRoleAIState_Global());

        }

        public override void OnBattleStart()
        {
            FSM.SetCurrentStateByID(BattleRoleAIStateEnum.PickTarget);
            FSM.SetGlobalStateByID(BattleRoleAIStateEnum.Global);
        }

        public override void OnUpdate()
        {
            FSM.UpdateState(Time.deltaTime);
        }

    }

}