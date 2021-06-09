using Qarth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public class ShipFactory
	{
        private ShipController m_ShipController = null;
        private ResLoader m_ShipResLoader = null;

        public ShipFactory(ShipController ship)
        {
            m_ShipController = ship;
            m_ShipResLoader = ResLoader.Allocate("ShipResLoader");
        }

        #region Public

        public void Build()
        {
            GameObject ship = GameObjectPoolMgr.S.Allocate(Define.SHIP);
            ship.transform.SetParent(GameplayMgr.S.EntityRoot);

            SpawnShipBody(ship.transform);

            SpawnShipUnits(ship);
        }

        public void Release()
        {
            m_ShipResLoader.ReleaseAllRes();
            m_ShipResLoader.Recycle2Cache();
            m_ShipResLoader = null;
        }

        #endregion

        #region Private

        private void SpawnShipBody(Transform ship)
        {
            string shipBodyPrefabName = "ShipBody";
            GameObject prefab = m_ShipResLoader.LoadSync(shipBodyPrefabName) as GameObject;
            GameObject obj = GameObject.Instantiate(prefab);
            obj.transform.SetParent(ship);
        }

        private void SpawnShipUnits(GameObject ship)
        {
            ShipModel shipModel = ModelMgr.S.GetModel<ShipModel>();
            for (int i = 0; i < shipModel.shipUnitModelList.Count; i++)
            {
                ShipUnitType shipUnitType = shipModel.shipUnitModelList[i].unitType;
                ShipUnitConfig unitConfig = ShipConfig.S.GetUnitConfig(shipUnitType);
                if (unitConfig != null)
                    SpawnShipUnit(ship.transform, unitConfig);
            }
        }

        private void SpawnShipUnit(Transform ship, ShipUnitConfig config)
        {
            GameObject prefab = m_ShipResLoader.LoadSync(config.prefabName) as GameObject;
            GameObject obj = GameObject.Instantiate(prefab);
            obj.transform.SetParent(ship);
            obj.transform.SetLocalPos(config.pos);

            ShipUnit shipUnit = obj.GetComponent<ShipUnit>();
            shipUnit.OnInit();
            m_ShipController.AddShipUnit(shipUnit.GetShipUnitType(), shipUnit);
        }

        #endregion

    }

}