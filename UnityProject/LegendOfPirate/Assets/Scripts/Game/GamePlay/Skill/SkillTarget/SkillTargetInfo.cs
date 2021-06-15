using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class SkillTargetInfo//释放一个技能一定有释放者和目标者，从这些才能相应的派生出 范围信息
    {
        public BattleRoleController Caster;//施法者
        public BattleRoleController Target;//目标者
    }

}