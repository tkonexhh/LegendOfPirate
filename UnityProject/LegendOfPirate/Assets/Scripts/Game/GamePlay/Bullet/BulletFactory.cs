using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BulletFactory
    {
        public static Bullet CreateBullet(BulletConfigSO configSO, Transform target)
        {
            Bullet bullet = new Bullet();
            bullet.prefab = configSO.Prefab;//TODO 改为池
            var bulletTarget = BulletTargetFactory.CreateBulletTarget(configSO.TargetType, target);
            bullet.move = BulletMoveFactory.CreateBulletMove(configSO.MoveType, bulletTarget);
            bullet.move.Speed = configSO.Speed;
            return bullet;
        }
    }


    public class BulletMoveFactory
    {
        public static BulletMove CreateBulletMove(BulletMoveType moveType, BulletTarget target)
        {
            switch (moveType)
            {
                case BulletMoveType.Linear:
                    return new BulletMove_Line(target);
                case BulletMoveType.Parabolic:
                    return new BulletMove_Parabolic(target);
            }

            return null;
        }
    }

    public class BulletTargetFactory
    {
        public static BulletTarget CreateBulletTarget(BulletTargetType targetType, Transform target)
        {
            switch (targetType)
            {
                case BulletTargetType.Position:
                    return new BulletTarget_Position(target);
                case BulletTargetType.Target:
                    return new BulletTarget_Transform(target);
            }
            return null;
        }
    }

}