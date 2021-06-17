using System;
using GFrame.Editor;
using Qarth.Extension;
using UnityEngine;
using UnityEngine.UI;
using Qarth;
using TMPro;

namespace GameWish.Game
{
	public partial class LevelItem
	{
		[SerializeField] private RectTransform m_UnLock;
		[SerializeField] private TextMeshProUGUI m_TmpLevel;
		[SerializeField] private RectTransform m_Progress;
		[SerializeField] private Image m_Slider;
		[SerializeField] private Button m_GetRewardBtn;
		[SerializeField] private RectTransform m_Lock;
		[SerializeField] private Button m_LockBtn;

	}
}
