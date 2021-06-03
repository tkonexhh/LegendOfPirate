using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    /// <summary>
    /// 永久触发被动技能
    /// 所触发的buff时间应该为-1
    /// </summary>
    public class SkillTrigger_Forver : SkillTrigger
    {
        public override void Start(BattleRoleController controller)
        {
            OnTrigger();
        }

        public override void Stop(BattleRoleController controller)
        {
        }
    }

}