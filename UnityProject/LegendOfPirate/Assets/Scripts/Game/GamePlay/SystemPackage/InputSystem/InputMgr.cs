using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using System;
using HedgehogTeam.EasyTouch;

namespace GameWish.Game
{
    public class InputMgr : TSingleton<InputMgr>
    {
        private List<IInputObserver> m_TouchObservers = new List<IInputObserver>();

        private bool m_IsTouchStartFromUI = false;

        private bool m_IsDragEnabled = true;

        private bool m_LongTimeNoTouch;

        private int m_TimerId = -1;
        private DateTime m_Time;

        public bool IsDragEnabled
        {
            get { return m_IsDragEnabled; }
            set { m_IsDragEnabled = value; }
        }

        public bool LongTimeNoTouch
        {
            get
            {
                return m_LongTimeNoTouch;
            }

            set
            {
                m_LongTimeNoTouch = value;
            }
        }

        public void Init()
        {
            EasyTouch.On_TouchStart += On_TouchStart;
            //EasyTouch.On_TouchDown += On_TouchDown;
            //EasyTouch.On_TouchUp += On_TouchUp;
            //EasyTouch.On_Drag += On_Drag;
            //EasyTouch.On_LongTap += On_LongTap;
            //EasyTouch.On_Swipe += On_Swipe;
        }

        public void AddTouchObserver(IInputObserver ob)
        {
            if (!m_TouchObservers.Contains(ob))
            {
                m_TouchObservers.Add(ob);
            }
        }

        public void RemoveTouchObserver(IInputObserver ob)
        {
            if (m_TouchObservers.Contains(ob))
            {
                m_TouchObservers.Remove(ob);
            }
        }

        private void On_TouchStart(Gesture gesture)
        {
            {
                foreach (var ob in m_TouchObservers)
                {
                    ob.On_TouchStart(gesture);
                }
            }
        }

        private void On_TouchDown(Gesture gesture)
        {
            m_Time = DateTime.Now;
            //HideTouchTip();

            foreach (var ob in m_TouchObservers)
            {
                ob.On_TouchDown(gesture);
            }
        }

        private void On_TouchUp(Gesture gesture)
        {
            //start check
            //if (AbTestActor.IsShowHandTip())
            //{
            //    Timer.S.Cancel(m_TimerId);
            //    m_TimerId = Timer.S.Post2Really(CheckLongTimeNoTouch, Define.TOUCHE_TIP_INTERVAL);
            //}    

            foreach (var ob in m_TouchObservers)
            {
                ob.On_TouchUp(gesture);
            }
        }

        private void On_Drag(Gesture gesture)
        {
            if (IsDragEnabled == false)
                return;

            foreach (var ob in m_TouchObservers)
            {
                ob.On_Drag(gesture, m_IsTouchStartFromUI);
            }
        }

        private void On_Swipe(Gesture gesture)
        {
            foreach (var ob in m_TouchObservers)
            {
                ob.On_Swipe(gesture);
            }
        }

        private void On_LongTap(Gesture gesture)
        {
            foreach (var ob in m_TouchObservers)
            {
                ob.On_LongTap(gesture);
            }
        }

        private void CheckLongTimeNoTouch(int i)
        {
            Log.i("show hand click tip");
            LongTimeNoTouch = true;
            //EffectMgr.S.ShowTouchTip(UIMgr.S.FindPanel(UIID.MainGamePanel).transform,Vector3.zero);
        }

        //private void HideTouchTip()
        //{
        //    if (LongTimeNoTouch)
        //    {
        //        LongTimeNoTouch = false;
        //        EffectMgr.S.HideTouchTip();
        //    }
        //}
    }
}