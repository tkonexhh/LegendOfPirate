//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public static partial class TDFoodSynthesisConfigTable
    {
        private static TDTableMetaData m_MetaData = new TDTableMetaData(TDFoodSynthesisConfigTable.Parse, "FoodSynthesisConfig");
        public static TDTableMetaData metaData
        {
            get { return m_MetaData; }
        }
        
        private static Dictionary<int, TDFoodSynthesisConfig> m_DataCache = new Dictionary<int, TDFoodSynthesisConfig>();
        private static List<TDFoodSynthesisConfig> m_DataList = new List<TDFoodSynthesisConfig >();
        
        public static void Parse(byte[] fileData)
        {
            m_DataCache.Clear();
            m_DataList.Clear();
            DataStreamReader dataR = new DataStreamReader(fileData);
            int rowCount = dataR.GetRowCount();
            int[] fieldIndex = dataR.GetFieldIndex(TDFoodSynthesisConfig.GetFieldHeadIndex());
    #if (UNITY_STANDALONE_WIN) || UNITY_EDITOR || UNITY_STANDALONE_OSX
            dataR.CheckFieldMatch(TDFoodSynthesisConfig.GetFieldHeadIndex(), "FoodSynthesisConfigTable");
    #endif
            for (int i = 0; i < rowCount; ++i)
            {
                TDFoodSynthesisConfig memberInstance = new TDFoodSynthesisConfig();
                memberInstance.ReadRow(dataR, fieldIndex);
                OnAddRow(memberInstance);
                memberInstance.Reset();
                CompleteRowAdd(memberInstance);
            }
            Log.i(string.Format("Parse Success TDFoodSynthesisConfig"));
        }

        private static void OnAddRow(TDFoodSynthesisConfig memberInstance)
        {
            int key = memberInstance.id;
            if (m_DataCache.ContainsKey(key))
            {
                Log.e(string.Format("Invaild,  TDFoodSynthesisConfigTable Id already exists {0}", key));
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

        public static List<TDFoodSynthesisConfig> dataList
        {
            get 
            {
                return m_DataList;
            }    
        }

        public static TDFoodSynthesisConfig GetData(int key)
        {
            if (m_DataCache.ContainsKey(key))
            {
                return m_DataCache[key];
            }
            else
            {
                Log.w(string.Format("Can't find key {0} in TDFoodSynthesisConfig", key));
                return null;
            }
        }
    }
}//namespace LR