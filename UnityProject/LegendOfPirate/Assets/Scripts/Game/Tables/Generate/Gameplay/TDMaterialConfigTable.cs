//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public static partial class TDMaterialConfigTable
    {
        private static TDTableMetaData m_MetaData = new TDTableMetaData(TDMaterialConfigTable.Parse, "MaterialConfig");
        public static TDTableMetaData metaData
        {
            get { return m_MetaData; }
        }
        
        private static Dictionary<int, TDMaterialConfig> m_DataCache = new Dictionary<int, TDMaterialConfig>();
        private static List<TDMaterialConfig> m_DataList = new List<TDMaterialConfig >();
        
        public static void Parse(byte[] fileData)
        {
            m_DataCache.Clear();
            m_DataList.Clear();
            DataStreamReader dataR = new DataStreamReader(fileData);
            int rowCount = dataR.GetRowCount();
            int[] fieldIndex = dataR.GetFieldIndex(TDMaterialConfig.GetFieldHeadIndex());
    #if (UNITY_STANDALONE_WIN) || UNITY_EDITOR || UNITY_STANDALONE_OSX
            dataR.CheckFieldMatch(TDMaterialConfig.GetFieldHeadIndex(), "MaterialConfigTable");
    #endif
            for (int i = 0; i < rowCount; ++i)
            {
                TDMaterialConfig memberInstance = new TDMaterialConfig();
                memberInstance.ReadRow(dataR, fieldIndex);
                OnAddRow(memberInstance);
                memberInstance.Reset();
                CompleteRowAdd(memberInstance, rowCount);
            }
            Log.i(string.Format("Parse Success TDMaterialConfig"));
        }

        private static void OnAddRow(TDMaterialConfig memberInstance)
        {
            int key = memberInstance.roleId;
            if (m_DataCache.ContainsKey(key))
            {
                Log.e(string.Format("Invaild,  TDMaterialConfigTable Id already exists {0}", key));
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

        public static List<TDMaterialConfig> dataList
        {
            get 
            {
                return m_DataList;
            }    
        }

        public static TDMaterialConfig GetData(int key)
        {
            if (m_DataCache.ContainsKey(key))
            {
                return m_DataCache[key];
            }
            else
            {
                Log.w(string.Format("Can't find key {0} in TDMaterialConfig", key));
                return null;
            }
        }
    }
}//namespace LR