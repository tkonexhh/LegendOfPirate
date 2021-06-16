using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class SkillAction_FlashForward : SkillAction
    {
        private SkillFashForwardType m_ForwardType;
        public SkillAction_FlashForward(SkillFashForwardType forwardType)
        {
            this.m_ForwardType = forwardType;
        }

        public override void ExcuteAction(Skill skill)
        {
            Debug.LogError("闪现啦");
            skill.SkillActionStepEnd();
            skill.Owner.AI.Pause();
            Vector3 targetPos = skill.TargetInfo.Target.transform.position;
            Vector3 forward = skill.Owner.transform.forward;
            float radius = skill.Owner.MonoReference.Collider.radius;

            if (m_ForwardType == SkillFashForwardType.Front)
            {
                targetPos -= forward * radius;
            }
            else
            {
                targetPos += forward * radius;
            }
            skill.Owner.transform.position = targetPos;
            skill.Owner.AI.Resume();
        }
    }

}