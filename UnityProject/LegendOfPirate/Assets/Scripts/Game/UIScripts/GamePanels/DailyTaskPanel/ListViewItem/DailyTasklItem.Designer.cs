using System;
using GFrame.Editor;
using Qarth.Extension;
using UnityEngine;
using UnityEngine.UI;
using Qarth;
using TMPro;

namespace GameWish.Game
{
	public partial class DailyTasklItem
	{
		[SerializeField] private Image m_ActiveIcon;
		[SerializeField] private TextMeshProUGUI m_TmpActiveCount;
		[SerializeField] private TextMeshProUGUI m_TmpTitle;
		[SerializeField] private Image m_ItemBg;
		[SerializeField] private Image m_Slider;
		[SerializeField] private TextMeshProUGUI m_TmpProgress;
		[SerializeField] private TextMeshProUGUI m_TmpLimit;
		[SerializeField] private Button m_BtnGet;
		[SerializeField] private TextMeshProUGUI m_TmpState;

	}
}
