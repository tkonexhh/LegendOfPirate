using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
	public class EnvMgr : TSingleton<EnvMgr>, IMgr
	{
        private ShipController m_ShipController = null;

        public ShipController ShipController { get => m_ShipController;}

        #region IMgr

        public void OnInit()
        {
            SpawnShip();

            SpawnSea();
        }

        public void OnUpdate()
        {
        }

        public void OnDestroyed()
        {

        }

        #endregion

        #region Private

        private void SpawnShip()
        {
            m_ShipController = new ShipController();
            m_ShipController.OnInit();
        }

        private void SpawnSea()
        {
            GameObject sea = GameObjectPoolMgr.S.Allocate(Define.SEA_PREFAB);
            sea.transform.SetParent(GameplayMgr.S.EntityRoot);
        }

        #endregion
    }

}