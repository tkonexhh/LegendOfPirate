using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using System;
using UnityEngine.SceneManagement;

namespace GameWish.Game
{
    public class GameLogicMgr : TSingleton<GameLogicMgr>, IMgr
    {
        private bool m_IsInited = false;

        #region IMgr
        public void OnInit()
        {
            Log.i("--------------World init--------------");

            m_IsInited = true;

            InputMgr.S.OnInit();

            ModelMgr.S.OnInit();

            BattleMgr.S.OnInit();

            DailyRefreshMgr.S.OnInit();

            EnvMgr.S.OnInit();

            ShipRolesMgr.S.OnInit();
            GameCameraMgr.S.ToSea();
            //UIMgr.S.OpenPanel(UIID.TestPanel);
        }

        public void OnUpdate()
        {
            if (m_IsInited == false)
                return;

            BattleMgr.S.OnUpdate();

            ModelMgr.S.OnUpdate();

            ShipRolesMgr.S.OnUpdate();
        }

        public void OnDestroyed()
        {
            DailyRefreshMgr.S.OnDestroyed();
        }

        #endregion

        #region Private
        private void RegisterEvents()
        {

        }

        private void UnregisterEvents()
        {

        }

        private void HandleEvent(int key, params object[] param)
        {

        }

        #endregion
    }
}