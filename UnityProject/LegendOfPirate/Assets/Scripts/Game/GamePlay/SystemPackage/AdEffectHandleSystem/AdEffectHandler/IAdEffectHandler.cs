using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
	public interface IAdEffectHandler 
	{
        void Handle(object[] args);
        AdType GetAdType();
        float GetLeftTime();
	}
	
}