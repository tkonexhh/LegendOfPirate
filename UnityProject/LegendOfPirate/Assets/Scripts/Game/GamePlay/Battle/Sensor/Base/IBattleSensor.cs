using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public interface IBattleSensor
    {
        BattleRoleController PickTarget(BattleRoleController picker);
        BattleRoleController[] PickTarget(BattleRoleController picker, int num);
    }

}