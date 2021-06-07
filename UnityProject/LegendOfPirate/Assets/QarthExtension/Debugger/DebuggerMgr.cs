using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class DebuggerMgr : TSingleton<DebuggerMgr>, IMgr
    {
        private readonly DebuggerWindowGroup m_DebuggerWindowRoot;

        public DebuggerMgr()
        {
            m_DebuggerWindowRoot = new DebuggerWindowGroup();
        }

        #region IMgr

        public void OnInit()
        {
            m_DebuggerWindowRoot.OnInit();
        }

        public void OnUpdate()
        {
            m_DebuggerWindowRoot.OnUpdate();
        }

        public void OnDestroyed()
        {
            m_DebuggerWindowRoot.OnDestroyed();
        }

        #endregion

        #region Public 

        public void RegisterWindow()
        {

        }

        #endregion
    }

}