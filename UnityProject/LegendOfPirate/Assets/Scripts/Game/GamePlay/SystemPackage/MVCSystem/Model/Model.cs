using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public class Model : IModel
	{

        #region IModel

        public virtual void OnInit()
        {
            LoadDataFromDb();
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void OnDestroyed()
        {
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