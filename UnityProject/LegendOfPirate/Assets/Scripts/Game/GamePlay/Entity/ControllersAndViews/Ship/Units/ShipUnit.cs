using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    // Ship的组件功能比较简单，所以此处不进行VC结构分层
    public class ShipUnit : ClickableView, IShipUnit
    {
        protected ShipUnitType m_ShipUnitType = ShipUnitType.None;
        protected ShipUnitStateMachine m_StateMachine = null;
        protected ShipUnitStateId m_CurState = ShipUnitStateId.Locked;

        #region IShipUnit

        public virtual void OnInit()
        {
            m_StateMachine = new ShipUnitStateMachine(this);
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

        public void SetState(ShipUnitStateId state)
        {
            if (m_CurState != state)
            {
                m_CurState = state;
                m_StateMachine.SetCurrentStateByID(m_CurState);
            }
        }
    }

}