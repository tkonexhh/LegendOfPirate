using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattleRoleComponent : AbstractBattleComponent
    {
        private BattleRoleControllerFactory m_RoleFactory;
        private List<BattleRoleController> m_RoleControllerLst;

        public override void Init()
        {
            m_RoleFactory = new BattleRoleControllerFactory();
            m_RoleControllerLst = new List<BattleRoleController>();
        }

        public override void OnBattleInit()
        {


            for (int i = 0; i < 10; i++)
            {
                BattleRoleController role = m_RoleFactory.CreateController();
                role.OnInit();
                m_RoleControllerLst.Add(role);
            }
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


    }

}