﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattleMgr : TMonoSingleton<BattleMgr>, IMgr
    {
        [SerializeField] private GameObject m_RolePrefab;

        private ResLoader m_Loader;
        private List<IBattleComponent> m_BattleComponentList;


        public ResLoader loader => m_Loader;
        public BattleRendererComponent BattleRendererComponent { get; private set; }



        public override void OnSingletonInit()
        {
            m_Loader = ResLoader.Allocate("BattleMgr");

            m_BattleComponentList = new List<IBattleComponent>();
            BattleRendererComponent = AddComponent(new BattleRendererComponent()) as BattleRendererComponent;

        }

        private IBattleComponent AddComponent(IBattleComponent component)
        {
            m_BattleComponentList.Add(component);
            return component;
        }

        #region IMgr
        public void OnInit()
        {
            GameObjectPoolMgr.S.AddPool("BattleRole", m_RolePrefab, 1000, 100);
            for (int i = 0; i < m_BattleComponentList.Count; i++)
            {
                m_BattleComponentList[i].Init();
            }
        }

        public void OnUpdate()
        {
            BattleUpate();
        }

        public void OnDestroyed() { }
        #endregion




        public void BattleInit()
        {
            for (int i = 0; i < m_BattleComponentList.Count; i++)
            {
                m_BattleComponentList[i].OnBattleInit();
            }
        }

        public void BattleStart()
        {
            for (int i = 0; i < m_BattleComponentList.Count; i++)
            {
                m_BattleComponentList[i].OnBattleStart();
            }
        }

        public void BattleEnd()
        {

        }

        public void BattleClean()
        {
            for (int i = 0; i < m_BattleComponentList.Count; i++)
            {
                m_BattleComponentList[i].OnBattleClean();
            }

            Resources.UnloadUnusedAssets();
            GC.Collect();
        }

        public void BattleUpate()
        {
            for (int i = 0; i < m_BattleComponentList.Count; i++)
            {
                m_BattleComponentList[i].OnBattleUpdate();
            }
        }






    }

}