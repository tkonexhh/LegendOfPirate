using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class ShipRoleStateWander : ShipRoleState
    {
        private List<Vector3> m_WalkablePosList;

        private Vector3 m_TargetPos;

        public ShipRoleStateWander(ShipRoleStateId stateEnum) : base(stateEnum)
        {

        }

        public override void Enter(ShipRoleController entity)
        {
            base.Enter(entity);

            //Log.e("Enter wander");
            m_WalkablePosList = EnvMgr.S.ShipController.ShipView.GetWalkablePosList();

            int random = Random.Range(0, m_WalkablePosList.Count);
            m_TargetPos = m_WalkablePosList[random];
        }

        public override void Execute(ShipRoleController entity, float dt)
        {
            base.Execute(entity, dt);

            if (!m_ShipRoleController.RoleView.RoleMonoRef.AstarAI.reachedDestination)
            {
                m_ShipRoleController.RoleView.SetTargetPos(m_TargetPos);
            }
        }
    }

}