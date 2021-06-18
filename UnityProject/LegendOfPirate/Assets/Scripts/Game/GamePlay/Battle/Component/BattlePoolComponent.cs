using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattlePoolComponent : AbstractBattleComponent
    {
        #region Override


        private ResLoader m_ResLoader;
        public override void Init()
        {
            m_ResLoader = ResLoader.Allocate("BattlePoolComponent");
            GameObjectPoolMgr.S.AddPool("BattleRole", BattleMgr.S.m_RolePrefab, 1000, 100);

            var battleFieldPrefab = m_ResLoader.LoadSync("BattleField") as GameObject;
            int count = BattleDefine.BATTLE_WIDTH * BattleDefine.BATTLE_HEIGHT;
            GameObjectPoolMgr.S.AddPool(BattleDefine.POOLNAME_BATTLEFIELD, battleFieldPrefab, count, count);
        }

        public override void OnBattleInit(BattleFieldConfigSO enemyConfigSO)
        {
            //根据本次战斗需要加载到池
        }

        public override void OnBattleClean()
        {
            //根据需要释放池
        }

        #endregion


        public void AddGameObjectToPool(GameObject prefab)
        {
            if (GameObjectPoolMgr.S.group.HasPool(prefab.name))
            {
                return;
            }
            GameObjectPoolMgr.S.AddPool(prefab.name, prefab, 5, 5);
        }

        public GameObject GetGameObject(string prefab)
        {
            return GameObjectPoolMgr.S.Allocate(prefab);
        }
    }

}