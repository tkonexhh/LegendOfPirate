using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using UnityEngine.UI;

namespace GameWish.Game
{
	public class ResElementLst : MonoBehaviour
	{
		[SerializeField] private USimpleListView m_ListView;
		[SerializeField] private RectTransform m_Content;
		
		public void SetElement(List<ResPair> resPairs) 
		{
			m_ListView.SetDataCount(resPairs.Count);
			var resElements = m_Content.GetComponentsInChildren<ResElement>();
			for (int i = 0; i < resElements.Length; i++) 
			{
				resElements[i].SetInit(resPairs[i]);
			}
		}
	}
	
}