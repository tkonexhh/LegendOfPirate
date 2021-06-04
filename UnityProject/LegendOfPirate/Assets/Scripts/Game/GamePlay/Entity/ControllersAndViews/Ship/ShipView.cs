using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
	public class ShipView : ClickableView
	{
        protected override void Awake()
        {
            base.Awake();

            m_Collider = GetComponentInChildren<Collider>();
        }

        protected override void OnClicked()
        {
            base.OnClicked();

            Log.i("On ShipView Clicked");
        }
    }
	
}