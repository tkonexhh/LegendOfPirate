using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattleMgr : TMonoSingleton<BattleMgr>
    {
        private ResLoader m_Loader;
        private List<IBattleComponent> m_BattleComponentList;

        public ResLoader loader => m_Loader;



        public override void OnSingletonInit()
        {
            m_Loader = ResLoader.Allocate("BattleMgr");
            AddComponent(new BattleRoleComponent());

        }

        private IBattleComponent AddComponent(IBattleComponent component)
        {
            m_BattleComponentList.Add(component);
            return component;
        }

        public void Init()
        {

        }

        public void BattleInit()
        {
            m_BattleComponentList.ForEach(c => c.OnBattleInit());

        }

        public void BattleStart()
        {

        }
    }

}