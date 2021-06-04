using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class DamageRangeFactory
    {
        public static DamageRange CreateDamageRange(DamageRangeType type, params object[] param)
        {
            List<float> paramInt = Helper.String2ListFloat(param[0] as string, ";");
            switch (type)
            {
                case DamageRangeType.Single:
                    return new DamageRange_Target((BattleRoleController)param[0]);
                case DamageRangeType.Circle:
                    {
                        CircleArgs args = (CircleArgs)param[0];
                        return new DamageRange_Circle(args.center, args.radius);
                    }
                case DamageRangeType.Rect:
                    {
                        RectArgs args = (RectArgs)param[0];
                        return new DamageRange_Rect(args.center, args.forward, args.width, args.height);
                    }
                case DamageRangeType.Sector:
                    {
                        SectorArgs args = (SectorArgs)param[0];
                        return new DamageRange_Sector(args.center, args.forward, args.radius, args.degree);
                    }
            }
            return null;
        }

        public struct CircleArgs
        {
            public Vector3 center;
            public float radius;
        }

        public struct RectArgs
        {
            public Vector3 center;
            public Vector3 forward;
            public float width;
            public float height;
        }

        public struct SectorArgs
        {
            public Vector3 center;
            public Vector3 forward;
            public float radius;
            public float degree;
        }
    }

}