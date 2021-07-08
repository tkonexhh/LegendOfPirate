using System;
using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using TMPro;

namespace GameWish.Game
{
	// Auto Generated Code. Don't Modify This Class 
	public partial class WarshipUpgradePanel
	{
		public const string Name = "WarshipUpgradePanel";
		
		[SerializeField] private Button m_BgBtn;
		[SerializeField] private Image m_TitleBg;
		[SerializeField] private Image m_TitleIcon;
		[SerializeField] private TextMeshProUGUI m_TitileName;
		[SerializeField] private Image m_LockBg;
		[SerializeField] private Image m_lockIcon;
		[SerializeField] private Button m_RightArrowBtn;
		[SerializeField] private Button m_LeftArrowBtn;
		[SerializeField] private Image m_WarShipIcon;
		[SerializeField] private Image m_CEBg;
		[SerializeField] private TextMeshProUGUI m_CEValue;
		[SerializeField] private Image m_LevelBg;
		[SerializeField] private TextMeshProUGUI m_CurLevel;
		[SerializeField] private Image m_LevelArrow;
		[SerializeField] private TextMeshProUGUI m_NextLevel;
		[SerializeField] private Image m_AggressivityIcon;
		[SerializeField] private Image m_AggressivityBarBg;
		[SerializeField] private Image m_AggressivityBar;
		[SerializeField] private TextMeshProUGUI m_CombatPowerGrowth;
		[SerializeField] private Image m_DefensivePowerIcon;
		[SerializeField] private Image m_DefensivePowerBarBg;
		[SerializeField] private Image m_DefensivePowerBar;
		[SerializeField] private TextMeshProUGUI m_DefensivePowerGrowth;
		[SerializeField] private Image m_UnlockTMPBg;
		[SerializeField] private TextMeshProUGUI m_UnlockValue;
		[SerializeField] private Image m_UpgradeRegionBg;
		[SerializeField] private Button m_UpgradeBtn;
		[SerializeField] private IUListView m_ScrollView;
		[SerializeField] private Button m_LeftDownExitBtn;
			
	}
}
