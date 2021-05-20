using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    public interface IEventObserver
    {
        int[] InterestEvents { get; }
        /// <summary>
        /// Register interest events
        /// </summary>
        void RegisterEvents();
        void UnregisterEvents();

        void HandleEvent(int eventId, params object[] param);

        void SendEvent(EventID eventId, params object[] param);
    }
}
