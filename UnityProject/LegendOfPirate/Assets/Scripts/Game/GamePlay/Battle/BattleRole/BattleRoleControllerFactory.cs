using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattleRoleControllerFactory : ControllerFactory<BattleRoleController>
    {
        public static BattleRoleController CreateBattleRole(RoleConfigSO configSO)
        {
            BattleRoleController role = ObjectPool<BattleRoleController>.S.Allocate();
            BattleMgr.S.Pool.AddGameObjectToPool(configSO.prefab);//将角色预制体添加到池
            // role.Renderer.prefab = configSO.prefab;
            role.Renderer.prefabName = configSO.prefab.name;
            for (int i = 0; i < configSO.childSkills.Count; i++)
            {
                role.Skill.AddSkill(SkillFactory.CreateSkill(configSO.childSkills[i]));
            }
            role.Data.AtkRange = configSO.AtkRange;
            role.Data.ColliderRange = configSO.ColliderRange;
            role.Data.Sensor = BattleSensorFactory.CreateBattleSensor(configSO.PickTarget.PickTargetType, configSO.PickTarget.SensorTypeEnum);
            role.Data.Attacker = BattleAttackerFactory.CreateBattleAttacker(configSO.Attack.AttackType);
            if (role.Data.Attacker is BattleAttacker_Shoot attacker)//配置远程攻击的子弹
            {
                BattleMgr.S.Pool.AddGameObjectToPool(configSO.Attack.Bullet.Prefab);//将子弹预制体添加到池
                BattleAttackerFactory.SetBullet(attacker, configSO.Attack.Bullet, configSO.Attack.BulletNum);
            }

            if (configSO.Attack.DamageRangeType == DamageRangeType.Range)
            {
                role.Data.RangeDamage = RangeDamageConfig.CreateRangeDamage(configSO.Attack.RangeDamage);
            }


            role.OnInit();
            return role;
        }


        public static void RecycleBattleRole(BattleRoleController controller)
        {
            controller.Recycle2Cache();
            ObjectPool<BattleRoleController>.S.Recycle(controller);
        }
    }

}