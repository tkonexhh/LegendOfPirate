using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Qarth;

namespace GameWish.Game
{
    [RequireComponent(typeof(Button))]
    public class ButtonClickStay : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        private bool click;
        private Button btn;

        private float interval = 0.95f;
        public Action clickAction;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (btn.interactable)
            {
                click = true;
                StartCoroutine(UpdateClick());
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            click = false;
            interval = 0.95f;
            StopCoroutine(UpdateClick());
        }

        // Use this for initialization
        void Start()
        {
            btn = GetComponent<Button>();
        }

        IEnumerator UpdateClick()
        {
            while (click && btn.interactable)
            {
                if (interval >= 0.1f)
                    interval -= 0.25f;
                interval = Mathf.Max(0.1f, interval);

                if (clickAction != null)
                    clickAction();
                yield return new WaitForSeconds(interval);
            }
        }


    }
}