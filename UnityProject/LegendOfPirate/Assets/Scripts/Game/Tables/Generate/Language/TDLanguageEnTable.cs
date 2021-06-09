//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public static partial class TDLanguageEnTable
    {
        private static TDTableMetaData m_MetaData = new TDTableMetaData(TDLanguageEnTable.Parse, "language_en");
        public static TDTableMetaData metaData
        {
            get { return m_MetaData; }
        }
        
        private static Dictionary<string, TDLanguageEn> m_DataCache = new Dictionary<string, TDLanguageEn>();
        private static List<TDLanguageEn> m_DataList = new List<TDLanguageEn >();
        
        public static void Parse(byte[] fileData)
        {
            m_DataCache.Clear();
            m_DataList.Clear();
            DataStreamReader dataR = new DataStreamReader(fileData);
            int rowCount = dataR.GetRowCount();
            int[] fieldIndex = dataR.GetFieldIndex(TDLanguageEn.GetFieldHeadIndex());
    #if (UNITY_STANDALONE_WIN) || UNITY_EDITOR || UNITY_STANDALONE_OSX
            dataR.CheckFieldMatch(TDLanguageEn.GetFieldHeadIndex(), "LanguageEnTable");
    #endif
            for (int i = 0; i < rowCount; ++i)
            {
                TDLanguageEn memberInstance = new TDLanguageEn();
                memberInstance.ReadRow(dataR, fieldIndex);
                OnAddRow(memberInstance);
                memberInstance.Reset();
                CompleteRowAdd(memberInstance);
            }
            Log.i(string.Format("Parse Success TDLanguageEn"));
        }

        private static void OnAddRow(TDLanguageEn memberInstance)
        {
            string key = memberInstance.id;
            if (m_DataCache.ContainsKey(key))
            {
                Log.e(string.Format("Invaild,  TDLanguageEnTable Id already exists {0}", key));
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

        public static List<TDLanguageEn> dataList
        {
            get 
            {
                return m_DataList;
            }    
        }

        public static TDLanguageEn GetData(string key)
        {
            if (m_DataCache.ContainsKey(key))
            {
                return m_DataCache[key];
            }
            else
            {
                Log.w(string.Format("Can't find key {0} in TDLanguageEn", key));
                return null;
            }
        }
    }
}//namespace LR