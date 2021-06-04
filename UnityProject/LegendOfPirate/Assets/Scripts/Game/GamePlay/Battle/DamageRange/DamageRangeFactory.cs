using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class DamageRangeFactory
    {
        public static DamageRange CreateDamageRange(DamageRangeType type, string param)
        {
            List<float> paramInt = Helper.String2ListFloat(param, ";");
            switch (type)
            {
                case DamageRangeType.Single:
                    return new DamageRange_Target();
                case DamageRangeType.Circle:
                    return new DamageRange_Circle(paramInt[0]);
                case DamageRangeType.Rect:
                    return new DamageRange_Rect(paramInt[0], paramInt[1]);
                case DamageRangeType.Sector:
                    return new DamageRange_Sector(paramInt[0], paramInt[1]);
            }
            return null;

        }
    }

}