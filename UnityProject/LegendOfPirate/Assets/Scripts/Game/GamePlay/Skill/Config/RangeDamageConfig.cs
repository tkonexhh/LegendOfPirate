using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GameWish.Game
{
    public class RangeDamageConfig
    {
        public static Picker CreateRangeDamage(RangeDamageConfig config)
        {
            Picker rangeDamage = null;
            if (config is RangeDamageConfig_Circle circleConfig) rangeDamage = new RangeDamage_Circle(circleConfig.Radius);
            else if (config is RangeDamageConfig_Rect rectConfig) rangeDamage = new RangeDamage_Rect(rectConfig.Width, rectConfig.Height);
            else if (config is RangeDamageConfig_Sector sectorConfig) rangeDamage = new RangeDamage_Sector(sectorConfig.Radius, sectorConfig.Dregee);
            else if (config is RangeDamageConfig_All allConfig) rangeDamage = new RangeDamage_All(allConfig.camp);
            return rangeDamage;
        }
    }

    [LabelText("圆形范围")]
    public class RangeDamageConfig_Circle : RangeDamageConfig
    {
        [LabelText("半径")] public float Radius;
    }

    [LabelText("矩形范围")]
    public class RangeDamageConfig_Rect : RangeDamageConfig
    {
        [LabelText("宽度")] public float Width;
        [LabelText("高度")] public float Height;
    }

    [LabelText("扇形范围")]
    public class RangeDamageConfig_Sector : RangeDamageConfig
    {
        [LabelText("半径")] public float Radius;
        [LabelText("角度")] public float Dregee;
    }

    [LabelText("全体")]
    public class RangeDamageConfig_All : RangeDamageConfig
    {
        public BattleCamp camp;
    }

}