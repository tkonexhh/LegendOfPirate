
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;

namespace GameWish.Game
{
    public partial class RewardBoxItem : MonoBehaviour
    {
        private DailyTaskPanel m_Panel;
        private TDDailyTaskReward m_Conf;
        public GameObject rewardShow;
        public void OnInit(DailyTaskPanel panel, TDDailyTaskReward conf)
        {
            m_Panel = panel;
            m_Conf = conf;

            m_TmpActive.text = m_Conf.activity.ToString();
            m_TmpReward.text = m_Conf.reward;
            m_RewardShow.SetActive(false);
            rewardShow = m_RewardShow;
        }

    }
}