//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public static partial class TDFacilityLaboratoryTable
    {
        private static TDTableMetaData m_MetaData = new TDTableMetaData(TDFacilityLaboratoryTable.Parse, "FacilityLaboratory");
        public static TDTableMetaData metaData
        {
            get { return m_MetaData; }
        }
        
        private static Dictionary<int, TDFacilityLaboratory> m_DataCache = new Dictionary<int, TDFacilityLaboratory>();
        private static List<TDFacilityLaboratory> m_DataList = new List<TDFacilityLaboratory >();
        
        public static void Parse(byte[] fileData)
        {
            m_DataCache.Clear();
            m_DataList.Clear();
            DataStreamReader dataR = new DataStreamReader(fileData);
            int rowCount = dataR.GetRowCount();
            int[] fieldIndex = dataR.GetFieldIndex(TDFacilityLaboratory.GetFieldHeadIndex());
    #if (UNITY_STANDALONE_WIN) || UNITY_EDITOR || UNITY_STANDALONE_OSX
            dataR.CheckFieldMatch(TDFacilityLaboratory.GetFieldHeadIndex(), "FacilityLaboratoryTable");
    #endif
            for (int i = 0; i < rowCount; ++i)
            {
                TDFacilityLaboratory memberInstance = new TDFacilityLaboratory();
                memberInstance.ReadRow(dataR, fieldIndex);
                OnAddRow(memberInstance);
                memberInstance.Reset();
                CompleteRowAdd(memberInstance, rowCount);
            }
            Log.i(string.Format("Parse Success TDFacilityLaboratory"));
        }

        private static void OnAddRow(TDFacilityLaboratory memberInstance)
        {
            int key = memberInstance.level;
            if (m_DataCache.ContainsKey(key))
            {
                Log.e(string.Format("Invaild,  TDFacilityLaboratoryTable Id already exists {0}", key));
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

        public static List<TDFacilityLaboratory> dataList
        {
            get 
            {
                return m_DataList;
            }    
        }

        public static TDFacilityLaboratory GetData(int key)
        {
            if (m_DataCache.ContainsKey(key))
            {
                return m_DataCache[key];
            }
            else
            {
                Log.w(string.Format("Can't find key {0} in TDFacilityLaboratory", key));
                return null;
            }
        }
    }
}//namespace LR