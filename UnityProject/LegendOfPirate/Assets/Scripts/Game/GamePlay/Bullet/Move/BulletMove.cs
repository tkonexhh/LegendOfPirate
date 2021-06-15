using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public abstract class BulletMove
    {
        public Transform BulletTrans { get; set; }
        public float Speed { get; set; }
        protected BulletTarget m_Target;
        public BulletMove(BulletTarget target)
        {
            m_Target = target;
        }

        public abstract void MoveToTarget();

        public bool Reached()
        {
            return Vector3.Distance(BulletTrans.position, m_Target.GetTargetPos()) < 0.03f;
        }
    }

}