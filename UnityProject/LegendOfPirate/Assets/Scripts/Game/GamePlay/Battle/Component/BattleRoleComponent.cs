using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BattleRoleComponent : AbstractBattleComponent
    {
        public override void OnBattleInit()
        {
            for (int i = 0; i < 10; i++)
            {
                RoleController role = new RoleController();
            }
        }
    }

}