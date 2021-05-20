using Qarth.Extension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qarth.Extension
{
    public abstract class EntityPresenter<TView> : IEntityPresenter<TView> where TView : class, IEntityView
    {
        protected TView m_View = null;

        public TView View { get { return m_View; } }

        public void Init(TView view)
        {
            m_View = view;           
        }

        public abstract void OnInit();
    }

}