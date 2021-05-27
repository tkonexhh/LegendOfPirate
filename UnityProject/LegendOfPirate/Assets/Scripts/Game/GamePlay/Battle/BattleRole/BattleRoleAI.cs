using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BattleRoleAI : IRoleAI
    {
        private BattleRoleController m_Controller;
        private BattleRoleController m_Target;

        public BattleRoleAI(BattleRoleController controller)
        {
            m_Controller = controller;
        }


        public void OnBattleStart()
        {
            PickTarget();
            MoveToTarget();
        }

        public void OnUpdate()
        {

        }

        private void PickTarget()
        {
            m_Target = BattleMgr.S.BattleRendererComponent.GetRandomController(BattleHelper.GetOppositeCamp(m_Controller.camp));
        }

        private void MoveToTarget()
        {
            m_Controller.fSM.SetState(BattleRoleStateEnum.Move);
            //TODO 移动改为在这个组件下运作
            m_Controller.fSM.SendMsg(0, m_Target);
        }
    }

}