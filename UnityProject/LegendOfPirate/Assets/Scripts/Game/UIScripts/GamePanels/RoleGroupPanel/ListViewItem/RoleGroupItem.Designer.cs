using System;
using GFrame.Editor;
using Qarth.Extension;
using UnityEngine;
using UnityEngine.UI;
using Qarth;

namespace GameWish.Game
{
	public partial class RoleGroupItem
	{
		[SerializeField] public GFrame.Editor.GButton ItemBgBtn;
		[SerializeField] public TMPro.TextMeshProUGUI NameTex;
		[SerializeField] public UnityEngine.UI.Image AvatarImg;
		[SerializeField] public RectTransform StarGroup;
		[SerializeField] public TMPro.TextMeshProUGUI ItemLevelTex;

		public void Clear()
		{
			ItemBgBtn = null;
			NameTex = null;
			AvatarImg = null;
			StarGroup = null;
			ItemLevelTex = null;
		}

	}
}
