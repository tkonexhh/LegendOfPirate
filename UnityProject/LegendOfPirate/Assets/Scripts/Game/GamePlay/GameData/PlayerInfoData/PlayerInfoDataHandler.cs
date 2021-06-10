using Qarth;
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
            if (callback != null)
            {
                callback.Invoke();
            }
            LoadData();
        }

        public override void SaveDataToServer(Action callback)
        {
            if (callback != null)
            {
                callback.Invoke();
            }
            LCDataMgr.S.SaveData("PlayerInfoData", m_Data);
        }

        private async void LoadData()
        {
            await LCDataMgr.S.LoadData("PlayerInfoData");
            if (!string.IsNullOrEmpty(LCDataMgr.S.DealData()))
                ParseJson(LCDataMgr.S.DealData());
        }
    }
}