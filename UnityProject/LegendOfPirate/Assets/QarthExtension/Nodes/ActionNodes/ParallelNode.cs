using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public class ParallelNode : ActionNode
	{
        private List<IActionNode> m_NodeList;

        public ParallelNode()
        {
            m_NodeList = new List<IActionNode>();
        }

        public override void Recycle2Cache()
        {
            base.Recycle2Cache();

            Recycle2Cache(this);
        }

        public override void OnCacheReset()
        {
            base.OnCacheReset();

            m_NodeList.Clear();
        }

        public ParallelNode SetParams(MonoBehaviour executeBehavior)
        {
            m_ExecuteBehavior = executeBehavior;

            return this;
        }

        public ParallelNode Add(IActionNode node)
        {
            if (!m_NodeList.Contains(node))
            {
                m_NodeList.Add(node);
            }

            return this;
        }

        public override void Execute()
        {
            OnStart();

            for (int i = 0; i < m_NodeList.Count; i++)
            {
                m_NodeList[i].AddOnEndCallback(OnNodeEnd);
            }

            m_ExecuteBehavior.StartCoroutine(ParallelCor());
        }

        private IEnumerator ParallelCor()
        {
            while (m_NodeList.Count > 0)
            {
                yield return null;
            }

            OnEnd();
        }

        private void OnNodeEnd(ActionNode node)
        {
            if (m_NodeList.Contains(node))
            {
                m_NodeList.Remove(node);
            }
        }
    }
	
}