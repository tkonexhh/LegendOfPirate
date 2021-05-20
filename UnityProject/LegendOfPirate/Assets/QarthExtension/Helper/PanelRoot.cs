using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
namespace GameWish.Game
{
    public class PanelRoot : MonoBehaviour 
    {
        [SerializeField]
        private Transform m_PanelRoot;
        [SerializeField]
        private GameObject m_ObjMaskBg;
        private void Awake()
        {
            SetCamViewPortRect();
        }
        public void SetCamViewPortRect()
        {
            if (m_ObjMaskBg)
                m_ObjMaskBg.SetActive(false);

#if UNITY_IOS
            if (ScreenAdjustMgr.S.GetScreenType() == ScreenType.IPhoneX)
            {
                m_PanelRoot.rectTransform().offsetMin = new Vector2(0, 30);
                m_PanelRoot.rectTransform().offsetMax = new Vector2(0, -55);
                if(m_ObjMaskBg!=null)
                m_ObjMaskBg.SetActive(true);
            }
            else
            {
                m_PanelRoot.rectTransform().offsetMin = Vector2.zero;
                m_PanelRoot.rectTransform().offsetMax = Vector2.zero;
                if (m_ObjMaskBg != null)
                m_ObjMaskBg.SetActive(false);
            }
#endif
        }
    }
}
