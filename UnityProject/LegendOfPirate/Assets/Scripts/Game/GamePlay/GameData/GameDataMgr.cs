using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using System;

namespace GameWish.Game
{
    /// <summary>
    /// GameData对外交互类
    /// </summary>
    public class GameDataMgr : TSingleton<GameDataMgr>,IResetHandler
    {
        private GameDataHandler m_GameDataHandler = null;

        public GameDataHandler GameDataHandler {
            get {
                return m_GameDataHandler;
            }
        }

        public void Init()
        {
            m_GameDataHandler = new GameDataHandler();

            RegisterEvents();

            m_GameDataHandler.GetPlayerInfodata().Init();
        }

        private void RegisterEvents()
        {
            //EventSystem.S.Register(EventID.OnLevelCompleted, HandleEvent);
            EventSystem.S.Register(EventID.OnAddCoinNum, HandleEvent);
        }

        private void HandleEvent(int eventId, params object[] param)
        {
            //if (eventId == (int)EventID.OnLevelCompleted)
            //{
            //    int levelIndex = (int)param[0];
            //    int starNum = (int)param[1];

            //    m_GameDataHandler.GetPlayerInfodata().OnLevelCompleted(levelIndex, starNum);
            //}
            //if (eventId == (int)EventID.OnAddCoinNum)
            //{
            //    int delta = (int)param[0];
            //    m_GameDataHandler.GetPlayerInfodata().AddCoinNum(delta);
            //}
        }

        public void Save()
        {
            m_GameDataHandler.Save();
        }

        /// <summary>
        /// 获取所有游戏数据
        /// </summary>
        /// <returns></returns>
        public GameData GetGameData()
        {
            return m_GameDataHandler.GetGameData();
        }

        public PlayerInfoData GetPlayerInfoData()
        {
            return m_GameDataHandler.GetPlayerInfodata();
        }

        //public ShopData GetShopData()
        //{
        //    return m_GameDataHandler.GetShopData();
        //}
        
        //public MainTaskData GetMainTaskData()
        //{
        //    return m_GameDataHandler.GetMainTaskData();
        //}

        public void OnReset()
        {
            GetPlayerInfoData().OnReset();
        }
    }
}