using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class GuideObjectMgr : TSingleton<GuideObjectMgr>
    {
        private List<IGuideObserver> m_GuideStepObservers = new List<IGuideObserver>();

        public void Init()
        {
            RegisterEvents();
        }

        public void Clear()
        {
            UnregisterEvents();

            m_GuideStepObservers.Clear();
        }

        private void RegisterEvents()
        {
            //EventSystem.S.Register(EventID.OnGuideStepChanged, HandleEvent);
        }

        private void UnregisterEvents()
        {
            //EventSystem.S.UnRegister(EventID.OnGuideStepChanged, HandleEvent);
        }

        private void HandleEvent(int id, params object[] param)
        {
            //if (id == (int)EventID.OnGuideStepChanged)
            //{
            //    foreach (IGuideObserver guide in m_GuideStepObservers)
            //    {
            //        guide.OnGuideStepChanged();
            //    }
            //}
        }

        public void AddObserver(IGuideObserver ob)
        {
            if (!m_GuideStepObservers.Contains(ob))
            {
                //if (AppConfig.S.isGuideActive && GameDataMgr.S.GetWorldData().GetCurAreaMaxUnlockedBlockId() < 6)
                //    ob.InitObjectByGuideStep();

                m_GuideStepObservers.Add(ob);
            }
        }

        public void RemoveObserver(IGuideObserver ob)
        {
            if (m_GuideStepObservers.Contains(ob))
            {
                m_GuideStepObservers.Remove(ob);
            }
        }

        //public bool IsGuideFinish()
        //{
        //    return GuideMgr.S.IsGuideFinish(Define.GUIDE_CLICK_MAP_BTN);
        //}

        public bool IsGuideFinish(int guide)
        {
            return GuideMgr.S.IsGuideFinish(guide);
        }

    }
}