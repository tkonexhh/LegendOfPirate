using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class GPUInstanceMgr : TMonoSingleton<GPUInstanceMgr>
    {
        [SerializeField] public int count;// => m_RenderGroupLst.Count;
        private List<GPUInstanceGroup> m_RenderGroupLst = new List<GPUInstanceGroup>();
        private Dictionary<string, GPUInstanceGroup> m_RenderGroupMap;

        public override void OnSingletonInit()
        {
            m_RenderGroupMap = new Dictionary<string, GPUInstanceGroup>();
        }


        public GPUInstanceGroup AddRenderGroup(GPUInstanceGroup group)
        {
            if (m_RenderGroupMap.ContainsKey(group.name))
            {
                return m_RenderGroupMap[group.name];
            }
            m_RenderGroupLst.Add(group);
            m_RenderGroupMap.Add(group.name, group);
            return group;
        }

        public bool HasRenderGroup(string groupName)
        {
            return m_RenderGroupMap.ContainsKey(groupName);
        }


        public GPUInstanceGroup GetRenderGroup(string group)
        {
            if (!m_RenderGroupMap.ContainsKey(group))
            {
                return null;
            }

            return m_RenderGroupMap[group];
        }



        private void Update()
        {
            for (int i = 0; i < m_RenderGroupLst.Count; i++)
            {
                m_RenderGroupLst[i].Draw();
            }
            count = m_RenderGroupLst.Count;
        }
    }

}