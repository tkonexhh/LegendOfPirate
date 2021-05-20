using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    /// <summary>
    /// 游戏重置，比如换地图/重生 等
    /// </summary>
	public interface IResetHandler
	{
        void OnReset();
	}
	
}