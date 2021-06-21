using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
	public class ShipView : ClickableView
	{
        private ShipBody m_ShipBody;

        public ShipBody ShipBody { get => m_ShipBody;}

        protected override void Awake()
        {
            base.Awake();

            //m_Collider = GetComponentInChildren<Collider>();
        }

        public override int GetSortingLayer()
        {
            return Define.INPUT_SORTING_ORDER_SHIP_UNIT;
        }

        protected override void OnClicked()
        {
            base.OnClicked();

            Log.i("On ShipView Clicked");
        }

        public void Init()
        {
            m_ShipBody = GetComponentInChildren<ShipBody>();
        }
    }
	
}