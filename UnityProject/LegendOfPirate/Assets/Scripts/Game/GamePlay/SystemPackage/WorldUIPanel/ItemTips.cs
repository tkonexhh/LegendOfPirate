using Qarth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameWish.Game
{
	public class ItemTips :  WorldUIBindTransform
	{

		[SerializeField]
		private Text m_ItemName;
		[SerializeField]
		private Text m_Desc;
		[SerializeField]
		private Image m_Icon;


		public void SetText(string name, string desc, Sprite icom)
		{
			m_ItemName.text = name;
			m_Desc.text = desc;
			m_Icon.sprite = icom;
		}
	}
}