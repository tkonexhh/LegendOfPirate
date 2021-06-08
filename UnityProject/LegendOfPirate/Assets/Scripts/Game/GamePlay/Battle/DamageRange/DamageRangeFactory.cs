using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class DamageRangeFactory
    {
        public static DamageRange CreateDamageRange(DamageRangeType type, IDealDamage owner, params object[] param)
        {
            List<float> paramInt = Helper.String2ListFloat(param[0] as string, ";");
            switch (type)
            {
                case DamageRangeType.Single:
                    return new DamageRange_Target(owner);
                case DamageRangeType.Circle:
                    {
                        return new DamageRange_Circle(owner, paramInt[0]);
                    }
                case DamageRangeType.Rect:
                    {
                        return new DamageRange_Rect(owner, paramInt[0], paramInt[1]);
                    }
                case DamageRangeType.Sector:
                    {
                        return new DamageRange_Sector(owner, paramInt[0], paramInt[1]);
                    }
            }
            return null;
        }
    }

}