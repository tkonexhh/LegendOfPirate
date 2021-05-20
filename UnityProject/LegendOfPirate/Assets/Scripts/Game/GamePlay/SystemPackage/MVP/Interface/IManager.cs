using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Qarth.Extension
{
	public interface IManager
    {
        void OnInit();
        void OnUpdate(float deltaTime);
        void OnDestroy();
	}
	
}