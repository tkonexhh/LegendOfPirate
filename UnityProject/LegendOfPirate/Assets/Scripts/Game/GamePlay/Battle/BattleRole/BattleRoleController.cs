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
        public BattleRoleData Data { get; private set; }
        public BattleRoleRenderer renderer { get; private set; }
        public BattleRoleAI AI { get; private set; }
        public BattleRoleBuff Buff { get; private set; }


        //---- Mono
        public BattleRoleMonoReference MonoReference { get; private set; }
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

            AI = new BattleRoleAI(this);
            Data = new BattleRoleData();
            Buff = new BattleRoleBuff(this);

            base.OnInit();
        }

        public override void OnFirstInit()
        {
            MonoReference = gameObject.GetComponent<BattleRoleMonoReference>();
        }

        public override void OnUpdate()
        {
            renderer.OnUpdate();
            Buff.OnUpdate();
            AI.OnUpdate();
        }

        public override void OnDestroyed()
        {
            ObjectPool<BattleRoleRenderer>.S.Recycle(renderer);
            GameObjectPoolMgr.S.Recycle(gameObject);
            renderer = null;
            AI = null;
            Buff = null;
            Data = null;
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
            Data.OnBattleStart();
            AI.OnBattleStart();
        }
    }

}