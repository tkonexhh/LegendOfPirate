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
        private Vector3[] m_Paths;
        private float m_Timer = 0.2f;

        public BulletMove_Parabolic(BulletTarget target) : base(target)
        {
            UpdatePath();
        }

        public override void MoveToTarget()
        {
            if (BulletTrans == null) return;

            m_Timer -= Time.deltaTime;
            if (m_Timer <= 0)
            {
                //降低批次
                UpdatePath();
            }
        }

        private void UpdatePath()
        {
            Vector3 centerPos = (BulletTrans.position + m_Target.GetTargetPos()) / 2;
            centerPos.y = 2;//子弹最大点
            m_Paths = BezierUtils.GetCubicBeizerList(BulletTrans.position, centerPos, m_Target.GetTargetPos(), 4);

        }
    }

}