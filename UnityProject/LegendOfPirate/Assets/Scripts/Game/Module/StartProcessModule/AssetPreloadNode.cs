using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
	public class AssetPreloadNode : ExecuteNode
	{
        public override void OnBegin()
        {
            base.OnBegin();

            Log.i("------Asset Preload Start");

            AssetPreloaderMgr.S.OnInit();      
        }

        public override void OnExecute()
        {
            isFinish = AssetPreloaderMgr.S.IsPreloaderDone();

            if (isFinish)
            {
                Log.i("------Asset Preload End");
                AssetPreloaderMgr.S.Release();
            }
        }
    }
	
}