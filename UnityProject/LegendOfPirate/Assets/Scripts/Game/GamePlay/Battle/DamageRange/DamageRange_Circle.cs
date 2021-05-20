using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    /// <summary>
    /// 伤害范围 - 圆形
    /// </summary>
    public class DamageRange_Circle : DamageRange
    {
        private Vector3 m_Center;
        private float m_Radius;

        public DamageRange_Circle(Vector3 center, float radius)
        {
            m_Center = center;
            m_Radius = radius;
        }


        public override List<EntityBase> PickTargets() { return null; }
    }

}