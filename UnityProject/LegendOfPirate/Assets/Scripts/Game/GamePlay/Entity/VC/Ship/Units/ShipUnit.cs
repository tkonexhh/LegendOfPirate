using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using UniRx;

namespace GameWish.Game
{
    // Ship的组件功能比较简单，所以此处不进行VC结构分层
    public class ShipUnit : ClickableView, IShipUnit
    {
        [SerializeField]
        protected Transform m_BodyRoot = null;
        protected ShipUnitStateMachine m_StateMachine = null;
        protected ShipUnitStateId m_CurState = ShipUnitStateId.Locked;
        protected GameObject m_Body = null;

        private ResLoader m_ResLoader = null;
        private ShipUnitModel m_UnitModel = null;

        #region IShipUnit

        public virtual void OnInit()
        {
            m_StateMachine = new ShipUnitStateMachine(this);
            m_ResLoader = ResLoader.Allocate("ShipUnitLoader" + GetShipUnitType().ToString());

            m_UnitModel = ModelMgr.S.GetModel<ShipModel>().GetShipUnitModel(GetShipUnitType());
            m_UnitModel.level.Subscribe(OnLevelChanged);

            m_Body = SpawnBody();
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void OnDestroyed()
        {
        }

        public virtual ShipUnitType GetShipUnitType()
        {
            return ShipUnitType.None;
        }

        #endregion

        #region Public Get
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

        #endregion

        #region Private

        private GameObject SpawnBody()
        {
            ShipUnitConfig unitConfig = ShipConfig.S.GetUnitConfig(GetShipUnitType());

            GameObject prefab = m_ResLoader.LoadSync(unitConfig.bodyPrefabName) as GameObject;
            GameObject obj = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            obj.transform.SetParent(m_BodyRoot);
            obj.transform.SetLocalPos(Vector3.zero);

            return obj;
        }

        private void OnLevelChanged(int newLevel)
        {
            //TODO Change body

        }
        #endregion
    }

}