using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameWish.Game
{
	public class ParallelNode : ActionNode
	{
        private Queue<IActionNode> m_NodeQueue;

        public ParallelNode()
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
        }

        public ParallelNode Append(IActionNode node)
        {
            m_NodeQueue.Enqueue(node);

            return this;
        }

        public override void Execute()
        {
            while (m_NodeQueue.Count > 0)
            {

            }
        }
    }
	
}