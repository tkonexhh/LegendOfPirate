using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    public interface IAssetPreloader
    {
        string[] NeedPreloadAssets { get; }
        bool IsLoadDone();
        void StartPreload();

    }
}