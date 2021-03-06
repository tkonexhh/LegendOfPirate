//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public static partial class TDPayShopConfigTable
    {
        private static TDTableMetaData m_MetaData = new TDTableMetaData(TDPayShopConfigTable.Parse, "PayShopConfig");
        public static TDTableMetaData metaData
        {
            get { return m_MetaData; }
        }
        
        private static Dictionary<int, TDPayShopConfig> m_DataCache = new Dictionary<int, TDPayShopConfig>();
        private static List<TDPayShopConfig> m_DataList = new List<TDPayShopConfig >();
        
        public static void Parse(byte[] fileData)
        {
            m_DataCache.Clear();
            m_DataList.Clear();
            DataStreamReader dataR = new DataStreamReader(fileData);
            int rowCount = dataR.GetRowCount();
            int[] fieldIndex = dataR.GetFieldIndex(TDPayShopConfig.GetFieldHeadIndex());
    #if (UNITY_STANDALONE_WIN) || UNITY_EDITOR || UNITY_STANDALONE_OSX
            dataR.CheckFieldMatch(TDPayShopConfig.GetFieldHeadIndex(), "PayShopConfigTable");
    #endif
            for (int i = 0; i < rowCount; ++i)
            {
                TDPayShopConfig memberInstance = new TDPayShopConfig();
                memberInstance.ReadRow(dataR, fieldIndex);
                OnAddRow(memberInstance);
                memberInstance.Reset();
                CompleteRowAdd(memberInstance, rowCount);
            }
            Log.i(string.Format("Parse Success TDPayShopConfig"));
        }

        private static void OnAddRow(TDPayShopConfig memberInstance)
        {
            int key = memberInstance.id;
            if (m_DataCache.ContainsKey(key))
            {
                Log.e(string.Format("Invaild,  TDPayShopConfigTable Id already exists {0}", key));
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

        public static List<TDPayShopConfig> dataList
        {
            get 
            {
                return m_DataList;
            }    
        }

        public static TDPayShopConfig GetData(int key)
        {
            if (m_DataCache.ContainsKey(key))
            {
                return m_DataCache[key];
            }
            else
            {
                Log.w(string.Format("Can't find key {0} in TDPayShopConfig", key));
                return null;
            }
        }
    }
}//namespace LR