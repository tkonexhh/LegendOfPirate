using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public class Weapon : ItemBase
	{
		/// <summary>
		/// �˺�
		/// </summary>
		public int Damage { get; set; }
		/// <summary>
		/// �ӳ�
		/// </summary>
		public int Addition { get; set; }
		/// <summary>
		/// ������ʽ
		/// </summary>
		public WeaponType WeaponType { get; set; }
	}
	
}