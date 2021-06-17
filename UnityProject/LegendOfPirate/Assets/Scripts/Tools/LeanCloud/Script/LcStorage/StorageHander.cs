using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LeanCloud.Storage;
using LeanCloud;
using System;
using Qarth;
namespace GameWish.Game
{
    public class StorageHander : TSingleton<StorageHander>
    {
        ///<summary>
        /// 判断玩家是否重名
        ///</summary>
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
        /// 获取数据对象 传入表的名字
        ///</summary>
        public async Task<ReadOnlyCollection<LCObject>> QueryObject(string className, Action successCallBack = null, Action failCallBack = null)
        {
            try
            {
                LCQuery<LCObject> query = new LCQuery<LCObject>(className);
                ReadOnlyCollection<LCObject> list = await query.Find();
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