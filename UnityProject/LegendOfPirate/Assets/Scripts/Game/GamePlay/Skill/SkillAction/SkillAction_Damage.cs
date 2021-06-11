using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class SkillAction_Damage : SkillAction
    {
        private int m_Damage;
        private BattleDamageType m_DamageType;
        private SkillTarget m_Target;

        public SkillAction_Damage(int damage, BattleDamageType damageType, SkillTarget target) //: base(owner)
        {
            this.m_Damage = damage;
            this.m_DamageType = damageType;
            this.m_Target = target;
        }

        public override void ExcuteAction(Skill skill)
        {
            RoleDamagePackage package = new RoleDamagePackage();
            package.damage = m_Damage;
            package.damageType = m_DamageType;
            BattleMgr.S.SendDamage(m_Target.PicketTarget(), package);
        }
    }

}