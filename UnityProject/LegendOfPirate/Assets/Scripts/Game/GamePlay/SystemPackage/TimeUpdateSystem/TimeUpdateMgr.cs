using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class TimeUpdateMgr : TSingleton<TimeUpdateMgr>
    {
        private int m_TimerId = -1;

        private List<ITimeObserver> m_Observers = new List<ITimeObserver>();
        private List<ITimeObserver> m_FinishedObservers = new List<ITimeObserver>();

        public void AddObserver(ITimeObserver ob)
        {
            if (!m_Observers.Contains(ob))
            {
                m_Observers.Add(ob);

                ob.OnStart();
            }
            else
            {
                Log.w("This time observer has been added before");
            }
        }

        public void RemoveObserver(ITimeObserver ob)
        {
            if (m_Observers.Contains(ob))
            {
                m_Observers.Remove(ob);
            }
            else
            {
                Log.w("This time observer can't be found");
            }
        }

        public void Start()
        {
            //foreach (ITimeObserver ob in m_Observers)
            //{
            //    ob.OnStart();
            //}

            m_TimerId = Timer.S.Post2Really(Tick, 1, -1);
        }

        public void End()
        {
            foreach (ITimeObserver ob in m_Observers)
            {
                ob.OnFinished();
            }
        }

        public void Tick(int count)
        {
            foreach (ITimeObserver ob in m_Observers)
            {
                int interval = ob.GetTickInterval();
                if (count > 0 && count % interval == 0)
                {
                    ob.OnTick(count);
                }

                //if (ob.GetTotalSeconds() > 0 && ob.GetTickCount() >= ob.GetTotalSeconds())
                if (ob.GetTickCount() >= ob.GetTotalSeconds())
                {
                    if (!m_FinishedObservers.Contains(ob))
                    {
                        m_FinishedObservers.Add(ob);
                    }
                }
            }

            // Remove finised observer
            if (m_FinishedObservers.Count > 0)
            {
                foreach (ITimeObserver ob in m_FinishedObservers)
                {
                    if (m_Observers.Contains(ob))
                    {
                        ob.OnFinished();
                        m_Observers.Remove(ob);
                    }
                }

                m_FinishedObservers.Clear();
            }
            EventSystem.S.Send(EventID.OnTimeRefresh);
        }

        public void Pause()
        {
            foreach (ITimeObserver ob in m_Observers)
            {
                ob.OnPause();
            }
        }

        public void Resume()
        {
            foreach (ITimeObserver ob in m_Observers)
            {
                ob.OnResume();
            }
        }
    }
}