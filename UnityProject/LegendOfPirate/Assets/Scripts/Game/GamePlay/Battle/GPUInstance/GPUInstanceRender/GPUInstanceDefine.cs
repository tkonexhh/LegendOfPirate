﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{

    public class GPUInstanceDefine
    {
        public static int MAX_CAPACITY = 1024;


    }

    public delegate void OnCellCreate(GPUInstanceCell cell);
    public delegate void OnCellDraw(MaterialPropertyBlock mpb);
}