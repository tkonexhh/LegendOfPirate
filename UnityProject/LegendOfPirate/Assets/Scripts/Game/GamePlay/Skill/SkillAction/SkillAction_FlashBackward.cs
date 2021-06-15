using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class SkillAction_FlashBackward : SkillAction
    {
        private float m_Distance;
        public SkillAction_FlashBackward(float distance) //: base(owner)
        {
            this.m_Distance = distance;
        }

        public override void ExcuteAction(Skill skill)
        {
            Vector3 targetPos = skill.Owner.transform.position;
            Vector3 forward = skill.Owner.transform.forward;
            float radius = skill.Owner.MonoReference.Collider.radius;

            skill.Owner.AI.Pause();

            targetPos -= forward * radius * m_Distance;
            skill.Owner.transform.position = targetPos;

            skill.Owner.AI.Resume();
            skill.SkillActionStepEnd();
        }
    }

}