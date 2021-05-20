using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using UnityEngine.UI;
using DG.Tweening;

public enum GuideMethod
{
    Method1,
    Method2,
    Method3,
    NoBlack,
    NoMessage,
}
namespace GameWish.Game
{
    public class MyGuidePanel : AbstractPanel
    {
        [SerializeField]
        private CircleShaderControl m_CircleShaderControl;

        [SerializeField]
        private RectTransform m_TargetRect;

        [SerializeField]
        private Transform m_GuideHandBg;
        [SerializeField]
        private Transform m_GuideMove;
        [SerializeField]
        private Transform m_Hand;
        [SerializeField]
        private Transform m_Hand2;
      
        [SerializeField]
        private Transform m_GuideMethod1;

        [SerializeField]
        private Transform m_GuideMethod2;
     
        [SerializeField]
        private Transform m_Method2Start;
        [SerializeField]
        private Transform m_Method2End;


        [SerializeField]
        private Transform m_GuideMethod3;
        [SerializeField]
        private Transform m_GuideMove3;
        [SerializeField]
        private Transform m_DownArrow;

        [SerializeField]
        private Image m_GuideTipsImg;
        [SerializeField]
        private Text m_GuideTipsText;




        private Vector3 m_StarHand;
        private Vector3 m_StarArrow;
        private GuideMethod m_GuestMethod = GuideMethod.Method1;
        private int m_GuidestepId = -1;
        protected override void OnUIInit()
        {
            base.OnUIInit();

            m_StarHand = m_Hand.transform.localPosition;

            m_StarArrow = m_DownArrow.transform.localPosition;
              
            

        }
        protected override void OnOpen()
        {
            base.OnOpen();
           
        }
        



        protected override void OnPanelOpen(params object[] args)
        {
            base.OnPanelOpen(args);
            if(args.Length > 0)
            {
                m_TargetRect = args[0] as RectTransform;
                m_Hand.transform.localPosition = m_StarHand;
                // m_TargetRect.GetComponent<Button>().onClick.AddListener(CloseSelfPanel);
                m_DownArrow.transform.localPosition = m_StarArrow;
            }
            if (args.Length > 1)
            {
                m_GuidestepId = (int)args[1];
            }

            if (args.Length > 2)
            {
                m_GuestMethod = (GuideMethod)args[2];
            }

            if(args.Length > 4)
            {
                m_Method2Start = args[3] as Transform;
                m_Method2End = args[4] as Transform;
            }

            //是否有黑色遮罩
            if(m_GuestMethod == GuideMethod.NoBlack)
            {
                m_CircleShaderControl.InitAsNoBlack(m_TargetRect);
            }
            else
            {
                m_CircleShaderControl.Init(m_TargetRect);
            }

            if(m_GuestMethod == GuideMethod.NoMessage)
            {
                m_GuideTipsImg.gameObject.SetActive(false);
            }
            else
            {
                m_GuideTipsImg.gameObject.SetActive(true);
            }

            CheckMyUIState();
            EventSystem.S.Send(EventID.OnGuidePanelOpen, m_GuidestepId);

        }


        protected override void OnClose()
        {
            m_CircleShaderControl.EndGuide();
            m_Hand.DOKill();
            m_Hand2.DOKill();
            m_DownArrow.DOKill();
           // m_TargetRect.GetComponent<Button>().onClick.RemoveListener(CloseSelfPanel);
        
            base.OnClose();

        }


        private void  LocadGuideHand()
        {
            m_GuideHandBg.transform.position = m_TargetRect.transform.position;
            Vector3 MovePos = m_GuideMove.transform.position;

            m_Hand.transform.DOMove(MovePos, 0.7f).SetLoops(-1,LoopType.Yoyo);

        }

        private void LocadGuideArrow()
        {
            m_GuideMethod3.transform.position = m_TargetRect.transform.position;
            Vector3 MovePos = m_GuideMove3.transform.position;

            m_DownArrow.transform.DOMove(MovePos, 0.7f).SetLoops(-1, LoopType.Yoyo);

        }
        private void HandMove2Target()
        {
            m_Hand2.transform.position = m_Method2Start.transform.position;
            Vector3 MovePos = m_Method2End.transform.position;

            m_Hand2.transform.DOMove(MovePos, 1.5f).SetLoops(-1, LoopType.Restart);

        }


        private void CheckMyUIState()
        {
            m_GuideMethod1.gameObject.SetActive(false);
            m_GuideMethod2.gameObject.SetActive(false);
            m_GuideMethod3.gameObject.SetActive(false);
            switch (m_GuestMethod)
            {
                case GuideMethod.Method1:
                    m_GuideMethod1.gameObject.SetActive(true);
                    LocadGuideHand();
                    break;
                case GuideMethod.Method2:
                    m_GuideMethod2.gameObject.SetActive(true);
                 
                    HandMove2Target();
                    break;
                case GuideMethod.Method3:
                    m_GuideMethod3.gameObject.SetActive(true);
                    LocadGuideArrow();

                    break;
                case GuideMethod.NoMessage:
                    m_GuideMethod1.gameObject.SetActive(true);
                    LocadGuideHand();

                    break;
                default:
                    break;
            }

        }




        public void LocateMyGuideTips(string GuideTips, Vector3 position)
        {
            m_GuideTipsImg.gameObject.SetActive(true);
            m_GuideTipsImg.transform.localPosition = position;
            m_GuideTipsText.text = GuideTips;
        }



    }
}

