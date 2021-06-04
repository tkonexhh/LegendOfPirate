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
        private float m_Radius;

        public DamageRange_Circle(float radius) : base()
        {
            m_Radius = radius;
        }

        public override List<BattleRoleController> PickTargets(Vector3 center) { return null; }
    }

}