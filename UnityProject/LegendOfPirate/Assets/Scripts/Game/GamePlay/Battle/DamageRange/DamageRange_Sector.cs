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
        private float m_Radius;
        private float m_Degree;
        public DamageRange_Sector(float radius, float degree) : base()
        {
            m_Radius = radius;
            m_Degree = degree;
        }

        public override List<BattleRoleController> PickTargets(Vector3 center) { return null; }
    }

}