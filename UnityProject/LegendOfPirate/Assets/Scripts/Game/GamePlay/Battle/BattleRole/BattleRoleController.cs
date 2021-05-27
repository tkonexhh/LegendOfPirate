using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using Pathfinding;


namespace GameWish.Game
{
    public class BattleRoleController : RoleController
    {
        public GameObject gameObject { get; private set; }
        public Transform transform { get; private set; }
        public BattleRoleRenderer renderer { get; private set; }
        public BattleRoleFSM fSM { get; private set; }
        public BattleRoleAI AI { get; private set; }

        //---- Mono
        public AIDestinationSetter AIDestination { get; private set; }
        //----


        public BattleCamp camp { get; private set; }//阵营


        #region Override
        public override void OnInit()
        {
            gameObject = GameObjectPoolMgr.S.Allocate("BattleRole");
            transform = gameObject.transform;
            transform.SetParent(BattleMgr.S.transform);

            renderer = ObjectPool<BattleRoleRenderer>.S.Allocate();
            renderer.OnInit();
            renderer.SetTarget(transform);

            fSM = new BattleRoleFSM(this);
            AI = new BattleRoleAI(this);

            base.OnInit();
        }

        public override void OnFirstInit()
        {
            AIDestination = gameObject.GetComponent<AIDestinationSetter>();
        }

        public override void OnUpdate()
        {
            renderer.OnUpdate();
            fSM.OnUpdate();
            AI.OnUpdate();
        }
        public override void OnDestroyed()
        {
            ObjectPool<BattleRoleRenderer>.S.Recycle(renderer);
            GameObjectPoolMgr.S.Recycle(gameObject);
            renderer = null;
            fSM = null;
            AI = null;
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
            AI.OnBattleStart();
        }
    }

}