using System;
using Qarth;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{   
    public class PlayerInfoData : IDataClass
    {
        public string coinNumStr;

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
    }
}