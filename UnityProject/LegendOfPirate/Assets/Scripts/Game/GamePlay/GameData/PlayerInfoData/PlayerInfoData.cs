using System;
using Qarth;
using System.Collections.Generic;
using UnityEngine;

namespace GameWish.Game
{   
    public class PlayerInfoData : IDataClass
    {
        public int coin;
        public int diamond;
        public int stamina;

        public void SetDefaultValue()
        {
            SetDataDirty();
        }

        public override void InitWithEmptyData()
        {

        }

        public override void OnDataLoadFinish()
        {

        }

        public void AddCoin(int delta)
        {
            coin += delta;
            coin = Math.Max(0, coin);

            SetDataDirty();
        }

        public void AddDiamond(int delta)
        {
            diamond += delta;
            diamond = Math.Max(0, diamond);

            SetDataDirty();
        }

        public void AddStamina(int delta)
        {
            stamina += delta;
            stamina = Math.Max(0, stamina);

            SetDataDirty();
        }
    }
}