using Qarth;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;

namespace GameWish.Game
{
	public class ItemUI : UListItemView
	{
		public TextMeshProUGUI m_ID;
		public TextMeshProUGUI m_Number;


        public void OnInit(int index, ItemBase itemBase)
        {
            transform.name = index.ToString();
            m_ID.text = itemBase.ID.ToString();
            m_Number.text = itemBase.Capacity.ToString();
        }
    }
	
}