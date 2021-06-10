//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public static partial class TDFacilityGardenTable
    {
        private static TDTableMetaData m_MetaData = new TDTableMetaData(TDFacilityGardenTable.Parse, "FacilityGarden");
        public static TDTableMetaData metaData
        {
            get { return m_MetaData; }
        }
        
        private static Dictionary<int, TDFacilityGarden> m_DataCache = new Dictionary<int, TDFacilityGarden>();
        private static List<TDFacilityGarden> m_DataList = new List<TDFacilityGarden >();
        
        public static void Parse(byte[] fileData)
        {
            m_DataCache.Clear();
            m_DataList.Clear();
            DataStreamReader dataR = new DataStreamReader(fileData);
            int rowCount = dataR.GetRowCount();
            int[] fieldIndex = dataR.GetFieldIndex(TDFacilityGarden.GetFieldHeadIndex());
    #if (UNITY_STANDALONE_WIN) || UNITY_EDITOR || UNITY_STANDALONE_OSX
            dataR.CheckFieldMatch(TDFacilityGarden.GetFieldHeadIndex(), "FacilityGardenTable");
    #endif
            for (int i = 0; i < rowCount; ++i)
            {
                TDFacilityGarden memberInstance = new TDFacilityGarden();
                memberInstance.ReadRow(dataR, fieldIndex);
                OnAddRow(memberInstance);
                memberInstance.Reset();
                CompleteRowAdd(memberInstance, rowCount);
            }
            Log.i(string.Format("Parse Success TDFacilityGarden"));
        }

        private static void OnAddRow(TDFacilityGarden memberInstance)
        {
            int key = memberInstance.level;
            if (m_DataCache.ContainsKey(key))
            {
                Log.e(string.Format("Invaild,  TDFacilityGardenTable Id already exists {0}", key));
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

        public static List<TDFacilityGarden> dataList
        {
            get 
            {
                return m_DataList;
            }    
        }

        public static TDFacilityGarden GetData(int key)
        {
            if (m_DataCache.ContainsKey(key))
            {
                return m_DataCache[key];
            }
            else
            {
                Log.w(string.Format("Can't find key {0} in TDFacilityGarden", key));
                return null;
            }
        }
    }
}//namespace LR