using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public class RewardHandlerBase<T> : IRewardHandler
	{
        protected T m_Value;

        public virtual void Init(T value)
        {
            m_Value = value;
        }

        public virtual void OnRewardClaimed()
        {

        }
    }
	
}