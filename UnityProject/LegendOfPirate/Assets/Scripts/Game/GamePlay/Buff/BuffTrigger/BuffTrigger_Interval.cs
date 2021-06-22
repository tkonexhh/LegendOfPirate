using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
    public class BuffTrigger_Interval : BuffTrigger
    {
        private float m_Time;
        private float m_Timer;

        public BuffTrigger_Interval(float time)
        {
            m_Time = time;
        }

        public override void Start(Buff buff)
        {
            base.Start(buff);
            m_Timer = 0;
            buff.Owner.AI.onUpdate += OnUpdate;
        }

        public override void Stop(Buff buff)
        {
            base.Stop(buff);
            buff.Owner.AI.onUpdate -= OnUpdate;
        }

        private void OnUpdate()
        {
            m_Timer += Time.deltaTime;
            if (m_Timer >= m_Time)
            {
                m_Timer = 0;
                OnTrigger();
            }
        }

    }

}