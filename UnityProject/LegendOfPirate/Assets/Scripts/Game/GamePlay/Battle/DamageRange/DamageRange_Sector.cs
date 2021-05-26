using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    /// <summary>
    /// 伤害范围 - 扇形
    /// </summary>
    public class DamageRange_Sector : DamageRange
    {
        private Vector3 m_Center;
        private float m_Radius;
        private float m_Degree;
        public DamageRange_Sector(Vector3 center, float radius, float degree)
        {
            m_Center = center;
            m_Radius = radius;
            m_Degree = degree;
        }

        public override List<IElement> PickTargets() { return null; }
    }

}