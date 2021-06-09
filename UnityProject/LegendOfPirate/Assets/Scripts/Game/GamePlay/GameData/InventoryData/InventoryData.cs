using System;
using Qarth;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{   
    public class InventoryData : IDataClass
    {
        public List<InventoryItemData> itemList = new List<InventoryItemData>();

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