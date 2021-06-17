using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LeanCloud.Storage;
using LeanCloud;
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
    }

}