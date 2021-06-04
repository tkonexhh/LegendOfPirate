using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public class SequenceNode : ActionNode
	{
        private Queue<IActionNode> m_NodeQueue;
        private IActionNode m_CurNode = null;

        public SequenceNode()
        {
            m_NodeQueue = new Queue<IActionNode>();
        }

        public override void Recycle2Cache()
        {
            base.Recycle2Cache();

            Recycle2Cache(this);
        }

        public override void OnCacheReset()
        {
            base.OnCacheReset();

            m_NodeQueue.Clear();

            m_CurNode = null;
        }

        public SequenceNode SetParams(MonoBehaviour executeBehavior)
        {
            m_ExecuteBehavior = executeBehavior;

            return this;
        }

        public SequenceNode Append(IActionNode node)
        {
            m_NodeQueue.Enqueue(node);

            return this;
        }

        public override void Execute()
        {
            OnStart();

            if (m_NodeQueue.Count <= 0)
            {
                OnEnd();
                return;
            }

            m_CurNode = m_NodeQueue.Dequeue();
            m_CurNode.Execute();

            m_ExecuteBehavior.StartCoroutine(SequenceCor());
        }

        private IEnumerator SequenceCor()
        {
            while (m_NodeQueue.Count > 0 || (m_CurNode != null && m_CurNode.IsFinished == false))
            {
                if (m_CurNode == null || m_CurNode.IsFinished)
                {
                    if (m_NodeQueue.Count > 0)
                    {
                        m_CurNode = m_NodeQueue.Dequeue();
                        m_CurNode.Execute();
                    }
                    else
                    {
                        m_CurNode = null;
                    }
                }

                yield return null;
            }

            OnEnd();
        }
    }
	
}