using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameWish.Game
{
	public class MenuSlot : MonoBehaviour
	{
		[SerializeField] private Image m_LockImg;
		[SerializeField] private TextMeshProUGUI m_CookMsg;
		public void SetSlot(bool unlock, string msg) 
		{
            m_LockImg.gameObject.SetActive(!unlock);
			m_CookMsg.text = msg;
		}
	}
	
}