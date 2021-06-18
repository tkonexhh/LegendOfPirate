using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class GameCameraMgr : TMonoSingleton<GameCameraMgr>
    {
        [SerializeField] private Camera m_SeaCamera;
        [SerializeField] private Camera m_BattleCamera;

        public Camera CurrentCamera { get; private set; }

        public void ToSea()
        {
            m_SeaCamera.gameObject.SetActive(true);
            m_BattleCamera.gameObject.SetActive(false);
            CurrentCamera = m_SeaCamera;
        }

        public void ToBattle()
        {
            m_SeaCamera.gameObject.SetActive(false);
            m_BattleCamera.gameObject.SetActive(true);
            CurrentCamera = m_BattleCamera;
        }
    }

}