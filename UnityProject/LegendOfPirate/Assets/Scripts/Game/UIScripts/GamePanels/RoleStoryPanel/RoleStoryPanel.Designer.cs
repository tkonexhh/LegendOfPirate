using System;
using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;

namespace GameWish.Game
{
	public partial class RoleStoryPanel
	{
		public const string Name = "RoleStoryPanel";
		
		[SerializeField]
		public UnityEngine.UI.Image Bg;
		[SerializeField]
		public TMPro.TextMeshProUGUI RoleStoryTitle;
		[SerializeField]
		public RectTransform UpperRegion;
		[SerializeField]
		public TMPro.TextMeshProUGUI RoleStoryCont;
		[SerializeField]
		public UnityEngine.UI.Image RoleStoryIcon;
		
		
	}
}
