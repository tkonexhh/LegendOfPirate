using System.Collections;
using System.Collections.Generic;
using HedgehogTeam.EasyTouch;
using UnityEngine;


namespace GameWish.Game
{
    public class ClickableView : View, IInputObserver
    {
        protected Collider m_Collider = null;

        protected virtual void Awake()
        {
            InputMgr.S.AddTouchObserver(this);    
        }

        public virtual bool BlockInput()
        {
            return true;
        }

        public virtual int GetSortingLayer()
        {
            return Define.INPUT_SORTING_ORDER_DEFAULT;
        }

        public bool On_Drag(Gesture gesture, bool isTouchStartFromUI)
        {
            return false;
        }

        public bool On_LongTap(Gesture gesture)
        {
            return false;
        }

        public bool On_Swipe(Gesture gesture)
        {
            return false;
        }

        public bool On_TouchDown(Gesture gesture)
        {
            return false;
        }

        public bool On_TouchStart(Gesture gesture)
        {
            if (gesture.IsOverUIElement())
                return false;

            RaycastHit hit;
            bool isHit = Physics.Raycast(Camera.main.ScreenPointToRay(gesture.position), out hit, 1000f, 1 << LayerMask.NameToLayer(Define.GAME_LAYER));
            if (isHit && hit.collider != null && hit.collider == m_Collider)
            {
                OnClicked();
                return true;
            }

            return false;
        }

        public bool On_TouchUp(Gesture gesture)
        {
            return false;
        }

        protected virtual void OnClicked()
        {
            Qarth.Log.i("On Clicked: " + transform.name);
        }
    }

}