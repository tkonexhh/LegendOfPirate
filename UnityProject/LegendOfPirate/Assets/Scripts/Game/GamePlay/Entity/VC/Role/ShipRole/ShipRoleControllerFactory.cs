using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class ShipRoleControllerFactory : ControllerFactory<ShipRoleController>
    {
        private ResLoader m_ShipRoleResLoader = null;

        public ShipRoleControllerFactory()
        {
            if (m_ShipRoleResLoader == null)
                m_ShipRoleResLoader = ResLoader.Allocate("ShipRoleLoader");
        }

        protected override void BuildController(ShipRoleController controller, IModel model)
        {
            base.BuildController(controller, model);

            RoleModel roleModel = model as RoleModel;

            GameObject body = SpawnGameObject(roleModel.id);
            ShipRoleView shipRoleView = body.AddComponent<ShipRoleView>();

            controller.SetRoleModel(roleModel).SetRoleView(shipRoleView).InitWhenAllocated(ShipRoleStateId.Idle);

        }

        private GameObject SpawnGameObject(int id)
        {
            //TODO Get Body Res By Id


            GameObject role = GameObjectPoolMgr.S.Allocate(Define.ROLE_PREFAB);
            role.transform.SetParent(EnvMgr.S.ShipController.ShipView.ShipBody.transform);

            GameObject bodyPrefab = m_ShipRoleResLoader.LoadSync("Pirate_Role_Enemy_OneEye_001@skin Variant") as GameObject;
            GameObject bodyObj = GameObject.Instantiate(bodyPrefab);
            bodyObj.transform.SetParent(role.transform);
            bodyObj.transform.SetLocalPos(Vector3.zero);

            role.transform.position = EnvMgr.S.ShipController.ShipView.ShipBody.roleSpawnPos.position;

            return role;
        }
    }

}