using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
    public partial class BattleFieldPanel : AbstractAnimPanel
    {
        protected override void OnUIInit()
        {
            base.OnUIInit();
            m_BtnBack.onClick.AddListener(() =>
            {
                CloseSelfPanel();
                UIMgr.S.OpenPanel(UIID.BattlePreparePanel);
            });

            m_ScrollView.SetCellRenderer(OnCellRenderer);
        }

        protected override void OnPanelOpen(params object[] args)
        {
            base.OnPanelOpen(args);

            AllocatePanelData(args);

            BindModelToUI();
            BindUIToModel();

            m_ScrollView.SetDataCount(10);
            BattleMgr.S.Camera.ToPrepare();
        }

        protected override void OnPanelHideComplete()
        {
            base.OnPanelHideComplete();

            CloseSelfPanel();
        }

        protected override void OnClose()
        {
            base.OnClose();
            BattleMgr.S.Camera.ToBattle();
            ReleasePanelData();
        }

        private void OnCellRenderer(Transform root, int index)
        {
            root.GetComponent<BattleFieldRole>().SetRole(index);
        }

    }


}
