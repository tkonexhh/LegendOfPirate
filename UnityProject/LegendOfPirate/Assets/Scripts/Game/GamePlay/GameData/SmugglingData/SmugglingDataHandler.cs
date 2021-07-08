using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Qarth;

namespace GameWish.Game
{
	public class SmugglingDataHandler : DataHandlerBase<SmuggleData>, IDataHandler
	{
		private const string DATA_NAME = "SmugglingData";

		public SmugglingDataHandler()
		{
		
		}
		public IDataClass GetDataClass()
        {
			return m_Data;
        }

        public override void LoadDataFromServer(Action callback)
        {
            base.LoadDataFromServer(callback);
        }

        public override void SaveDataToServer(Action successCallback, Action failCallback)
        {
            base.SaveDataToServer(successCallback, failCallback);
        }
    }
	
}