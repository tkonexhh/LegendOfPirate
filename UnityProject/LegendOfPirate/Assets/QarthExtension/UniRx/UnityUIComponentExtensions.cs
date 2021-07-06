using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Qarth;
using System;

namespace UniRx
{
	public static partial class UnityUIComponentExtensions
	{
		/// <summary>
		/// 正向绑定active 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static IDisposable SubscribeToPositiveActive(this IObservable<bool> source, Component obj)
		{
			return source.SubscribeWithState(obj.gameObject, (x, s) => s.SetActive(x));
		}

		/// <summary>
		/// 反向绑定active 
		/// </summary>
		/// <param name="source"></param>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static IDisposable SubscribeToNegativeActive(this IObservable<bool> source, Component obj)
		{
			return source.SubscribeWithState(obj.gameObject, (x, s) => s.SetActive(!x));
		}

		public static IDisposable SubscribeToPositiveInteractable(this IObservable<bool> source, Selectable selectable)
		{
			return source.SubscribeWithState(selectable, (x, s) => s.interactable = x);
		}
		public static IDisposable SubscribeToNegativeInteractable(this IObservable<bool> source, Selectable selectable)
		{
			return source.SubscribeWithState(selectable, (x, s) => s.interactable = !x);
		}
	}
}