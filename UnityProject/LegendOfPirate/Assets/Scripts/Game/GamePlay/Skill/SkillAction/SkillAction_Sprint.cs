using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class SkillAction_Sprint : SkillAction
    {
        private float m_Range;
        private float m_Speed;
        private SkillTargetType m_Target;

        private Skill m_Skill;

        private Sprint m_Sprint;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="range"></param>
        /// <param name="time"></param>
        /// <param name="target"> 施法者的话 就直接朝向前方，目标的话 就直接跑向目标冲刺</param>
        public SkillAction_Sprint(float range, float speed, SkillTargetType target) //: base(owner)
        {
            this.m_Range = range;
            this.m_Speed = speed;
            this.m_Target = target;
        }

        public override void ExcuteAction(Skill skill)
        {
            Debug.LogError("冲刺啦！");
            if (m_Target == SkillTargetType.Caster)
            {
                m_Sprint = new Sprint_Forward(skill, m_Range, m_Speed);
            }
            else
            {
                m_Sprint = new Sprint_Target(skill, m_Range, m_Speed);
            }

            m_Sprint.OnExcute();
            m_Sprint.onEnd += OnSprintEnd;
            m_Skill = skill;
            skill.Owner.AI.Pause();
            skill.Owner.onUpdate += OnUpdate;
            skill.Owner.MonoReference.onCollisionEnter += onCollisionEnter;
        }

        private void OnUpdate()
        {
            m_Sprint.OnUpdate();
        }

        private void onCollisionEnter(Collision other)
        {
            if (other.gameObject.layer == BattleHelper.GetOppoLayerByCamp(m_Skill.Owner.camp))
            {
                OnSprintEnd();
            }
        }

        private void OnSprintEnd()
        {
            m_Sprint.onEnd -= OnSprintEnd;
            m_Sprint = null;
            m_Skill.Owner.onUpdate -= OnUpdate;
            m_Skill.Owner.MonoReference.onCollisionEnter -= onCollisionEnter;
            m_Skill.Owner.AI.Resume();
            m_Skill.Owner.MonoReference.Rigidbody.velocity = Vector3.zero;
            Debug.LogError("冲刺结束");
            m_Skill.SkillActionStepEnd();
        }

        class Sprint
        {
            public Vector3 EndPos;
            protected Skill Skill;
            protected float Range;
            protected float Speed;

            public Run onEnd;

            public Sprint(Skill skill, float range, float speed)
            {
                this.Skill = skill;
                this.Range = range;
                this.Speed = speed;
            }

            public virtual void OnExcute() { }
            public virtual void OnUpdate() { }
        }

        class Sprint_Forward : Sprint
        {
            public Sprint_Forward(Skill skill, float range, float speed) : base(skill, range, speed) { }

            public override void OnExcute()
            {
                Vector3 direction = Skill.Owner.transform.forward;
                float minRange = Mathf.Min(Range, Vector3.Distance(Skill.Owner.transform.position, Skill.TargetInfo.Target.transform.position));
                EndPos = Skill.Owner.transform.position + direction * (minRange - Skill.Owner.MonoReference.Collider.radius);
            }

            public override void OnUpdate()
            {
                Skill.Owner.transform.position = Vector3.MoveTowards(Skill.Owner.transform.position, EndPos, Speed * Time.deltaTime);

                if (Vector3.Distance(Skill.Owner.transform.position, EndPos) < 0.01f)
                {
                    if (onEnd != null)
                        onEnd();
                }
            }
        }

        class Sprint_Target : Sprint
        {
            private float m_MoveDistance;
            public Sprint_Target(Skill skill, float range, float speed) : base(skill, range, speed) { }

            public override void OnExcute()
            {
                m_MoveDistance = 0;
            }

            public override void OnUpdate()
            {
                Vector3 oldPos = Skill.Owner.transform.position;
                EndPos = Skill.TargetInfo.Target.transform.position - Skill.Owner.transform.forward * Skill.Owner.MonoReference.Collider.radius;
                Skill.Owner.transform.position = Vector3.MoveTowards(oldPos, EndPos, Speed * Time.deltaTime);
                m_MoveDistance += Vector3.Distance(oldPos, Skill.Owner.transform.position);
                if (m_MoveDistance >= Range)
                {
                    if (onEnd != null)
                        onEnd();
                }

                if (Vector3.Distance(Skill.Owner.transform.position, EndPos) < 0.01f)
                {
                    if (onEnd != null)
                        onEnd();
                }
            }
        }
    }



}