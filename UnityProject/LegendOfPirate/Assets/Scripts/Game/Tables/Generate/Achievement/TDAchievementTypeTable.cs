//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public static partial class TDAchievementTypeTable
    {
        private static TDTableMetaData m_MetaData = new TDTableMetaData(TDAchievementTypeTable.Parse, "achievement_type");
        public static TDTableMetaData metaData
        {
            get { return m_MetaData; }
        }
        
        private static Dictionary<int, TDAchievementType> m_DataCache = new Dictionary<int, TDAchievementType>();
        private static List<TDAchievementType> m_DataList = new List<TDAchievementType >();
        
        public static void Parse(byte[] fileData)
        {
            m_DataCache.Clear();
            m_DataList.Clear();
            DataStreamReader dataR = new DataStreamReader(fileData);
            int rowCount = dataR.GetRowCount();
            int[] fieldIndex = dataR.GetFieldIndex(TDAchievementType.GetFieldHeadIndex());
    #if (UNITY_STANDALONE_WIN) || UNITY_EDITOR || UNITY_STANDALONE_OSX
            dataR.CheckFieldMatch(TDAchievementType.GetFieldHeadIndex(), "AchievementTypeTable");
    #endif
            for (int i = 0; i < rowCount; ++i)
            {
                TDAchievementType memberInstance = new TDAchievementType();
                memberInstance.ReadRow(dataR, fieldIndex);
                OnAddRow(memberInstance);
                memberInstance.Reset();
                CompleteRowAdd(memberInstance);
            }
            Log.i(string.Format("Parse Success TDAchievementType"));
        }

        private static void OnAddRow(TDAchievementType memberInstance)
        {
            int key = memberInstance.id;
            if (m_DataCache.ContainsKey(key))
            {
                Log.e(string.Format("Invaild,  TDAchievementTypeTable Id already exists {0}", key));
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

        public static List<TDAchievementType> dataList
        {
            get 
            {
                return m_DataList;
            }    
        }

        public static TDAchievementType GetData(int key)
        {
            if (m_DataCache.ContainsKey(key))
            {
                return m_DataCache[key];
            }
            else
            {
                Log.w(string.Format("Can't find key {0} in TDAchievementType", key));
                return null;
            }
        }
    }
}//namespace LR