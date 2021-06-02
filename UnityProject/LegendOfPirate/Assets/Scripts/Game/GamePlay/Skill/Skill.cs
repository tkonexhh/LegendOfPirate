using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class Skill
    {
        public int id;
        public string name;
        public float range;
        public float cd;
        public IBattleSensor Sensor { get; set; }//技能选择器
        public Buff appendBuff;//附加Buff


        public BattleRoleController PicketTarget(BattleRoleController picker)
        {
            return Sensor.PickTarget(picker);
        }
    }

    /// <summary>
    /// 主动技能
    /// </summary>
    public class InitiativeSkill : Skill
    {
        // public AttackType attackType;



    }

    /// <summary>
    /// 被动技能
    /// </summary>
    public class PassiveSkill : Skill
    {

    }

}