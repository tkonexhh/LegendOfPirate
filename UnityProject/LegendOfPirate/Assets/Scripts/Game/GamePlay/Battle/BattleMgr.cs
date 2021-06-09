﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattleMgr : TMonoSingleton<BattleMgr>, IMgr
    {
        public GameObject m_RolePrefab;
        [SerializeField] private BuffConfigSO m_DemoBuffSO;

        public RoleConfigSO DemoRoleSO;
        public SkillConfigSO DemoSkillSO;

        private List<IBattleComponent> m_BattleComponentList;
        public bool Started { get; private set; }


        public BattleRendererComponent BattleRendererComponent { get; private set; }



        public override void OnSingletonInit()
        {

            m_BattleComponentList = new List<IBattleComponent>();
            AddComponent(new BattlePoolComponent());
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
            Started = true;
            for (int i = 0; i < m_BattleComponentList.Count; i++)
            {
                m_BattleComponentList[i].OnBattleStart();
            }
        }

        public void BattleEnd(bool isSuccess)
        {
            Started = false;
            Debug.LogError("BattleEnd:" + isSuccess);
            for (int i = 0; i < m_BattleComponentList.Count; i++)
            {
                m_BattleComponentList[i].OnBattleEnd(isSuccess);
            }
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

            //XXX test add buff
            if (Input.GetKeyDown(KeyCode.W))
            {
                Buff buff = BuffFactory.CreateBuff(m_DemoBuffSO);
                for (int i = 0; i < BattleRendererComponent.ourControllers.Count; i++)
                {
                    CreateBuff(BattleRendererComponent.ourControllers[i], buff);
                }
            }
        }



        #region Buff
        public void CreateBuff(BattleRoleController controller, Buff buff)
        {
            controller.Buff.AddBuff(buff);
        }

        public void SendDamage(BattleRoleController controller, RoleDamagePackage roleDamagePackage)
        {
            if (controller.AI.onHurt != null)
            {
                controller.AI.onHurt();
            }
            controller.Data.GetDamage(roleDamagePackage);
        }
        #endregion


    }

}