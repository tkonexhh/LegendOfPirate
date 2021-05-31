using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BattleRoleAIGlobalState : BattleRoleAIState
    {
        public override void Execute(BattleRoleAI ai, float dt)
        {
            if (ai.Target == null)
            {
                //目标消失了，需要换一个目标
                ai.FSM.SetCurrentStateByID(BattleRoleAIStateEnum.PickTarget);
            }

            //检测血量
        }
    }

}