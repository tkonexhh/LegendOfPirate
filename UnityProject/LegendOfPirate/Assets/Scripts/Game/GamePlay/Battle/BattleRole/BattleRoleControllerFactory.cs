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
            role.Renderer.prefab = configSO.prefab;
            for (int i = 0; i < configSO.childSkills.Count; i++)
            {
                role.Skill.AddSkill(SkillFactory.CreateSkill(configSO.childSkills[i]));
            }
            // role.Data.Sensor = BattleSensorFactory.CreateBattleSensor(configSO.PickTarget.PickTargetType, configSO.PickTarget.SensorTypeEnum);
            // role.Data.Attacker = BattleAttackerFactory.CreateBattleAttacker(configSO.Attack.AttackType);
            // role.Data.DamageRange = DamageRangeFactory.CreateDamageRange(configSO.Attack.DamageRangeType, role, configSO.Attack.RangeArgs);
            role.OnInit();
            return role;
        }
    }

}