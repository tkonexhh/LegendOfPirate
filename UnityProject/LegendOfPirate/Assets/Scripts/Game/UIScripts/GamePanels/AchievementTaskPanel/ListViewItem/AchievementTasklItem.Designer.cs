using System;
using GFrame.Editor;
using Qarth.Extension;
using UnityEngine;
using UnityEngine.UI;
using Qarth;
using TMPro;

namespace GameWish.Game
{
    public partial class AchievementTasklItem
    {
        [SerializeField] private TextMeshProUGUI m_DescTex;
        [SerializeField] private Image m_ProgressBgImg;
        [SerializeField] private Image m_SliderImg;
        [SerializeField] private TextMeshProUGUI m_SliderTex;
        [SerializeField] private GameObject m_AchievementRewardItem;
        [SerializeField] private TextMeshProUGUI m_RewardCountTex;
        [SerializeField] private Button m_GetBtn;
        [SerializeField] private TextMeshProUGUI m_TmpState;
        [SerializeField] private RectTransform m_AchievementRewardContent;

    }
}
