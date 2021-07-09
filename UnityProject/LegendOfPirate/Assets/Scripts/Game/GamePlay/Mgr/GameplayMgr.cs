using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using System;
//using BitBenderGames;
using UnityEngine.SceneManagement;
using Int64 = System.Int64;
using Random = UnityEngine.Random;

namespace GameWish.Game
{
    public partial class GameplayMgr : TMonoSingleton<GameplayMgr>
    {
        [SerializeField] private Transform m_EntityRoot;

        public Transform EntityRoot { get => m_EntityRoot; set => m_EntityRoot = value; }

        private bool m_IsGameStart = false;

        public void InitGameplay()
        {
            Init();
        }

        private void Init()
        {
            AudioMgr.S.OnSingletonInit();
            Application.runInBackground = true;

            EventSystem.S.Register(EngineEventID.OnApplicationQuit, ApplicationQuit);
            EventSystem.S.Register(EngineEventID.OnApplicationPauseChange, OnGamePauseChange);
            EventSystem.S.Register(EngineEventID.OnApplicationFocusChange, OnGameFocusChange);

            //Set language
            I18Mgr.S.SwitchLanguage(SystemLanguage.English);

            GameMgr.S.StartGuide();

            RemoteConfigMgr.S.StartChecker(null);


            UIMgr.S.ClosePanelAsUIID(UIID.LogoPanel);
            UIMgr.S.OpenPanel(UIID.LoginPanel);
            //UIMgr.S.OpenPanel(UIID.MainMenuPanel);

            //MusicMgr.S.PlayBgMusic();

            DebuggerMgr.S.OnInit();

            GameLogicMgr.S.OnInit();

            m_IsGameStart = true;
        }

        private void OnGamePauseChange(int key, params object[] args)
        {
            bool pause = (bool)args[0];
            if (!pause)
            {
            }
            else
            {
            }
        }

        private void OnGameFocusChange(int key, params object[] args)
        {
            bool focusState = (bool)args[0];
            if (focusState)
            {
                return;
            }

            GameDataMgr.S.SaveDataToLocal();
        }

        private void ApplicationQuit(int key, params object[] args)
        {

        }

        private void Update()
        {
            if (m_IsGameStart == false)
                return;

            GameLogicMgr.S.OnUpdate();
        }

        private void OnGUI()
        {
            DebuggerMgr.S.OnGUI();
        }

        private void OnDestroy()
        {
            GameLogicMgr.S.OnDestroyed();
        }
    }
}