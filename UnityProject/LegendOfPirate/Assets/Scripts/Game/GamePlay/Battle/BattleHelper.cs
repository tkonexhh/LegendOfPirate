using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattleHelper
    {

        public static BattleCamp GetOppositeCamp(BattleCamp camp)
        {
            if (camp == BattleCamp.Our)
            {
                return BattleCamp.Enemy;
            }
            else
            {
                return BattleCamp.Our;
            }
        }


        public static int CalcAtkDamage(BattleRoleRuntimeModel model)
        {
            int damage = model.ATK;
            //判断是否暴击
            if (RandomHelper.Range(0, 100) < model.CriticalRate)
            {
                // Debug.LogError("暴击");
                damage *= 2;
            }

            return damage;
        }

        public static int CalcATKHurt(int damage, BattleRoleRuntimeModel model)
        {
            damage = (int)(damage / (1.0f + model.Amor));
            return damage;
        }

        public static int CalcSkillHurt(int damage, BattleRoleRuntimeModel model)
        {
            return 0;
        }

    }

}