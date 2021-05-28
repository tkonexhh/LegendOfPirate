using System;
using Qarth;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{   
    public class PlayerInfoData : IDataClass
    {
        public int coinNum;


        public void SetDefaultValue()
        {
            SetDataDirty();
        }

        public override void InitWithEmptyData()
        {

        }

        public override void OnDataLoadFinish()
        {

        }

        public void AddCoin(int delta)
        {
            coinNum += delta;
            coinNum = Math.Max(0, coinNum);

            SetDataDirty();
        }
    }
}