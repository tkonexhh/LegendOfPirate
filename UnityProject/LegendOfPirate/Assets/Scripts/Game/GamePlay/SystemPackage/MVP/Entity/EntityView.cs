using Qarth.Extension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Qarth.Extension
{
    public class EntityView : MonoBehaviour, IEntityView
    {
        protected IEntityPresenter<EntityView> m_Presenter;

        public IEntityPresenter<EntityView> Presenter { get { return m_Presenter; } }

        public void SetPresenter(IEntityPresenter<EntityView> presenter)
        {
            m_Presenter = presenter;
        }
    }
}