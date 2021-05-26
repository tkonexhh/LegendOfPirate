using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattleMgr : TMonoSingleton<BattleMgr>, IMgr
    {
        private ResLoader m_Loader;
        private List<IBattleComponent> m_BattleComponentList;

        public ResLoader loader => m_Loader;



        public override void OnSingletonInit()
        {
            m_Loader = ResLoader.Allocate("BattleMgr");

            m_BattleComponentList = new List<IBattleComponent>();
            AddComponent(new BattleRoleComponent());

        }

        private IBattleComponent AddComponent(IBattleComponent component)
        {
            m_BattleComponentList.Add(component);
            return component;
        }

        #region IMgr
        public void OnInit()
        {
            GameObjectPoolMgr.S.AddPool("BattleRole", new GameObject(), 1000, 100);
        }

        public void OnUpdate()
        {
            BattleUpate();
        }

        public void OnDestroyed() { }
        #endregion




        public void BattleInit()
        {
            for (int i = 0; i < m_BattleComponentList.Count; i++)
            {
                m_BattleComponentList[i].OnBattleInit();
            }

        }

        public void BattleStart()
        {

        }

        public void BattleEnd()
        {
            for (int i = 0; i < m_BattleComponentList.Count; i++)
            {
                m_BattleComponentList[i].OnBattleClean();
            }
        }

        public void BattleUpate()
        {
            for (int i = 0; i < m_BattleComponentList.Count; i++)
            {
                m_BattleComponentList[i].OnBattleUpdate();
            }
        }


    }

}