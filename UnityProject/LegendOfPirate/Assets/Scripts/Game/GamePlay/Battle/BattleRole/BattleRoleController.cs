using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BattleRoleController : RoleController
    {
        private BattleRoleRenderer m_Renderer;

        #region Override
        public override void OnInit()
        {
            base.OnInit();
            m_Renderer = ObjectPool<BattleRoleRenderer>.S.Allocate();
            m_Renderer.OnInit();
        }

        public override void OnUpdate()
        {
            m_Renderer.OnUpdate();
        }
        public override void OnDestroyed()
        {
            ObjectPool<BattleRoleRenderer>.S.Recycle(m_Renderer);
            m_Renderer = null;
        }
        #endregion
    }

}