using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class ShipUnitStateMachine : FSMStateMachine<ShipUnit>
    {
        public ShipUnitStateMachine(ShipUnit entity) : base(entity)
        {
            stateFactory = new FSMStateFactory<ShipUnit>(false);

            //stateFactory.RegisterState(BattleStateEnum.Fighting, new BattleState_Fighting());
            //stateFactory.RegisterState(BattleStateEnum.End, new BattleState_End());
        }
    }

}