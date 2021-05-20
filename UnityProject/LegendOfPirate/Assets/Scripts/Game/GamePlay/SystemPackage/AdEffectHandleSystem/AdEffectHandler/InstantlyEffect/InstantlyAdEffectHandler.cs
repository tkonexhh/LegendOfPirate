using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    public abstract class InstantlyAdEffectHandler : IAdEffectHandler
    {
        private AdType m_AdType;

        #region Public methods
        public InstantlyAdEffectHandler(AdType adType)
        {
            m_AdType = adType;
        }
        #endregion

        #region IAdEffectHandler
        public virtual void Handle(object[] args)
        {

        }

        public AdType GetAdType()
        {
            return m_AdType;
        }

        public float GetLeftTime()
        {
            return 0f;
        }
        #endregion
    }

}