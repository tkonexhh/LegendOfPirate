//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public static partial class TDSmuggleTable
    {
        private static TDTableMetaData m_MetaData = new TDTableMetaData(TDSmuggleTable.Parse, "Smuggle");
        public static TDTableMetaData metaData
        {
            get { return m_MetaData; }
        }
        
        private static Dictionary<int, TDSmuggle> m_DataCache = new Dictionary<int, TDSmuggle>();
        private static List<TDSmuggle> m_DataList = new List<TDSmuggle >();
        
        public static void Parse(byte[] fileData)
        {
            m_DataCache.Clear();
            m_DataList.Clear();
            DataStreamReader dataR = new DataStreamReader(fileData);
            int rowCount = dataR.GetRowCount();
            int[] fieldIndex = dataR.GetFieldIndex(TDSmuggle.GetFieldHeadIndex());
    #if (UNITY_STANDALONE_WIN) || UNITY_EDITOR || UNITY_STANDALONE_OSX
            dataR.CheckFieldMatch(TDSmuggle.GetFieldHeadIndex(), "SmuggleTable");
    #endif
            for (int i = 0; i < rowCount; ++i)
            {
                TDSmuggle memberInstance = new TDSmuggle();
                memberInstance.ReadRow(dataR, fieldIndex);
                OnAddRow(memberInstance);
                memberInstance.Reset();
                CompleteRowAdd(memberInstance, rowCount);
            }
            Log.i(string.Format("Parse Success TDSmuggle"));
        }

        private static void OnAddRow(TDSmuggle memberInstance)
        {
            int key = memberInstance.id;
            if (m_DataCache.ContainsKey(key))
            {
                Log.e(string.Format("Invaild,  TDSmuggleTable Id already exists {0}", key));
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

        public static List<TDSmuggle> dataList
        {
            get 
            {
                return m_DataList;
            }    
        }

        public static TDSmuggle GetData(int key)
        {
            if (m_DataCache.ContainsKey(key))
            {
                return m_DataCache[key];
            }
            else
            {
                Log.w(string.Format("Can't find key {0} in TDSmuggle", key));
                return null;
            }
        }
    }
}//namespace LR