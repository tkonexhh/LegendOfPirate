using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using System;

namespace GameWish.Game
{
	public interface IActionNode : System.IDisposable, ICacheType, ICacheAble
	{
        void OnStart();
        void OnTick();
        void OnEnd();

        bool IsFinished { get;}

        void Execute();

        ActionNode AddOnStartCallback(Action<ActionNode> callback);
        ActionNode AddOnTickCallback(Action<ActionNode> callback);
        ActionNode AddOnEndCallback(Action<ActionNode> callback);

    }
	
}