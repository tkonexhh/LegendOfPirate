//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public static partial class TDMainTaskTable
    {
        private static TDTableMetaData m_MetaData = new TDTableMetaData(TDMainTaskTable.Parse, "MainTask");
        public static TDTableMetaData metaData
        {
            get { return m_MetaData; }
        }
        
        private static Dictionary<int, TDMainTask> m_DataCache = new Dictionary<int, TDMainTask>();
        private static List<TDMainTask> m_DataList = new List<TDMainTask >();
        
        public static void Parse(byte[] fileData)
        {
            m_DataCache.Clear();
            m_DataList.Clear();
            DataStreamReader dataR = new DataStreamReader(fileData);
            int rowCount = dataR.GetRowCount();
            int[] fieldIndex = dataR.GetFieldIndex(TDMainTask.GetFieldHeadIndex());
    #if (UNITY_STANDALONE_WIN) || UNITY_EDITOR || UNITY_STANDALONE_OSX
            dataR.CheckFieldMatch(TDMainTask.GetFieldHeadIndex(), "MainTaskTable");
    #endif
            for (int i = 0; i < rowCount; ++i)
            {
                TDMainTask memberInstance = new TDMainTask();
                memberInstance.ReadRow(dataR, fieldIndex);
                OnAddRow(memberInstance);
                memberInstance.Reset();
                CompleteRowAdd(memberInstance, rowCount);
            }
            Log.i(string.Format("Parse Success TDMainTask"));
        }

        private static void OnAddRow(TDMainTask memberInstance)
        {
            int key = memberInstance.taskID;
            if (m_DataCache.ContainsKey(key))
            {
                Log.e(string.Format("Invaild,  TDMainTaskTable Id already exists {0}", key));
            }
            else
            {
                m_DataCache.Add(key, memberInstance);
                m_DataList.Add(memberInstance);
            }
        }    
        
        public static void Reload(byte[] fileData)
        {
            Parse(fileData);
        }

        public static int count
        {
            get 
            {
                return m_DataCache.Count;
            }
        }

        public static List<TDMainTask> dataList
        {
            get 
            {
                return m_DataList;
            }    
        }

        public static TDMainTask GetData(int key)
        {
            if (m_DataCache.ContainsKey(key))
            {
                return m_DataCache[key];
            }
            else
            {
                Log.w(string.Format("Can't find key {0} in TDMainTask", key));
                return null;
            }
        }
    }
}//namespace LR