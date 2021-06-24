using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;


namespace GameWish.Game
{
	public enum FoodQualityType 
	{
		/// <summary>
		/// ��ɫ-��ͨ
		/// </summary>
		Normal,
		/// <summary>
		/// ��ɫ-����
		/// </summary>
		Advanced,
		/// <summary>
		/// ��ɫ-ϡ��
		/// </summary>
		Rare,
		/// <summary>
		/// ��ɫ-ʷʫ
		/// </summary>
		Epic,
		/// <summary>
		/// ��ɫ-��˵
		/// </summary>
		Legendary,
		/// <summary>
		/// ��ɫ-����
		/// </summary>
		Immortal,
	}
}