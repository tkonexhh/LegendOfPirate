using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class ShipRolesMgr : TSingleton<ShipRolesMgr>, IMgr
    {
        private ShipRoleControllerFactory m_RoleFactory = null;
        private Dictionary<int, ShipRoleController> m_ShipRoleDic = null;

        #region IMgr

        public void OnInit()
        {
            m_RoleFactory = new ShipRoleControllerFactory();
            m_ShipRoleDic = new Dictionary<int, ShipRoleController>();

            SpawnShipRoles();
        }

        public void OnUpdate()
        {
        }

        public void OnDestroyed()
        {
        }

        #endregion


        #region Private

        private void SpawnShipRoles()
        {
            RoleGroupModel roleGroupModel = ModelMgr.S.GetModel<RoleGroupModel>();
            RoleModel roleModel;
            for (int i = 0; i < roleGroupModel.roleItemList.Count; i++)
            {
                roleModel = roleGroupModel.roleItemList[i];
                ShipRoleController shipRoleController = m_RoleFactory.CreateController(roleModel);
  
                AddRoleToDic(roleModel.id, shipRoleController);
            }
        }

        private void AddRoleToDic(int id, ShipRoleController controller)
        {
            if (!m_ShipRoleDic.ContainsKey(id))
            {
                m_ShipRoleDic.Add(id, controller);
            }
            else
            {
                Log.e("Role Added Before: " + id.ToString());
            }
        }
        #endregion
    }

}