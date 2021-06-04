using System;
using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;

namespace GameWish.Game
{
	public partial class EvolutionSolePanel
	{
		public const string Name = "EvolutionSolePanel";
		
		[SerializeField]
		public UnityEngine.UI.Image Bg;
		[SerializeField]
		public RectTransform UpperRegion;
		[SerializeField]
		public TMPro.TextMeshProUGUI Title;
		[SerializeField]
		public UnityEngine.UI.Image SoulFragmentIcon;
		[SerializeField]
		public TMPro.TextMeshProUGUI SoulFragmentNumber;
		[SerializeField]
		public RectTransform SoleAccessTra;
		
		
	}
}
