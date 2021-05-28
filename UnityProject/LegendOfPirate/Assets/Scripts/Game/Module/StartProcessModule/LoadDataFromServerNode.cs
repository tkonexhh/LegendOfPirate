using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
	public class LoadDataFromServerNode : ExecuteNode
	{
        public override void OnBegin()
        {
            base.OnBegin();

            Log.i("------start load data from server");

            GameDataMgr.S.Init(()=> 
            {
                isFinish = true;
                Log.i("------Load data from server finished");
            });        
        }
    }
	
}