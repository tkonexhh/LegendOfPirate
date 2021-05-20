using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Qarth;
using System;

namespace GameWish.Game
{
    public class RatePanel : AbstractPanel
    {
        [SerializeField]
        private Toggle[] m_StarToggles;
        [SerializeField]
        private Text m_TitleText;
        [SerializeField]
        private Text m_DescribeLabel;

        private int m_StarIndex;

        protected override void OnUIInit()
        {
            m_TitleText.text = TDLanguageTable.Get("RateTitle");
            m_DescribeLabel.text = TDLanguageTable.Get("RateDescription");

            for (int i = 0; i < m_StarToggles.Length; i++)
            {
                int index = i;
                m_StarToggles[i].onValueChanged.AddListener((bool isSelect) =>
                {
                    if (isSelect)
                    {
                        ShowStar(index);
                    }
                });
            }
        }


        protected override void OnOpen()
        {
            base.OnOpen();
            for (int i = 0; i < 5; i++)
            {
                m_StarToggles[i].isOn = false;
              
            }
            OpenDependPanel(EngineUI.MaskPanel, -1, null);
        }

        //protected override void OnPanelHideComplete()
        //{
        //    base.OnPanelHideComplete();
        //    CloseDependPanel(EngineUI.MaskPanel);
        //    CloseSelfPanel();

        //}

        //public override BackKeyCodeResult OnBackKeyDown()
        //{
        //    HideSelfWithAnim();
        //    return BackKeyCodeResult.PROCESS_AND_BLOCK;
        //}

        protected override void OnClose()
        {
            base.OnClose();
            //UIMgrExtend.S.CheckNextQueue();
        }

        private void ShowStar(int index)
        {
            PlayerPrefs.SetInt("HasRate", 1);
           // DataAnalysisMgr.S.CustomEvent("RateStar", index.ToString());
            for (int i = 0; i <= index; i++)
            {
                m_StarToggles[i].isOn = true;
                //m_StarToggles[i].GetComponent<Image>().sprite = FindSprite("G-3",true);
            }
            m_StarIndex = index;

            // if(m_StarIndex != -1)
            // {
            //     m_SubmitButton.enabled = true;
            // }

            //m_DescribeLabel.text = TDLanguageTable.Get("StarDes_" + index); 

            Timer.S.Post2Really((i) =>
            {

                 
                if (m_StarIndex == 4)
                {
                    Log.i("OpenMarketRatePage");
                    SocialMgr.S.OpenMarketRatePage();
                }
                if (!IsClose)
                {
                    CloseSelfPanel();
                    IsClose = true;
                }
              

            }, 0.3f);

           // HideSelfWithAnim();
        }

        private bool IsClose = false;

        private void OnSubmitButtonClick()
        {
            if (m_StarIndex == 4  || m_StarIndex == 3)
            {
                Log.i("OpenMarketRatePage");
                SocialMgr.S.OpenMarketRatePage();
                DataAnalysisMgr.S.LevelFailed("Rate","5");
            }
        }

        #region 
        [SerializeField]
        private Image m_SlideBg;
        private bool m_IsRating;

        private void OnMouseDown()
        {
            m_IsRating = true;
        }

        #endregion

    }
}
