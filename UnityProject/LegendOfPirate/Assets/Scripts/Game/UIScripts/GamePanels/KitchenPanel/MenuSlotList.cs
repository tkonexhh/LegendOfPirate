using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public class MenuSlotList : MonoBehaviour
	{
		[SerializeField] private List<MenuSlot> m_MenuSlotList;
		public void SetMenuSlot(int level) 
		{
			for (int i = 0; i < m_MenuSlotList.Count; i++) 
			{
				if (i < level)
				{
					m_MenuSlotList[i].SetSlot(true, TDFacilityKitchenTable.dataList[level - 1].unlockFoodID.ToString());
				}
				else 
				{
					m_MenuSlotList[i].SetSlot(false, string.Format("Kitchen Lv{0}",level) );
				}
			}
		}
	}
	
}