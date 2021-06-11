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


        protected float m_Timer;


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

        public virtual void Cast()
        {
            m_Timer = 0;
            if (onCast != null)
            {
                onCast();
            }
        }

        public void ExcuteSkill()
        {
            if (SkillActions != null)
            {
                for (int i = 0; i < SkillActions.Count; i++)
                {
                    SkillActions[i].ExcuteAction(this);
                }
            }
        }

        public virtual void Release()
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

    // /// <summary>
    // /// 主动技能
    // /// </summary>
    // public class InitiativeSkill : Skill, IDealDamage
    // {
    //     public BattleAttacker attacker;
    //     public DamageRange damageRange;

    //     public override void Cast()
    //     {
    //         m_Timer = 0;
    //         Debug.LogError("InitiativeSkill Cast");
    //         attacker.Attack(this, PicketTarget());
    //     }

    //     public override void Release()
    //     {
    //         damageRange = null;
    //         attacker = null;
    //     }

    //     #region IDealDamage
    //     public void DealDamage()
    //     {
    //         Debug.LogError("Skill Deal Damage");
    //         var targets = damageRange.PickTargets(Owner.camp);
    //         int damage = BattleHelper.CalcSkillDamage(Owner.Data.buffedData);
    //         for (int i = 0; i < targets.Count; i++)
    //         {
    //             RoleDamagePackage damagePackage = new RoleDamagePackage();
    //             damagePackage.damageType = BattleDamageType.Skill;
    //             damagePackage.damage = damage;
    //             BattleMgr.S.SendDamage(targets[i], damagePackage);
    //         }
    //     }

    //     //TODO修改实现
    //     public Vector3 DamageCenter()
    //     {
    //         return PicketTarget().transform.position;
    //     }

    //     //TODO修改实现
    //     public Vector3 DamageForward()
    //     {
    //         return PicketTarget().transform.forward;
    //     }

    //     public Transform DamageTransform()
    //     {
    //         return Owner.transform;
    //     }

    //     public DamageRange GetDamageRange()
    //     {
    //         return damageRange;
    //     }
    //     #endregion

    //}

}