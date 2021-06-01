using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameWish.Game
{
	public class DailyRefreshTest : MonoBehaviour
	{
        private IEnumerator Start()
        {
            yield return new WaitForSeconds(2);

            DailyRefreshMgr.S.Register(DateTime.Now + TimeSpan.FromHours(-1), 16, () =>
            {
                Debug.LogError("Test1");
            });

            DailyRefreshMgr.S.Register(DateTime.Now + TimeSpan.FromHours(1), 16, () =>
            {
                Debug.LogError("Test2");
            });
        }
    }
	
}