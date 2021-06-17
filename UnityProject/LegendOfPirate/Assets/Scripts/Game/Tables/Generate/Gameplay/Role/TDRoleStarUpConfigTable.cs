//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public static partial class TDRoleStarUpConfigTable
    {
        private static TDTableMetaData m_MetaData = new TDTableMetaData(TDRoleStarUpConfigTable.Parse, "RoleStarUpConfig");
        public static TDTableMetaData metaData
        {
            get { return m_MetaData; }
        }
        
        private static Dictionary<int, TDRoleStarUpConfig> m_DataCache = new Dictionary<int, TDRoleStarUpConfig>();
        private static List<TDRoleStarUpConfig> m_DataList = new List<TDRoleStarUpConfig >();
        
        public static void Parse(byte[] fileData)
        {
            m_DataCache.Clear();
            m_DataList.Clear();
            DataStreamReader dataR = new DataStreamReader(fileData);
            int rowCount = dataR.GetRowCount();
            int[] fieldIndex = dataR.GetFieldIndex(TDRoleStarUpConfig.GetFieldHeadIndex());
    #if (UNITY_STANDALONE_WIN) || UNITY_EDITOR || UNITY_STANDALONE_OSX
            dataR.CheckFieldMatch(TDRoleStarUpConfig.GetFieldHeadIndex(), "RoleStarUpConfigTable");
    #endif
            for (int i = 0; i < rowCount; ++i)
            {
                TDRoleStarUpConfig memberInstance = new TDRoleStarUpConfig();
                memberInstance.ReadRow(dataR, fieldIndex);
                OnAddRow(memberInstance);
                memberInstance.Reset();
                CompleteRowAdd(memberInstance, rowCount);
            }
            Log.i(string.Format("Parse Success TDRoleStarUpConfig"));
        }

        private static void OnAddRow(TDRoleStarUpConfig memberInstance)
        {
            int key = memberInstance.starLevel;
            if (m_DataCache.ContainsKey(key))
            {
                Log.e(string.Format("Invaild,  TDRoleStarUpConfigTable Id already exists {0}", key));
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

        public static List<TDRoleStarUpConfig> dataList
        {
            get 
            {
                return m_DataList;
            }    
        }

        public static TDRoleStarUpConfig GetData(int key)
        {
            if (m_DataCache.ContainsKey(key))
            {
                return m_DataCache[key];
            }
            else
            {
                Log.w(string.Format("Can't find key {0} in TDRoleStarUpConfig", key));
                return null;
            }
        }
    }
}//namespace LR