using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using System;

namespace GameWish.Game
{
    /// <summary>
    /// 所有的Model初始化后都注册到ModelMgr中，需要用到Model的从ModelMgr中获取，
    /// 后面考虑改为依赖注入方式
    /// </summary>
	public class ModelMgr : TSingleton<ModelMgr>, IMgr
	{
        private Dictionary<Type, IModel> m_ModelDic = new Dictionary<Type, IModel>();
       
        #region IMgr
        public void OnInit()
        {
        }

        public void OnUpdate()
        {
        }

        public void OnDestroyed()
        {
        }

        #endregion

        #region Public
        public void RegisterModel<T>(IModel model)
        {
            if (!m_ModelDic.ContainsKey(typeof(T)))
            {
                m_ModelDic.Add(typeof(T), model);
            }
            else
            {
                Log.w("Model has been added before: " + model.ToString());
            }
        }

        public T GetModel<T>()
        {
            if (m_ModelDic.ContainsKey(typeof(T)))
            {
                return (T)m_ModelDic[typeof(T)];
            }

            return default(T);
        }

        #endregion

    }

}