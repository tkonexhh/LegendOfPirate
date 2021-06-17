using Qarth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public interface IDataHandler
	{
        IDataClass GetDataClass();
        void SaveDataToServer(Action successCallback, Action failCallback);
        void SaveDataToLocal();
    }
	
}