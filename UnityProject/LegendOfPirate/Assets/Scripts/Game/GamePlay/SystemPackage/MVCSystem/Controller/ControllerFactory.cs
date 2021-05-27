using Qarth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    /// <summary>
    /// 将组装Controller的逻辑放在工厂类中，减少Controller的工作量
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public class ControllerFactory<T>: TSingleton<ControllerFactory<T>> where T : IController, new()
	{
        public T CreateController(IModel model)
        {
            T controller = ObjectPool<T>.S.Allocate();

            BuildController(controller, model);

            return controller;
        }

        /// <summary>
        /// 在这里进行Controller的组装
        /// </summary>
        /// <param name="controller"></param>
        protected virtual void BuildController(T controller, IModel model)
        {
        }
	}
	
}