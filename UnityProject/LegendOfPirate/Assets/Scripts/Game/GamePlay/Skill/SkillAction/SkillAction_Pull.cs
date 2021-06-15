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

            m_EndPos = targetPos + hitDir * m_Distance;

            skill.Owner.AI.Pause();
            skill.TargetInfo.Target.onUpdate += OnUpdate;
        }


        private void OnUpdate()
        {
            m_Skill.TargetInfo.Target.transform.position = Vector3.MoveTowards(m_Skill.TargetInfo.Target.transform.position, m_EndPos, m_Speed * Time.deltaTime);

            if (Vector3.Distance(m_Skill.TargetInfo.Target.transform.position, m_EndPos) < 0.01f)
            {
                OnHitBackEnd();
            }
        }

        private void OnHitBackEnd()
        {
            m_Skill.TargetInfo.Target.onUpdate -= OnUpdate;
            m_Skill.Owner.AI.Resume();
            m_Skill.SkillActionStepEnd();
        }
    }

}