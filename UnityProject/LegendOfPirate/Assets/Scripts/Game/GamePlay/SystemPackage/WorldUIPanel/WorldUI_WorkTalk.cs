using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Qarth;
using TMPro;
using System;

namespace GameWish.Game
{
    public class WorldUI_WorkTalk : WorldUIBindTransform
    {
        [SerializeField] private TextMeshProUGUI m_TxtTalk;

        public bool isPlayer = false;
        protected override bool IsNeedUpdate()
        {
            return false;
        }

        public void SetStyle(InjuryType injuryType)
        {
            switch (injuryType)
            {
                case InjuryType.CommonInjury:
                    m_TxtTalk.color = Color.white;
                    break;
                case InjuryType.CriticalInjury:
                    m_TxtTalk.color = Color.yellow;
                    break;
                case InjuryType.AbnormalInjury:
                    m_TxtTalk.color = Color.blue;
                    break;
            }
        }
      
        public void SetText(string text)
        {
            m_TxtTalk.text = text;
        }

        public void UpdatePosition()
        {
            m_Binding?.Update();
        }

        private Vector3 m_StartPoint;
        private Vector3 m_MiddlePoint;
        private Vector3 m_EndPoint;

        private bool m_IsExercise = false;
        private float m_DeletaT = 0;

        private float m_ExerciseTime = 0;
        private float m_Height = 0;
        private float ticker = 0.0f;

        public Action OnOverEvent;


        private void Update()
        {
            if (m_IsExercise)
            {
                ticker += Time.deltaTime;
                m_DeletaT = ticker / m_ExerciseTime;  // 
                m_DeletaT = Mathf.Clamp(m_DeletaT, 0.0f, 1.0f);
                transform.localPosition = Bezier_2(m_StartPoint, m_MiddlePoint, m_EndPoint, m_DeletaT);
                //Debug.LogError("m_DeletaT = " + m_DeletaT);
                //Debug.LogError("ticker = " + ticker);
                if (Mathf.Abs(m_DeletaT-1)<0.3f)
                {
                    m_IsExercise = false;
                    OnOverEvent?.Invoke();
                }
            }
        }

        /// <summary>
        /// 二次贝塞尔
        /// </summary>
        public  Vector3 Bezier_2(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            return (1 - t) * ((1 - t) * p0 + t * p1) + t * ((1 - t) * p1 + t * p2);
        }
        /// <summary>
        /// 三次贝塞尔
        /// </summary>
        public  Vector3 Bezier_3(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            return (1 - t) * ((1 - t) * ((1 - t) * p0 + t * p1) + t * ((1 - t) * p1 + t * p2)) + t * ((1 - t) * ((1 - t) * p1 + t * p2) + t * ((1 - t) * p2 + t * p3));
        }

        public void SetExerciseTime(float time,float height,float xDelta,Action action)
        {
            m_IsExercise = true;
            ticker = 0;
            OnOverEvent = action;
            m_ExerciseTime = time;
            m_Height = height;
            m_StartPoint = transform.localPosition;
            m_EndPoint = m_StartPoint+new Vector3 (xDelta,0,0);

            m_MiddlePoint = new Vector3(m_StartPoint.x+0.75f *(m_EndPoint.x- m_StartPoint.x), m_Height,0);

        }
    }
}
