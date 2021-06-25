//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public static partial class TDAchievementTaskTable
    {
        private static TDTableMetaData m_MetaData = new TDTableMetaData(TDAchievementTaskTable.Parse, "AchievementTask");
        public static TDTableMetaData metaData
        {
            get { return m_MetaData; }
        }
        
        private static Dictionary<int, TDAchievementTask> m_DataCache = new Dictionary<int, TDAchievementTask>();
        private static List<TDAchievementTask> m_DataList = new List<TDAchievementTask >();
        
        public static void Parse(byte[] fileData)
        {
            m_DataCache.Clear();
            m_DataList.Clear();
            DataStreamReader dataR = new DataStreamReader(fileData);
            int rowCount = dataR.GetRowCount();
            int[] fieldIndex = dataR.GetFieldIndex(TDAchievementTask.GetFieldHeadIndex());
    #if (UNITY_STANDALONE_WIN) || UNITY_EDITOR || UNITY_STANDALONE_OSX
            dataR.CheckFieldMatch(TDAchievementTask.GetFieldHeadIndex(), "AchievementTaskTable");
    #endif
            for (int i = 0; i < rowCount; ++i)
            {
                TDAchievementTask memberInstance = new TDAchievementTask();
                memberInstance.ReadRow(dataR, fieldIndex);
                OnAddRow(memberInstance);
                memberInstance.Reset();
                CompleteRowAdd(memberInstance, rowCount);
            }
            Log.i(string.Format("Parse Success TDAchievementTask"));
        }

        private static void OnAddRow(TDAchievementTask memberInstance)
        {
            int key = memberInstance.achievementID;
            if (m_DataCache.ContainsKey(key))
            {
                Log.e(string.Format("Invaild,  TDAchievementTaskTable Id already exists {0}", key));
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

        public static List<TDAchievementTask> dataList
        {
            get 
            {
                return m_DataList;
            }    
        }

        public static TDAchievementTask GetData(int key)
        {
            if (m_DataCache.ContainsKey(key))
            {
                return m_DataCache[key];
            }
            else
            {
                Log.w(string.Format("Can't find key {0} in TDAchievementTask", key));
                return null;
            }
        }
    }
}//namespace LR