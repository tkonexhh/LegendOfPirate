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

        #endregion
    }
}