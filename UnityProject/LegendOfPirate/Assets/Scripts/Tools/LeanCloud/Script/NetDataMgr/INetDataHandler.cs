using System;

namespace GameWish.Game
{
    public interface INetDataHandler
    {
        ///<summary>
        /// 保存数据到服务端
        ///</summary>
        /// <param name="className">存储数据的对象名</param>
        /// <param name="content">存储对象的内容</param>
        bool SaveNetData(string className, object data);

        ///<summary>
        /// 下载数据到客户端 通用下载接口
        ///</summary>
        /// <param name="className">下载数据的对象名</param>
        /// <param name="callback1">json -->对象 完成回调 ParseJson(string json)</param>
        /// <param name="callback2">数据加载完成回调 OnLoadDone </param>
        void LoadNetData(string className, Action<string> callback1, Action callback2);

        ///<summary>
        /// 处理数据
        ///</summary>
        string DealData();

    }

}