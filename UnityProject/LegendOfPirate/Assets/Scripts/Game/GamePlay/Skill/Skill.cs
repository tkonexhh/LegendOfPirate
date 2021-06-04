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
        public float value;
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
    public class InitiativeSkill : Skill
    {
        public BattleAttacker attacker;
        public DamageRange damageRange;

        public override void Cast(BattleRoleController owner)
        {
            base.Cast(owner);
            damageRange.owner = owner;
            timer = 0;
            Debug.LogError("InitiativeSkill Cast");
            // attacker.Attack(owner, PicketTarget());
        }


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