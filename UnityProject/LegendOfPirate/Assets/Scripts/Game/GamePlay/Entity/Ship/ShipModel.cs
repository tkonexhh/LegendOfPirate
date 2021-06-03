using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace GameWish.Game
{
    [ModelAutoRegister]
    public class ShipModel : DbModel
	{
        public IntReactiveProperty level;

        protected override void LoadDataFromDb()
        {

        }
    }

}