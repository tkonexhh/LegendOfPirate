using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class DamageRangeFactory
    {
        public static DamageRange CreateDamageRange(DamageRangeType type, string param)
        {

            switch (type)
            {
                case DamageRangeType.Single:
                    return new DamageRange_Target();
                case DamageRangeType.Circle:
                    {
                        List<float> paramInt = new List<float>();
                        return new DamageRange_Circle(paramInt[0]);
                    }
                case DamageRangeType.Rect:
                    {
                        List<float> paramInt = new List<float>();
                        return new DamageRange_Rect(paramInt[0], paramInt[1]);
                    }
                case DamageRangeType.Sector:
                    {
                        List<float> paramInt = new List<float>();
                        return new DamageRange_Sector(paramInt[0], paramInt[1]);
                    }

            }
            return null;

        }
    }

}