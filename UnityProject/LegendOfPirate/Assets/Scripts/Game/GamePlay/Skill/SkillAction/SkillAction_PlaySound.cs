using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class SkillAction_PlaySound : SkillAction
    {
        private AudioClip m_AudioClip;
        private SkillTargetType m_Target;

        public SkillAction_PlaySound(AudioClip audioClip, SkillTargetType target) //: base(owner)
        {
            this.m_AudioClip = audioClip;
            this.m_Target = target;
        }

        public override void ExcuteAction(Skill skill)
        {
            Debug.LogError("播放音效啦!");
            skill.SkillActionStepEnd();
            switch (m_Target)
            {
                case SkillTargetType.Caster:
                    // skill.TargetInfo.Caster.Data.buffedData.Hp += m_HealAmount;
                    break;
                case SkillTargetType.Target:
                    // skill.TargetInfo.Target.Data.buffedData.Hp += m_HealAmount;
                    break;
            }
        }
    }

}