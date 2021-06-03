using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public abstract class DbModel : Model
	{

        #region IModel

        public override void OnInit()
        {
            LoadDataFromDb();
        }

        #endregion
        protected abstract void LoadDataFromDb();

    }

}