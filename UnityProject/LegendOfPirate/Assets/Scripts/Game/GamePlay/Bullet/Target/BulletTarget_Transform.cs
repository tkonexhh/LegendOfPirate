<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BulletTarget_Transform : BulletTarget
    {
        private Vector3 m_LastPos;
        public BulletTarget_Transform(Transform target) : base(target)
        {
            m_LastPos = GetTargetPos();
        }

        public override Vector3 GetTargetPos()
        {
            if (m_Target == null)//如果敌人打到一半死了,
            {
                return m_LastPos;
            }
            else
            {
                Debug.LogError(m_LastPos);
                m_LastPos = m_Target.position + m_OffsetPos;
                return m_LastPos;
            }

        }
    }

=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BulletTarget_Transform : BulletTarget
    {
        private Vector3 m_LastPos;
        public BulletTarget_Transform(Transform target) : base(target)
        {
            m_LastPos = GetTargetPos();
        }

        public override Vector3 GetTargetPos()
        {
            if (m_Target == null)//如果敌人打到一半死了,
            {
                return m_LastPos;
            }
            else
            {
                m_LastPos = m_Target.position + m_OffsetPos;
                return m_LastPos;
            }

        }
    }

>>>>>>> origin/Zyj
}