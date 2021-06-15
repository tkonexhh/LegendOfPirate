using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public class ForgeRoom : ShipUnit
	{
        public override void OnInit()
        {
            base.OnInit();
        }

        protected override void OnClicked()
        {
            base.OnClicked();

            Qarth.UIMgr.S.OpenPanel(UIID.ForgePanel);
        }

        public override ShipUnitType GetShipUnitType()
        {
            return ShipUnitType.ForgeRoom;
        }
    }
	
}