using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class AssetPreloader : IAssetPreloader
    {
        protected bool m_IsLoadDone = false;

        #region IAssetPreloader

        public virtual string[] NeedPreloadAsset => throw new System.NotImplementedException();

        public bool IsLoadDone()
        {
            return m_IsLoadDone;
        }

        public virtual void StartPreload()
        {
            m_IsLoadDone = false;

        }

        #endregion
    }

}