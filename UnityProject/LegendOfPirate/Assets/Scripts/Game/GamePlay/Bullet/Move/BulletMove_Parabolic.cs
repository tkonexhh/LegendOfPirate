using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace GameWish.Game
{
    /// <summary>
    /// 抛物线
    /// </summary>
    public class BulletMove_Parabolic : BulletMove
    {
        private float m_Timer = 0.2f;
        private static float MAX_HEIGHT = 2.5f;
        private float m_Y;
        private float m_TargetY;
        private Vector3 m_TempPos = Vector3.zero;

        public BulletMove_Parabolic(BulletTarget target) : base(target)
        {

        }

        public override void MoveToTarget()
        {
            if (BulletTrans == null) return;

            var targetPos = m_Target.GetTargetPos();

            // BulletTrans.position = Vector3.MoveTowards(BulletTrans.position, targetPos, Speed);
            if (m_TempPos == Vector3.zero)
            {
                m_TempPos = BulletTrans.position;
                m_Y = 0;//m_TempPos.y;
                m_TargetY = Mathf.Min(MAX_HEIGHT, Vector3.Distance(BulletTrans.position, targetPos));
            }

            //TODO 优化抛物线
            //拆解
            //水平方向的运动
            m_TempPos = Vector3.MoveTowards(m_TempPos, targetPos, Speed);

            m_Y = Mathf.MoveTowards(m_Y, m_TargetY * 2, Speed / 2);
            if (m_Y > m_TargetY)
            {
                m_TempPos.y += 2 * m_TargetY - m_Y;
            }
            else
            {
                m_TempPos.y += m_Y;
            }
            BulletTrans.LookAt(m_TempPos.normalized);
            BulletTrans.position = m_TempPos;

            // Debug.LogError(m_TempPos);
        }


    }
}