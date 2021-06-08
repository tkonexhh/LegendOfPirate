using UnityEngine;
using LeanCloud;

namespace GameWish.Game
{
    public class StartUpLeanCloud : MonoBehaviour
    {
        private void Awake()
        {
            //连接LC后端云
            LCApplication.Initialize(LeanCloudConfig.S.appid, LeanCloudConfig.S.appkey, LeanCloudConfig.S.server);
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