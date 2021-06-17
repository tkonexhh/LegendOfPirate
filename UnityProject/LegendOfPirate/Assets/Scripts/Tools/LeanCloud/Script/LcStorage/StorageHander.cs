using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LeanCloud.Storage;
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
    }

}