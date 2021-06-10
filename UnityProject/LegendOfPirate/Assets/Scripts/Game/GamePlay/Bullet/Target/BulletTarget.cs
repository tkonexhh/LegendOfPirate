using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public abstract class BulletTarget
    {
        protected Transform m_Target;
        protected Vector3 m_OffsetPos = new Vector3(0, 1.2f, 0);
        public BulletTarget(Transform target)
        {
            m_Target = target;
        }

        public abstract Vector3 GetTargetPos();
    }

}