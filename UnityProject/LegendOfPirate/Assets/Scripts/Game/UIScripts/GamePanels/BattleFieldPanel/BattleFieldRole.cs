using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using UnityEngine.EventSystems;
using TMPro;

namespace GameWish.Game
{
    public class BattleFieldRole : IUListItemView, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private TextMeshProUGUI m_TMPTitle;

        public void SetRole(int index)
        {
            m_TMPTitle.text = index.ToString();
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.LogError("OnBeginDrag");
        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.LogError("OnDrag");
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.LogError("OnEndDrag");
        }
    }

}