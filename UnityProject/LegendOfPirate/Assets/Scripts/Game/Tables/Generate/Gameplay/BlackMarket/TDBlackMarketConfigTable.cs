//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public static partial class TDBlackMarketConfigTable
    {
        private static TDTableMetaData m_MetaData = new TDTableMetaData(TDBlackMarketConfigTable.Parse, "BlackMarketConfig");
        public static TDTableMetaData metaData
        {
            get { return m_MetaData; }
        }
        
        private static Dictionary<int, TDBlackMarketConfig> m_DataCache = new Dictionary<int, TDBlackMarketConfig>();
        private static List<TDBlackMarketConfig> m_DataList = new List<TDBlackMarketConfig >();
        
        public static void Parse(byte[] fileData)
        {
            m_DataCache.Clear();
            m_DataList.Clear();
            DataStreamReader dataR = new DataStreamReader(fileData);
            int rowCount = dataR.GetRowCount();
            int[] fieldIndex = dataR.GetFieldIndex(TDBlackMarketConfig.GetFieldHeadIndex());
    #if (UNITY_STANDALONE_WIN) || UNITY_EDITOR || UNITY_STANDALONE_OSX
            dataR.CheckFieldMatch(TDBlackMarketConfig.GetFieldHeadIndex(), "BlackMarketConfigTable");
    #endif
            for (int i = 0; i < rowCount; ++i)
            {
                TDBlackMarketConfig memberInstance = new TDBlackMarketConfig();
                memberInstance.ReadRow(dataR, fieldIndex);
                OnAddRow(memberInstance);
                memberInstance.Reset();
                CompleteRowAdd(memberInstance, rowCount);
            }
            Log.i(string.Format("Parse Success TDBlackMarketConfig"));
        }

        private static void OnAddRow(TDBlackMarketConfig memberInstance)
        {
            int key = memberInstance.id;
            if (m_DataCache.ContainsKey(key))
            {
                Log.e(string.Format("Invaild,  TDBlackMarketConfigTable Id already exists {0}", key));
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

        public static List<TDBlackMarketConfig> dataList
        {
            get 
            {
                return m_DataList;
            }    
        }

        public static TDBlackMarketConfig GetData(int key)
        {
            if (m_DataCache.ContainsKey(key))
            {
                return m_DataCache[key];
            }
            else
            {
                Log.w(string.Format("Can't find key {0} in TDBlackMarketConfig", key));
                return null;
            }
        }
    }
}//namespace LR