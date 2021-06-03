using System;
using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;

namespace GameWish.Game
{
	public partial class RoleDetailsPanel
	{
		public const string Name = "RoleDetailsPanel";
		
		[SerializeField]
		public TMPro.TextMeshProUGUI RoleName;
		[SerializeField]
		public TMPro.TextMeshProUGUI RegionRoleName;
		[SerializeField]
		public RectTransform StartRegion;
		[SerializeField]
		public TMPro.TextMeshProUGUI RoleLevel;
		[SerializeField]
		public UnityEngine.UI.Image ExperienceBar;
		[SerializeField]
		public TMPro.TextMeshProUGUI ExperienceValue;
		[SerializeField]
		public UnityEngine.UI.Image RoleModel;
		[SerializeField]
		public UnityEngine.UI.Button StoryBtn;
		[SerializeField]
		public UnityEngine.UI.Button LeftRoleBtn;
		[SerializeField]
		public UnityEngine.UI.Button RightRoleBtn;
		[SerializeField]
		public RectTransform EquipRegion;
		[SerializeField]
		public TMPro.TextMeshProUGUI SkillTitle;
		[SerializeField]
		public RectTransform SkillRegion;
		[SerializeField]
		public UnityEngine.UI.Image UpgradeMaterials;
		[SerializeField]
		public TMPro.TextMeshProUGUI UpgradeMaterialsValue;
		[SerializeField]
		public UnityEngine.UI.Button UpgradeMaterialsBtn;
		[SerializeField]
		public UnityEngine.UI.ScrollRect RoleSelecScrollView;
		[SerializeField]
		public RectTransform RoleSelectRegion;
	}
}
