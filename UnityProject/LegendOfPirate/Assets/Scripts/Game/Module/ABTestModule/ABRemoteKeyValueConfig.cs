using System;
using System.Collections;
using System.Collections.Generic;
using Qarth;
using UnityEngine;

namespace GameWish.Game
{

    [Serializable]
    public class ABRemoteKeyValueConfig : ScriptableObject
    {
        private static ABRemoteKeyValueConfig s_Instance;

        [SerializeField] private List<RemotePair> keyValuePair = new List<RemotePair>();
        [SerializeField] private List<TableRemotePair> tableRemotePair = new List<TableRemotePair>();

        public Dictionary<string,RemotePair> keyValuesMap=new Dictionary<string,RemotePair>();
        public Dictionary<string,TableRemotePair> tableValuesMap=new Dictionary<string, TableRemotePair>();

        private static ABRemoteKeyValueConfig LoadInstance()
        {
            ResLoader loader = ResLoader.Allocate("ABConfigLoader", null);

            UnityEngine.Object obj = loader.LoadSync("Resources/ABRemoteConfig");
            if (obj == null)
            {
                Log.e("Not Find  ABRemoteConfig, Will Use Default Config.");
                loader.Recycle2Cache();
                return null;
            }

            //Log.i("Success Load SDK Config.");
            s_Instance = obj as ABRemoteKeyValueConfig;

            ABRemoteKeyValueConfig newAB = GameObject.Instantiate(s_Instance);

            s_Instance = newAB;

            loader.Recycle2Cache();

            return s_Instance;
        }

        public static ABRemoteKeyValueConfig S
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = LoadInstance();
                }

                return s_Instance;
            }
        }

        private void CreatDataMap()
        {
            foreach (var pair in keyValuePair)
            {
                keyValuesMap.Add(pair.key.ToString(),pair);
            }

            foreach (var pair in tableRemotePair)
            {
                tableValuesMap.Add(pair.key.ToString(),pair);
            }
        }

        public void InitSDKConfig()
        {
            Log.i("Init[ABRemoteConfig]");
            CreatDataMap();
        }
    }

    [Serializable]
    public class RemotePair
    {
        public RemoteConfigKey key;
        public string defaultValue;//为了测试 实际的默认值为value[0]
        public List<string> values;
    }

    [Serializable]
    public class TableRemotePair
    {
        public RemoteConfigKey key;
        public string tableClassName; //表的类名
        public string defaultValue;//为了测试 实际的默认值为value[0]
        public List<string> values;
    }

}