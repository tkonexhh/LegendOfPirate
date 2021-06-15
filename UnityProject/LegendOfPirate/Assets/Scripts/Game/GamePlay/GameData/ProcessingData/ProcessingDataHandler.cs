using Qarth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    public class ProcessingDataHandler : DataHandlerBase<KitchenData>, IDataHandler
    {
        public ProcessingDataHandler()
        {

        }

        public IDataClass GetDataClass()
        {
            return m_Data;
        }

        public override void LoadDataFromServer(Action callback)
        {
            NetDataMgr.S.LoadNetData("ProcessingData", ParseJson, callback);
        }

        public override void SaveDataToServer(Action callback)
        {
            if (callback != null)
            {
                callback.Invoke();
            }

            NetDataMgr.S.SaveNetData("ProcessingData", m_Data);
        }
    }
}