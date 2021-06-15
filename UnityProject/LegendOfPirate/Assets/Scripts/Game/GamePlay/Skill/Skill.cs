using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class Skill : ICacheAble
    {
        public int id;
        public string name;
        public float range;
        public float CD;

        public IBattleSensor Sensor { get; set; }//技能选择器
        public BattleRoleController Owner { get; private set; }
        public SkillTrigger skillTrigger { get; set; }//技能触发器
        public List<SkillAction> SkillActions { get; set; }

        public bool isReady => CD != SkillDefine.INFINITETIME && m_Timer >= CD;

        public Run onCreate;//技能创建时
        public Run onCast;//技能释放时
        private SkillTargetInfo m_TargetInfo;

        public SkillTargetInfo TargetInfo => m_TargetInfo;

        public Run onSkillEnd;

        protected float m_Timer;
        private int m_ActionStep = 0;

        public void OnCreate(BattleRoleController owner)
        {
            Owner = owner;
            skillTrigger.Start(this);
            if (onCreate != null)
            {
                onCreate();
            }
            m_Timer = 0;
        }

        public void Cast()
        {
            m_Timer = 0;
            if (onCast != null)
            {
                onCast();
            }

        }

        public void ExcuteSkill()
        {
            var target = Sensor.PickTarget(Owner);
            m_TargetInfo = new SkillTargetInfo();
            m_TargetInfo.Caster = Owner;
            m_TargetInfo.Target = target;
            if (SkillActions != null)
            {
                m_ActionStep = 0;
                SkillActions[m_ActionStep].ExcuteAction(this);
                // for (int i = 0; i < SkillActions.Count; i++)
                // {
                //     SkillActions[i].ExcuteAction(this);
                // }
            }
            else
            {
                if (onSkillEnd != null)
                {
                    onSkillEnd();
                }
            }
        }

        public void SkillActionStepEnd()
        {
            m_ActionStep++;
            if (m_ActionStep >= SkillActions.Count)
            {
                if (onSkillEnd != null)
                {
                    onSkillEnd();
                }
            }
            else
            {
                SkillActions[m_ActionStep].ExcuteAction(this);
            }
        }


        public void Release()
        {
            skillTrigger.Stop(this);
        }


        public void Update()
        {
            if (CD == SkillDefine.INFINITETIME) return;

            m_Timer += Time.deltaTime;
            m_Timer = Mathf.Clamp(m_Timer, 0, CD);
        }

        public BattleRoleController PicketTarget()
        {
            return Sensor.PickTarget(Owner);
        }

        //是否可以释放
        public bool CanCast()
        {
            var target = PicketTarget();
            if (target == null)
                return false;

            if (Vector3.Distance(target.transform.position, Owner.transform.position) < range)
                return true;

            return false;
        }



        #region ICacheAble

        public bool cacheFlag
        {
            get; set;
        }

        public virtual void OnCacheReset()
        {
            Owner = null;
            Sensor = null;
        }

        #endregion

    }

}