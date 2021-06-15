using Qarth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    public class LibraryDataHandler : DataHandlerBase<KitchenData>, IDataHandler
    {
        public LibraryDataHandler()
        {

        }

        public IDataClass GetDataClass()
        {
            return m_Data;
        }

        public override void LoadDataFromServer(Action callback)
        {
            NetDataMgr.S.LoadNetData("LibraryData", ParseJson, callback);
        }

        public override void SaveDataToServer(Action callback)
        {
            if (callback != null)
            {
                callback.Invoke();
            }

            NetDataMgr.S.SaveNetData("LibraryData", m_Data);
        }
    }
}