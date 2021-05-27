using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattleRoleController : RoleController
    {
        private BattleRoleRenderer m_Renderer;
        private BattleRoleFSM m_FSM;

        public BattleRoleRenderer renderer => m_Renderer;

        #region Override
        public override void OnInit()
        {
            base.OnInit();
            m_Renderer = ObjectPool<BattleRoleRenderer>.S.Allocate();
            m_Renderer.OnInit();

            m_FSM = new BattleRoleFSM(this);
            m_FSM.SetCurrentStateByID(BattleRoleStateEnum.Idle);
        }

        public override void OnUpdate()
        {
            m_Renderer.OnUpdate();
            m_FSM.UpdateState(Time.deltaTime);
        }
        public override void OnDestroyed()
        {
            ObjectPool<BattleRoleRenderer>.S.Recycle(m_Renderer);
            m_Renderer = null;
        }

        public override void Recycle2Cache()
        {
            base.Recycle2Cache();
            Debug.LogError("Recycle2Cache");

        }
        #endregion
    }

}