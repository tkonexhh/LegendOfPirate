using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    [AssetAutoPreload]
	public class ShipAssetPreloader : AssetPreloader
	{
        protected override void SetNeedPreloadAssets()
        {
            m_NeedPreloadAssets = new string[] 
            {
                "CartoonSea",
                "Ship"
            };
        }
    }
	
}