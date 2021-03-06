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



        //TODO 移动到其他component中
        public Run onAttack;
        public Run onMove;
        public Run onHurt;
        public Run onUpdate;

        private bool m_Paused = false;

        public BattleRoleAI(BattleRoleController controller) : base(controller)
        {
            FSM = new FSMStateMachine<BattleRoleAI>(this);
            FSM.stateFactory = new FSMStateFactory<BattleRoleAI>(false);
            FSM.stateFactory.RegisterState(BattleRoleAIStateEnum.PickTarget, new BattleRoleAIState_PickTarget());
            FSM.stateFactory.RegisterState(BattleRoleAIStateEnum.MoveToTarget, new BattleRoleAIState_MoveToTarget());
            FSM.stateFactory.RegisterState(BattleRoleAIStateEnum.Attack, new BattleRoleAIState_Attack());
            FSM.stateFactory.RegisterState(BattleRoleAIStateEnum.Skill, new BattleRoleAIState_Skill());
            FSM.stateFactory.RegisterState(BattleRoleAIStateEnum.Dead, new BattleRoleAIState_Dead());
            FSM.stateFactory.RegisterState(BattleRoleAIStateEnum.Victory, new BattleRoleAIState_Victory());
            FSM.stateFactory.RegisterState(BattleRoleAIStateEnum.Global, new BattleRoleAIState_Global());
        }

        public override void OnBattleStart()
        {
            controller.MonoReference.Collider.enabled = true;
            FSM.SetCurrentStateByID(BattleRoleAIStateEnum.PickTarget);
            FSM.SetGlobalStateByID(BattleRoleAIStateEnum.Global);
        }

        public override void OnUpdate()
        {
            if (m_Paused)
            {
                return;
            }

            FSM.UpdateState(Time.deltaTime);
            if (onUpdate != null)
            {
                onUpdate();
            }
        }

        public void Pause()
        {
            m_Paused = true;
        }

        public void Resume()
        {
            m_Paused = false;
        }

    }

}