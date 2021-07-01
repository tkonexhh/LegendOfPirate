﻿//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public static partial class TDDailySelectionConfigTable
    {
        private static TDTableMetaData m_MetaData = new TDTableMetaData(TDDailySelectionConfigTable.Parse, "DailySelectionConfig");
        public static TDTableMetaData metaData
        {
            get { return m_MetaData; }
        }
        
        private static Dictionary<int, TDDailySelectionConfig> m_DataCache = new Dictionary<int, TDDailySelectionConfig>();
        private static List<TDDailySelectionConfig> m_DataList = new List<TDDailySelectionConfig >();
        
        public static void Parse(byte[] fileData)
        {
            m_DataCache.Clear();
            m_DataList.Clear();
            DataStreamReader dataR = new DataStreamReader(fileData);
            int rowCount = dataR.GetRowCount();
            int[] fieldIndex = dataR.GetFieldIndex(TDDailySelectionConfig.GetFieldHeadIndex());
    #if (UNITY_STANDALONE_WIN) || UNITY_EDITOR || UNITY_STANDALONE_OSX
            dataR.CheckFieldMatch(TDDailySelectionConfig.GetFieldHeadIndex(), "DailySelectionConfigTable");
    #endif
            for (int i = 0; i < rowCount; ++i)
            {
                TDDailySelectionConfig memberInstance = new TDDailySelectionConfig();
                memberInstance.ReadRow(dataR, fieldIndex);
                OnAddRow(memberInstance);
                memberInstance.Reset();
                CompleteRowAdd(memberInstance, rowCount);
            }
            Log.i(string.Format("Parse Success TDDailySelectionConfig"));
        }

        private static void OnAddRow(TDDailySelectionConfig memberInstance)
        {
            int key = memberInstance.id;
            if (m_DataCache.ContainsKey(key))
            {
                Log.e(string.Format("Invaild,  TDDailySelectionConfigTable Id already exists {0}", key));
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

        public static List<TDDailySelectionConfig> dataList
        {
            get 
            {
                return m_DataList;
            }    
        }

        public static TDDailySelectionConfig GetData(int key)
        {
            if (m_DataCache.ContainsKey(key))
            {
                return m_DataCache[key];
            }
            else
            {
                Log.w(string.Format("Can't find key {0} in TDDailySelectionConfig", key));
                return null;
            }
        }
    }
}//namespace LR