using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using System;
using System.Reflection;

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
            AutoRegisterAllModels();
        }

        public void OnUpdate()
        {
        }

        public void OnDestroyed()
        {
        }

        #endregion

        #region Public

        public T GetModel<T>()
        {
            if (m_ModelDic.ContainsKey(typeof(T)))
            {
                return (T)m_ModelDic[typeof(T)];
            }

            return default(T);
        }

        #endregion

        #region Private

        private void AutoRegisterAllModels()
        {
            //foreach (Assembly assembly in AssemblyHelper.AllAssemblies)
            Assembly assembly = AssemblyHelper.DefaultCSharpAssembly;
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.GetCustomAttribute<ModelAutoRegisterAttribute>() != null)
                    {
                        object modelObj = Activator.CreateInstance(type);
                        IModel model = (IModel)modelObj;
                        if (model != null)
                        {
                            if (!m_ModelDic.ContainsKey(type))
                            {
                                model.OnInit();
                                m_ModelDic.Add(type, model);
                            }
                        }
                        else
                        {
                            Log.e("IModel class not found!");
                        }
                    }

                }
            }
        }

        #endregion
    }

}