using Qarth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    public class InventoryDataHandler : DataHandlerBase<InventoryData>, IDataHandler
    {
        public InventoryDataHandler()
        {

        }

        public IDataClass GetDataClass()
        {
            return m_Data;
        }

       public override void LoadDataFromServer(Action callback)
        {
            NetDataMgr.S.LoadNetData("InventoryData", ParseJson, callback);
        }

        public override void SaveDataToServer(Action callback)
        {
            if (callback != null)
            {
                callback.Invoke();
            }
            NetDataMgr.S.SaveNetData("InventoryData", m_Data);
        }
    }
}