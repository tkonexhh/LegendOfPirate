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
        public IDealDamage owner { set; get; }

        public DamageRange(IDealDamage owner)
        {
            this.owner = owner;
        }

        public abstract List<BattleRoleController> PickTargets(BattleCamp camp);
    }

}