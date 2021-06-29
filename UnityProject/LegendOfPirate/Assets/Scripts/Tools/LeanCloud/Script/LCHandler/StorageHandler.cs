using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LeanCloud.Storage;
using LeanCloud;
using UnityEngine;
using System;
using Qarth;
namespace GameWish.Game
{
    public class StorageHandler : TSingleton<StorageHandler>
    {
        ///<summary>
        /// 判断玩家是否重名
        ///</summary>
        /// <param name="name">玩家名字</param>
        public async Task<bool> QueryUserName(string name)
        {
            LCQuery<UserStorage> query = new LCQuery<UserStorage>("UserStorage");
            query.WhereEndsWith("name", name);
            ReadOnlyCollection<UserStorage> list = await query.Find();
            if (list.Count > 0)
            {
                return true;
            }
            return false;
        }

        ///<summary>
        /// 通用结构 获取数据对象 传入表的名字
        ///</summary>
        /// <param name="className">存储对象名</param>
        /// <param name="successCallBack">成功回调</param>
        /// <param name="failCallBack">失败回调</param>
        public async Task<ReadOnlyCollection<T>> QueryObject<T>(string className, Action successCallBack = null, Action failCallBack = null) where T : LCObject
        {
            try
            {
                LCQuery<T> query = new LCQuery<T>(className);
                ReadOnlyCollection<T> list = await query.Find();
                Log.i(className + "数据查询成功");
                successCallBack?.Invoke();
                return list;
            }
            catch (LCException e)
            {
                Log.e(e);
                failCallBack?.Invoke();
                return null;
            }
        }

        ///<summary>
        /// 通用结构 获取某个数据对象 传入表的名字 以及该数据对象的key值 跟value值 value值是 int 类型
        ///</summary>
        /// <param name="className">存储对象名</param>
        /// <param name="key">存储对象的某个key</param>
        /// <param name="value">需要查询的value值</param>
        /// <param name="successCallBack">成功回调</param>
        /// <param name="failCallBack">失败回调</param>
        public async Task<ReadOnlyCollection<T>> QuerySpecialObject<T>(string className, string key, int value, Action successCallBack = null, Action failCallBack = null) where T : LCObject
        {
            try
            {
                LCQuery<T> query = new LCQuery<T>(className);
                query.WhereEqualTo(key, value);
                ReadOnlyCollection<T> list = await query.Find();
                Log.i(className + "数据查询成功");
                successCallBack?.Invoke();
                return list;
            }
            catch (LCException e)
            {
                Log.e(e);
                failCallBack?.Invoke();
                return null;
            }
        }


        ///<summary>
        /// 通用结构 获取某个数据对象 传入表的名字 以及该数据对象的key值 跟value值  value值是string类型
        ///</summary>
        /// <param name="className">存储对象名</param>
        /// <param name="key">存储对象的某个key</param>
        /// <param name="value">需要查询的value值</param>
        /// <param name="successCallBack">成功回调</param>
        /// <param name="failCallBack">失败回调</param>
        public async Task<ReadOnlyCollection<T>> QuerySpecialObject<T>(string className, string key, string value, Action successCallBack = null, Action failCallBack = null) where T : LCObject
        {
            try
            {
                LCQuery<T> query = new LCQuery<T>(className);
                query.WhereEndsWith(key, value);
                ReadOnlyCollection<T> list = await query.Find();
                Log.i(className + "数据查询成功");
                successCallBack?.Invoke();
                return list;
            }
            catch (LCException e)
            {
                Log.e(e);
                failCallBack?.Invoke();
                return null;
            }
        }

        ///<summary>
        /// 更新对象数据
        ///</summary>
        /// <param name="className">存储对象名</param>
        /// <param name="objectId">存储对象的唯一标识</param>
        /// <param name="key">存储对象的某个key</param>
        /// <param name="value">需要查询的value值</param>
        /// <param name="successCallBack">成功回调</param>
        /// <param name="failCallBack">失败回调</param>
        public async void UpdateObject(string className, string objectId, string key, int value, Action successCallBack = null, Action failCallBack = null)
        {
            try
            {
                //查询指定对象
                LCObject lcObject = LCObject.CreateWithoutData(className, objectId);
                //更新对象内容
                lcObject[key] = value;
                // 将对象保存到云端
                await lcObject.Save();
                Log.i(className + "数据更新成功");
                successCallBack?.Invoke();
            }
            catch (LCException e)
            {
                Log.e(e);
                failCallBack?.Invoke();
            }
        }
    }

}