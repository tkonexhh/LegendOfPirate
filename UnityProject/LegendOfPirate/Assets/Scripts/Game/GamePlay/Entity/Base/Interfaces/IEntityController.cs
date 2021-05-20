using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    public interface IEntityController
    {
        void Init();
        void Release();
        void Update();
        void Reset();
    }
}
