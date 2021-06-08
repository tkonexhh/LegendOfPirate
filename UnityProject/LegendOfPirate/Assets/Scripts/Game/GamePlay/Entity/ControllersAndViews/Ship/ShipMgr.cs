using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class ShipMgr : TSingleton<ShipMgr>, IMgr
    {
        private Dictionary<ShipUnitType, IShipUnit> m_ShipUnitDic = null;

        private ResLoader m_ShipResLoader = null;

        #region IMgr

        public void OnInit()
        {
            m_ShipUnitDic = new Dictionary<ShipUnitType, IShipUnit>();
            m_ShipResLoader = ResLoader.Allocate("ShipResLoader");

            SpawnSea();
            SpawnShip();
        }

        public void OnUpdate()
        {
        }

        public void OnDestroyed()
        {
            m_ShipUnitDic = null;

            m_ShipResLoader.ReleaseAllRes();
            m_ShipResLoader.Recycle2Cache();
            m_ShipResLoader = null;
        }

        #endregion

        #region Public

        public void OnShipUnitUnlocked(ShipUnitType type, IShipUnit shipUnit)
        {
            if (!m_ShipUnitDic.ContainsKey(type))
            {
                m_ShipUnitDic.Add(type, shipUnit);
            }
            else
            {
                Log.e("Component has been added before : " + type.ToString());
            }
        }

        public IShipUnit GetShipUnit(ShipUnitType type)
        {
            IShipUnit com = null;
            m_ShipUnitDic.TryGetValue(type, out com);

            return com;
        }
        #endregion

        #region Priavte

        private void SpawnSea()
        {
            GameObject sea = GameObjectPoolMgr.S.Allocate(Define.SEA);
            sea.transform.SetParent(GameplayMgr.S.EntityRoot);
        }

        private void SpawnShip()
        {
            GameObject ship = GameObjectPoolMgr.S.Allocate(Define.SHIP);
            ship.transform.SetParent(GameplayMgr.S.EntityRoot);

            ShipModel shipModel = ModelMgr.S.GetModel<ShipModel>();
            for (int i = 0; i < shipModel.shipUnitModelList.Count; i++)
            {
                ShipUnitType shipUnitType = shipModel.shipUnitModelList[i].unitType;
                ShipUnitConfig unitConfig = ShipConfig.S.GetUnitConfig(shipUnitType);
                SpawnShipUnit(ship.transform, unitConfig);
            }
        }

        private void SpawnShipUnit(Transform ship, ShipUnitConfig config)
        {
            GameObject prefab = m_ShipResLoader.LoadSync(config.prefabName) as GameObject;
            GameObject obj = GameObject.Instantiate(prefab);
            obj.transform.SetParent(ship);
            obj.transform.SetLocalPos(config.pos);
        }
        #endregion

    }

}