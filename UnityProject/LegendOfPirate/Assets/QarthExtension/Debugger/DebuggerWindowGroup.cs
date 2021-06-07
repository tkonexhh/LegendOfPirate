using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class DebuggerWindowGroup : IDebuggerWindowGroup
    {
        private readonly List<KeyValuePair<string, IDebuggerWindow>> m_DebuggerWindows;
        private string[] m_DebuggerWindowNames;

        private int m_SelectedIndex;

        /// <summary>
        /// 获取调试器窗口数量。
        /// </summary>
        public int DebuggerWindowCount
        {
            get
            {
                return m_DebuggerWindows.Count;
            }
        }

        /// <summary>
        /// 获取或设置当前选中的调试器窗口索引。
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                return m_SelectedIndex;
            }
            set
            {
                m_SelectedIndex = value;
            }
        }

        /// <summary>
        /// 获取当前选中的调试器窗口。
        /// </summary>
        public IDebuggerWindow SelectedWindow
        {
            get
            {
                if (m_SelectedIndex >= m_DebuggerWindows.Count)
                {
                    return null;
                }

                return m_DebuggerWindows[m_SelectedIndex].Value;
            }
        }

        public DebuggerWindowGroup()
        {
            m_SelectedIndex = 0;
            m_DebuggerWindows = new List<KeyValuePair<string, IDebuggerWindow>>();
            m_DebuggerWindowNames = null;
        }

        #region IDebuggerWindowGroup

        public void Initialize(params object[] args)
        {
        }

        public void Shutdown()
        {
            foreach (KeyValuePair<string, IDebuggerWindow> debuggerWindow in m_DebuggerWindows)
            {
                debuggerWindow.Value.Shutdown();
            }

            m_DebuggerWindows.Clear();
        }

        public void OnEnter()
        {
            SelectedWindow.OnEnter();
        }

        public void OnLeave()
        {
        }

        public void OnUpdate()
        {
        }

        public void OnDraw()
        {
        }

        #endregion

        #region Public Get

        /// <summary>
        /// 获取调试组的调试器窗口名称集合。
        /// </summary>
        public string[] GetDebuggerWindowNames()
        {
            return m_DebuggerWindowNames;
        }

        /// <summary>
        /// 获取调试器窗口。
        /// </summary>
        /// <param name="path">调试器窗口路径。</param>
        /// <returns>要获取的调试器窗口。</returns>
        public IDebuggerWindow GetDebuggerWindow(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            int pos = path.IndexOf('/');
            if (pos < 0 || pos >= path.Length - 1)
            {
                return InternalGetDebuggerWindow(path);
            }

            string debuggerWindowGroupName = path.Substring(0, pos);
            string leftPath = path.Substring(pos + 1);
            DebuggerWindowGroup debuggerWindowGroup = (DebuggerWindowGroup)InternalGetDebuggerWindow(debuggerWindowGroupName);
            if (debuggerWindowGroup == null)
            {
                return null;
            }

            return debuggerWindowGroup.GetDebuggerWindow(leftPath);
        }


        #endregion

        #region Public Set

        /// <summary>
        /// 选中调试器窗口。
        /// </summary>
        /// <param name="path">调试器窗口路径。</param>
        /// <returns>是否成功选中调试器窗口。</returns>
        public bool SelectDebuggerWindow(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            int pos = path.IndexOf('/');
            if (pos < 0 || pos >= path.Length - 1)
            {
                return InternalSelectDebuggerWindow(path);
            }

            string debuggerWindowGroupName = path.Substring(0, pos);
            string leftPath = path.Substring(pos + 1);
            DebuggerWindowGroup debuggerWindowGroup = (DebuggerWindowGroup)InternalGetDebuggerWindow(debuggerWindowGroupName);
            if (debuggerWindowGroup == null || !InternalSelectDebuggerWindow(debuggerWindowGroupName))
            {
                return false;
            }

            return debuggerWindowGroup.SelectDebuggerWindow(leftPath);
        }


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
                RefreshDebuggerWindowNames();
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
                    RefreshDebuggerWindowNames();
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
                debuggerWindow.Shutdown();
                RefreshDebuggerWindowNames();
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

        #region Private

        private void RefreshDebuggerWindowNames()
        {
            int index = 0;
            m_DebuggerWindowNames = new string[m_DebuggerWindows.Count];
            foreach (KeyValuePair<string, IDebuggerWindow> debuggerWindow in m_DebuggerWindows)
            {
                m_DebuggerWindowNames[index++] = debuggerWindow.Key;
            }
        }

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

        private bool InternalSelectDebuggerWindow(string name)
        {
            for (int i = 0; i < m_DebuggerWindows.Count; i++)
            {
                if (m_DebuggerWindows[i].Key == name)
                {
                    m_SelectedIndex = i;
                    return true;
                }
            }

            return false;
        }

        #endregion
    }

}