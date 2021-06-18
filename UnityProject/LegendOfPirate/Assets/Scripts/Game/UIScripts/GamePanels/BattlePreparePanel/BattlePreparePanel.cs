using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
    public partial class BattlePreparePanel : AbstractAnimPanel
    {
        protected override void OnUIInit()
        {
            base.OnUIInit();
            BindModelToUI();
            BindUIToModel();

            m_BtnField.onClick.AddListener(() =>
            {
                CloseSelfPanel();
                UIMgr.S.OpenPanel(UIID.BattleFieldPanel);
                // BattleMgr.S.Camera
            });

            m_BtnBack.onClick.AddListener(() =>
            {
                CloseSelfPanel();
                GameCameraMgr.S.ToSea();
            });

            m_BtnBattleStart.onClick.AddListener(() =>
            {
                CloseSelfPanel();
                BattleMgr.S.BattleStart();
            });
        }

        protected override void OnPanelOpen(params object[] args)
        {
            base.OnPanelOpen(args);

            AllocatePanelData(args);

            // BattleMgr.S.BattleInit(BattleMgr.S.DemoEnemyFieldConfigSO);
        }

        protected override void OnPanelHideComplete()
        {
            base.OnPanelHideComplete();

            CloseSelfPanel();
        }

        protected override void OnClose()
        {
            base.OnClose();

            ReleasePanelData();
        }

    }
}
