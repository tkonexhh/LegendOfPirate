//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public static partial class TDFacilityWarshipTable
    {
        private static TDTableMetaData m_MetaData = new TDTableMetaData(TDFacilityWarshipTable.Parse, "FacilityWarship");
        public static TDTableMetaData metaData
        {
            get { return m_MetaData; }
        }
        
        private static Dictionary<int, TDFacilityWarship> m_DataCache = new Dictionary<int, TDFacilityWarship>();
        private static List<TDFacilityWarship> m_DataList = new List<TDFacilityWarship >();
        
        public static void Parse(byte[] fileData)
        {
            m_DataCache.Clear();
            m_DataList.Clear();
            DataStreamReader dataR = new DataStreamReader(fileData);
            int rowCount = dataR.GetRowCount();
            int[] fieldIndex = dataR.GetFieldIndex(TDFacilityWarship.GetFieldHeadIndex());
    #if (UNITY_STANDALONE_WIN) || UNITY_EDITOR || UNITY_STANDALONE_OSX
            dataR.CheckFieldMatch(TDFacilityWarship.GetFieldHeadIndex(), "FacilityWarshipTable");
    #endif
            for (int i = 0; i < rowCount; ++i)
            {
                TDFacilityWarship memberInstance = new TDFacilityWarship();
                memberInstance.ReadRow(dataR, fieldIndex);
                OnAddRow(memberInstance);
                memberInstance.Reset();
                CompleteRowAdd(memberInstance, rowCount);
            }
            Log.i(string.Format("Parse Success TDFacilityWarship"));
        }

        private static void OnAddRow(TDFacilityWarship memberInstance)
        {
            int key = memberInstance.warshipId;
            if (m_DataCache.ContainsKey(key))
            {
                Log.e(string.Format("Invaild,  TDFacilityWarshipTable Id already exists {0}", key));
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

        public static List<TDFacilityWarship> dataList
        {
            get 
            {
                return m_DataList;
            }    
        }

        public static TDFacilityWarship GetData(int key)
        {
            if (m_DataCache.ContainsKey(key))
            {
                return m_DataCache[key];
            }
            else
            {
                Log.w(string.Format("Can't find key {0} in TDFacilityWarship", key));
                return null;
            }
        }
    }
}//namespace LR