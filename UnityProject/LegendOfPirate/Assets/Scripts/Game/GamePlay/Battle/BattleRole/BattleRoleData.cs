using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BattleRoleData : BattleRoleComponent
    {
        public BattleRoleModel originData;
        public BattleRoleModel equipedData;
        public BattleRoleModel buffedData;


        public override void OnBattleStart()
        {
            buffedData = new BattleRoleModel();
            buffedData.MoveSpeed = 2.0f;
        }

    }

    /// <summary>
    /// 攻击包
    /// </summary>
    public class RoleDamage
    {
        public BattleDamageType damageType;
    }

    /// <summary>
    /// 攻击类型
    /// </summary>
    public enum BattleDamageType
    {
        Normal,//普通攻击
        Skill,//技能攻击
    }

}