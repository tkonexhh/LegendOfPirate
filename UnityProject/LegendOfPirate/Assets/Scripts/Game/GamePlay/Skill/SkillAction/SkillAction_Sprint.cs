using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class SkillAction_Sprint : SkillAction
    {
        private float m_Range;
        private float m_Speed;
        private SkillTargetType m_Target;

        private Skill m_Skill;
        private Vector3 m_Direction;
        private Vector3 m_EndPos;

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
            m_Target = target;
        }

        public override void ExcuteAction(Skill skill)
        {
            Debug.LogError("冲刺啦！");
            skill.Owner.Renderer.CrossFadeAnim(BattleDefine.ROLEANIM_RUN, 0.1f);
            m_Skill = skill;
            skill.Owner.AI.Pause();
            m_Direction = skill.Owner.transform.forward;
            m_EndPos = skill.Owner.transform.position + m_Direction.normalized * m_Range;
            m_EndPos.y = skill.Owner.transform.position.y;
            skill.Owner.onUpdate += OnUpdate;
        }

        private void OnUpdate()
        {
            m_Skill.Owner.transform.position = Vector3.MoveTowards(m_Skill.Owner.transform.position, m_EndPos, m_Speed * Time.deltaTime);
            if (Vector3.Distance(m_Skill.Owner.transform.position, m_EndPos) < 0.01f)
            {
                m_Skill.Owner.onUpdate -= OnUpdate;
                OnSprintEnd();
            }
        }

        private void OnSprintEnd()
        {
            m_Skill.Owner.AI.Resume();
            m_Skill.Owner.MonoReference.Rigidbody.velocity = Vector3.zero;
            Debug.LogError("冲刺结束");
        }
    }

}