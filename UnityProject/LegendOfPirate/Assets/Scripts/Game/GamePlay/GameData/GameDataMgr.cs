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

        private PlayerInfoDataHandler m_PlayerInfoDataHandler = null;

        #region Public

        public void Init()
        {
            m_PlayerInfoDataHandler = new PlayerInfoDataHandler();
            m_PlayerInfoDataHandler.LoadData();

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
            m_PlayerInfoDataHandler.Save();
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

        #endregion

    }
}