using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class ShipController : Controller
    {
        private Dictionary<ShipUnitType, ShipUnit> m_ShipUnitDic = null;

        private ShipFactory m_ShipFactory = null;

        private ShipView m_ShipView = null;

        public ShipView ShipView { get => m_ShipView; set => m_ShipView = value; }

        #region Controller

        public override void OnInit()
        {
            m_ShipUnitDic = new Dictionary<ShipUnitType, ShipUnit>();

            m_ShipFactory = new ShipFactory(this);
            m_ShipFactory.Build();
        }

        public override void OnUpdate()
        {

        }

        public override void OnDestroyed()
        {
            m_ShipUnitDic.Clear();
            m_ShipUnitDic = null;

            m_ShipFactory.Release();
            m_ShipFactory = null;
        }

        #endregion

        #region Public Set

        public void OnShipUnitUnlocked(ShipUnitType type, ShipUnit shipUnit)
        {
            AddShipUnit(type, shipUnit);
        }

        public void AddShipUnit(ShipUnitType type, ShipUnit shipUnit)
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

        #endregion

        #region Public Get

        public ShipUnit GetShipUnit(ShipUnitType type)
        {
            ShipUnit com = null;
            m_ShipUnitDic.TryGetValue(type, out com);

            return com;
        }

        #endregion

        #region Priavte


        #endregion

    }

}