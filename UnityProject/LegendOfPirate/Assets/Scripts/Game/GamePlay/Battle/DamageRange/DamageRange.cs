using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    /// <summary>
    /// 伤害范围
    /// </summary>
    public abstract class DamageRange
    {
        public BattleRoleController owner { set; get; }

        public DamageRange()
        {
            // m_Owner = owner;
        }

        public abstract List<BattleRoleController> PickTargets(Vector3 center);
    }

}