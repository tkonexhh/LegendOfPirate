using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using Sirenix.OdinInspector;
using Cinemachine;

namespace GameWish.Game
{
    public class BattleMgr : TMonoSingleton<BattleMgr>, IMgr
    {
        public GameObject m_RolePrefab;
        [SerializeField] private BuffConfigSO m_DemoBuffSO;

        public RoleConfigSO DemoRoleSO;
        public SkillConfigSO DemoSkillSO;
        public BattleFieldConfigSO DemoEnemyFieldConfigSO;
        private int m_LevelId;

        //===
        [LabelText("战场相机"), BoxGroup("Camera")] public CinemachineVirtualCameraBase CameraBattle;
        [LabelText("排兵相机"), BoxGroup("Camera")] public CinemachineVirtualCameraBase CameraField;
        //---

        private List<IBattleComponent> m_BattleComponentList;
        public bool Started { get; private set; }

        public BattleFieldComponent Field { get; private set; }
        public BattleRendererComponent Role { get; private set; }
        public BattlePoolComponent Pool { get; private set; }
        public BattleBulletComponent Bullet { get; private set; }
        public BattleCameraComponent Camera { get; private set; }

        public override void OnSingletonInit()
        {
            m_BattleComponentList = new List<IBattleComponent>();
            Pool = AddComponent(new BattlePoolComponent()) as BattlePoolComponent;
            Field = AddComponent(new BattleFieldComponent()) as BattleFieldComponent;
            Role = AddComponent(new BattleRendererComponent()) as BattleRendererComponent;
            Bullet = AddComponent(new BattleBulletComponent()) as BattleBulletComponent;
            Camera = AddComponent(new BattleCameraComponent()) as BattleCameraComponent;
            AddComponent(new BattleDragComponent());
        }

        private IBattleComponent AddComponent(IBattleComponent component)
        {
            component.BattleMgr = this;
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


        public void BattleInit(BattleFieldConfigSO enemyConfigSO, int levelId = 1)
        {
            for (int i = 0; i < m_BattleComponentList.Count; i++)
            {
                m_BattleComponentList[i].OnBattleInit(enemyConfigSO);
            }
            m_LevelId = levelId;
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
            if (isSuccess)
            {
                UIMgr.S.ClosePanelAsUIID(UIID.BattlePreparePanel);
                UIMgr.S.OpenPanel(UIID.BattleWinPanel, null, m_LevelId);
            }
            else
            {
                Debug.LogError("Open fail panel!");
            }
            BattleMgr.S.BattleClean();
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
                for (int i = 0; i < Role.ourControllers.Count; i++)
                {
                    Role.ourControllers[i].Buff.AddBuff(buff);
                }
            }
        }



        #region public

        public void SendDamage(BattleRoleController controller, RoleDamagePackage roleDamagePackage)
        {
            if (controller == null)
                return;

            if (controller.AI.onHurt != null)
                controller.AI.onHurt();
            controller.Data.GetDamage(roleDamagePackage);


        }
        #endregion


    }

}