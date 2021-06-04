using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class Skill
    {
        public int id;
        public string name;
        public float range;
        public float cd;
        public float timer;
        public IBattleSensor Sensor { get; set; }//技能选择器
        public Buff appendBuff;//附加Buff
        protected BattleRoleController m_Owner;

        public bool isReady => timer >= cd;


        public BattleRoleController PicketTarget()
        {
            return Sensor.PickTarget(m_Owner);
        }

        public virtual void Cast(BattleRoleController owner)
        {
            m_Owner = owner;
        }

        public void Update()
        {
            timer += Time.deltaTime;
            timer = Mathf.Clamp(timer, 0, cd);
        }

        //是否可以释放
        public bool CanCast()
        {
            var target = PicketTarget();
            if (target == null)
                return false;

            if (Vector3.Distance(target.transform.position, m_Owner.transform.position) < range)
                return true;

            return false;
        }

    }

    /// <summary>
    /// 主动技能
    /// </summary>
    public class InitiativeSkill : Skill, IDealDamage
    {
        public BattleAttacker attacker;
        public DamageRange damageRange;

        public override void Cast(BattleRoleController owner)
        {
            base.Cast(owner);
            timer = 0;
            Debug.LogError("InitiativeSkill Cast");
            attacker.Attack(this);
        }

        #region IDealDamage
        public void DealDamage()
        {
            Debug.LogError("Skill Deal Damage");
            var targets = damageRange.PickTargets(m_Owner.camp);
            int damage = BattleHelper.CalcSkillDamage(m_Owner.Data.buffedData);
            for (int i = 0; i < targets.Count; i++)
            {
                RoleDamagePackage damagePackage = new RoleDamagePackage();
                damagePackage.damageType = BattleDamageType.Skill;
                damagePackage.damage = damage;
                BattleMgr.S.SendDamage(targets[i], damagePackage);
            }
        }

        //TODO修改实现
        public Vector3 DamageCenter()
        {
            return PicketTarget().transform.position;
        }

        //TODO修改实现
        public Vector3 DamageForward()
        {
            return PicketTarget().transform.forward;
        }
        #endregion
    }

    /// <summary>
    /// 被动技能
    /// </summary>
    public class PassiveSkill : Skill
    {
        public SkillTrigger skillTrigger;

        public override void Cast(BattleRoleController owner)
        {
            base.Cast(owner);
            timer = cd;
            skillTrigger.onSkillTrigger += OnSkillTrigger;
            skillTrigger.Start(m_Owner);
        }

        public void Release()
        {
            skillTrigger.onSkillTrigger -= OnSkillTrigger;
            skillTrigger.Stop(m_Owner);
        }

        private void OnSkillTrigger()
        {
            if (timer >= cd)
            {
                timer = 0;
                if (appendBuff != null)
                    m_Owner.Buff.AddBuff(appendBuff);
            }

        }
    }

}