using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public interface IDealDamage
    {
        void DealDamage();

        Vector3 DamageCenter();
        Vector3 DamageForward();
    }

}