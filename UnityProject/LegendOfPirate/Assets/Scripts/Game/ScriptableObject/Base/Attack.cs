using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GameWish.Game
{
    [LabelText("攻击方式")]
    public class Attack
    {
        public AttackType AttackType;
        public DamageRangeType DamageRangeType;
        [LabelText("伤害范围")]
        [ShowIf("DamageRangeType", DamageRangeType.Range)]
        public RangeDamageConfig RangeDamage;

        //===

        //===远程攻击
        [ShowIf("AttackType", AttackType.Shoot), LabelText("子弹对象")]
        public BulletConfigSO Bullet;

        [ShowIf("AttackType", AttackType.Shoot), LabelText("子弹数量")]
        public int BulletNum = 1;
        //===
    }

}