using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public interface IDealDamage
    {
        void DealDamage();
        Transform DamageTransform();//伤害发出位置,远程的就是枪口位置
        RangeDamage GetRangeDamage();//得到伤害范围
    }

}