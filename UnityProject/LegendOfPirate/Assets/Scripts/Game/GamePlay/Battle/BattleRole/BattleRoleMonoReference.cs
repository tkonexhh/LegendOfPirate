using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Sirenix.OdinInspector;
using Qarth;

namespace GameWish.Game
{
    public class BattleRoleMonoReference : RoleMonoReference
    {
        [Space(20)]
        public Rigidbody Rigidbody;


        [LabelText("远程攻击角色枪口子弹位置")]
        public Transform ShootPos;


        public Run<Collision> onCollisionEnter;

        private void OnCollisionEnter(Collision other)
        {
            //Debug.LogError(other.gameObject.name);
            if (onCollisionEnter != null)
                onCollisionEnter(other);
        }
    }

}