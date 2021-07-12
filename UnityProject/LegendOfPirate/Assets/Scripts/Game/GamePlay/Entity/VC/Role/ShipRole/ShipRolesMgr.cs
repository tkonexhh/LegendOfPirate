using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using System.Linq;

namespace GameWish.Game
{
    public class ShipRolesMgr : TSingleton<ShipRolesMgr>, IMgr
    {
        private ShipRoleControllerFactory m_RoleFactory = null;
        private Dictionary<int, ShipRoleController> m_ShipRoleDic = null;
        private List<ShipRoleController> m_ShipRoleList = null;

        public ShipRoleControllerFactory RoleFactory { get => m_RoleFactory;}

        #region IMgr

        public void OnInit()
        {
            m_RoleFactory = new ShipRoleControllerFactory();
            m_ShipRoleDic = new Dictionary<int, ShipRoleController>();
            m_ShipRoleList = new List<ShipRoleController>();

            SpawnShipRoles();
        }

        public void OnUpdate()
        {
            for (int i = 0; i < m_ShipRoleList.Count; i++)
            {
                m_ShipRoleList[i].OnUpdate();
            }
        }

        public void OnDestroyed()
        {
        }

        #endregion


        #region Public Set

        //public void RecycleShipRoleController(ShipRoleController controller)
        //{
        //    m_RoleFactory.RecycleController(controller);
        //}

        #endregion

        #region Private

        private void SpawnShipRoles()
        {
            RoleGroupModel roleGroupModel = ModelMgr.S.GetModel<RoleGroupModel>();
            RoleModel roleModel;
            for (int i = 0; i < roleGroupModel.m_RoleItemList.Count; i++)
            {
                roleModel = roleGroupModel.m_RoleItemList[i];
                ShipRoleController shipRoleController = m_RoleFactory.CreateController(roleModel);
  
                AddRole(roleModel.id, shipRoleController);
            }
        }

        private void AddRole(int id, ShipRoleController controller)
        {
            if (!m_ShipRoleDic.ContainsKey(id))
            {
                m_ShipRoleDic.Add(id, controller);
                m_ShipRoleList.Add(controller);
            }
            else
            {
                Log.e("Role Added Before: " + id.ToString());
            }
        }

        private void RemoveRole(int id)
        {
            if (m_ShipRoleDic.ContainsKey(id))
            {
                m_ShipRoleDic.Remove(id);
                m_ShipRoleList.Remove(m_ShipRoleList.FirstOrDefault(i => i.RoleModel.id == id));
            }
            else
            {
                Log.e("Role Not Found: " + id.ToString());
            }
        }
        #endregion
    }

}