//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public static partial class TDPurchaseContentTable
    {
        private static TDTableMetaData m_MetaData = new TDTableMetaData(TDPurchaseContentTable.Parse, "PurchaseContent");
        public static TDTableMetaData metaData
        {
            get { return m_MetaData; }
        }
        
        private static Dictionary<string, TDPurchaseContent> m_DataCache = new Dictionary<string, TDPurchaseContent>();
        private static List<TDPurchaseContent> m_DataList = new List<TDPurchaseContent >();
        
        public static void Parse(byte[] fileData)
        {
            m_DataCache.Clear();
            m_DataList.Clear();
            DataStreamReader dataR = new DataStreamReader(fileData);
            int rowCount = dataR.GetRowCount();
            int[] fieldIndex = dataR.GetFieldIndex(TDPurchaseContent.GetFieldHeadIndex());
    #if (UNITY_STANDALONE_WIN) || UNITY_EDITOR || UNITY_STANDALONE_OSX
            dataR.CheckFieldMatch(TDPurchaseContent.GetFieldHeadIndex(), "PurchaseContentTable");
    #endif
            for (int i = 0; i < rowCount; ++i)
            {
                TDPurchaseContent memberInstance = new TDPurchaseContent();
                memberInstance.ReadRow(dataR, fieldIndex);
                OnAddRow(memberInstance);
                memberInstance.Reset();
                CompleteRowAdd(memberInstance);
            }
            Log.i(string.Format("Parse Success TDPurchaseContent"));
        }

        private static void OnAddRow(TDPurchaseContent memberInstance)
        {
            string key = memberInstance.id;
            if (m_DataCache.ContainsKey(key))
            {
                Log.e(string.Format("Invaild,  TDPurchaseContentTable Id already exists {0}", key));
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

        public static List<TDPurchaseContent> dataList
        {
            get 
            {
                return m_DataList;
            }    
        }

        public static TDPurchaseContent GetData(string key)
        {
            if (m_DataCache.ContainsKey(key))
            {
                return m_DataCache[key];
            }
            else
            {
                Log.w(string.Format("Can't find key {0} in TDPurchaseContent", key));
                return null;
            }
        }
    }
}//namespace LR