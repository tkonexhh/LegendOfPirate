using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
	public class FishingPlatform : ShipUnit
	{
        public override void OnInit()
        {
            base.OnInit();
        }

        protected override void OnClicked()
        {
            base.OnClicked();

            UIMgr.S.OpenPanel(UIID.FishingPlatformPanel);
        }

        public override ShipUnitType GetShipUnitType()
        {
            return ShipUnitType.FishingPlatform;
        }
    }
	
}