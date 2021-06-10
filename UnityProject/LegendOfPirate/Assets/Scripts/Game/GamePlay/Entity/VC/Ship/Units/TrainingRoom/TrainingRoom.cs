using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public class TrainingRoom : ShipUnit
	{
        public override void OnInit()
        {
            base.OnInit();
        }

        protected override void OnClicked()
        {
            base.OnClicked();

            //TODO: Open Kitchen Panel
        }

        public override ShipUnitType GetShipUnitType()
        {
            return ShipUnitType.TrainingRoom;
        }
    }
	
}