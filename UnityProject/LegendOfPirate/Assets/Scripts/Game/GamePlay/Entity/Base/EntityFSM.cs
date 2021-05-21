using UnityEngine;
using System.Collections;
using Qarth;

namespace GameWish.Game
{
    public class EntityFSM : BaseComponent
    {
        protected FSMStateMachine<EntityBase> m_FSMEntity;
        public FSMStateMachine<EntityBase> fsmMachine
        {
            get { return m_FSMEntity; }
        }

        public override void InitComponent(EntityBase owner)
        {
            base.InitComponent(owner);

            m_FSMEntity = new FSMStateMachine<EntityBase>(m_Owner);
            m_FSMEntity.stateFactory = new FSMStateFactory<EntityBase>(false);
        }

        public override void Tick(float deltaTime)
        {
            if (m_FSMEntity != null)
                m_FSMEntity.UpdateState(deltaTime);
        }

        public virtual void RegisterState()
        {

        }
    }
}