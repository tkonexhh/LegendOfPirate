using Qarth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public class BottomTrainingRole : UListItemView
	{
		private int m_Index = -1;
		public void OnInit(int index)
		{
			m_Index = index;
		}
	}
	
}