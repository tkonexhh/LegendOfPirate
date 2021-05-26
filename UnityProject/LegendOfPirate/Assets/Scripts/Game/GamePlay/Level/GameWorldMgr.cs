using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using System;
using UnityEngine.SceneManagement;

namespace GameWish.Game
{
    public class GameWorldMgr : TSingleton<GameWorldMgr>, IMgr
    {
        private bool m_IsInited = false;

        #region IMgr
        public void OnInit()
        {
            Log.i("--------------World init--------------");
            m_IsInited = true;

            ModelMgr.S.OnInit();
            BattleMgr.S.Init();

        }

        public void OnUpdate()
        {
            if (m_IsInited == false)
                return;

        }

        public void OnDestroyed()
        {

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