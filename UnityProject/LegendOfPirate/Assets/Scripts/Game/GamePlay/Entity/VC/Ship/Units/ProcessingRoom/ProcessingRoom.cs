using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public class ProcessingRoom : ShipUnit
	{
        public override void OnInit()
        {
            base.OnInit();
        }

        protected override void OnClicked()
        {
            base.OnClicked();

            Qarth.UIMgr.S.OpenPanel(UIID.ProgressingRoomPanel);
        }

        public override ShipUnitType GetShipUnitType()
        {
            return ShipUnitType.ProcessingRoom;
        }
    }
	
}