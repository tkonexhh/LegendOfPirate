using UnityEngine;
using LeanCloud;
using Qarth;
namespace GameWish.Game
{
    public class LeanCloudMgr : TSingleton<LeanCloudMgr>
    {
        public void OnInit()
        {
            //连接LC后端云
            string aa = LeanCloudConfig.S.appid;
            LCApplication.Initialize(LeanCloudConfig.S.appid, LeanCloudConfig.S.appkey, LeanCloudConfig.S.server);
            //子类化对象注册
            GameStorageMgr.S.SetUp();
            //回调监听
            LCLogger.LogDelegate = (level, log) =>
            {
                switch (level)
                {
                    case LCLogLevel.Debug:
                        Debug.Log(log);
                        break;
                    case LCLogLevel.Warn:
                        Debug.LogWarning(log);
                        break;
                    case LCLogLevel.Error:
                        Debug.LogError(log);
                        break;
                    default:
                        break;
                }
            };
        }
    }

}