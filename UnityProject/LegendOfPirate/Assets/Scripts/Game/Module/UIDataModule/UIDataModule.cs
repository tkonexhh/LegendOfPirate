using UnityEngine;
using System.Collections;
using Qarth;

namespace GameWish.Game
{
    public class UIDataModule : AbstractModule
    {
        public static void RegisterStaticPanel()
        {
            InitUIPath();
            UIDataTable.SetABMode(false);

            UIDataTable.AddPanelData(UIID.LogoPanel, null, "LogoPanel/LogoPanel");
        }

        protected override void OnComAwake()
        {
            InitUIPath();
            RegisterAllPanel();
        }

        private static void InitUIPath()
        {
            PanelData.PREFIX_PATH = "Resources/UI/Panels/{0}";
            PageData.PREFIX_PATH = "Resources/UI/Panels/{0}";
        }

        private void RegisterAllPanel()
        {
            UIDataTable.SetABMode(true);


            //UIDataTable.AddPanelData(UIID.FloatMessagePanel1, null, "Common/FloatMessagePanel1", true, 1);
            UIDataTable.AddPanelData(EngineUI.MsgBoxPanel, null, "Common/MsgBoxPanel", true, 1);
            UIDataTable.AddPanelData(EngineUI.HighlightMaskPanel, null, "Guide/HighlightMaskPanel", true, 0);
            UIDataTable.AddPanelData(EngineUI.GuideHandPanel, null, "Guide/GuideHandPanel", true, 0);
            UIDataTable.AddPanelData(EngineUI.MaskPanel, null, "Common/MaskPanel", true, 1);
            UIDataTable.AddPanelData(EngineUI.ColorFadeTransition, null, "Common/ColorFadeTransition", true, 1);
            UIDataTable.AddPanelData(SDKUI.AdDisplayer, null, "Common/AdDisplayer", false, 1);
            UIDataTable.AddPanelData(SDKUI.OfficialVersionAdPanel, null, "OfficialVersionAdPanel");
            UIDataTable.AddPanelData(EngineUI.RatePanel, null, "Common/RatePanel");


            //effect panel
            UIDataTable.AddPanelData(UIID.UIParticalPanel, null, "Common/UIParticalPanel");

            //在开发阶段使用该模式方便调试
            UIDataTable.SetABMode(false);

            //guide
            //UIDataTable.AddPanelData(UIID.MyGuidePanel, null, "GuidePanel/MyGuidePanel", true);
            //UIDataTable.AddPanelData(UIID.MyGuideTipsPanel, null, "GuidePanel/MyGuideTipsPanel");
            //UIDataTable.AddPanelData(UIID.WorldGuideClickPanel, null, "GuidePanel/WorldGuideClickPanel");
            //UIDataTable.AddPanelData(UIID.GuideMaskPanel, null, "GuidePanel/GuideMaskPanel");

            UIDataTable.AddPanelData(UIID.MainGamePanel, null, "GamePanels/MainGamePanel/MainGamePanel", true, 1);
            UIDataTable.AddPanelData(UIID.TopPanel, null, "GamePanels/TopPanel/TopPanel", true, 1);

            UIDataTable.AddPanelData(UIID.SettingPanel, null, "GamePanels/SettingPanel/SettingPanel");

            UIDataTable.AddPanelData(UIID.SettingPanel, null, "GamePanels/SettingPanel/SettingPanel");

            UIDataTable.AddPanelData(UIID.MyFloatMessagePanel, null, "GamePanels/MyFloatMessagePanel/MyFloatMessagePanel");

            UIDataTable.AddPanelData(UIID.MainMenuPanel, null, "GamePanels/MainMenuPanel/MainMenuPanel");
        }
    }
}
