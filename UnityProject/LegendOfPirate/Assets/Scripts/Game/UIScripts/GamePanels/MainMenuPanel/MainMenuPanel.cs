using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
    public partial class MainMenuPanel : AbstractAnimPanel
    {
        protected override void OnUIInit()
        {
            base.OnUIInit();
        }

        protected override void OnPanelOpen(params object[] args)
        {
            base.OnPanelOpen(args);
            AllocatePanelData(args);
            BindModelToUI();
            BindUIToModel();
            OnClickAddListener();
            RegisterEvents();
            InitPanelData();
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

            UnregisterEvents();
        }
    }
}
