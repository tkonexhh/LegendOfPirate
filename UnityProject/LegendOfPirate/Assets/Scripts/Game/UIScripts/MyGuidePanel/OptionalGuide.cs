using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Qarth;

namespace GameWish.Game
{
	public class OptionalGuide : MonoBehaviour
	{
        [SerializeField]
        private Image m_GuideHand;

        [SerializeField]
        private Transform m_GuideTarget;

        [SerializeField]
        private Text m_GuideText;

        private string m_GuideStr;

        private Tweener m_Tweener;

        private Button m_TargetBtn;

        private string m_GuideferbsString;

        private bool IsInit = false;
        private bool IsGuideOver = false;

        private Vector3 StartVector3;

        public bool StartGuide(string Guidestring,Button button) 
        {

            if (!IsNeedGuide())
            {
                return false;
            }

            if (!IsInit)
            {
                m_GuideferbsString = string.Format("OptionalGuide{0}", Guidestring);
                IsInit = true;
                IsGuideOver = PlayerPrefs.GetInt(m_GuideferbsString, 0) == 1;
                StartVector3 = m_GuideHand.transform.position;
                if (IsGuideOver) return false;
            }
         //   Log.e("开始弱引导" + m_GuideferbsString);
            gameObject.SetActive(true);
            m_GuideStr = Guidestring;
            m_TargetBtn = button;
          
           
             m_Tweener =  m_GuideHand.transform.DOMove(m_GuideTarget.transform.position, 0.7f).SetLoops(-1, LoopType.Yoyo);
            m_GuideText.text = TDLanguageTable.Get(Guidestring);

            m_TargetBtn.onClick.AddListener(ClickBtnCallBack);
            return true;
        }

        private void ClickBtnCallBack()
        {
            StopGuide();
        }


        public void StopGuide()
        {
            m_Tweener.Kill();
            PlayerPrefs.SetInt(m_GuideferbsString, 1);
            gameObject.SetActive(false);
            m_TargetBtn.onClick.RemoveListener(ClickBtnCallBack);
        }

        public void HideGuide()
        {
            if (!gameObject.activeSelf) return;
            m_Tweener.Kill();
            m_GuideHand.transform.position = StartVector3;
            gameObject.SetActive(false);
            m_TargetBtn.onClick.RemoveListener(ClickBtnCallBack);
            IsInit = false;
        }

        public bool IsNeedGuide()
        {
            return !IsGuideOver && !IsInit;
        }
        




	}
	
}