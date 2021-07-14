//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public static partial class TDPubTable
    {
        private static TDTableMetaData m_MetaData = new TDTableMetaData(TDPubTable.Parse, "Pub");
        public static TDTableMetaData metaData
        {
            get { return m_MetaData; }
        }
        
        private static Dictionary<int, TDPub> m_DataCache = new Dictionary<int, TDPub>();
        private static List<TDPub> m_DataList = new List<TDPub >();
        
        public static void Parse(byte[] fileData)
        {
            m_DataCache.Clear();
            m_DataList.Clear();
            DataStreamReader dataR = new DataStreamReader(fileData);
            int rowCount = dataR.GetRowCount();
            int[] fieldIndex = dataR.GetFieldIndex(TDPub.GetFieldHeadIndex());
    #if (UNITY_STANDALONE_WIN) || UNITY_EDITOR || UNITY_STANDALONE_OSX
            dataR.CheckFieldMatch(TDPub.GetFieldHeadIndex(), "PubTable");
    #endif
            for (int i = 0; i < rowCount; ++i)
            {
                TDPub memberInstance = new TDPub();
                memberInstance.ReadRow(dataR, fieldIndex);
                OnAddRow(memberInstance);
                memberInstance.Reset();
                CompleteRowAdd(memberInstance, rowCount);
            }
            Log.i(string.Format("Parse Success TDPub"));
        }

        private static void OnAddRow(TDPub memberInstance)
        {
            int key = memberInstance.iD;
            if (m_DataCache.ContainsKey(key))
            {
                Log.e(string.Format("Invaild,  TDPubTable Id already exists {0}", key));
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

        public static List<TDPub> dataList
        {
            get 
            {
                return m_DataList;
            }    
        }

        public static TDPub GetData(int key)
        {
            if (m_DataCache.ContainsKey(key))
            {
                return m_DataCache[key];
            }
            else
            {
                Log.w(string.Format("Can't find key {0} in TDPub", key));
                return null;
            }
        }
    }
}//namespace LR