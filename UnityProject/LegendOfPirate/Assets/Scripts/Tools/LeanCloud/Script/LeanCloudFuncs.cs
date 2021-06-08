using System.Collections.Generic;
using System.Threading.Tasks;
using LeanCloud.Storage;
using UnityEngine;
using System;
using Qarth;

namespace GameWish.Game
{
    /// <summary>
    /// 云函数请求方法 待扩展
    /// </summary>
    public class LeanCloudFuncs : TSingleton<LeanCloudFuncs>
    {
        /// <summary>
        /// 云函数请求获取表的数据 Run方式 返回常规数据类型 int string 等
        /// </summary>
        /// <param name="cloudFunc">云函数名称</param>
        /// <param name="contentKey">" { "className", "Hello" },{ "key", object }..."</param>
        public async Task<T> RunRequest<T>(string cloudFunc, Dictionary<string, object> contentKey = null) where T : struct
        {
            try
            {
                T response = await LCCloud.Run<T>(cloudFunc, contentKey);
                Debug.Log(response);
                return response;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return default(T);
            }
        }

        /// <summary>
        /// 云函数请求获取表的数据 Run方式 返回Dictionary
        /// </summary>
        /// <param name="cloudFunc">云函数名称</param>
        /// <param name="contentKey">" { "className", "Hello" },{ "key", object }..."</param>
        public async Task<object> RunRequest(string cloudFunc, Dictionary<string, object> contentKey = null)
        {
            try
            {
                Dictionary<string, object> response = await LCCloud.Run(cloudFunc, contentKey);
                Debug.Log(response["result"]);
                return response["result"];
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return null;
            }
        }

        /// <summary>
        /// 云函数请求获取表的数据 RPC方式 返回object类型
        /// </summary>
        /// <param name="cloudFunc">云函数名称</param>
        /// <param name="contentKey">" { "className", "Hello" },{ "key", object }..."</param>
        public async Task<object> RPCRequest(string cloudFunc, Dictionary<string, object> contentKey = null)
        {
            try
            {
                object response = await LCCloud.RPC(cloudFunc, contentKey);
                Debug.Log(response);
                return response;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return null;
            }
        }
    }

}