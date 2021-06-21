using UnityEngine;
using Qarth;


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
                BattleMgr.S.BattleClean();
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
                BattleFieldConfigSO curEnemy = CreateEnemy(levelId);
                BattleMgr.S.BattleInit(curEnemy == null ? BattleMgr.S.DemoEnemyFieldConfigSO : curEnemy, int.Parse(levelId));
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
