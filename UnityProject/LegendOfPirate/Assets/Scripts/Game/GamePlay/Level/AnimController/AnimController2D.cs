using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

namespace GameWish.Game
{
    public class AnimController2D : IAnimateController
    {
        private SkeletonAnimation m_SkeletonAnimation;

        public AnimController2D(SkeletonAnimation sa)
        {
            m_SkeletonAnimation = sa;
            m_SkeletonAnimation.Initialize(false);
        }

        public void PlayAnim(string anim,bool loop)
        {
            m_SkeletonAnimation.AnimationState.SetAnimation(0, anim, loop);
        }

        public void PlayAnim(int anim, bool loop)
        {
        }
    }

}