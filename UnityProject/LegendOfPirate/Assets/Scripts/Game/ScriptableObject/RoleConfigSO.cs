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

        [BoxGroup("战斗")]
        [HideReferenceObjectPicker]
        public PickTarget PickTarget = new PickTarget();

        [BoxGroup("战斗")]
        public Attack Attack;

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

    [LabelText("攻击方式")]
    public class Attack
    {
        public AttackType AttackType;
        public DamageRangeType DamageRangeType;
        [HideIf("DamageRangeType", DamageRangeType.Single), LabelText("伤害范围参数")]
        public string RangeArgs;
        //===

        //===远程攻击
        [ShowIf("AttackType", AttackType.Shoot), LabelText("子弹对象")]
        public BulletConfigSO Bullet;

        [ShowIf("AttackType", AttackType.Shoot), LabelText("子弹数量")]
        public int BulletNum = 1;
        //===
    }



}