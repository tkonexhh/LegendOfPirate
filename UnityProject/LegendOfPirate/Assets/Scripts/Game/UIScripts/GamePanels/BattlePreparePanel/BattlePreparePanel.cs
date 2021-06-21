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
                HideSelfPanel();
                UIMgr.S.OpenPanel(UIID.BattleFieldPanel);
                // BattleMgr.S.Camera
            });

            m_BtnBack.onClick.AddListener(() =>
            {
                CloseSelfPanel();
                UIMgr.S.OpenPanel(UIID.MainMenuPanel);
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

            if (args != null && args.Length > 0)
            {
                string levelId = args[0].ToString();
                Debug.Log("levelId=" + levelId);
                BattleMgr.S.BattleClean();
                BattleMgr.S.BattleInit(CreateEnemy(levelId) == null ? BattleMgr.S.DemoEnemyFieldConfigSO : CreateEnemy(levelId));
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

            ReleasePanelData();
        }

    }
}
