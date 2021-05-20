//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public static partial class TDGuideStepCTable
    {
        private static TDTableMetaData m_MetaData = new TDTableMetaData(TDGuideStepCTable.Parse, "guide_step_c");
        public static TDTableMetaData metaData
        {
            get { return m_MetaData; }
        }
        
        private static Dictionary<int, TDGuideStepC> m_DataCache = new Dictionary<int, TDGuideStepC>();
        private static List<TDGuideStepC> m_DataList = new List<TDGuideStepC >();
        
        public static void Parse(byte[] fileData)
        {
            m_DataCache.Clear();
            m_DataList.Clear();
            DataStreamReader dataR = new DataStreamReader(fileData);
            int rowCount = dataR.GetRowCount();
            int[] fieldIndex = dataR.GetFieldIndex(TDGuideStepC.GetFieldHeadIndex());
    #if (UNITY_STANDALONE_WIN) || UNITY_EDITOR || UNITY_STANDALONE_OSX
            dataR.CheckFieldMatch(TDGuideStepC.GetFieldHeadIndex(), "GuideStepCTable");
    #endif
            for (int i = 0; i < rowCount; ++i)
            {
                TDGuideStepC memberInstance = new TDGuideStepC();
                memberInstance.ReadRow(dataR, fieldIndex);
                OnAddRow(memberInstance);
                memberInstance.Reset();
                CompleteRowAdd(memberInstance);
            }
            Log.i(string.Format("Parse Success TDGuideStepC"));
        }

        private static void OnAddRow(TDGuideStepC memberInstance)
        {
            int key = memberInstance.id;
            if (m_DataCache.ContainsKey(key))
            {
                Log.e(string.Format("Invaild,  TDGuideStepCTable Id already exists {0}", key));
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

        public static List<TDGuideStepC> dataList
        {
            get 
            {
                return m_DataList;
            }    
        }

        public static TDGuideStepC GetData(int key)
        {
            if (m_DataCache.ContainsKey(key))
            {
                return m_DataCache[key];
            }
            else
            {
                Log.w(string.Format("Can't find key {0} in TDGuideStepC", key));
                return null;
            }
        }
    }
}//namespace LR