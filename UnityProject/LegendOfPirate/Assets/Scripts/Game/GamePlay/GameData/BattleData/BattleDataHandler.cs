using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattleDataHandler : DataHandlerBase<BattleData>, IDataHandler
    {
        private const string DATA_NAME = "BattleData";
        public BattleDataHandler()
        {

        }

        public IDataClass GetDataClass()
        {
            return m_Data;
        }

        public override void LoadDataFromServer(Action callback)
        {
            NetDataMgr.S.LoadNetData(DATA_NAME, ParseJson, callback);
        }

        public override void SaveDataToServer(Action successCallback, Action failCallback)
        {
            base.SaveDataToServer(successCallback, failCallback);

            NetDataMgr.S.SaveNetData(DATA_NAME, m_Data, successCallback, failCallback);
        }
    }
}