using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using Pathfinding;


namespace GameWish.Game
{
    public class BattleRoleController : RoleController
    {
        private BattleRoleRenderer m_Renderer;
        private BattleRoleFSM m_FSM;
        private BattleRoleAI m_AI;


        public BattleRoleRenderer renderer => m_Renderer;
        public GameObject gameObject { get; private set; }
        public Transform transform { get; private set; }
        public BattleCamp camp { get; private set; }
        public BattleRoleFSM fSM => m_FSM;

        //---- Mono
        public AIDestinationSetter AIDestination { get; private set; }
        //----


        #region Override
        public override void OnInit()
        {
            gameObject = GameObjectPoolMgr.S.Allocate("BattleRole");
            transform = gameObject.transform;
            transform.SetParent(BattleMgr.S.transform);

            m_Renderer = ObjectPool<BattleRoleRenderer>.S.Allocate();
            m_Renderer.OnInit();
            m_Renderer.SetTarget(transform);

            m_FSM = new BattleRoleFSM(this);
            m_AI = new BattleRoleAI(this);

            base.OnInit();
        }

        public override void OnFirstInit()
        {
            AIDestination = gameObject.GetComponent<AIDestinationSetter>();
        }

        public override void OnUpdate()
        {
            m_Renderer.OnUpdate();
            m_FSM.OnUpdate();
            m_AI.OnUpdate();
        }
        public override void OnDestroyed()
        {
            ObjectPool<BattleRoleRenderer>.S.Recycle(m_Renderer);
            GameObjectPoolMgr.S.Recycle(gameObject);
            m_Renderer = null;
            m_FSM = null;
        }

        public override void Recycle2Cache()
        {
            base.Recycle2Cache();
        }
        #endregion

        public void SetCamp(BattleCamp camp)
        {
            this.camp = camp;
        }

        public void BattleStart()
        {
            m_AI.OnBattleStart();

        }
    }

}