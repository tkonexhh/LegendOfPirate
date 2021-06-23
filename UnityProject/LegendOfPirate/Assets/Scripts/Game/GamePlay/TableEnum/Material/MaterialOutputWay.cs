using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;


namespace GameWish.Game
{
	public enum  MaterialOutputWay 
	{
		/// <summary>
		/// ��Դ��
		/// </summary>
		ResourceIsland = 1,
		/// <summary>
		/// ս������
		/// </summary>
		BattleReward = 2,
		/// <summary>
		/// ��԰
		/// </summary>
		VegetableGarden = 3,
		/// <summary>
		/// ����̨
		/// </summary>
		DiaoYuTai = 4,
		/// <summary>
		/// �ӹ���
		/// </summary>
		ProcessingRoom = 5,
		/// <summary>
		/// �̳�
		/// </summary>
		ShoppingMall = 6,
		/// <summary>
		/// �
		/// </summary>
		Activity = 7,
	}
}