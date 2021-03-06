using System.Threading.Tasks;
using LeanCloud.Storage;
using UnityEngine;
using LeanCloud;
using LitJson;
using System;

namespace GameWish.Game
{
    ///<summary>
    /// LC服务器
    ///</summary>
    public class LCDataMgr : INetDataHandler
    {
        private LCObject m_data;

        ///<summary>
        /// 保存数据到服务端
        ///</summary>
        /// <param name="className">存储数据的对象名</param>
        /// <param name="content">存储对象的内容</param>
        public bool SaveNetData(string className, object data, Action successCallBack = null, Action failCallBack = null)
        {
            if (data == null)
            {
                Debug.LogWarning(className + "SerializeJson data is Null.");
                failCallBack?.Invoke();
                return false;
            }

            string content = JsonMapper.ToJson(data);
            //若 objectid 为空，则未创建该数据，需要创建；否则直接更新，防止对象多次创建。
            // Debug.Log(className + "PlayerPrefs.GetString(className) =" + PlayerPrefs.GetString(className));
            if (string.IsNullOrEmpty(PlayerPrefs.GetString(className)))
            {
                LCObjectMgr.S.CreateObject(className, content, successCallBack, failCallBack);
            }
            else
            {
                LCObjectMgr.S.UpdateObject(className, content, successCallBack, failCallBack);
            }
            return true;
        }



        ///<summary>
        /// 下载数据到客户端
        ///</summary>
        /// <param name="className">存储数据的对象名</param>
        private async Task<string> LoadNetData(string className, Action successCallBack = null, Action failCallBack = null)
        {
            m_data = await LCObjectMgr.S.QueryObject(className, failCallBack);

            if (m_data != null)
            {
                try
                {
                    if (m_data != null && m_data["content"] != null)
                    {
                        string content = m_data["content"] as string;
                        Debug.Log(className + "数据拉取成功");
                        successCallBack?.Invoke();
                        return content;
                    }
                    return "";
                }
                catch (LCException e)
                {
                    Debug.LogError(e);
                    failCallBack?.Invoke();
                    return "";
                }
            }
            return "";
        }

        ///<summary>
        /// 下载数据到客户端 通用下载接口
        ///</summary>
        /// <param name="className">下载数据的对象名</param>
        /// <param name="callback1">json -->对象 完成回调 ParseJson(string json)</param>
        /// <param name="callback2">数据加载完成回调 OnLoadDone </param>
        public async void LoadNetData(string className, Action<string> callback1, Action callback2, Action successCallBack = null, Action failCallBack = null)
        {
            string content = await LoadNetData(className, successCallBack, failCallBack);
            if (callback1 != null)
            {
                callback1(content);
                callback2?.Invoke();
            }
        }

        ///<summary>
        /// 解析数据
        ///</summary>
        public string DealData()
        {
            return "";
        }

    }
}