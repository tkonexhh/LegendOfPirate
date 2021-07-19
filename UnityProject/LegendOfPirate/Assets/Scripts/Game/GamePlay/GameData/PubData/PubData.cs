using Qarth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public class PubData : IDataClass
	{
        #region Data
        public PubDBData pubDBData = new PubDBData();

        public PubDBData PubDBData { get { return pubDBData; } }
        #endregion

        #region IData
        public void SaveManually()
        {
            SetDataDirty();

            GameDataMgr.S.SaveDataToLocal();
        }

        public override void InitWithEmptyData()
        {

        }

        public override void OnDataLoadFinish()
        {

        }
        #endregion

        #region Public
        public void ReduceLuckyDrawNumber(int delta)
        {
            pubDBData.remainingTimes = Mathf.Max(Define.INT_NUMBER_ZERO, pubDBData.remainingTimes - delta);

            SaveManually();
        }

        public void RefreshRemainingTimes()
        {
            pubDBData.remainingTimes = PubModel.DAILY_LIMIT;

            SaveManually();
        }

        public void RefreshLastTime()
        {
            pubDBData.lastRefreshTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 0, 0);

            SaveManually();
        }
        #endregion
    }
}