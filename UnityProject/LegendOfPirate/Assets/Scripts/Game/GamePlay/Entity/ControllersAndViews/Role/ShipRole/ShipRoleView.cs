using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
	public class ShipRoleView : ClickableView
	{
        protected override void Awake()
        {
            base.Awake();

            m_Collider = GetComponentInChildren<Collider>();
        }

        public override int GetSortingLayer()
        {
            return Define.INPUT_SORTING_ORDER_SHIP_ROLE;
        }

        protected override void OnClicked()
        {
            base.OnClicked();

            Log.i("On Role Clicked");
        }
    }
	
}