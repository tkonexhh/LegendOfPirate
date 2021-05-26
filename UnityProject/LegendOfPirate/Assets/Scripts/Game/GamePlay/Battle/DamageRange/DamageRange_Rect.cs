using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    /// <summary>
    /// 伤害范围 - 矩形
    /// </summary>
    public class DamageRange_Rect : DamageRange
    {
        private float m_Width;
        private float m_Height;
        private Vector3 m_Pos;

        public DamageRange_Rect(float width, float height, Vector3 pos)
        {
            m_Width = width;
            m_Height = height;
            m_Pos = pos;
        }

        public override List<IElement> PickTargets() { return null; }
    }

}