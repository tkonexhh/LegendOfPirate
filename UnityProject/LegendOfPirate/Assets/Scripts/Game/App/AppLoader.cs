using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class AppLoader : MonoBehaviour
    {
        private void Awake()
        {
            Log.i("Init[{0}]", ApplicationMgr.S.GetType().Name);            
        }

        private void Start()
        {
            Destroy(gameObject);
        }

        
    }
}
