using System.Collections.Generic;
using System.Threading.Tasks;
using LeanCloud.Storage;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class GameCloudFunc : TSingleton<GameCloudFunc>
    {
        private string m_ShowText;
        ///<summary>	
        /// 简单输出一个字符串
        ///</summary>
        public async Task<string> Ping()
        {
            m_ShowText = await LeanCloudFuncs.S.RunRequest("Ping") as string;
            return m_ShowText;
        }

        ///<summary>	
        /// 传入一个字符串 输出字符串组合
        ///</summary>
        public async Task<string> Hello()
        {
            string result = await LeanCloudFuncs.S.RunRequest("Hello", new Dictionary<string, object> {
                { "name", "world" }
            }) as string;
            m_ShowText = result;
            return m_ShowText;
        }

        ///<summary>	
        /// 大转盘 抽卡
        ///</summary>
        public async Task<string> GetDraw()
        {
            object results = await LeanCloudFuncs.S.RunRequest("LuckyDraw", new Dictionary<string, object> { { "curKind", "nolucky" } });
            List<object> heros = results as List<object>;
            string info = $"恭喜获得：{string.Join(", ", heros)}";
            m_ShowText = info;
            return m_ShowText;
        }

        ///<summary>	
        /// 获取一个表的指定数据
        ///</summary>
        public async Task<string> OnGetObject()
        {
            object results = await LeanCloudFuncs.S.RPCRequest("GetObject", new Dictionary<string, object> { { "className", "Hero" }, { "name", "chuck" } });
            List<object> heros = results as List<object>;
            foreach (object hero in heros)
            {
                //类似json结构
                LCObject temp = hero as LCObject;
                string info = temp["name"] as string;
                m_ShowText = info;
            }
            return m_ShowText;
        }
        ///<summary>	
        /// 获取一个表的所有数据结构 指定了表
        ///</summary>
        public async Task<string> OnGetObjectMap()
        {
            object results = await LeanCloudFuncs.S.RPCRequest("GetObjectMap");
            Dictionary<string, object> dict = results as Dictionary<string, object>;
            Debug.Log(dict.Count);
            string info = "";
            foreach (KeyValuePair<string, object> kv in dict)
            {
                LCObject obj = kv.Value as LCObject;
                info += "_" + obj["name"] as string;
                Debug.Log(obj["name"]);
            }
            m_ShowText = info;
            return m_ShowText;
        }

        ///<summary>	
        /// 获取一个表的所有数据结构
        ///</summary>
        public async Task<string> OnGetObjects()
        {
            object results = await LeanCloudFuncs.S.RPCRequest("GetObject_New", new Dictionary<string, object> { { "className", "Hero" } });
            List<object> heros = results as List<object>;
            string info = "";
            foreach (object hero in heros)
            {
                //类似json结构
                LCObject temp = hero as LCObject;
                // string serialzedString = temp.ToString();
                // LCObject newObject = LCObject.ParseObject(serialzedString);
                info += "_" + temp["name"] as string;
                Debug.Log(temp["name"]);
            }
            m_ShowText = info;
            return m_ShowText;
        }
    }
}

