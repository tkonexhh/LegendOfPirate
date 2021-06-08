using System.Collections.Generic;
using System.Threading.Tasks;
using LeanCloud.Storage;
using UnityEngine.UI;
using UnityEngine;
using System;
using Qarth;


namespace GameWish.Game
{
    public class GameCloudFunc : LeanCloudFuncs
    {
        [SerializeField] private Text m_ShowText;

        public async Task Ping()
        {
            Dictionary<string, object> response = await LCCloud.Run("ping");

        }


        public async Task Hello()
        {
            string result = await LCCloud.Run<string>("hello", new Dictionary<string, object> {
                { "name", "world" }
            });

        }


        public async Task GetObject()
        {
            LCObject hello = new LCObject("Hello");
            await hello.Save();
            object reponse = await LCCloud.RPC("getObject", new Dictionary<string, object> {
                { "className", "Hello" },
                { "id", hello.ObjectId }
            });
            LCObject obj = reponse as LCObject;

        }


        public async Task GetObjects()
        {
            object response = await LCCloud.RPC("getObjects");
            List<object> list = response as List<object>;
            // IEnumerable<LCObject> objects = list.Cast<LCObject>();

            // foreach (LCObject obj in objects)
            // {
            //     int balance = (int)obj["balance"];

            // }
        }


        public async Task GetObjectMap()
        {
            object response = await LCCloud.RPC("getObjectMap");
            Dictionary<string, object> dict = response as Dictionary<string, object>;

            foreach (KeyValuePair<string, object> kv in dict)
            {
                LCObject obj = kv.Value as LCObject;

            }
        }

        public async void OnDrawClicked()
        {
            try
            {
                Dictionary<string, object> results = await LCCloud.Run("LuckyDraw", new Dictionary<string, object> { { "curKind", "lucky" } });
                List<object> heros = results["result"] as List<object>;
                string info = $"恭喜获得：{string.Join(", ", heros)}";
                Debug.Log(info);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        public async void OnHelloClicked()
        {
            try
            {

                Dictionary<string, object> results = await LCCloud.Run("hello", new Dictionary<string, object> { { "name", "ChuckFly" } });
                string msg = results["result"] as string;
                Debug.Log(msg);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        public async void OnGetObjectClicked()
        {
            try
            {
                object results = await LCCloud.RPC("GetObject_New", new Dictionary<string, object> { { "className", "Hero" } });
                List<object> heros = results as List<object>;
                string info = "";
                foreach (object hero in heros)
                {
                    LCObject temp = hero as LCObject;
                    // string serialzedString = temp.ToString();
                    // LCObject newObject = LCObject.ParseObject(serialzedString);
                    info += "_" + temp["name"] as string;
                    Debug.Log(temp["name"]);

                }
                // string info = $"{string.Join(", ", dict)}";
                Debug.Log(info);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        public async void OnGetObjectMapClicked()
        {
            try
            {
                object results = await LCCloud.RPC("getObjectMap");
                Dictionary<string, object> dict = results as Dictionary<string, object>;
                Debug.Log(dict.Count);
                foreach (KeyValuePair<string, object> kv in dict)
                {
                    LCObject obj = kv.Value as LCObject;
                    Debug.Log(obj["name"]);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}

