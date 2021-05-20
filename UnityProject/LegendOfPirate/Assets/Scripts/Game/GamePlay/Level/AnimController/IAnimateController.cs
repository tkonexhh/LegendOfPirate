using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public interface IAnimateController
	{
        void PlayAnim(int anim, bool loop);
        void PlayAnim(string anim,bool loop);
	}
	
}