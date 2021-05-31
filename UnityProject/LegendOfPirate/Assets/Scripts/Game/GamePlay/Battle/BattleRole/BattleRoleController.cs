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
        public IBattleSensor Sensor { get; private set; }

        //---- Mono
        public BattleRoleMonoReference MonoReference { get; private set; }
        // public IAstarAI 
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
            renderer.transform = transform;

            fSM = new BattleRoleFSM(this);
            AI = new BattleRoleAI(this);

            Sensor = new BattleSensor_Nearest(this);

            base.OnInit();
        }

        public override void OnFirstInit()
        {
            MonoReference = gameObject.GetComponent<BattleRoleMonoReference>();
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