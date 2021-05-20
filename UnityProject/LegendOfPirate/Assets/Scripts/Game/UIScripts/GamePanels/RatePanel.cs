using System.Collections;
using System.Collections.Generic;
using GameWish.Game;
using Qarth;
using UnityEngine;
using UnityEngine.UI;

public class RatePanel : AbstractAnimPanel
{

    [SerializeField] private Toggle[] m_StarToggles;

    [SerializeField] private Text m_TitleText;
    [SerializeField] private Text m_DescribText;
    [SerializeField] private Text m_RateTipText;
    [SerializeField] private Button m_CloseBtn;

    protected override void OnUIInit()
    {
        base.OnUIInit();
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

        //m_TitleText.text = TDLanguageTable.Get("RatePanel_RateContent");
        m_DescribText.text = TDLanguageTable.Get("rate_panel_rateContent");
        m_RateTipText.text = TDLanguageTable.Get("rate_panel_rate_tip");

        m_CloseBtn.onClick.AddListener(() => { HideSelfWithAnim(); });
    }

    protected override void OnPanelOpen(params object[] args)
    {
        base.OnPanelOpen(args);
        OpenDependPanel(EngineUI.MaskPanel,-1);
    }

    protected override void OnPanelShowComplete()
    {
        base.OnPanelShowComplete();
    }

    protected override void OnPanelHideComplete()
    {
        base.OnPanelHideComplete();
        CloseSelfPanel();
    }

    private void ShowStar(int index)
    {
        PlayerPrefs.SetInt(Define.RATE_RECORD,-1);

        DataAnalysisMgr.S.CustomEvent("RateStar", index.ToString());
        for (int i = 0; i <= index; i++)
        {
            m_StarToggles[i].isOn = true;
        }

        if (index >= 3)
        {
            SocialMgr.S.OpenMarketRatePage();
        }
        HideSelfWithAnim();
    }

    public override BackKeyCodeResult OnBackKeyDown()
    {
        HideSelfWithAnim();
        return BackKeyCodeResult.PROCESS;
    }
}
