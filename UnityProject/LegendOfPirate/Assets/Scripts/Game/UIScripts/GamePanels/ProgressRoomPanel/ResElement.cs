using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UniRx;

namespace GameWish.Game
{
	public class ResElement : MonoBehaviour
	{
		[SerializeField] private Image m_ElementImg;
		[SerializeField] private TextMeshProUGUI m_CountText;
		[SerializeField] private int resElementId;
		public void SetInit(ResPair resElement)
		{
			m_CountText.text = string.Format("0/{0}", resElement.resCount);
		}
	}
	
}