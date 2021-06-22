//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public static partial class TDDailyTaskTable
    {
        private static TDTableMetaData m_MetaData = new TDTableMetaData(TDDailyTaskTable.Parse, "DailyTask");
        public static TDTableMetaData metaData
        {
            get { return m_MetaData; }
        }
        
        private static Dictionary<int, TDDailyTask> m_DataCache = new Dictionary<int, TDDailyTask>();
        private static List<TDDailyTask> m_DataList = new List<TDDailyTask >();
        
        public static void Parse(byte[] fileData)
        {
            m_DataCache.Clear();
            m_DataList.Clear();
            DataStreamReader dataR = new DataStreamReader(fileData);
            int rowCount = dataR.GetRowCount();
            int[] fieldIndex = dataR.GetFieldIndex(TDDailyTask.GetFieldHeadIndex());
    #if (UNITY_STANDALONE_WIN) || UNITY_EDITOR || UNITY_STANDALONE_OSX
            dataR.CheckFieldMatch(TDDailyTask.GetFieldHeadIndex(), "DailyTaskTable");
    #endif
            for (int i = 0; i < rowCount; ++i)
            {
                TDDailyTask memberInstance = new TDDailyTask();
                memberInstance.ReadRow(dataR, fieldIndex);
                OnAddRow(memberInstance);
                memberInstance.Reset();
                CompleteRowAdd(memberInstance, rowCount);
            }
            Log.i(string.Format("Parse Success TDDailyTask"));
        }

        private static void OnAddRow(TDDailyTask memberInstance)
        {
            int key = memberInstance.taskID;
            if (m_DataCache.ContainsKey(key))
            {
                Log.e(string.Format("Invaild,  TDDailyTaskTable Id already exists {0}", key));
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

        public static List<TDDailyTask> dataList
        {
            get 
            {
                return m_DataList;
            }    
        }

        public static TDDailyTask GetData(int key)
        {
            if (m_DataCache.ContainsKey(key))
            {
                return m_DataCache[key];
            }
            else
            {
                Log.w(string.Format("Can't find key {0} in TDDailyTask", key));
                return null;
            }
        }
    }
}//namespace LR