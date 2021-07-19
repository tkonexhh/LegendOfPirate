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
        /// �Ե�һ�����������
        /// </summary>
        /// <param name="source"></param>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static List<IDisposable> SubscribeToActive(this IObservable<bool> source, GameObject obj1, GameObject obj2)
        {
            List<IDisposable> disposables = new List<IDisposable>();
            disposables.Add(SubscribeToPositiveActive(source, obj1));
            disposables.Add(SubscribeToNegativeActive(source, obj2));
            return disposables;
        }

        public static List<IDisposable> SubscribeToActive(this IObservable<bool> source, Component obj1, Component obj2)
        {
            List<IDisposable> disposables = new List<IDisposable>();
            disposables.Add(SubscribeToPositiveActive(source, obj1));
            disposables.Add(SubscribeToNegativeActive(source, obj2));
            return disposables;
        }

        /// <summary>
        /// �����active 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IDisposable SubscribeToPositiveActive(this IObservable<bool> source, Component obj)
        {
            return source.SubscribeWithState(obj.gameObject, (x, s) => s.SetActive(x));
        }

        public static IDisposable SubscribeToPositiveActive(this IObservable<bool> source, GameObject obj)
        {
            return source.SubscribeWithState(obj, (x, s) => s.SetActive(x));
        }

        public static IDisposable SubscribeToSprite(this IObservable<Sprite> source, Image obj)
        {
            return source.SubscribeWithState(obj, (x, s) => s.sprite = x);
        }

        /// <summary>
        /// �����active 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IDisposable SubscribeToNegativeActive(this IObservable<bool> source, Component obj)
        {
            return source.SubscribeWithState(obj.gameObject, (x, s) => s.SetActive(!x));
        }

        public static IDisposable SubscribeToNegativeActive(this IObservable<bool> source, GameObject obj)
        {
            return source.SubscribeWithState(obj, (x, s) => s.SetActive(!x));
        }

        public static IDisposable SubscribeToPositiveInteractable(this IObservable<bool> source, Selectable selectable)
        {
            return source.SubscribeWithState(selectable, (x, s) => s.interactable = x);
        }

        public static IDisposable SubscribeToNegativeInteractable(this IObservable<bool> source, Selectable selectable)
        {
            return source.SubscribeWithState(selectable, (x, s) => s.interactable = !x);
        }

        /// <summary>
        /// ������  fillAmountʹ��
        /// </summary>
        /// <param name="source"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public static IDisposable SubscribeToFillAmount(this IObservable<float> source, Image image)
        {
            return source.SubscribeWithState(image, (x, s) => s.fillAmount = x);
        }

        /// <summary>
        /// �󶨴���ɫ��TMP
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="color"></param>
        /// <param name="text"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static IDisposable SubscribeToColorTextMeshPro<T>(this IObservable<T> source, TMPro.TextMeshProUGUI text, string color, string format = "{0}")
        {
            if (!text.richText)
                text.richText = true;
            return source.SubscribeWithState(text, (x, t) => t.text = string.Format("<color=" + color + ">" + format + "</color>", x));
        }
    }
}