using Qarth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    public class DataHandlerBase<T> : DataClassHandler<T> where T : IDataClass, new()
    {
        protected DataDirtyRecorder m_DataDirtyRecorder = null;

        public DataHandlerBase()
        {
            m_DataDirtyRecorder = new DataDirtyRecorder();

        }

        public void LoadData(Action callback)
        {
            if (GameDataMgr.s_DataMode == DataMode.Server)
            {
                LoadDataFromServer(callback);
            }
            else
            {
                Load();

                if (callback != null)
                {
                    callback.Invoke();
                }
            }
        }

        public void Save(Action callback)
        {
            if (GameDataMgr.s_DataMode == DataMode.Server)
            {
                SaveDataToServer(callback);
            }
            else
            {
                Save(true);

                if (callback != null)
                {
                    callback.Invoke();
                }
            }
        }

        public virtual void LoadDataFromServer(Action callback)
        {
          
        }

        public virtual void SaveDataToServer(Action callback)
        {

        }
    }
}