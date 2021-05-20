using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class ConditionCheckerBase<T> : IConditionChecker
    {
        protected T m_TargetValue;

        public virtual void Init(T targetValue)
        {
            m_TargetValue = targetValue;
        }

        public virtual string GetCurrentValue()
        {
            return string.Empty;
        }

        public virtual string GetTargetValue()
        {
            return m_TargetValue.ToString();
        }

        public virtual bool IsFinished()
        {
            return false;
        }

        public virtual float GetProgressPercent()
        {
            return float.Parse(GetCurrentValue())/(float.Parse(GetTargetValue()));
        }
    }

}