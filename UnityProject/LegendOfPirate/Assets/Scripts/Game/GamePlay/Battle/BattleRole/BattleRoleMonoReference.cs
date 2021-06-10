using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Sirenix.OdinInspector;

namespace GameWish.Game
{
    public class BattleRoleMonoReference : MonoBehaviour
    {
        public AIPath AstarAI;
        public Collider Collider;

        [LabelText("远程攻击角色枪口子弹位置")]
        public Transform ShootPos;
    }

}