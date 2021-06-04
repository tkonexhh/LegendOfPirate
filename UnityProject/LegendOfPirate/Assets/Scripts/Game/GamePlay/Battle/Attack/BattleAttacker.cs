using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public abstract class BattleAttacker
    {
        public abstract void Attack(BattleRoleController attacker);
    }

    public class BattleAttacker_Lock : BattleAttacker
    {
        public override void Attack(BattleRoleController attacker)
        {
            //TODO 监听动画攻击事件
            // DamageRangeFactory.CreateDamageRange
            var targets = attacker.AI.DamageRange.PickTargets(attacker.transform.position);
            int damage = BattleHelper.CalcAtkDamage(attacker.Data.buffedData);
            for (int i = 0; i < targets.Count; i++)
            {
                RoleDamagePackage damagePackage = new RoleDamagePackage();
                damagePackage.damageType = BattleDamageType.Normal;
                damagePackage.damage = damage;
                BattleMgr.S.SendDamage(targets[i], damagePackage);
            }


        }
    }

    public class BattleAttacker_Shoot : BattleAttacker
    {
        public int bulletNum;
        public GameObject bullet;

        public override void Attack(BattleRoleController attacker)
        {
            //监听发射发射出子弹
        }
    }

}