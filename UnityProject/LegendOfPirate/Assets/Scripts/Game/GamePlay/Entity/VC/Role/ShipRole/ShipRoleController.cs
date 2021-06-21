using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    //对应BattleRole,ShipRole是在船上生成的角色
    public class ShipRoleController : RoleController
	{
        private ShipRoleView m_RoleView = null;
        private RoleModel m_RoleModel = null;
        private ShipRoleStateMachine m_StateMachine = null;

        private ShipRoleStateId m_CurState = ShipRoleStateId.None;

        public ShipRoleView RoleView { get => m_RoleView;}
        public RoleModel RoleModel { get => m_RoleModel;}

        #region Override

        public override void OnCacheReset()
        {
            base.OnCacheReset();

            m_StateMachine = null;
            m_RoleView = null;
            m_RoleModel = null;
        }

        public override void Recycle2Cache()
        {
            base.Recycle2Cache();

            ShipRolesMgr.S.RoleFactory.RecycleController(this);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (m_StateMachine != null)
            {
                m_StateMachine.UpdateState(Time.deltaTime);
            }
        }
        #endregion

        #region Public Set

        public ShipRoleController InitWhenAllocated(ShipRoleStateId state)
        {
            m_StateMachine = new ShipRoleStateMachine(this);
            SetState(state);

            return this;
        }

        public ShipRoleController SetRoleView(ShipRoleView roleView)
        {
            m_RoleView = roleView;
            m_RoleView.Init();

            return this;
        }

        public ShipRoleController SetRoleModel(RoleModel roleModel)
        {
            m_RoleModel = roleModel;

            return this;
        }

        #endregion

        private void SetState(ShipRoleStateId stateId)
        {
            if (m_CurState != stateId)
            {
                m_CurState = stateId;

                m_StateMachine.SetCurrentStateByID(stateId);
            }
        }
    }

}