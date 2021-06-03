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


        public BattleRoleController PicketTarget(BattleRoleController picker)
        {
            return Sensor.PickTarget(picker);
        }

        public virtual void Cast(BattleRoleController owner)
        {
            m_Owner = owner;
        }
    }

    /// <summary>
    /// 主动技能
    /// </summary>
    public class InitiativeSkill : Skill
    {
        // public AttackType attackType;



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
                if (appendBuff != null)
                    m_Owner.Buff.AddBuff(appendBuff);
            }

        }
    }

}