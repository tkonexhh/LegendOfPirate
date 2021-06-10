using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BulletTarget_Position : BulletTarget
    {
        private Vector3 m_Pos;
        public BulletTarget_Position(Transform target) : base(target)
        {
            m_Pos = target.position;
        }

        public override Vector3 GetTargetPos()
        {
            return m_Pos + m_OffsetPos;
        }
    }

}