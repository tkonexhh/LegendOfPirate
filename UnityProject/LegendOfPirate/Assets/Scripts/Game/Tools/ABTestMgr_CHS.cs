using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using GameWish.Game;
using Qarth;
using UnityEngine;

public class ABTestMgr_CHS : TMonoSingleton<ABTestMgr_CHS>
{

    private static Dictionary<string, ABDataWrap> m_DataMap = new Dictionary<string, ABDataWrap>();

    public ABRemoteKeyValueConfig config;

    private static bool m_IsInitSuccess = false;

    private class ABDataWrap
    {
        public string key;
        public string defaultValue;

        public ABDataWrap(string key, string defaultValue)
        {
            this.key = key;
            this.defaultValue = defaultValue;
        }

        public string GetValueString()
        {
#if AB_ENABLED
#if UNITY_ANDROID
            string value = LibraABMgr.S.GetLibraConf(key, defaultValue);
            if (string.IsNullOrEmpty(value))
            {
                value = defaultValue;
            }
            return value;
#elif UNITY_IOS
            string value = defaultValue;
            if( m_IsInitSuccess)
            {
                value = TTEventSender.GetABTestConf(key, defaultValue);
                if (string.IsNullOrEmpty(value))
                {
                    value = defaultValue;
                }
            }
            return value;
#endif
#endif
            return defaultValue;
        }
    }

    public override void OnSingletonInit()
    {
        base.OnSingletonInit();
        Log.i("ABTestMgr_CHS init");
        ABRemoteKeyValueConfig.S.InitSDKConfig();
        AddDataWrap();
    }

    public IEnumerator CheckABRemoteInit(Action<bool> onLoadFinish)
    {
#if AB_ENABLED
#if UNITY_ANDROID
        LibraABMgr.S.GetProperties();
        m_IsInitSuccess = true;
        onLoadFinish(true);
#elif UNITY_IOS
        TTEventSender.GetABTestConfs();
        EventSystem.S.Register(SDKEventID.OnLibraABAllFetch, (KeyCode, args) => {
            m_IsInitSuccess = true;
        });
        onLoadFinish(true);
#endif
#else
        onLoadFinish(true);
#endif
        yield return null;
    }

    //添加需要的key
    private void AddDataWrap()
    {
        if (config == null)
        {
            config = ABRemoteKeyValueConfig.S;
        }
        foreach (var pair in config.keyValuesMap)
        {
            string defaultValue = "";
            if (pair.Value.values.Count > 0)
                defaultValue = pair.Value.defaultValue;

            m_DataMap.Add(pair.Key.ToString(), new ABDataWrap(pair.Key.ToString(), defaultValue));
        }

        foreach (var tableRemotePair in config.tableValuesMap)
        {
            string defaultValue = "";
            if (tableRemotePair.Value.values.Count > 0)
                defaultValue = tableRemotePair.Value.defaultValue;

            m_DataMap.Add(tableRemotePair.Key, new ABDataWrap(tableRemotePair.Key.ToString(), defaultValue));
        }
    }

    public string GetValueString(string key)
    {
        if (m_DataMap.ContainsKey(key))
        {
            return m_DataMap[key].GetValueString();
        }
        else
        {
            Log.i("not contain this key: " + key);
            return string.Empty;
        }
    }

    public void ReloadTable(Action<bool> onLoadFinish)
    {
        List<string> tableNames = new List<string>();
        List<string> typeNames = new List<string>();

        foreach (var con in config.tableValuesMap)
        {
            string table = GetValueString(con.Key);

            //table名字不是默认值
            if (config.tableValuesMap[con.Key].values[0] != table)
            {
                tableNames.Add(table);
                typeNames.Add(config.tableValuesMap[con.Key].tableClassName);
            }

        }

        List<TDTableMetaData> dataList = new List<TDTableMetaData>();
        for (int i = 0; i < tableNames.Count; i++)
        {
            Type type = Type.GetType("GameWish.Game." + typeNames[i]);
            MethodInfo method = type.GetMethod("Parse");
            DTableOnParse parse = (DTableOnParse)Delegate.CreateDelegate(typeof(DTableOnParse), method);
            dataList.Add(new TDTableMetaData(parse, tableNames[i]));
        }

        //if (AbTestActor.GetGuideTableMetaDatas() != null)
        //{
        //    foreach (var data in AbTestActor.GetGuideTableMetaDatas())
        //    {
        //        dataList.Add(data);
        //    }
        //}

        //StartCoroutine(TableMgr.S.ReadAll(dataList.ToArray(), () =>
        //{
        //    Log.i("reload table");
        //    ModifyConfig();
        //    if (onLoadFinish != null)
        //    {
        //        onLoadFinish(FirebaseRemoteConfigMgr.S.IsInit);
        //    }
        //}));
    }


    //修改config中关于表的数据
    public void ModifyConfig()
    {
        //AbTestActor.ModifyDefineValue();
    }

}
