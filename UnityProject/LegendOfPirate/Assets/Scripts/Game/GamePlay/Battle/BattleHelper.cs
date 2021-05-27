using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BattleHelper
    {

        public static BattleCamp GetOppositeCamp(BattleCamp camp)
        {
            if (camp == BattleCamp.Our)
            {
                return BattleCamp.Enemy;
            }
            else
            {
                return BattleCamp.Our;
            }
        }

    }

}