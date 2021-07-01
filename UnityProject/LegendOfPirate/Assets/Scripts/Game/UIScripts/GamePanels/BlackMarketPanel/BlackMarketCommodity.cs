using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;


namespace GameWish.Game
{
	public class BlackMarketCommodity : UListItemView
	{
		#region  Ù–‘
		[SerializeField]
		private TextMeshProUGUI m_BlackMarketName;	
		[SerializeField]
		private Image m_Icom;	
		
		[SerializeField,Header("State1")]
		private GameObject m_State1;
		[SerializeField, Header("State2")]
		private GameObject m_State2;
		#endregion

		#region ∑Ω∑®
		public void OnInit(BlackMarketCommodityModule blackMarketCommodityModule)
		{
			
		}
		#endregion

		#region Region3
		#endregion

		#region Region4
		#endregion
	}

}