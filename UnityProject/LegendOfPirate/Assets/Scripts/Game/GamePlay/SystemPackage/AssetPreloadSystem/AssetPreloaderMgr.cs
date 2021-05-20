using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
	public class AssetPreloaderMgr : TSingleton<AssetPreloaderMgr>
	{
        private List<IAssetPreloader> m_AssetPreloaderList = new List<IAssetPreloader>();

        private int m_LoadDoneCount = 0;

        public void Init()
        {
            Log.i("Asset preloader start........");

        }

        private void AddAssetPreloader(IAssetPreloader assetPreloader)
        {
            if (!m_AssetPreloaderList.Contains(assetPreloader))
            {
                assetPreloader.StartPreload();
                m_AssetPreloaderList.Add(assetPreloader);
            }
        }

        public bool IsPreloaderDone()
        {
            return m_AssetPreloaderList.Count <= m_LoadDoneCount;
            //bool isDone = true;

            //foreach (IAssetPreloader assetPreloader in m_AssetPreloaderList)
            //{
            //    if (assetPreloader.IsLoadDone() == false)
            //    {
            //        isDone = false;
            //        break;
            //    }
            //}

            //return isDone;
        }

        public void OnLoadDone()
        {
            m_LoadDoneCount++;
        }
	}
	
}