using Qarth;
using System.Threading.Tasks;
using LeanCloud.Storage;
namespace GameWish.Game
{
    ///<summary>
    /// 服务器数据管理类
    ///</summary>
    public class NetDataMgr : TSingleton<NetDataMgr>
    {
        ///<summary>
        /// 保存数据到服务端
        ///</summary>
        /// <param name="className">存储数据的对象名</param>
		/// <param name="content">存储对象的内容</param>
        public virtual bool SaveData(string className, object data)
        {
            return true;
        }
        ///<summary>
        /// 下载数据到客户端
        ///</summary>
		/// <param name="className">存储数据的对象名</param>
        public virtual Task<LCObject> LoadData(string className)
        {
            return null;
        }
        ///<summary>
        /// 处理数据
        ///</summary>
        public virtual string DealData()
        {
            return "";
        }
    }
}