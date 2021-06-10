//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public static partial class TDEquipmentSynthesisConfigTable
    {
        private static TDTableMetaData m_MetaData = new TDTableMetaData(TDEquipmentSynthesisConfigTable.Parse, "EquipmentSynthesisConfig");
        public static TDTableMetaData metaData
        {
            get { return m_MetaData; }
        }
        
        private static Dictionary<int, TDEquipmentSynthesisConfig> m_DataCache = new Dictionary<int, TDEquipmentSynthesisConfig>();
        private static List<TDEquipmentSynthesisConfig> m_DataList = new List<TDEquipmentSynthesisConfig >();
        
        public static void Parse(byte[] fileData)
        {
            m_DataCache.Clear();
            m_DataList.Clear();
            DataStreamReader dataR = new DataStreamReader(fileData);
            int rowCount = dataR.GetRowCount();
            int[] fieldIndex = dataR.GetFieldIndex(TDEquipmentSynthesisConfig.GetFieldHeadIndex());
    #if (UNITY_STANDALONE_WIN) || UNITY_EDITOR || UNITY_STANDALONE_OSX
            dataR.CheckFieldMatch(TDEquipmentSynthesisConfig.GetFieldHeadIndex(), "EquipmentSynthesisConfigTable");
    #endif
            for (int i = 0; i < rowCount; ++i)
            {
                TDEquipmentSynthesisConfig memberInstance = new TDEquipmentSynthesisConfig();
                memberInstance.ReadRow(dataR, fieldIndex);
                OnAddRow(memberInstance);
                memberInstance.Reset();
                CompleteRowAdd(memberInstance);
            }
            Log.i(string.Format("Parse Success TDEquipmentSynthesisConfig"));
        }

        private static void OnAddRow(TDEquipmentSynthesisConfig memberInstance)
        {
            int key = memberInstance.id;
            if (m_DataCache.ContainsKey(key))
            {
                Log.e(string.Format("Invaild,  TDEquipmentSynthesisConfigTable Id already exists {0}", key));
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

        public static List<TDEquipmentSynthesisConfig> dataList
        {
            get 
            {
                return m_DataList;
            }    
        }

        public static TDEquipmentSynthesisConfig GetData(int key)
        {
            if (m_DataCache.ContainsKey(key))
            {
                return m_DataCache[key];
            }
            else
            {
                Log.w(string.Format("Can't find key {0} in TDEquipmentSynthesisConfig", key));
                return null;
            }
        }
    }
}//namespace LR