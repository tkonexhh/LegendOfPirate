using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;
using static GameWish.Game.TDPayShopConfigTable;
using System;

namespace GameWish.Game
{
	public class DiamondsRegionModel 
	{
		public PayShopConfig payShopConfig;
		public PurchaseDiamondsItem purchaseDiamondsItem;

		public StringReactiveProperty giftName;
		public FloatReactiveProperty giftPrice;
		public IntReactiveProperty giftNumber;

		public DiamondsRegionModel(PayShopConfig payShopConfig)
		{
			this.payShopConfig = payShopConfig;

			giftName = new StringReactiveProperty(this.payShopConfig.giftName);
			giftPrice = new FloatReactiveProperty(this.payShopConfig.giftPrice);
			giftNumber = new IntReactiveProperty(this.payShopConfig.giftCont.count);
		}

		public void SetModelData(PurchaseDiamondsItem diamondsItem)
        {
			this.purchaseDiamondsItem = diamondsItem;

		}
    }
}