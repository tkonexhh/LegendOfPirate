using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;


namespace GameWish.Game
{
	public enum EquipAttributeType
	{
		/// <summary>
		/// ¹¥»÷
		/// </summary>
		ATK,
		/// <summary>
		/// ÑªÁ¿
		/// </summary>
		HP,
		/// <summary>
		/// ÉúÃü»Ö¸´
		/// </summary>
		ACRJ,
		/// <summary>
		/// ÉÁ±Ü
		/// </summary>
		Dodge,
		/// <summary>
		/// ±©»÷ÂÊ
		/// </summary>
		CRI,
		/// <summary>
		/// ¹¥ËÙ
		/// </summary>
		ASPD,
		/// <summary>
		/// Á¬»÷
		/// </summary>
		Combos,
		/// <summary>
		/// »¤¼×
		/// </summary>
		Armor,
	}
}