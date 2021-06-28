using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
    public partial class LevelPeviewPanel : AbstractAnimPanel
    {
        protected override void OnUIInit()
        {
            base.OnUIInit();

            AllocatePanelData();

            BindModelToUI();

            BindUIToModel();

            OnClickAddListener();
        }

        protected override void OnPanelOpen(params object[] args)
        {
            base.OnPanelOpen(args);
            if (args != null && args.Length > 0)
            {
                int levelID = (int)args[0];
                SetCurLevelID(levelID);
            }
            OpenDependPanel(EngineUI.MaskPanel, -1, null);
        }

        protected override void OnPanelHideComplete()
        {
            base.OnPanelHideComplete();

            CloseSelfPanel();
        }

        protected override void OnClose()
        {
            base.OnClose();
            CloseDependPanel(EngineUI.MaskPanel);
        }

        protected override void BeforDestroy()
        {
            base.BeforDestroy();

            ReleasePanelData();
        }

    }
}
