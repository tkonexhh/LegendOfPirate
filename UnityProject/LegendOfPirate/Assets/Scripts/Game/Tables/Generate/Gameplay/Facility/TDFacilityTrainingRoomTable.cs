//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public static partial class TDFacilityTrainingRoomTable
    {
        private static TDTableMetaData m_MetaData = new TDTableMetaData(TDFacilityTrainingRoomTable.Parse, "FacilityTrainingRoom");
        public static TDTableMetaData metaData
        {
            get { return m_MetaData; }
        }
        
        private static Dictionary<int, TDFacilityTrainingRoom> m_DataCache = new Dictionary<int, TDFacilityTrainingRoom>();
        private static List<TDFacilityTrainingRoom> m_DataList = new List<TDFacilityTrainingRoom >();
        
        public static void Parse(byte[] fileData)
        {
            m_DataCache.Clear();
            m_DataList.Clear();
            DataStreamReader dataR = new DataStreamReader(fileData);
            int rowCount = dataR.GetRowCount();
            int[] fieldIndex = dataR.GetFieldIndex(TDFacilityTrainingRoom.GetFieldHeadIndex());
    #if (UNITY_STANDALONE_WIN) || UNITY_EDITOR || UNITY_STANDALONE_OSX
            dataR.CheckFieldMatch(TDFacilityTrainingRoom.GetFieldHeadIndex(), "FacilityTrainingRoomTable");
    #endif
            for (int i = 0; i < rowCount; ++i)
            {
                TDFacilityTrainingRoom memberInstance = new TDFacilityTrainingRoom();
                memberInstance.ReadRow(dataR, fieldIndex);
                OnAddRow(memberInstance);
                memberInstance.Reset();
                CompleteRowAdd(memberInstance, rowCount);
            }
            Log.i(string.Format("Parse Success TDFacilityTrainingRoom"));
        }

        private static void OnAddRow(TDFacilityTrainingRoom memberInstance)
        {
            int key = memberInstance.level;
            if (m_DataCache.ContainsKey(key))
            {
                Log.e(string.Format("Invaild,  TDFacilityTrainingRoomTable Id already exists {0}", key));
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

        public static List<TDFacilityTrainingRoom> dataList
        {
            get 
            {
                return m_DataList;
            }    
        }

        public static TDFacilityTrainingRoom GetData(int key)
        {
            if (m_DataCache.ContainsKey(key))
            {
                return m_DataCache[key];
            }
            else
            {
                Log.w(string.Format("Can't find key {0} in TDFacilityTrainingRoom", key));
                return null;
            }
        }
    }
}//namespace LR