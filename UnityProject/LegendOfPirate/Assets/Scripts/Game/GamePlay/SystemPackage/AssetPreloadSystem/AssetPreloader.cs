using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class AssetPreloader : IAssetPreloader
    {
        protected bool m_IsLoadDone = false;

        #region IAssetPreloader

        public string[] NeedPreloadAssets => m_NeedPreloadAssets;

        protected string[] m_NeedPreloadAssets;

        private ResLoader m_ResLoader = null;

        public bool IsLoadDone()
        {
            return m_IsLoadDone;
        }

        public void StartPreload()
        {
            m_IsLoadDone = false;

            m_ResLoader = ResLoader.Allocate();

            SetNeedPreloadAssets();

            if (m_NeedPreloadAssets.Length > 0)
            {
                for (int i = 0; i < m_NeedPreloadAssets.Length; i++)
                {
                    m_ResLoader.Add2Load(m_NeedPreloadAssets[i]);
                }

                m_ResLoader.LoadAsync(OnResLoadFinish);
            }
            else
            {
                OnResLoadFinish();
            }
        }

        protected virtual void SetNeedPreloadAssets()
        {

        }

        private void OnResLoadFinish()
        {
            m_IsLoadDone = true;

            Log.i("Asset preload finish....");
        }

        #endregion
    }

}