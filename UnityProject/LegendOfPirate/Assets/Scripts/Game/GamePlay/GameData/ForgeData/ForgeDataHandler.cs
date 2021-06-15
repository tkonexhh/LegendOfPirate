using Qarth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    public class ForgeDataHandler : DataHandlerBase<ForgeData>, IDataHandler
    {
        public ForgeDataHandler()
        {

        }

        public IDataClass GetDataClass()
        {
            return m_Data;
        }

        public override void LoadDataFromServer(Action callback)
        {
            NetDataMgr.S.LoadNetData("ForgeData", ParseJson, callback);
        }

        public override void SaveDataToServer(Action callback)
        {
            if (callback != null)
            {
                callback.Invoke();
            }

            NetDataMgr.S.SaveNetData("ForgeData", m_Data);
        }
    }
}