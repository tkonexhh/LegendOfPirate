//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq.Expressions;
//using System.Reflection;
//using GameWish.Game;
//using Qarth;
//using UnityEngine;

//public class ABTestMgr : TMonoSingleton<ABTestMgr>
//{

//    private static Dictionary<string, FirebaseDataWrap> m_DataMap = new Dictionary<string, FirebaseDataWrap>();

//    public ABRemoteKeyValueConfig config;


//    public override void OnSingletonInit()
//    {
//        base.OnSingletonInit();
//        Log.i("abtestmgr init");
//        ABRemoteKeyValueConfig.S.InitSDKConfig();
//        AddDataWrap();
//    }

//    public IEnumerator CheckFirebaseRemoteInit(Action<bool> onLoadFinish)
//    {
//        int time = 0;
//        while (!FirebaseRemoteConfigMgr.S.IsInit && time < 3)
//        {
//            yield return new WaitForSeconds(1);
//            time++;
//        }

//        ReloadTable(onLoadFinish);
//        //if (FirebaseRemoteConfigMgr.S.IsInit)
//        //{

//        //}
//        //else
//        //{
//        //    if (onLoadFinish != null)
//        //    {
//        //        onLoadFinish(false);
//        //    }
//        //}
//    }

//    //添加需要的key
//    private void AddDataWrap()
//    {

//        if (config == null)
//        {
//            config = ABRemoteKeyValueConfig.S;
//        }
//        foreach (var pair in config.keyValuesMap)
//        {
//            string defaultValue = "";
//            if (pair.Value.values.Count > 0)
//                defaultValue = pair.Value.defaultValue;

//            m_DataMap.Add(pair.Key.ToString(), new FirebaseDataWrap(pair.Key.ToString(), defaultValue));
//        }

//        foreach (var tableRemotePair in config.tableValuesMap)
//        {
//            string defaultValue = "";
//            if (tableRemotePair.Value.values.Count > 0)
//                defaultValue = tableRemotePair.Value.defaultValue;

//            m_DataMap.Add(tableRemotePair.Key, new FirebaseDataWrap(tableRemotePair.Key.ToString(), defaultValue));
//        }
//    }


//    public long GetValueLong(string key)
//    {
//        if (m_DataMap.ContainsKey(key))
//        {
//            return FirebaseRemoteConfigMgr.S.GetValueLong(m_DataMap[key]);
//        }
//        else
//        {
//            Log.i("not contain this key");
//            return 0;
//        }
//    }

//    public bool GetValueBool(string key)
//    {
//        if (m_DataMap.ContainsKey(key))
//        {
//            return FirebaseRemoteConfigMgr.S.GetValueBool(m_DataMap[key]);
//        }
//        else
//        {
//            Log.i("not contain this key");
//            return false;
//        }
//    }

//    public string GetValueString(string key)
//    {
//        if (m_DataMap.ContainsKey(key))
//        {
//            return FirebaseRemoteConfigMgr.S.GetValueString(m_DataMap[key]);
//        }
//        else
//        {
//            Log.i("not contain this key");
//            return "";
//        }
//    }

//    public double GetValueDouble(string key)
//    {
//        if (m_DataMap.ContainsKey(key))
//        {
//            return FirebaseRemoteConfigMgr.S.GetValueDouble(m_DataMap[key]);
//        }
//        else
//        {
//            Log.i("not contain this key");
//            return 0f;
//        }
//    }


//    public void ReloadTable(Action<bool> onLoadFinish)
//    {
//        List<string> tableNames = new List<string>();
//        List<string> typeNames = new List<string>();

//        foreach (var con in config.tableValuesMap)
//        {
//            string table = GetValueString(con.Key);

//            //table名字不是默认值
//            if (config.tableValuesMap[con.Key].values[0] != table)
//            {
//                tableNames.Add(table);
//                typeNames.Add(config.tableValuesMap[con.Key].tableClassName);
//            }

//        }

//        List<TDTableMetaData> dataList = new List<TDTableMetaData>();
//        for (int i = 0; i < tableNames.Count; i++)
//        {
//            Type type = Type.GetType("GameWish.Game." + typeNames[i]);
//            MethodInfo method = type.GetMethod("Parse");
//            DTableOnParse parse = (DTableOnParse)Delegate.CreateDelegate(typeof(DTableOnParse), method);
//            dataList.Add(new TDTableMetaData(parse, tableNames[i]));
//        }

//        if (AbTestActor.GetGuideTableMetaDatas() != null)
//        {
//            foreach (var data in AbTestActor.GetGuideTableMetaDatas())
//            {
//                dataList.Add(data);
//            }
//        }

//        StartCoroutine(TableMgr.S.ReadAll(dataList.ToArray(), () =>
//        {
//            Log.i("reload table");
//            ModifyConfig();
//            if (onLoadFinish != null)
//            {
//                onLoadFinish(FirebaseRemoteConfigMgr.S.IsInit);
//            }
//        }));
//    }


//    //修改config中关于表的数据
//    public void ModifyConfig()
//    {
//        AbTestActor.ModifyDefineValue();
//    }

//}
