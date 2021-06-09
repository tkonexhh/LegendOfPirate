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

        #region Public Set

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