using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class DamageRange_Target : DamageRange
    {
        public DamageRange_Target() : base()
        {

        }

        public override List<BattleRoleController> PickTargets(Vector3 center)
        {
            return new List<BattleRoleController>() { owner.AI.Target };
        }
    }

}