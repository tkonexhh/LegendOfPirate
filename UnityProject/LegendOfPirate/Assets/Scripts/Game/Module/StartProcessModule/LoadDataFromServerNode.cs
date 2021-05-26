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

            //if (GameDataMgr.s_DataMode == DataMode.Local)
            {
                GameDataMgr.S.Init();

                isFinish = true;
            }
        }
    }
	
}