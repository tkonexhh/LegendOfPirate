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

        public DamageRange_Rect(float width, float height) : base()
        {
            m_Width = width;
            m_Height = height;
        }

        public override List<BattleRoleController> PickTargets(Vector3 center) { return null; }
    }

}