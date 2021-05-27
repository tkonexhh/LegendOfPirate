using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BattleRoleFSM
    {
        private BattleRoleController m_Controller;
        private BattleRoleFSMStateMachine m_FSM;

        public BattleRoleFSM(BattleRoleController controller)
        {
            m_Controller = controller;
            m_FSM = new BattleRoleFSMStateMachine(controller);
        }

        public void OnUpdate()
        {
            m_FSM.UpdateState(Time.deltaTime);
        }

        public void SetState(BattleRoleStateEnum state)
        {
            m_FSM.SetCurrentStateByID(state);
        }

        public void SendMsg(int key, params object[] args)
        {
            m_FSM.currentState.OnMsg(m_Controller, key, args);
        }
    }

}