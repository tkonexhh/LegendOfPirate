using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class SkillAction_Bullet : SkillAction
    {
        private int m_Damage;
        private BulletConfigSO m_BulletConfigSO;
        private DamageRangeType m_DamageRangeType;
        private string m_RangeArgs;

        public SkillAction_Bullet(int damage, BulletConfigSO bulletConfig, DamageRangeType damageRangeType, string rangeArgs) //: base(owner)
        {
            m_Damage = damage;
            m_BulletConfigSO = bulletConfig;
            m_DamageRangeType = damageRangeType;
            m_RangeArgs = rangeArgs;
        }

        public override void ExcuteAction(Skill skill)
        {
            Bullet bullet = BulletFactory.CreateBullet(m_BulletConfigSO, skill.TargetInfo.Target.transform);
            bullet.owner = skill.Owner;
            bullet.Damage = m_Damage;

            var damageRange = DamageRangeFactory.CreateDamageRange(m_DamageRangeType, skill.Owner, m_RangeArgs);
            bullet.DamageRange = damageRange;
            bullet.Init(skill.Owner.transform);
            BattleMgr.S.Bullet.AddBullet(bullet);

            skill.SkillActionStepEnd();
        }
    }

}