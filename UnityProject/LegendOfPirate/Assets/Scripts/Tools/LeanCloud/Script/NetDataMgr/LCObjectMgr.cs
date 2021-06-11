using System.Threading.Tasks;
using LeanCloud.Storage;
using UnityEngine;
using LeanCloud;
using Qarth;


namespace GameWish.Game
{
    public class LCObjectMgr : TSingleton<LCObjectMgr>
    {
        ///<summary>
        /// 创建数据对象
        ///</summary>
        public async void CreateObject(string className, string content)
        {
            try
            {
                // 构建对象
                LCObject lcObject = new LCObject(className);
                // 为属性赋值
                lcObject["content"] = content;
                // 将对象保存到云端
                await lcObject.Save();
                string objectId = lcObject.ObjectId;
                Log.i(className + "ObjectId=" + objectId);
                PlayerPrefs.SetString(className, objectId);
                Log.i(className + "数据创建成功");
            }
            catch (LCException e)
            {
                Log.e(e);
            }
        }

        ///<summary>
        /// 获取数据对象
        ///</summary>
        public async Task<LCObject> QueryObject(string className)
        {
            if (!string.IsNullOrEmpty(PlayerPrefs.GetString(className)))
            {
                try
                {
                    LCQuery<LCObject> query = new LCQuery<LCObject>(className);
                    LCObject data = await query.Get(PlayerPrefs.GetString(className));
                    Log.i(className + "数据查询成功");
                    return data;
                }
                catch (LCException e)
                {
                    Log.e(e);
                    return null;
                }
            }
            else
            {
                Log.i(className + "对象并未上传服务器");
                return null;
            }
        }

        ///<summary>
        /// 更新对象数据
        ///</summary>
        public async void UpdateObject(string className, string content)
        {
            try
            {
                //查询指定对象
                LCObject lcObject = LCObject.CreateWithoutData(className, PlayerPrefs.GetString(className));
                //更新对象内容
                lcObject["content"] = content;
                // 将对象保存到云端
                await lcObject.Save();
                Log.i(className + "数据更新成功");
            }
            catch (LCException e)
            {
                Log.e(e);
            }
        }
    }
}

