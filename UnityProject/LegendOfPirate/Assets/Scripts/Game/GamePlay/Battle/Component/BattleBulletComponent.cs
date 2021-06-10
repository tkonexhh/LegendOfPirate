using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BattleBulletComponent : AbstractBattleComponent
    {
        private List<Bullet> m_Bullet;


        #region Override
        public override void Init()
        {
            m_Bullet = new List<Bullet>();
        }

        public override void OnBattleInit()
        {
        }

        public override void OnBattleUpdate()
        {
            for (int i = m_Bullet.Count - 1; i >= 0; i--)
            {
                m_Bullet[i].Update();
            }
        }

        public override void OnBattleClean()
        {
            for (int i = m_Bullet.Count - 1; i >= 0; i--)
            {
                //TODO 先直接移除
                var bullet = m_Bullet[i];
                bullet.Release();
                m_Bullet.RemoveAt(i);
            }
        }
        #endregion


        public void AddBullet(Bullet bullet)
        {
            m_Bullet.Add(bullet);
        }

        public void RemoveBullet(Bullet bullet)
        {
            m_Bullet.Remove(bullet);
            bullet.Release();
        }
    }

}