using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public abstract class BattleAttacker
    {
        public abstract void Attack(IDealDamage attacker);
    }

    public class BattleAttacker_Lock : BattleAttacker
    {
        public override void Attack(IDealDamage attacker)
        {
            //TODO 监听动画攻击事件
            // DamageRangeFactory.CreateDamageRange
            attacker.DealDamage();
        }
    }

    public class BattleAttacker_Shoot : BattleAttacker
    {
        public int bulletNum;
        public GameObject bullet;

        public override void Attack(IDealDamage attacker)
        {
            //监听发射发射出子弹

            //子弹带有attacker信息
        }
    }

}