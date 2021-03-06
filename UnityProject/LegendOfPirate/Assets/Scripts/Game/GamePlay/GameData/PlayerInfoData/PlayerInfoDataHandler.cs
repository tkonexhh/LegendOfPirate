using Qarth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    public class PlayerInfoDataHandler : DataHandlerBase<PlayerInfoData>, IDataHandler
    {
        private const string DATA_NAME = "PlayerInfoData";
        public PlayerInfoDataHandler()
        {

        }

        public IDataClass GetDataClass()
        {
            return m_Data;
        }

        public override void LoadDataFromServer(Action callback)
        {
            NetDataMgr.S.LoadNetData(DATA_NAME, ParseJson, callback, () => { Log.i("存档读取成功!!!"); }, () => { Log.e("存档读取失败,加载网络重连面板!!!"); });
        }

        public override void SaveDataToServer(Action successCallback, Action failCallback)
        {
            base.SaveDataToServer(successCallback, failCallback);

            NetDataMgr.S.SaveNetData(DATA_NAME, m_Data, successCallback, failCallback);
        }

    }
}