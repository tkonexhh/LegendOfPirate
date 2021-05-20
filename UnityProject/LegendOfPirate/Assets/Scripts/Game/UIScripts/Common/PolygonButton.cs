using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace GameWish.Game
{
    public class PolygonButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        public Action onClick = null;

        private Image m_Image = null;

        private void Start()
        {
            m_Image = GetComponent<Image>();
            m_Image.alphaHitTestMinimumThreshold = 0.1f;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (onClick != null)
            {
                onClick.Invoke();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            m_Image.color = Color.gray;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            m_Image.color = Color.white;
        }
    }
}