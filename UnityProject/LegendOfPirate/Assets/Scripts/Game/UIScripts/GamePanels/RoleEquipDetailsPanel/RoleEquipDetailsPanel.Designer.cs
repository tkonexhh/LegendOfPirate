using System;
using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;

namespace GameWish.Game
{
	public partial class RoleEquipDetailsPanel
	{
		public const string Name = "RoleEquipDetailsPanel";
		
		[SerializeField]
		public UnityEngine.UI.Image Bg;
		[SerializeField]
		public TMPro.TextMeshProUGUI EquipName;
		[SerializeField]
		public RectTransform UpperRegion;
		[SerializeField]
		public UnityEngine.UI.Image EquipIcon;
		[SerializeField]
		public TMPro.TextMeshProUGUI EquipType;
		[SerializeField]
		public TMPro.TextMeshProUGUI EquipAttribute;
		[SerializeField]
		public TMPro.TextMeshProUGUI EquipNumber;
		[SerializeField]
		public UnityEngine.UI.Button EquipBtn;
		
		
	}
}
