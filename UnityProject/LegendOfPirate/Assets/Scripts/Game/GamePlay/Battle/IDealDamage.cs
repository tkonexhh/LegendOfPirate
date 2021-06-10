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
        Transform DamageTransform();//伤害发出位置,远程的就是枪口位置
        DamageRange GetDamageRange();//得到伤害范围
    }

}