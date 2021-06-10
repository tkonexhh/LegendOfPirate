using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GameWish.Game
{
    [CreateAssetMenu(menuName = "Game/BulletConfigSO", fileName = "new_BulletConfigSO")]
    public class BulletConfigSO : SerializedScriptableObject
    {
        [LabelText("子弹ID")]
        public int ID;
        [LabelText("子弹速度")]
        public float Speed = 0.5f;
        [LabelText("子弹预制体")]
        public GameObject Prefab;
        [LabelText("爆炸特效")]
        public GameObject Effect;

        public BulletMoveType MoveType;
        public BulletTargetType TargetType;
    }

}