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

        private bool m_EnableInput = true;

        public void Init()
        {
            m_EnableInput = true;
            EasyTouch.On_TouchStart += On_TouchStart;
            EasyTouch.On_TouchDown += On_TouchDown;
            EasyTouch.On_TouchUp += On_TouchUp;
            EasyTouch.On_Drag += On_Drag;
            //EasyTouch.On_LongTap += On_LongTap;
            EasyTouch.On_Swipe += On_Swipe;
        }

        public void AddTouchObserver(IInputObserver ob)
        {
            if (!m_TouchObservers.Contains(ob))
            {
                m_TouchObservers.Add(ob);

                m_TouchObservers.Sort((a, b) => { return b.GetSortingLayer() - a.GetSortingLayer(); });
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
            if (!m_EnableInput)
                return;

            foreach (var ob in m_TouchObservers)
            {
                bool touched = ob.On_TouchStart(gesture);

                if (touched && ob.BlockInput())
                    break;
            }

        }

        private void On_TouchDown(Gesture gesture)
        {
            if (!m_EnableInput)
                return;

            m_Time = DateTime.Now;
            //HideTouchTip();

            foreach (var ob in m_TouchObservers)
            {
                bool touched = ob.On_TouchDown(gesture);

                if (touched && ob.BlockInput())
                    break;
            }
        }

        private void On_TouchUp(Gesture gesture)
        {
            if (!m_EnableInput)
                return;

            //start check
            //if (AbTestActor.IsShowHandTip())
            //{
            //    Timer.S.Cancel(m_TimerId);
            //    m_TimerId = Timer.S.Post2Really(CheckLongTimeNoTouch, Define.TOUCHE_TIP_INTERVAL);
            //}    

            foreach (var ob in m_TouchObservers)
            {
                bool touched = ob.On_TouchUp(gesture);

                if (touched && ob.BlockInput())
                    break;
            }
        }

        private void On_Drag(Gesture gesture)
        {
            if (!m_EnableInput)
                return;

            if (IsDragEnabled == false)
                return;

            foreach (var ob in m_TouchObservers)
            {
                bool touched = ob.On_Drag(gesture, m_IsTouchStartFromUI);

                if (touched && ob.BlockInput())
                    break;
            }

            //Log.e("On drag");
        }

        private void On_Swipe(Gesture gesture)
        {
            if (!m_EnableInput)
                return;

            foreach (var ob in m_TouchObservers)
            {
                bool touched = ob.On_Swipe(gesture);

                if (touched && ob.BlockInput())
                    break;
            }

            //Log.e("On swipe");
        }

        private void On_LongTap(Gesture gesture)
        {
            foreach (var ob in m_TouchObservers)
            {
                bool touched = ob.On_LongTap(gesture);

                if (touched && ob.BlockInput())
                    break;
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

        public void EnableInput()
        {
            m_EnableInput = true;
        }

        public void DisableInput()
        {
            m_EnableInput = false;
        }
    }
}