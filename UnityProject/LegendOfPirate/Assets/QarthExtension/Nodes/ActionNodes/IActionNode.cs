using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public interface IActionNode : System.IDisposable
	{
        void OnStart();
        void OnTick();
        void OnEnd();

	}
	
}