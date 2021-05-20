using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using UnityEngine.UI;
using DG.Tweening;
using System;
using System.Text;

namespace GameWish.Game
{
    public class MyGuideTipsPanel : AbstractPanel
    {
        [SerializeField]
        private Text m_GuideTips;
        [SerializeField]
        private Transform m_Root;
        [SerializeField]
        private Button m_ContinueBtn;

        [SerializeField]
        private Image m_GuideManImg;
        [SerializeField]
        private Image m_GuideTextBgImg;
        [SerializeField]
        private Image m_NextImg;

        [SerializeField]
        private Vector3 m_Offset;


        private string[] m_StringArray;

        private int m_TextIndex = 0;
        private Action m_EndCallBack;


        private Tweener m_TextTween;
        private Tweener m_FirstImgTween;
        private Tweener m_SecondImgTween;
        private Tweener m_NextTween;

        private Sequence m_ShowAnimeSequence;

        private bool m_IsTypewriting = false;
        private bool m_IsEndTypeWriting = false;
        private bool m_IsCanContinueNext = false;

        protected override void OnUIInit()
        {
            base.OnUIInit();
            m_ContinueBtn.onClick.AddListener(TapContinue);
        }
        protected override void BeforDestroy()
        {
            base.BeforDestroy();
            m_ContinueBtn.onClick.RemoveAllListeners();
         
        }
        protected override void OnOpen()
        {
            base.OnOpen();
        
        }
        protected override void OnClose()
        {
            base.OnClose();
         
        }
    



        protected override void OnPanelOpen(params object[] args)
        {
            base.OnPanelOpen(args);


            if (args.Length > 0)
            {
                 m_StringArray = (string[])args[0];

            }


            if (args.Length > 1)
            {
                m_EndCallBack = args[1] as Action;
            }

            if (args.Length > 2)
            {
                m_Offset = (Vector3)args[2];
                m_Root.transform.localPosition = m_Offset;
            }

            m_TextIndex = 0;
            m_NextImg.gameObject.SetActive(false);
            ShowGuideImgByAnimate();

        }






 

        private void ShowGuideImgByAnimate()
        {
            m_GuideManImg.color = new Color(1,1,1,0);
            m_GuideTextBgImg.color = new Color(1, 1, 1, 0);
            m_GuideTips.text = string.Empty;
            m_ShowAnimeSequence = DOTween.Sequence();
            m_ShowAnimeSequence.Append(m_GuideManImg.DOFade(1, 0.2f));
            m_ShowAnimeSequence.AppendInterval(0.3f);
            m_ShowAnimeSequence.Append(m_GuideTextBgImg.DOFade(1, 0.2f));
            m_ShowAnimeSequence.AppendCallback(()=> {

                DoTypeWritingText(m_StringArray[0]);

            });


        }
        //持续点击
        private void TapContinue()
        {
            if (m_IsEndTypeWriting && m_IsCanContinueNext)
            {
                ToNextStep();
                m_IsCanContinueNext = false;
            }
            //else
            //{
            //    CompleteTextImmediately();
            //}
        }
        private void ToNextStep()
        {

            if (m_TextIndex >= m_StringArray.Length - 1)
            {
                OnEndBtnClick();
            }
            else
            {
                m_TextIndex++;
                DoTypeWritingText(m_StringArray[m_TextIndex]);
            }


        }
        private void OnEndBtnClick()
        {
            CloseSelfPanel();
            if (m_EndCallBack != null)
            {
                m_EndCallBack();
            }


        }

        private void CompleteTextImmediately()
        {

            if (m_IsTypewriting)
            {
                m_TextTween.Complete();
                m_GuideManImg.color = Color.white;
                m_GuideTextBgImg.color = Color.white;
                m_IsTypewriting = false;
            }
        }


        public void DoTypeWritingText(string GuideText)
        {
            m_GuideTips.text = string.Empty;
            m_IsTypewriting = true;
            m_IsEndTypeWriting = false;
            m_TextTween.Kill();


            m_NextImg.gameObject.SetActive(false);

            m_TextTween =  m_GuideTips.DoText(GuideText, 0.03f).SetEase(Ease.Linear).OnComplete(()=> {

                m_IsTypewriting = false;
                m_IsEndTypeWriting = true;
                Timer.S.Post2Really((x) => {

                    m_IsCanContinueNext = true;
                    m_NextImg.gameObject.SetActive(true);
                    if(m_NextTween == null)
                    {
                        m_NextTween = m_NextImg.transform.DOBlendableMoveBy(new Vector3(0.1f, 0, 0), 0.5f).SetLoops(-1, LoopType.Yoyo);
                    }
                   
                }, 0.5f);
            });
        }

 








    }
}

