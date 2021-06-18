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

        #endregion

        #region Public Set

        public ShipRoleController InitWhenAllocated()
        {
            m_StateMachine = new ShipRoleStateMachine(this);

            return this;
        }

        public ShipRoleController SetRoleView(ShipRoleView roleView)
        {
            m_RoleView = roleView;

            return this;
        }

        public ShipRoleController SetRoleModel(RoleModel roleModel)
        {
            m_RoleModel = roleModel;

            return this;
        }

        #endregion
    }

}