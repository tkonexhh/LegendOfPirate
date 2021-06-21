
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;

namespace GameWish.Game
{
    public partial class RewardItem : MonoBehaviour
    {
        public void OnUIInit(BattleWinPanel battleWinPanel, string reward)
        {
            m_TmpName.text = reward;
        }


    }
}