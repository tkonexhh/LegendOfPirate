using System;
using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;

namespace GameWish.Game
{
	public partial class RoleSkillPanel
	{
		public const string Name = "RoleSkillPanel";
		
		[SerializeField]
		public UnityEngine.UI.Image Bg;
		[SerializeField]
		public TMPro.TextMeshProUGUI RoleSkillTitle;
		[SerializeField]
		public RectTransform UpperRegion;
		[SerializeField]
		public UnityEngine.UI.Image RoleSkillIcon;
		[SerializeField]
		public TMPro.TextMeshProUGUI RoleSkillLevel;
		[SerializeField]
		public TMPro.TextMeshProUGUI RoleSkillExplain;
		[SerializeField]
		public TMPro.TextMeshProUGUI NextLevelTitle;
		[SerializeField]
		public TMPro.TextMeshProUGUI NextLevelExplain;
		[SerializeField]
		public UnityEngine.UI.Image SkillUpgradeMaterialsIcon;
		[SerializeField]
		public TMPro.TextMeshProUGUI SkillUpgradeMaterialsnumber;
		[SerializeField]
		public UnityEngine.UI.Button SkillUogradeBtn;
		[SerializeField]
		public TMPro.TextMeshProUGUI SkillUogradeText;
		
		
	}
}
