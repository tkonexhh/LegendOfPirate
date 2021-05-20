using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{
    public enum AdType
    {
        PowerUp,
        SummonGiant,
        SummonReinforcements, //Ô®¾ü
        Supply,
        SummonPowerUp,

        AutoDoubleSummon,
        DoubleIncome,
        InstantCoin,


    }

    public enum AdEffectType
    {
        Immidiatly,
        Persistently,
    }
}