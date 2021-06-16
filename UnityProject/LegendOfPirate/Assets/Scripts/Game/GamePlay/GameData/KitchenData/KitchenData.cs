using System;
using Qarth;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{   
    public class KitchenData : IDataClass
    {
        public string cookStartTime = string.Empty;

        public override void InitWithEmptyData()
        {

        }

        public override void OnDataLoadFinish()
        {

        }


        public void OnStartCook(DateTime dateTime)
        {
            cookStartTime = dateTime.ToString();
            SetDataDirty();
        }
    }
}