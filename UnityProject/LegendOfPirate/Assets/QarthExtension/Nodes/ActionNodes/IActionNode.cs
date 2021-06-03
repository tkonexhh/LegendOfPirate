using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
	public interface IActionNode : System.IDisposable, ICacheType, ICacheAble
	{
        void OnStart();
        void OnTick();
        void OnEnd();

        bool IsFinished { get;}

        void Execute();
	}
	
}