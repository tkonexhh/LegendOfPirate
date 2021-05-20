using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class UIMsgHandler : TSingleton<UIMsgHandler>, IGameStateObserver
    {
        public void Init()
        {
            GameStateMgr.S.AddObserver(this);

            //EventSystem.S.Register(EventID.OnPlayerDead, HandleEvent);
        }

        private void HandleEvent(int eventId, params object[] param)
        {
            //if (eventId == (int)EventID.OnPlayerDead)
            //{
            //    UIMgr.S.OpenPanel(UIID.RevivePanel);
            //}
        }

        #region GameState
        public void OnGameInit()
        {
            
        }

        public void OnGameOver(int levelIndex, int star)
        {

        }

        public void OnGamePaused()
        {
        }

        public void OnGamePlaying()
        {
        }

        public void OnGameRestarted()
        {
            //UIMgr.S.OpenPanel(UIID.MainGamePanel, LevelMgr.S.CurrentLevelIndex);        
        }

        public void OnGameResumed()
        {
        }

        public void OnGameStart(int levelIndex)
        {

        }

        public void OnPlayerDead()
        {

        }

        public void OnPlayRevived()
        {

        }
        #endregion
    }
}