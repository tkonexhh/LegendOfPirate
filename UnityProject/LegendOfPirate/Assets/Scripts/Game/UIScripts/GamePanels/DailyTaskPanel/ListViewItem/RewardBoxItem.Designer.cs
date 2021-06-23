using System;
using GFrame.Editor;
using Qarth.Extension;
using UnityEngine;
using UnityEngine.UI;
using Qarth;
using TMPro;

namespace GameWish.Game
{
    public partial class RewardBoxItem
    {
        [SerializeField] private TextMeshProUGUI m_TmpActive;
        [SerializeField] private GameObject m_RewardShow;
        [SerializeField] private Image m_RewardIcon;
        [SerializeField] private TextMeshProUGUI m_TmpReward;
        [SerializeField] private Button m_BtnRewardMask;

    }
}
