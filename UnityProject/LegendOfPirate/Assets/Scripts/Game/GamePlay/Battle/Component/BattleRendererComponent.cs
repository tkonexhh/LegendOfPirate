using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattleRendererComponent : AbstractBattleComponent
    {
        private BattleRoleControllerFactory m_RoleFactory;
        private List<BattleRoleController> m_OurRoleControllerLst;
        private List<BattleRoleController> m_EnemyRoleControllerLst;

        #region Override
        public override void Init()
        {
            m_RoleFactory = new BattleRoleControllerFactory();
            m_OurRoleControllerLst = new List<BattleRoleController>();
            m_EnemyRoleControllerLst = new List<BattleRoleController>();
        }

        public override void OnBattleInit()
        {
            InitOwerRole();
            InitEnemyRole();
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
        }

        public override void OnBattleClean()
        {
            for (int i = m_OurRoleControllerLst.Count - 1; i >= 0; i--)
            {
                ObjectPool<BattleRoleController>.S.Recycle(m_OurRoleControllerLst[i]);
                m_OurRoleControllerLst.RemoveAt(i);
            }

            for (int i = m_EnemyRoleControllerLst.Count - 1; i >= 0; i--)
            {
                ObjectPool<BattleRoleController>.S.Recycle(m_EnemyRoleControllerLst[i]);
                m_EnemyRoleControllerLst.RemoveAt(i);
            }
        }
        #endregion

        private void InitOwerRole()
        {
            Vector3 startPos = new Vector3(-70, 0, 60);
            int width = 80;
            for (int i = 0; i < 2000; i++)
            {
                BattleRoleController role = m_RoleFactory.CreateController();
                role.OnInit();
                int x = i % width;
                int y = i / width;
                role.renderer.transform.position = startPos + new Vector3(1.5f * x, 0, 1.5f * y);
                role.renderer.transform.rotation = Quaternion.Euler(0, 180, 0);
                m_OurRoleControllerLst.Add(role);
            }
        }

        private void InitEnemyRole()
        {
            Vector3 startPos = new Vector3(-70, 0, -60);
            int width = 80;
            for (int i = 0; i < 2000; i++)
            {
                BattleRoleController role = m_RoleFactory.CreateController();
                role.OnInit();
                int x = i % width;
                int y = i / width;
                role.renderer.transform.position = startPos + new Vector3(1.5f * x, 0, 1.5f * y);
                m_EnemyRoleControllerLst.Add(role);
            }
        }
    }

}