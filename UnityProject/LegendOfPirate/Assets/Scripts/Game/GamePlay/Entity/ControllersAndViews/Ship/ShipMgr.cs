using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class ShipMgr : TSingleton<ShipMgr>, IMgr
    {
        private Dictionary<ShipUnitType, IShipUnit> m_ShipUnitDic = null;

        #region IMgr

        public void OnInit()
        {
            m_ShipUnitDic = new Dictionary<ShipUnitType, IShipUnit>();
        }

        public void OnUpdate()
        {
        }

        public void OnDestroyed()
        {
            m_ShipUnitDic = null;
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

        }

        private void SpawnShip()
        {

        }

        #endregion

    }

}