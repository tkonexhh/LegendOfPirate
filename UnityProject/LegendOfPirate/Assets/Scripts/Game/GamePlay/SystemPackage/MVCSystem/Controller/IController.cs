using Qarth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public interface IController : ICacheAble, ICacheType
	{
        void Init();
	}
	
}