using System.Threading.Tasks;
using LeanCloud.Storage;
using UnityEngine;
using LeanCloud;
using LitJson;

namespace GameWish.Game
{
    ///<summary>
    /// LC服务器
    ///</summary>
    public class LCDataMgr : NetDataMgr
    {
        private LCObject m_data;
        ///<summary>
        /// 保存数据到服务端
        ///</summary>
        /// <param name="className">存储数据的对象名</param>
        /// <param name="content">存储对象的内容</param>
        public override bool SaveData(string className, object data)
        {
            if (data == null)
            {
                Debug.LogWarning("SerializeJson data is Null.");
                return false;
            }

            string content = JsonMapper.ToJson(data);
            //若 objectid 为空，则未创建该数据，需要创建；否则直接更新，防止对象多次创建。
            Debug.Log("PlayerPrefs.GetString(className) =" + string.IsNullOrEmpty(PlayerPrefs.GetString(className)));
            if (string.IsNullOrEmpty(PlayerPrefs.GetString(className)))
            {
                LCObjectMgr.S.CreateObject(className, content);
            }
            else
            {
                LCObjectMgr.S.UpdateObject(className, content);
            }
            return true;
        }
		
        ///<summary>
        /// 下载数据到客户端
        ///</summary>
		/// <param name="className">存储数据的对象名</param>
        public override async Task<LCObject> LoadData(string className)
        {
            if (LCObjectMgr.S.QueryObject(className) != null)
            {
                try
                {
                    m_data = await LCObjectMgr.S.QueryObject(className) as LCObject;
                    Debug.Log("数据拉取成功");
                    return m_data;
                }
                catch (LCException e)
                {
                    Debug.LogError(e);
                    return null;
                }
            }
            return null;
        }

        ///<summary>
        /// 解析数据
        ///</summary>
        public override string DealData()
        {
            if (m_data != null && m_data["content"] != null)
            {
                string content = m_data["content"] as string;
                return content;
            }
            return "";
        }

    }
}