using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;


namespace GameWish.Game
{
	public class InternalPurchaseData : IDataClass
	{
        public ConsumptionData consumptionDatas = new ConsumptionData();

        #region IData
        public void SetDefaultValue()
        {
            SetDataDirty();
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

        #region Private
        #endregion
    }
}