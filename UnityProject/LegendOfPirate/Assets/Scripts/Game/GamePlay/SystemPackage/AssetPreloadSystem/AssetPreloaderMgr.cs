using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using System.Reflection;
using System;

namespace GameWish.Game
{
	public class AssetPreloaderMgr : TSingleton<AssetPreloaderMgr>, IMgr
	{
        private List<IAssetPreloader> m_AssetPreloaderList = new List<IAssetPreloader>();

        #region IMgr

        public void OnInit()
        {
            AutoRegisterAllAssetPreloaders();
        }

        public void OnUpdate()
        {
        }

        public void OnDestroyed()
        {
        }

        #endregion

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
            bool isDone = true;

            foreach (IAssetPreloader assetPreloader in m_AssetPreloaderList)
            {
                if (assetPreloader.IsLoadDone() == false)
                {
                    isDone = false;
                    break;
                }
            }

            return isDone;
        }

        public void Release()
        {
            m_AssetPreloaderList.Clear();
            m_AssetPreloaderList = null;
        }

        #region Private

        private void AutoRegisterAllAssetPreloaders()
        {
            Assembly assembly = AssemblyHelper.DefaultCSharpAssembly;
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.GetCustomAttribute<AssetAutoPreloadAttribute>() != null)
                    {
                        object preloaderObj = Activator.CreateInstance(type);
                        IAssetPreloader preloader = (IAssetPreloader)preloaderObj;
                        if (preloader != null)
                        {
                            if (!m_AssetPreloaderList.Contains(preloader))
                            {
                                m_AssetPreloaderList.Add(preloader);
                            }
                        }
                        else
                        {
                            Log.e("IAssetPreloader class not found!");
                        }
                    }

                }
            }
        }

        #endregion
    }

}