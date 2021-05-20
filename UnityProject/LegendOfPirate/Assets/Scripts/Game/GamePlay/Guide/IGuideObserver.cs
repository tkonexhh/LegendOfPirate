using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    public interface IGuideObserver
    {
        void InitObjectByGuideStep();
        void OnGuideStepChanged();
    }
}