using UnityEngine;
using System.Collections;
using Qarth;

namespace GameWish.Game
{
    public class EntityModel : BaseComponent
    {
        protected ResLoader m_Loader;
        protected GameObject m_ObjModel;

        public Transform TrsModel
        {
            get
            {
                return m_ObjModel == null ? null : m_ObjModel.transform;
            }
        }

        public GameObject ObjModel
        {
            get { return m_ObjModel; }
        }

        public EntityModel(ResLoader loader)
        {
            m_Loader = loader;
        }

        public override void OnDestory()
        {
            m_Loader = null;
            m_ObjModel = null;
        }
    }
}