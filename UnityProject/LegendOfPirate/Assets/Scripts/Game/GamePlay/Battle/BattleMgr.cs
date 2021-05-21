using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattleMgr : TMonoSingleton<BattleMgr>
    {
        private ResLoader m_Loader;

        public ResLoader loader => m_Loader;

        public override void OnSingletonInit()
        {
            m_Loader = ResLoader.Allocate("BattleMgr");
        }

        public void Init()
        {
            for (int i = 0; i < 10; i++)
            {
                RoleBase role = new RoleBase();

            }
        }

        private void Update()
        {
            EntityMgr.S.Tick(Time.deltaTime);
        }
    }

}