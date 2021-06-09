//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public static partial class TDEquipmentConfigTable
    {
        private static TDTableMetaData m_MetaData = new TDTableMetaData(TDEquipmentConfigTable.Parse, "EquipmentConfig");
        public static TDTableMetaData metaData
        {
            get { return m_MetaData; }
        }
        
        private static Dictionary<int, TDEquipmentConfig> m_DataCache = new Dictionary<int, TDEquipmentConfig>();
        private static List<TDEquipmentConfig> m_DataList = new List<TDEquipmentConfig >();
        
        public static void Parse(byte[] fileData)
        {
            m_DataCache.Clear();
            m_DataList.Clear();
            DataStreamReader dataR = new DataStreamReader(fileData);
            int rowCount = dataR.GetRowCount();
            int[] fieldIndex = dataR.GetFieldIndex(TDEquipmentConfig.GetFieldHeadIndex());
    #if (UNITY_STANDALONE_WIN) || UNITY_EDITOR || UNITY_STANDALONE_OSX
            dataR.CheckFieldMatch(TDEquipmentConfig.GetFieldHeadIndex(), "EquipmentConfigTable");
    #endif
            for (int i = 0; i < rowCount; ++i)
            {
                TDEquipmentConfig memberInstance = new TDEquipmentConfig();
                memberInstance.ReadRow(dataR, fieldIndex);
                OnAddRow(memberInstance);
                memberInstance.Reset();
                CompleteRowAdd(memberInstance);
            }
            Log.i(string.Format("Parse Success TDEquipmentConfig"));
        }

        private static void OnAddRow(TDEquipmentConfig memberInstance)
        {
            int key = memberInstance.equipmentId;
            if (m_DataCache.ContainsKey(key))
            {
                Log.e(string.Format("Invaild,  TDEquipmentConfigTable Id already exists {0}", key));
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

        public static List<TDEquipmentConfig> dataList
        {
            get 
            {
                return m_DataList;
            }    
        }

        public static TDEquipmentConfig GetData(int key)
        {
            if (m_DataCache.ContainsKey(key))
            {
                return m_DataCache[key];
            }
            else
            {
                Log.w(string.Format("Can't find key {0} in TDEquipmentConfig", key));
                return null;
            }
        }
    }
}//namespace LR