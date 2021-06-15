﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattleRendererComponent : AbstractBattleComponent
    {
        private List<BattleRoleController> m_OurRoleControllerLst;
        private List<BattleRoleController> m_EnemyRoleControllerLst;


        public List<BattleRoleController> ourControllers => m_OurRoleControllerLst;
        public List<BattleRoleController> enemyControllers => m_EnemyRoleControllerLst;

        #region Override
        public override void Init()
        {
            m_OurRoleControllerLst = new List<BattleRoleController>();
            m_EnemyRoleControllerLst = new List<BattleRoleController>();
        }

        public override void OnBattleInit()
        {
            InitOwerRole();
            InitEnemyRole();
        }

        public override void OnBattleStart()
        {
            for (int i = 0; i < m_OurRoleControllerLst.Count; i++)
            {
                m_OurRoleControllerLst[i].BattleStart();
            }

            for (int i = 0; i < m_EnemyRoleControllerLst.Count; i++)
            {
                m_EnemyRoleControllerLst[i].BattleStart();
            }
        }

        public override void OnBattleUpdate()
        {
            if (m_OurRoleControllerLst == null || m_EnemyRoleControllerLst == null)
                return;

            for (int i = 0; i < m_OurRoleControllerLst.Count; i++)
            {
                m_OurRoleControllerLst[i].OnUpdate();
            }

            for (int i = 0; i < m_EnemyRoleControllerLst.Count; i++)
            {
                m_EnemyRoleControllerLst[i].OnUpdate();
            }

            if (BattleMgr.S.Started)
            {
                if (m_OurRoleControllerLst.Count == 0)
                {
                    BattleMgr.S.BattleEnd(false);
                }

                if (m_EnemyRoleControllerLst.Count == 0)
                {
                    BattleMgr.S.BattleEnd(true);
                }
            }

        }

        public override void OnBattleEnd(bool isSuccess)
        {
            if (isSuccess)
            {
                for (int i = m_OurRoleControllerLst.Count - 1; i >= 0; i--)
                {
                    m_OurRoleControllerLst[i].AI.FSM.SetCurrentStateByID(BattleRoleAIStateEnum.Victory);
                }
            }
            else
            {
                for (int i = m_EnemyRoleControllerLst.Count - 1; i >= 0; i--)
                {
                    m_EnemyRoleControllerLst[i].AI.FSM.SetCurrentStateByID(BattleRoleAIStateEnum.Victory);
                }
            }
        }

        public override void OnBattleClean()
        {
            for (int i = m_OurRoleControllerLst.Count - 1; i >= 0; i--)
            {
                m_OurRoleControllerLst[i].Recycle2Cache();
                m_OurRoleControllerLst.RemoveAt(i);
            }

            for (int i = m_EnemyRoleControllerLst.Count - 1; i >= 0; i--)
            {
                m_EnemyRoleControllerLst[i].Recycle2Cache();
                m_EnemyRoleControllerLst.RemoveAt(i);
            }
        }
        #endregion

        private void InitOwerRole()
        {
            Vector3 startPos = new Vector3(-70, 0, 40);
            int width = 80;
            for (int i = 0; i < 5; i++)
            {
                BattleRoleController role = BattleRoleControllerFactory.CreateBattleRole(BattleMgr.S.DemoRoleSO);
                role.SetCamp(BattleCamp.Our);
                int x = i % width;
                int y = i / width;
                role.transform.localPosition = startPos + new Vector3(4.5f * x, 0, 1.5f * y);
                role.transform.localRotation = Quaternion.Euler(0, 180, 0);
                m_OurRoleControllerLst.Add(role);
            }
        }

        private void InitEnemyRole()
        {
            Vector3 startPos = new Vector3(-70, 0, -40);
            int width = 80;
            for (int i = 0; i < 5; i++)
            {
                BattleRoleController role = BattleRoleControllerFactory.CreateBattleRole(BattleMgr.S.DemoRoleSO);
                role.SetCamp(BattleCamp.Enemy);
                int x = i % width;
                int y = i / width;
                role.transform.localPosition = startPos + new Vector3(4.5f * x, 0, 1.5f * y);
                role.transform.localRotation = Quaternion.identity;
                m_EnemyRoleControllerLst.Add(role);
            }
        }

        public List<BattleRoleController> GetControllersByCamp(BattleCamp camp)
        {
            if (camp == BattleCamp.Our)
            {
                return m_OurRoleControllerLst;
            }
            else
            {
                return m_EnemyRoleControllerLst;
            }
        }

        public void RemoveController(BattleRoleController controller)
        {
            if (controller.camp == BattleCamp.Our)
            {
                m_OurRoleControllerLst.Remove(controller);
            }
            else
            {
                m_EnemyRoleControllerLst.Remove(controller);
            }
        }
    }

}