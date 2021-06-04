using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class DamageRange_Target : DamageRange
    {
        private BattleRoleController m_Controller;
        public DamageRange_Target(BattleRoleController controller) : base()
        {
            m_Controller = controller;
        }

        public override List<BattleRoleController> PickTargets(BattleCamp camp)
        {
            return new List<BattleRoleController>() { m_Controller.AI.Target };
        }
    }

}