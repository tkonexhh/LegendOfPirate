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

            StartPreload();
        }

        public void OnUpdate()
        {
        }

        public void OnDestroyed()
        {
        }

        #endregion

        #region Public

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
            foreach (IAssetPreloader assetPreloader in m_AssetPreloaderList)
            {
                assetPreloader.Release();
            }

            m_AssetPreloaderList.Clear();
            m_AssetPreloaderList = null;
        }

        #endregion

        #region Private

        private void AddAssetPreloader(IAssetPreloader assetPreloader)
        {
            if (assetPreloader != null)
            {
                if (!m_AssetPreloaderList.Contains(assetPreloader))
                {
                    m_AssetPreloaderList.Add(assetPreloader);
                }
                else
                {
                    Log.e("IAssetPreloader Already Exists!");
                }
            }
            else
            {
                Log.e("IAssetPreloader class not found!");
            }
        }

        private void StartPreload()
        {
            for (int i = 0; i < m_AssetPreloaderList.Count; i++)
            {
                m_AssetPreloaderList[i].StartPreload();
            }
        }

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

                        AddAssetPreloader(preloader);                       
                    }

                }
            }
        }

        #endregion
    }

}