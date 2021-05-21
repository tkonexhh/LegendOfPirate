using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public class Model : IModel
	{

        #region IModel

        public void Init()
        {
            LoadDataFromDb();
        }

        public virtual IModel DeepCopy()
        {
            return null;
        }

        #endregion

        protected virtual void LoadDataFromDb()
        {

        }
    }

}