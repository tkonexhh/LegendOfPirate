using Qarth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    public class RoleGroupDataHandler : DataHandlerBase<RoleGroupData>, IDataHandler
    {
        public RoleGroupDataHandler()
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
        }

        public override void SaveDataToServer(Action callback)
        {
            if (callback != null)
            {
                callback.Invoke();
            }
        }
    }
}