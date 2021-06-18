using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public abstract class BattleAttacker
    {
        public abstract void Attack(IDealDamage attacker, BattleRoleController target);
    }

    public class BattleAttacker_Lock : BattleAttacker
    {
        public override void Attack(IDealDamage attacker, BattleRoleController target)
        {
            //TODO 监听动画攻击事件
            // DamageRangeFactory.CreateDamageRange
            attacker.DealDamage();
        }
    }

    public class BattleAttacker_Shoot : BattleAttacker
    {
        public int bulletNum;
        public BulletConfigSO bulletSO;

        public override void Attack(IDealDamage attacker, BattleRoleController target)
        {
            //监听发射发射出子弹
            for (int i = 0; i < bulletNum; i++)
            {
                Bullet bullet = BulletFactory.CreateBullet(bulletSO, target.transform);
                bullet.owner = attacker;
                bullet.RangeDamage = attacker.GetRangeDamage();
                bullet.Init(attacker.DamageTransform());
                BattleMgr.S.Bullet.AddBullet(bullet);
            }

            //子弹带有attacker信息
        }
    }

}