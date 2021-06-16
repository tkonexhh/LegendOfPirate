using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class SkillAction_Pull : SkillAction
    {
        private float m_Distance = 15.0f;
        private float m_Speed = 35.0f;
        private Vector3 m_EndPos;

        private Skill m_Skill;
        public SkillAction_Pull(float distance, float speed) //: base(owner)
        {
            this.m_Distance = distance;
            this.m_Speed = speed;
        }

        public override void ExcuteAction(Skill skill)
        {
            Debug.LogError("拉近啦");
            m_Skill = skill;
            Vector3 hitDir = skill.TargetInfo.Caster.transform.forward;
            Vector3 targetPos = skill.TargetInfo.Target.transform.position;
            Vector3 selfPos = skill.TargetInfo.Caster.transform.position;

            float distance = Mathf.Min(m_Distance, Vector3.Distance(selfPos, targetPos));

            m_EndPos = targetPos - hitDir * m_Distance;

            skill.Owner.AI.Pause();
            skill.TargetInfo.Target.onUpdate += OnUpdate;
            skill.Owner.MonoReference.onCollisionEnter += onCollisionEnter;
        }


        private void OnUpdate()
        {
            m_Skill.TargetInfo.Target.transform.position = Vector3.MoveTowards(m_Skill.TargetInfo.Target.transform.position, m_EndPos, m_Speed * Time.deltaTime);

            if (Vector3.Distance(m_Skill.TargetInfo.Target.transform.position, m_EndPos) < 0.01f)
            {
                OnPullEnd();
            }
        }

        private void onCollisionEnter(Collision other)
        {
            if (other.gameObject.layer == BattleHelper.GetOppoLayerByCamp(m_Skill.Owner.camp))
            {
                OnPullEnd();
            }
        }

        private void OnPullEnd()
        {
            m_Skill.TargetInfo.Target.onUpdate -= OnUpdate;
            m_Skill.Owner.MonoReference.onCollisionEnter -= onCollisionEnter;
            m_Skill.Owner.AI.Resume();
            m_Skill.SkillActionStepEnd();
        }
    }

}