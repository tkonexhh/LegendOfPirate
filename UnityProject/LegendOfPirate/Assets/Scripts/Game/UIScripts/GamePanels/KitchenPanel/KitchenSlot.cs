using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameWish.Game
{
	public class KitchenSlot : MonoBehaviour
	{
		[SerializeField] private Image m_LockImage;
		public void UnLockKitchenSlot(bool unlock) 
		{
			m_LockImage.gameObject.SetActive(unlock);
		}
		public void UseSlot() 
		{
		
		}
	}
	
}