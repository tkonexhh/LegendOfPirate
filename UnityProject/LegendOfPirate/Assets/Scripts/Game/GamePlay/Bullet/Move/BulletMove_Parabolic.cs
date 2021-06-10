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
        private static float MAX_HEIGHT = 2.0f;
        private float m_Y;

        public BulletMove_Parabolic(BulletTarget target) : base(target)
        {

        }

        public override void MoveToTarget()
        {
            if (BulletTrans == null) return;

            var targetPos = m_Target.GetTargetPos();
            // BulletTrans.LookAt(targetPos);
            BulletTrans.position = Vector3.MoveTowards(BulletTrans.position, targetPos, Speed);

            m_Y = Mathf.MoveTowards(m_Y, (MAX_HEIGHT - BulletTrans.position.y) + (MAX_HEIGHT - targetPos.y), Speed);
            //拆解
            //水平方向的运动
            var pos_H = Vector3.MoveTowards(BulletTrans.position, targetPos, Speed);
            if (m_Y > MAX_HEIGHT)
            {
                pos_H.y = MAX_HEIGHT - m_Y;
            }
            else
            {
                pos_H.y = m_Y;
            }

            BulletTrans.position = pos_H;
        }

        // private void UpdatePath()
        // {
        //     Vector3 centerPos = (BulletTrans.position + m_Target.GetTargetPos()) / 2;
        //     centerPos.y = 2;//子弹最大点
        //     m_Paths = BezierUtils.GetCubicBeizerList(BulletTrans.position, centerPos, m_Target.GetTargetPos(), 4);

        // }
    }

}