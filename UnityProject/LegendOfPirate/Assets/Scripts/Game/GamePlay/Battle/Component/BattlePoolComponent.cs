using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattlePoolComponent : AbstractBattleComponent
    {
        #region Override
        public override void Init()
        {
            GameObjectPoolMgr.S.AddPool("BattleRole", BattleMgr.S.m_RolePrefab, 1000, 100);
        }

        public override void OnBattleInit()
        {
            //根据本次战斗需要加载到池
        }

        public override void OnBattleClean()
        {
            //根据需要释放池
        }

        #endregion
    }

}