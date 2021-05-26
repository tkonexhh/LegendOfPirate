using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace GameWish.Game
{
    public class BaseComponent
    {
        protected IElement m_Owner;
        public virtual void InitComponent(IElement owner)
        {
            m_Owner = owner;
        }

        #region to override funcs
        public virtual void Start() { }
        public virtual void Tick(float deltaTime) { }
        public virtual void OnDestory() { }
        public virtual void OnDrawGizmos() { }
        #endregion

    }
}