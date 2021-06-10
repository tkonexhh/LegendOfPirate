using Qarth;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    [CreateAssetMenu(menuName = "Game/ShipConfig", fileName = "ShipConfig")]

    public class ShipConfig : SerializedScriptableObject
    {
        [LabelText("组件配置列表")]
        public Dictionary<ShipUnitType, ShipUnitConfig> unitConfigDic = new Dictionary<ShipUnitType, ShipUnitConfig>();

        private  static ResLoader s_ResLoader;

        #region 初始化过程
        private static ShipConfig s_Instance;

        private static ShipConfig LoadInstance()
        {
            s_ResLoader = ResLoader.Allocate("ShipConfig", null);

            UnityEngine.Object obj = s_ResLoader.LoadSync("ShipConfig");
            if (obj == null)
            {
                Log.e("Not Find Ship Config, Will Use Default Ship Config.");
                s_ResLoader.ReleaseAllRes();

                return null;
            }

            Log.i("Success Load Ship Config.");

            s_Instance = obj as ShipConfig;

            return s_Instance;
        }

        #endregion

        public static ShipConfig S
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

        public static void Release()
        {
            s_Instance = null;

            s_ResLoader.Recycle2Cache();
            s_ResLoader = null;
        }

        public ShipUnitConfig GetUnitConfig(ShipUnitType shipUnitType)
        {
            if (unitConfigDic.ContainsKey(shipUnitType))
            {
                return unitConfigDic[shipUnitType];
            }

            Log.e("Ship Unit Not Found: " + shipUnitType.ToString());

            return null;
        }
    }

    public class ShipUnitConfig
    {
        //[LabelText("组件类型")]
        //public ShipUnitType shipUnitType;
        [LabelText("组件资源名")]
        public string prefabName;
        [LabelText("组件位置")]
        public Vector3 pos;
        [LabelText("组件Body资源名")]
        public string bodyPrefabName;
    }
}