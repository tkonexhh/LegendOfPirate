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

        public static string s_path { get { return dataFilePath; } }

        public DataHandlerBase()
        {
            m_DataDirtyRecorder = new DataDirtyRecorder();

        }

        public virtual void LoadData()
        {
            Load();

            // TODO: Load data from server
        }

        public virtual void Save()
        {
            Save(true);
        }
    }
}