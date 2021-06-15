using Qarth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    public class TrainingDataHandler : DataHandlerBase<KitchenData>, IDataHandler
    {
        public TrainingDataHandler()
        {

        }

        public IDataClass GetDataClass()
        {
            return m_Data;
        }

        public override void LoadDataFromServer(Action callback)
        {
            NetDataMgr.S.LoadNetData("TrainingData", ParseJson, callback);
        }

        public override void SaveDataToServer(Action callback)
        {
            if (callback != null)
            {
                callback.Invoke();
            }

            NetDataMgr.S.SaveNetData("TrainingData", m_Data);
        }
    }
}