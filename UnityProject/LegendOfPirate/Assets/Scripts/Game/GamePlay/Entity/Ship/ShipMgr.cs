using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class ShipMgr : TSingleton<ShipMgr>, IMgr
    {
        private Dictionary<ShipComponentType, IShipComponent> m_ShipComponentDic = null;

        #region IMgr

        public void OnInit()
        {
            m_ShipComponentDic = new Dictionary<ShipComponentType, IShipComponent>();
        }

        public void OnUpdate()
        {
        }

        public void OnDestroyed()
        {
            m_ShipComponentDic = null;
        }

        #endregion

        #region Public

        public void AddShipComponent(ShipComponentType type, IShipComponent shipComponent)
        {
            if (!m_ShipComponentDic.ContainsKey(type))
            {
                m_ShipComponentDic.Add(type, shipComponent);
            }
            else
            {
                Log.e("Component has been added before : " + type.ToString());
            }
        }

        public IShipComponent GetShipComponent(ShipComponentType type)
        {
            IShipComponent com = null;
            m_ShipComponentDic.TryGetValue(type, out com);

            return com;
        }
        #endregion

    }

}