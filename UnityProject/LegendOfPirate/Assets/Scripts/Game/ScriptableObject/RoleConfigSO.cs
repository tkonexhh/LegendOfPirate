using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GameWish.Game
{
    [CreateAssetMenu(menuName = "Game/RoleConfigSO", fileName = "new_RoleConfigSO")]
    public class RoleConfigSO : ScriptableObject
    {
        public AttackTypeEnum attackType;


        [Header("GPUInstance")]
        public Mesh mesh;
        public Material material;
        public TextAsset animInfoText;
    }

}