using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using System;

namespace GameWish.Game
{
	public class ModelContainer : TSingleton<ModelContainer>
	{
        private Dictionary<Type, IModel> m_ModelDic = new Dictionary<Type, IModel>();
        
        public void RegisterModel(Type type, IModel model)
        {
            if (!m_ModelDic.ContainsKey(type))
            {
                m_ModelDic.Add(type, model);
            }
            else
            {
                Log.w("Model has been added before: " + type.ToString());
            }
        }
	}
	
}