using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public class Kitchen : ShipUnit
	{
        public override void OnInit()
        {
            base.OnInit();

            m_ShipUnitType = ShipUnitType.Kitchen;
        }

        protected override void OnClicked()
        {
            base.OnClicked();

            //TODO: Open Kitchen Panel
        }
    }
	
}