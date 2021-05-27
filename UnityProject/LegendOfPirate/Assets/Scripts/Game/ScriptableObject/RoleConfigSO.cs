using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    [CreateAssetMenu(menuName = "Game/RoleConfigSO", fileName = "new_ConfigSO")]
    public class RoleConfigSO : ScriptableObject
    {
        public AttackTypeEnum attackType;


        [Header("GPUInstance")]
        public Mesh mesh;
        public Material material;
        public TextAsset animInfoText;
    }

}