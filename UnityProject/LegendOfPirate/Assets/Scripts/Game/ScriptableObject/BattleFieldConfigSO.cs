using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GameWish.Game
{
    [CreateAssetMenu(menuName = "Game/BattleFieldConfigSO", fileName = "new_BattleFieldConfigSO")]
    public class BattleFieldConfigSO : SerializedScriptableObject
    {
        [LabelText("战场ID")]
        public int ID;
        [LabelText("敌人等级")]
        public int Level;

        [TabGroup("敌人阵容配置")]
        public BattleFieldCell[,] Enemys = new BattleFieldCell[BattleDefine.BATTLE_WIDTH, BattleDefine.BATTLE_HEIGHT];
    }

    [Serializable]
    public class BattleFieldCell
    {
        public RoleConfigSO roleConfig;
    }

}