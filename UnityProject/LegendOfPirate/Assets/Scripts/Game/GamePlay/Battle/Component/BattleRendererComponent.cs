using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattleRendererComponent : AbstractBattleComponent
    {
        private BattleRoleControllerFactory m_RoleFactory;
        private List<BattleRoleController> m_RoleControllerLst;

        #region Override
        public override void Init()
        {
            m_RoleFactory = new BattleRoleControllerFactory();
            m_RoleControllerLst = new List<BattleRoleController>();
        }

        public override void OnBattleInit()
        {
            InitOwerRole();
            InitEnemyRole();
        }

        public override void OnBattleUpdate()
        {
            if (m_RoleControllerLst == null)
                return;

            for (int i = 0; i < m_RoleControllerLst.Count; i++)
            {
                m_RoleControllerLst[i].OnUpdate();
            }
        }

        public override void OnBattleClean()
        {
            for (int i = m_RoleControllerLst.Count - 1; i >= 0; i--)
            {
                ObjectPool<BattleRoleController>.S.Recycle(m_RoleControllerLst[i]);
                m_RoleControllerLst.RemoveAt(i);
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
                m_RoleControllerLst.Add(role);
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
                m_RoleControllerLst.Add(role);
            }
        }
    }

}