using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;
using UnityEngine.UI;

namespace GameWish.Game
{
    public class EffectActivePanel : AbstractAnimPanel
    {
        [SerializeField]
        private Text m_TitleText = null;
        //[SerializeField]
        //private Text m_DescriptionText = null;
        [SerializeField]
        private Image m_ContentImage = null;
        [SerializeField]
        private Button m_ConfirmBtn = null;
        [SerializeField]
        private Text m_DetailText = null;

        //[SerializeField] private Transform m_ParticalObj;

        private Vector3 m_IconScale = Vector3.one;

        private Action m_ClostAction;

        public static bool s_IsOpening = false;

        protected override void OnUIInit()
        {
            base.OnUIInit();

            if (I18Mgr.S.language == SystemLanguage.German)
            {
                m_DetailText.fontSize = 30;
            }
            else if (I18Mgr.S.language == SystemLanguage.English)
            {
                m_DetailText.fontSize = 35;
            }
        }

        protected override void OnPanelOpen(params object[] args)
        {
            base.OnPanelOpen(args);

            OpenDependPanel(EngineUI.MaskPanel, -1);

            EffectActivePanelData data = (EffectActivePanelData)args[0];

            if (args.Length > 1)
            {
                m_IconScale = (Vector3)args[1];
            }

            Refresh(data);

            s_IsOpening = true;
            //UnityExtensions.CallWithDelay(this, delegate
            //{
            //    foreach (var p in m_ParticalObj.GetComponentsInChildren<ParticleSystem>())
            //    {
            //        p.GetComponent<Renderer>().sortingOrder = m_SortingOrder;
            //    }
            //}, -1);


            StartCoroutine(WaiteToClose());
        }

        protected override void OnPanelShowComplete()
        {
            base.OnPanelShowComplete();
        }

        protected override void OnPanelHideBegin()
        {
            base.OnPanelHideBegin();
            if (m_ClostAction != null)
            {
                m_ClostAction.Invoke();
            }
        }

        protected override void OnPanelHideComplete()
        {
            base.OnPanelHideComplete();
            CloseSelfPanel();
        }

        protected override void OnClose()
        {
            base.OnClose();
            m_ClostAction = null;
            s_IsOpening = false;
            //EventSystem.S.Send(EventID.OnEffectActivePanelClosed);
        }

        private void Refresh(EffectActivePanelData data)
        {
            //if (!string.IsNullOrEmpty(data.bgImageName))
            //{
            //    m_BgImage.sprite = SpriteLoader.S.GetSpriteByName(data.bgImageName);
            //}

            if (!string.IsNullOrEmpty(data.contentImageName))
            {
                m_ContentImage.sprite = SpriteLoader.S.GetSpriteByName(data.contentImageName);
                m_ContentImage.SetNativeSize();
                m_ContentImage.transform.localScale = m_IconScale;
            }

            m_TitleText.text = data.titleText;
            m_DetailText.text = data.descriptionText;
            //m_DescriptionText.text = data.descriptionText;

            //m_CloseBtn.onClick.RemoveAllListeners();
            //m_CloseBtn.onClick.AddListener(() =>
            //{
            //    if (data.closeCallback != null)
            //    {
            //        data.closeCallback.Invoke();
            //    }

            //    HideSelfWithAnim();
            //});

            m_ConfirmBtn.onClick.RemoveAllListeners();
            m_ConfirmBtn.onClick.AddListener(() =>
            {
                if (data.confirmCallback != null)
                {
                    data.confirmCallback.Invoke();
                }
               // AudioMgr.S.PlaySound(Define.SOUND_BUTTON_CLICK);
                HideSelfWithAnim();
            });

            if (data.closeCallback != null)
            {
                m_ClostAction = data.closeCallback;
            }
        }

        IEnumerator WaiteToClose()
        {
            yield return new WaitForSeconds(1.5f);
            HideSelfWithAnim();
        }

        private void HandleEvent(int id, params object[] param)
        {
            //switch ((EventID)id)
            //{
            //    case EventID.OnAddCoinNum:
            //        m_Body.SetUpgradeBtnEnable();
            //        break;
            //}
        }
    }
}