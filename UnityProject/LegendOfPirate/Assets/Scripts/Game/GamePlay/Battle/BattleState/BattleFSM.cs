using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public enum BattleStateEnum
    {
        PreFight,//前置战斗 海战
        Prepare,//准备状态
        Fighting,
        End,
    }
    public class BattleFSM : FSMStateMachine<EntityBase>
    {
        public BattleFSM(EntityBase entity) : base(entity)
        {
            stateFactory.RegisterState(BattleStateEnum.PreFight, new BattleState_PrepFight());
            stateFactory.RegisterState(BattleStateEnum.Prepare, new BattleState_Prepare());
            stateFactory.RegisterState(BattleStateEnum.Fighting, new BattleState_Fighting());
            stateFactory.RegisterState(BattleStateEnum.End, new BattleState_End());
        }
    }

}