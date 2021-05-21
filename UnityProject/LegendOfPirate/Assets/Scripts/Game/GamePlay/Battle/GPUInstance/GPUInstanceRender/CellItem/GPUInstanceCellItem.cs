using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    public class GPUInstanceCellItem
    {
        public Vector3 pos;
        public Quaternion rotation = Quaternion.identity;
        public Vector3 scale;

        public int cellIndex;
    }
}