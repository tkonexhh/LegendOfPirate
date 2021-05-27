using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using System;

namespace GameWish.Game
{
    public enum DataMode
    {
        Server,
        Local
    }

    /// <summary>
    /// GameData对外交互类
    /// </summary>
    public class GameDataMgr : TSingleton<GameDataMgr>
    {
        public static DataMode s_DataMode = DataMode.Server;

        private List<ILoadDataFromServer> m_DataHanlderList;

        private PlayerInfoDataHandler m_PlayerInfoDataHandler = null;
        private RoleDataHandler m_RoleDataHandler = null;

        private int m_LoadDoneCount = 0;
        private Action m_OnLoadDoneCallback = null;

        #region Public

        public void Init(Action callback)
        {
            m_OnLoadDoneCallback = callback;

            m_DataHanlderList = new List<ILoadDataFromServer>();

            m_PlayerInfoDataHandler = new PlayerInfoDataHandler();
            m_RoleDataHandler = new RoleDataHandler();

            m_DataHanlderList.Add(m_PlayerInfoDataHandler);
            m_DataHanlderList.Add(m_RoleDataHandler);

            m_PlayerInfoDataHandler.LoadData(OnLoadDone);
            m_RoleDataHandler.LoadData(OnLoadDone);

            RegisterEvents();
        }

        public static List<String> GetAllDataPaths()
        {
            List<String> pathList = new List<string>();

            pathList.Add(PlayerInfoDataHandler.GetDataFilePathByType(typeof(PlayerInfoDataHandler)));

            return pathList;
        }

        public void Save()
        {
            m_PlayerInfoDataHandler.Save(null);
        }

        public PlayerInfoData GetPlayerInfoData()
        {
            return PlayerInfoDataHandler.data;
        }

        #endregion

        #region Private
        private void RegisterEvents()
        {

        }

        private void HandleEvent(int eventId, params object[] param)
        {

        }

        private void OnLoadDone()
        {
            m_LoadDoneCount++;

            if (m_LoadDoneCount >= m_DataHanlderList.Count)
            {
                Log.i("Data load done!");

                if (m_OnLoadDoneCallback != null)
                {
                    m_OnLoadDoneCallback.Invoke();
                }
            }
        }
        #endregion

    }
}