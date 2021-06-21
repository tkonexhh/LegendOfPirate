using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
    public partial class BattleWinPanel : AbstractAnimPanel
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
                string levelId = args[0].ToString();
                
                GetOnlineLevelReward(int.Parse(levelId));
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

        }

        protected override void BeforDestroy()
        {
            base.BeforDestroy();

            ReleasePanelData();
        }

    }
}
