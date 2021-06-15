﻿using Qarth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    public class PlayerInfoDataHandler : DataHandlerBase<PlayerInfoData>, IDataHandler
    {
        public PlayerInfoDataHandler()
        {

        }

        public IDataClass GetDataClass()
        {
            return m_Data;
        }

        public override void LoadDataFromServer(Action callback)
        {
            NetDataMgr.S.LoadNetData("PlayerInfoData", ParseJson, callback);
        }

        public override void SaveDataToServer(Action callback)
        {
            if (callback != null)
            {
                callback.Invoke();
            }
            NetDataMgr.S.SaveNetData("PlayerInfoData", m_Data);
        }

    }
}