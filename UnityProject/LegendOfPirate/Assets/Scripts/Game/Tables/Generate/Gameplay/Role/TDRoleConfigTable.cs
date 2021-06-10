//Auto Generate Don't Edit it
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Qarth;

namespace GameWish.Game
{
    public static partial class TDRoleConfigTable
    {
        private static TDTableMetaData m_MetaData = new TDTableMetaData(TDRoleConfigTable.Parse, "RoleConfig");
        public static TDTableMetaData metaData
        {
            get { return m_MetaData; }
        }
        
        private static Dictionary<int, TDRoleConfig> m_DataCache = new Dictionary<int, TDRoleConfig>();
        private static List<TDRoleConfig> m_DataList = new List<TDRoleConfig >();
        
        public static void Parse(byte[] fileData)
        {
            m_DataCache.Clear();
            m_DataList.Clear();
            DataStreamReader dataR = new DataStreamReader(fileData);
            int rowCount = dataR.GetRowCount();
            int[] fieldIndex = dataR.GetFieldIndex(TDRoleConfig.GetFieldHeadIndex());
    #if (UNITY_STANDALONE_WIN) || UNITY_EDITOR || UNITY_STANDALONE_OSX
            dataR.CheckFieldMatch(TDRoleConfig.GetFieldHeadIndex(), "RoleConfigTable");
    #endif
            for (int i = 0; i < rowCount; ++i)
            {
                TDRoleConfig memberInstance = new TDRoleConfig();
                memberInstance.ReadRow(dataR, fieldIndex);
                OnAddRow(memberInstance);
                memberInstance.Reset();
                CompleteRowAdd(memberInstance, rowCount);
            }
            Log.i(string.Format("Parse Success TDRoleConfig"));
        }

        private static void OnAddRow(TDRoleConfig memberInstance)
        {
            int key = memberInstance.roleId;
            if (m_DataCache.ContainsKey(key))
            {
                Log.e(string.Format("Invaild,  TDRoleConfigTable Id already exists {0}", key));
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

        public static List<TDRoleConfig> dataList
        {
            get 
            {
                return m_DataList;
            }    
        }

        public static TDRoleConfig GetData(int key)
        {
            if (m_DataCache.ContainsKey(key))
            {
                return m_DataCache[key];
            }
            else
            {
                Log.w(string.Format("Can't find key {0} in TDRoleConfig", key));
                return null;
            }
        }
    }
}//namespace LR