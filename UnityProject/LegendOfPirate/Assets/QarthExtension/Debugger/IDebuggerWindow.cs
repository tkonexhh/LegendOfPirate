using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public interface IDebuggerWindow : IElement
	{
        void OnOpen();
        void OnClose();
	}
	
}