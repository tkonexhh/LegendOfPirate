using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public interface IGameStateObserver
    {
        void OnGameInit();

        void OnGameStart(int levelIndex);

        void OnGamePlaying();

        void OnGamePaused();

        void OnGameResumed();

        void OnGameOver(int levelIndex, int star);

        void OnGameRestarted();

        void OnPlayerDead();

        void OnPlayRevived();
    }
}
