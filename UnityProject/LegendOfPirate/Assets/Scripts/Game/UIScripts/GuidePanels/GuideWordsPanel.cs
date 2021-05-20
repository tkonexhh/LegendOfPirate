using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Qarth;
using UnityEngine;
using UnityEngine.UI;

public class GuideWordsPanel : AbstractPanel
{
    [Header("MyFields")] 
    [SerializeField] private Text m_DisText;

    [SerializeField] private Transform m_Body;

    protected override void OnPanelOpen(params object[] args)
    {
        base.OnPanelOpen(args);

        if (args != null)
        {
            m_DisText.text = TDLanguageTable.Get((string) args[1]);
            if (bool.Parse((string)args[0]))
            {
                OpenDependPanel(EngineUI.MaskPanel,-1);
            }
        }

        //m_Body.DOLocalMoveX(-144, 0.3f);
    }
}
