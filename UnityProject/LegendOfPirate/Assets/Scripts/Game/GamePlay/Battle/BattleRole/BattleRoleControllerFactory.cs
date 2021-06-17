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
            //TODO 
            BattleMgr.S.Pool.AddGameObjectToPool(configSO.prefab);
            // role.Renderer.prefab = configSO.prefab;
            role.Renderer.prefabName = configSO.prefab.name;
            for (int i = 0; i < configSO.childSkills.Count; i++)
            {
                role.Skill.AddSkill(SkillFactory.CreateSkill(configSO.childSkills[i]));
            }
            role.Data.AtkRange = configSO.AtkRange;
            role.Data.Sensor = BattleSensorFactory.CreateBattleSensor(configSO.PickTarget.PickTargetType, configSO.PickTarget.SensorTypeEnum);
            role.Data.Attacker = BattleAttackerFactory.CreateBattleAttacker(configSO.Attack.AttackType);
            if (role.Data.Attacker is BattleAttacker_Shoot attacker)//配置远程攻击的子弹
            {
                BattleAttackerFactory.SetBullet(attacker, configSO.Attack.Bullet, configSO.Attack.BulletNum);
            }
            role.Data.DamageRange = DamageRangeFactory.CreateDamageRange(configSO.Attack.DamageRangeType, role, configSO.Attack.RangeArgs);
            role.OnInit();
            return role;
        }
    }

}