﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public interface IModel
	{
        void Init();

        IModel DeepCopy();
	}
	
}