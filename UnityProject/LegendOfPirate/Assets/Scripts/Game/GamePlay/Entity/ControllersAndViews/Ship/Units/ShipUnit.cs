using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    // Ship的组件功能比较简单，所以此处不进行VC结构分层
    public class ShipUnit : ClickableView, IShipUnit
    {
        protected ShipUnitType m_ShipUnitType = ShipUnitType.None;

        #region IShipUnit

        public virtual void OnInit()
        {

        }

        public virtual void OnUpdate()
        {
        }

        public virtual void OnDestroyed()
        {
        }

        #endregion

        public override int GetSortingLayer()
        {
            return Define.INPUT_SORTING_ORDER_SHIP_UNIT;
        }
    }

}