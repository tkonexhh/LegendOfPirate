using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using System;
using UnityEngine.SceneManagement;

namespace GameWish.Game
{
    public class GameWorldMgr : TSingleton<GameWorldMgr>, IMgr, IResetHandler
    {
        private bool m_IsInited = false;

        public void Init()
        {
            Log.i("--------------World init--------------");
            m_IsInited = true;
        }

        public void Update()
        {
            if (m_IsInited == false)
                return;

        }

        public void OnReset()
        {
            GameDataMgr.S.OnReset();
        }

        private void RegisterEvents()
        {

        }

        private void UnregisterEvents()
        {

        }

        private void HandleEvent(int key, params object[] param)
        {

        }
    }
}