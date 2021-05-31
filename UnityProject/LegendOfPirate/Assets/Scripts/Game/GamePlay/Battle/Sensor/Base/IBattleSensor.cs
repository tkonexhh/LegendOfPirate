using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public interface IBattleSensor
    {
        BattleRoleController PickTarget();
        BattleRoleController[] PickTarget(int num);
    }

}