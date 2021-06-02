using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GameWish.Game
{
    [CreateAssetMenu(menuName = "Game/RoleConfigSO", fileName = "new_RoleConfigSO")]
    public class RoleConfigSO : SerializedScriptableObject
    {
        [LabelText("人物ID")]
        public int ID = 0;

        [LabelText("人物名称")]
        public string RoleName = "RoleName";
        [HideReferenceObjectPicker]
        public PickTarget PickTarget = new PickTarget();

        public Attack Attack;

        [ToggleGroup("EnableAttackBuff", "攻击Buff")]
        public bool EnableAttackBuff;
        [ToggleGroup("EnableAttackBuff")]
        public BuffConfigSO BuffConfigSO;

        [Space(30)]
        public List<SkillConfigSO> childSkills;


        [Header("GPUInstance")]
        public Mesh mesh;
        public Material material;
        public TextAsset animInfoText;
    }



}