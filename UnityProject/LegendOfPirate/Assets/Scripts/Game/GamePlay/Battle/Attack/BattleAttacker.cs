using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public abstract class BattleAttacker
    {
        public abstract void Attack(BattleRoleController attacker, BattleRoleController target);
    }

    public class BattleAttacker_Lock : BattleAttacker
    {
        public override void Attack(BattleRoleController attacker, BattleRoleController target)
        {
            attacker.DealDamage();
        }
    }

    public class BattleAttacker_Shoot : BattleAttacker
    {
        public int bulletNum;
        public BulletConfigSO bulletSO;

        public override void Attack(BattleRoleController attacker, BattleRoleController target)
        {
            //监听发射发射出子弹
            for (int i = 0; i < bulletNum; i++)
            {
                Bullet bullet = BulletFactory.CreateBullet(bulletSO, target.transform);
                bullet.owner = attacker;
                bullet.RangeDamage = attacker.Data.RangeDamage;
                bullet.Damage = attacker.Data.buffedData.ATK;
                bullet.Init(attacker.MonoReference.ShootPos);
                BattleMgr.S.Bullet.AddBullet(bullet);
            }

            //子弹带有attacker信息
        }
    }

}