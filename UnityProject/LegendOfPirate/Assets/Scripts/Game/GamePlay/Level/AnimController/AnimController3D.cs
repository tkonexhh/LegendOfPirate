using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    public class AnimController3D : IAnimateController
    {
        private Animator m_Animator;

        public AnimController3D(Animator animator)
        {
            m_Animator = animator;
        }

        public Animator Animator { get => m_Animator; }

        public void PlayAnim(string anim, bool loop)
        {
            if (m_Animator != null) {
                if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName(anim))
                {
                    return;
                }
                m_Animator.SetTrigger(anim);
            }            
        }

        public void PlayAnim(int anim, bool loop)
        {
        }
    }
}