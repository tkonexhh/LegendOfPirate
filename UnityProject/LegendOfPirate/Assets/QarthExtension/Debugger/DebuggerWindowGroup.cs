using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class DebuggerWindowGroup : IDebuggerWindowGroup
    {
        private readonly List<KeyValuePair<string, IDebuggerWindow>> m_DebuggerWindows;

        public DebuggerWindowGroup()
        {
            m_DebuggerWindows = new List<KeyValuePair<string, IDebuggerWindow>>();
        }

        #region IDebuggerWindowGroup

        public void OnInit()
        {
        }

        public void OnOpen()
        {
        }

        public void OnUpdate()
        {
        }

        public void OnClose()
        {
        }

        public void OnDestroyed()
        {
        }

        #endregion

        #region Public

        public void RegisterDebuggerWindow(string path, IDebuggerWindow debuggerWindow)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new Exception("Path is invalid.");
            }

            int pos = path.IndexOf('/');
            if (pos < 0 || pos >= path.Length - 1)
            {
                if (InternalGetDebuggerWindow(path) != null)
                {
                    throw new Exception("Debugger window has been registered.");
                }

                m_DebuggerWindows.Add(new KeyValuePair<string, IDebuggerWindow>(path, debuggerWindow));
                //RefreshDebuggerWindowNames();
            }
            else
            {
                string debuggerWindowGroupName = path.Substring(0, pos);
                string leftPath = path.Substring(pos + 1);
                DebuggerWindowGroup debuggerWindowGroup = (DebuggerWindowGroup)InternalGetDebuggerWindow(debuggerWindowGroupName);
                if (debuggerWindowGroup == null)
                {
                    if (InternalGetDebuggerWindow(debuggerWindowGroupName) != null)
                    {
                        throw new Exception("Debugger window has been registered, can not create debugger window group.");
                    }

                    debuggerWindowGroup = new DebuggerWindowGroup();
                    m_DebuggerWindows.Add(new KeyValuePair<string, IDebuggerWindow>(debuggerWindowGroupName, debuggerWindowGroup));
                    //RefreshDebuggerWindowNames();
                }

                debuggerWindowGroup.RegisterDebuggerWindow(leftPath, debuggerWindow);
            }
        }

        /// <summary>
        /// 解除注册调试器窗口。
        /// </summary>
        /// <param name="path">调试器窗口路径。</param>
        /// <returns>是否解除注册调试器窗口成功。</returns>
        public bool UnregisterDebuggerWindow(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            int pos = path.IndexOf('/');
            if (pos < 0 || pos >= path.Length - 1)
            {
                IDebuggerWindow debuggerWindow = InternalGetDebuggerWindow(path);
                bool result = m_DebuggerWindows.Remove(new KeyValuePair<string, IDebuggerWindow>(path, debuggerWindow));
                debuggerWindow.OnDestroyed();
                //RefreshDebuggerWindowNames();
                return result;
            }

            string debuggerWindowGroupName = path.Substring(0, pos);
            string leftPath = path.Substring(pos + 1);
            DebuggerWindowGroup debuggerWindowGroup = (DebuggerWindowGroup)InternalGetDebuggerWindow(debuggerWindowGroupName);
            if (debuggerWindowGroup == null)
            {
                return false;
            }

            return debuggerWindowGroup.UnregisterDebuggerWindow(leftPath);
        }

        #endregion

        private IDebuggerWindow InternalGetDebuggerWindow(string name)
        {
            foreach (KeyValuePair<string, IDebuggerWindow> debuggerWindow in m_DebuggerWindows)
            {
                if (debuggerWindow.Key == name)
                {
                    return debuggerWindow.Value;
                }
            }

            return null;
        }
    }

}