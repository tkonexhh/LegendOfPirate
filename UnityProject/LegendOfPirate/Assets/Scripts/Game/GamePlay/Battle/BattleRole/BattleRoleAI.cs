using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattleRoleAI : BattleRoleComponent
    {

        public BattleRoleController Target { get; set; }//当前选择目标
        public FSMStateMachine<BattleRoleAI> FSM { get; private set; }
        public IBattleSensor Sensor { get; private set; }//索敌方式
        public BattleAttacker Attacker { get; private set; }//攻击方式
        public DamageRange DamageRange { get; private set; }//伤害范围


        //TODO 移动到其他component中
        public Run onAttack;
        public Run onMove;
        public Run onHurt;
        public Run onUpdate;

        public BattleRoleAI(BattleRoleController controller) : base(controller)
        {
            Sensor = BattleSensorFactory.CreateBattleSensor(PickTargetType.Enemy, SensorTypeEnum.Nearest);
            this.controller.renderer.PlayAnim("Idle", true);

            FSM = new FSMStateMachine<BattleRoleAI>(this);
            FSM.stateFactory = new FSMStateFactory<BattleRoleAI>(false);
            FSM.stateFactory.RegisterState(BattleRoleAIStateEnum.PickTarget, new BattleRoleAIState_PickTarget());
            FSM.stateFactory.RegisterState(BattleRoleAIStateEnum.MoveToTarget, new BattleRoleAIState_MoveToTarget());
            FSM.stateFactory.RegisterState(BattleRoleAIStateEnum.Attack, new BattleRoleAIState_Attack());
            FSM.stateFactory.RegisterState(BattleRoleAIStateEnum.Skill, new BattleRoleAIState_Skill());
            FSM.stateFactory.RegisterState(BattleRoleAIStateEnum.Dead, new BattleRoleAIState_Dead());
            FSM.stateFactory.RegisterState(BattleRoleAIStateEnum.Victory, new BattleRoleAIState_Victory());
            FSM.stateFactory.RegisterState(BattleRoleAIStateEnum.Global, new BattleRoleAIState_Global());

            Attacker = new BattleAttacker_Lock();
            DamageRange = new DamageRange_Target();
            DamageRange.owner = controller;
        }

        public override void OnBattleStart()
        {
            FSM.SetCurrentStateByID(BattleRoleAIStateEnum.PickTarget);
            FSM.SetGlobalStateByID(BattleRoleAIStateEnum.Global);
        }

        public override void OnUpdate()
        {
            FSM.UpdateState(Time.deltaTime);
            if (onUpdate != null)
            {
                onUpdate();
            }
        }

    }

}