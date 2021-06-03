using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BattleAttackerFactory
    {
        public static BattleAttacker CreateBattleAttacker(AttackType attackType)
        {
            switch (attackType)
            {
                case AttackType.Lock:
                    return new BattleAttacker_Lock();
                case AttackType.Shoot:
                    return new BattleAttacker_Shoot();
            }

            return null;
        }

        public static void SetBullet(BattleAttacker_Shoot attacker, GameObject bullet, int num)
        {
            attacker.bullet = bullet;
            attacker.bulletNum = num;
        }
    }

}