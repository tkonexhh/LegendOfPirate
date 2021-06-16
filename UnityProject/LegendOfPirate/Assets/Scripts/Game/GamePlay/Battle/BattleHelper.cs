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

        public static int GetOppoLayerByCamp(BattleCamp camp)
        {
            if (camp == BattleCamp.Our)
            {
                return LayerDefine.LAYER_ROLE_ENEMY;
            }
            else
            {
                return LayerDefine.LAYER_ROLE_OUR;
            }
        }

        public static int GetLayerByCamp(BattleCamp camp)
        {
            if (camp == BattleCamp.Our)
            {
                return LayerDefine.LAYER_ROLE_OUR;
            }
            else
            {
                return LayerDefine.LAYER_ROLE_ENEMY;
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

        public static int CalcAtkDamage(int atk)
        {
            return atk;
        }

        public static int CalcSkillDamage(BattleRoleRuntimeModel model)
        {
            int damage = 1;
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