using UnityEngine;
using Sirenix.OdinInspector;
namespace GameWish.Game
{
    [CreateAssetMenu(fileName = "LeanCloudConfig", menuName = "Game/LeanCloudConfig")]
    public class LeanCloudConfig : SerializedScriptableObject
    {
        private static LeanCloudConfig s_Instance;
        private const string LC_CONFIG_PATH = "Resources/Config/LeanCloudConfig";
        [LabelText("LC应用ID")]
        public string appid = "6QTmq2s6V5k2SowYfn8qMMVC-gzGzoHsz";
        [LabelText("LC应用kEY")]
        public string appkey = "4ufGHkDExUKTnvXqjpP0eGdf";
        [LabelText("LC服务器地址")]
        public string server = "https://6qtmq2s6.lc-cn-n1-shared.com";
        [LabelText("LC房主KEY")]
        public string masterKey = "暂时不需要填写";
        private static LeanCloudConfig LoadInstance()
        {
            UnityEngine.Object obj = Resources.Load(ResourcesPath2Path(LC_CONFIG_PATH));
            if (obj == null)
            {
                Debug.LogError("Not Find LeanCloud Config File.");
                return null;
            }
            s_Instance = obj as LeanCloudConfig;
            return s_Instance;
        }
        public static LeanCloudConfig S
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = LoadInstance();
                }
                return s_Instance;
            }
        }
        private static string ResourcesPath2Path(string path)
        {
            return path.Substring(10);
        }
    }
}