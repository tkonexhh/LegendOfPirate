using UnityEngine;
using System.Collections;
using Qarth;
using System;

namespace GameWish.Game
{
    [TMonoSingletonAttribute("[Game]/GameMgr")]
    public class GameMgr : AbstractModuleMgr, ISingleton
    {
        private static GameMgr s_Instance;
        private int m_GameplayInitSchedule = 0;

        public bool showGuide;

        public static GameMgr S
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = MonoSingleton.CreateMonoSingleton<GameMgr>();
                }
                return s_Instance;
            }
        }

        public void InitGameMgr()
        {
            Log.i("Init[GameMgr]");
            
        }

        public void OnSingletonInit()
        {
            
        }

        protected override void OnActorAwake()
        {           
            ShowLogoPanel();
        }

        protected override void OnActorStart()
        {
            StartProcessModule module = AddMonoCom<StartProcessModule>();

            module.SetFinishListener(OnStartProcessFinish);            
        }

        protected void ShowLogoPanel()
        {
            UIDataModule.RegisterStaticPanel();

            Action callback = OnLogoPanelFinish;
            UIMgr.S.OpenTopPanel(UIID.LogoPanel, null, callback);
        }

        protected void OnLogoPanelFinish()
        {
            ++m_GameplayInitSchedule;
            TryStartGameplay();
        }

        protected void OnStartProcessFinish()
        {
            Log.i("Start process finished");

            ++m_GameplayInitSchedule;
            TryStartGameplay();
        }

        protected void TryStartGameplay()
        {            
            if (m_GameplayInitSchedule < 2)
            {
                return;
            }

            Log.i("Start game play");

            AdsMgr.S.PreloadAllAd();
            GameplayMgr.S.InitGameplay();
        }

        public void StartGuide()
        {
            GetCom<GuideModule>().StartGuide();
        }

        private void OnApplicationQuit()
        {
            //databaseModule.SaveLeaveTime("Application quit");           
        }
    }
}
