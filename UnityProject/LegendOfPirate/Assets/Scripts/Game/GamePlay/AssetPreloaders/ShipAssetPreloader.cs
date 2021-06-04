using System.Collections;
using System.Collections.Generic;
using Qarth;
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
                Define.SEA,
                Define.SHIP
            };
        }

        protected override void OnResLoadFinish(bool result, IRes res)
        {
            if (result)
            {
                GameObject go = res.asset as GameObject;
                GameObjectPoolMgr.S.AddPool(res.name, go, 1, 1);
            }
            else
            {
                Log.e("Res Load Failed: " + res.name);
            }
        }
    }
	
}