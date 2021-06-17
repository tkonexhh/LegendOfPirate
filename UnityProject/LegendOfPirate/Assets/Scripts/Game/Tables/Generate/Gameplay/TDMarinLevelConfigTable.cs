//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public static partial class TDMarinLevelConfigTable
    {
        private static TDTableMetaData m_MetaData = new TDTableMetaData(TDMarinLevelConfigTable.Parse, "MarinLevelConfig");
        public static TDTableMetaData metaData
        {
            get { return m_MetaData; }
        }
        
        private static Dictionary<int, TDMarinLevelConfig> m_DataCache = new Dictionary<int, TDMarinLevelConfig>();
        private static List<TDMarinLevelConfig> m_DataList = new List<TDMarinLevelConfig >();
        
        public static void Parse(byte[] fileData)
        {
            m_DataCache.Clear();
            m_DataList.Clear();
            DataStreamReader dataR = new DataStreamReader(fileData);
            int rowCount = dataR.GetRowCount();
            int[] fieldIndex = dataR.GetFieldIndex(TDMarinLevelConfig.GetFieldHeadIndex());
    #if (UNITY_STANDALONE_WIN) || UNITY_EDITOR || UNITY_STANDALONE_OSX
            dataR.CheckFieldMatch(TDMarinLevelConfig.GetFieldHeadIndex(), "MarinLevelConfigTable");
    #endif
            for (int i = 0; i < rowCount; ++i)
            {
                TDMarinLevelConfig memberInstance = new TDMarinLevelConfig();
                memberInstance.ReadRow(dataR, fieldIndex);
                OnAddRow(memberInstance);
                memberInstance.Reset();
                CompleteRowAdd(memberInstance, rowCount);
            }
            Log.i(string.Format("Parse Success TDMarinLevelConfig"));
        }

        private static void OnAddRow(TDMarinLevelConfig memberInstance)
        {
            int key = memberInstance.level;
            if (m_DataCache.ContainsKey(key))
            {
                Log.e(string.Format("Invaild,  TDMarinLevelConfigTable Id already exists {0}", key));
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

        public static List<TDMarinLevelConfig> dataList
        {
            get 
            {
                return m_DataList;
            }    
        }

        public static TDMarinLevelConfig GetData(int key)
        {
            if (m_DataCache.ContainsKey(key))
            {
                return m_DataCache[key];
            }
            else
            {
                Log.w(string.Format("Can't find key {0} in TDMarinLevelConfig", key));
                return null;
            }
        }
    }
}//namespace LR