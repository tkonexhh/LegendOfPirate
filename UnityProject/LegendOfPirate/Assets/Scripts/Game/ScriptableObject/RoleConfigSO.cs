using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GameWish.Game
{
    [CreateAssetMenu(menuName = "Game/RoleConfigSO", fileName = "new_RoleConfigSO")]
    public class RoleConfigSO : SerializedScriptableObject
    {
        [BoxGroup("基础配置")]
        [LabelText("人物ID")]
        public int ID = 0;

        [BoxGroup("基础配置")]
        [LabelText("人物名称")]
        public string RoleName = "RoleName";


        [BoxGroup("战斗")]
        [LabelText("攻击距离")]
        public float AtkRange = 2.0f;
        public float ColliderRange = 0.3f;

        [BoxGroup("战斗")]
        [HideReferenceObjectPicker]
        public PickTarget PickTarget = new PickTarget();

        [BoxGroup("战斗")]
        [HideReferenceObjectPicker]
        public Attack Attack = new Attack();

        [BoxGroup("战斗")]
        [LabelText("拥有技能")]
        public List<SkillConfigSO> childSkills;

        [BoxGroup("美术资源")]
        [LabelText("预制体")]
        public GameObject prefab;

        [BoxGroup("美术资源")]
        [LabelText("角色头像")]
        public Sprite Icon;


        [Space(50)]
        [TextArea, LabelText("描述")]
        public string StatusDescription;

    }





}