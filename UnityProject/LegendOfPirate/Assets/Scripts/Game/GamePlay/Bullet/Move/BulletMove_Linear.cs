using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace GameWish.Game
{
    public class BulletMove_Line : BulletMove
    {
        public BulletMove_Line(BulletTarget target) : base(target) { }


        public override void MoveToTarget()
        {
            if (BulletTrans == null) return;
            var targetPos = m_Target.GetTargetPos();
            BulletTrans.LookAt(targetPos);
            BulletTrans.position = Vector3.MoveTowards(BulletTrans.position, targetPos, Speed);

        }
    }

}