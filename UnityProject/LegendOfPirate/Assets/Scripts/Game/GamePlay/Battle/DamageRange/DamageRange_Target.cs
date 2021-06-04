using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class DamageRange_Target : DamageRange
    {
        public DamageRange_Target(IDealDamage owner) : base(owner)
        {
        }

        public override List<BattleRoleController> PickTargets(BattleCamp camp)
        {
            var targets = new List<BattleRoleController>();
            if (owner is BattleRoleController role)
            {
                targets.Add(role.AI.Target);
            }
            return targets;
        }
    }

}