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
            UIDataTable.AddPanelData(EngineUI.FloatMessagePanel, null, "Common/FloatMessagePanel1", true, 1);
            UIDataTable.AddPanelData(EngineUI.MsgBoxPanel, null, "Common/MsgBoxPanel", true, 1);
            UIDataTable.AddPanelData(EngineUI.HighlightMaskPanel, null, "Guide/HighlightMaskPanel", true, 0);
            UIDataTable.AddPanelData(EngineUI.GuideHandPanel, null, "Guide/GuideHandPanel", true, 0);
            UIDataTable.AddPanelData(EngineUI.MaskPanel, null, "Common/MaskPanel", true, 1);
            UIDataTable.AddPanelData(EngineUI.ColorFadeTransition, null, "Common/ColorFadeTransition", true, 1);
            UIDataTable.AddPanelData(SDKUI.AdDisplayer, null, "Common/AdDisplayer", false, 1);
            UIDataTable.AddPanelData(SDKUI.OfficialVersionAdPanel, null, "OfficialVersionAdPanel");
            UIDataTable.AddPanelData(EngineUI.RatePanel, null, "Common/RatePanel");
            UIDataTable.AddPanelData(EngineUI.RatePanel, null, "Common/RatePanel");
            UIDataTable.AddPanelData(UIID.WorldUIPanel, null, "Common/WorldUIPanel");


            //effect panel

            //在开发阶段使用该模式方便调试
            UIDataTable.SetABMode(false);

            //guide
            //UIDataTable.AddPanelData(UIID.MyGuidePanel, null, "GuidePanel/MyGuidePanel", true);
            //UIDataTable.AddPanelData(UIID.MyGuideTipsPanel, null, "GuidePanel/MyGuideTipsPanel");
            //UIDataTable.AddPanelData(UIID.WorldGuideClickPanel, null, "GuidePanel/WorldGuideClickPanel");
            //UIDataTable.AddPanelData(UIID.GuideMaskPanel, null, "GuidePanel/GuideMaskPanel");

            //UIDataTable.AddPanelData(UIID.MainGamePanel, null, "GamePanels/MainGamePanel/MainGamePanel", true, 1);
            //UIDataTable.AddPanelData(UIID.TopPanel, null, "GamePanels/TopPanel/TopPanel", true, 1);

            //UIDataTable.AddPanelData(UIID.SettingPanel, null, "GamePanels/SettingPanel/SettingPanel");

            //UIDataTable.AddPanelData(UIID.SettingPanel, null, "GamePanels/SettingPanel/SettingPanel");

            //UIDataTable.AddPanelData(UIID.MyFloatMessagePanel, null, "GamePanels/MyFloatMessagePanel/MyFloatMessagePanel");
            #region 登录注册创角
            UIDataTable.AddPanelData(UIID.LoginPanel, null, "LoginPanel/LoginPanel");
            UIDataTable.AddPanelData(UIID.CreateUserPanel, null, "GamePanels/CreateUserPanel/CreateUserPanel");
            #endregion

            //主菜单功能
            UIDataTable.AddPanelData(UIID.MainMenuPanel, null, "GamePanels/MainMenuSystem/MainMenuPanel/MainMenuPanel");
            #region 仓库
            UIDataTable.AddPanelData(UIID.InventoryItemDetailPanel, null, "GamePanels/MainMenuSystem/WareHouse/InventoryItemDetailPanel/InventoryItemDetailPanel");
            UIDataTable.AddPanelData(UIID.WareHousePanel, null, "GamePanels/MainMenuSystem/WareHouse/WareHousePanel/WareHousePanel");
            #endregion
            #region 内购界面
            UIDataTable.AddPanelData(UIID.ChargingInterfacePanel, null, "GamePanels/MainMenuSystem/ChargingInterfacePanel/ChargingInterfacePanel");
            #endregion

            #region 角色相关
            UIDataTable.AddPanelData(UIID.RoleGrowthPanel, null, "GamePanels/RolePanel/RoleGrowthPanel/RoleGrowthPanel");
            UIDataTable.AddPanelData(UIID.RoleDetailsPanel, null, "GamePanels/RolePanel/RoleDetailsPanel/RoleDetailsPanel");
            UIDataTable.AddPanelData(UIID.RoleEquipDetailsPanel, null, "GamePanels/RolePanel/RoleEquipDetailsPanel/RoleEquipDetailsPanel");
            UIDataTable.AddPanelData(UIID.RoleEquipGetPanel, null, "GamePanels/RolePanel/RoleEquipGetPanel/RoleEquipGetPanel");
            UIDataTable.AddPanelData(UIID.EvolutionSolePanel, null, "GamePanels/RolePanel/EvolutionSolePanel/EvolutionSolePanel");
            UIDataTable.AddPanelData(UIID.RoleSkillUpgradePanel, null, "GamePanels/RolePanel/RoleSkillUpgradePanel/RoleSkillUpgradePanel");
            UIDataTable.AddPanelData(UIID.RoleStoryPanel, null, "GamePanels/RolePanel/RoleStoryPanel/RoleStoryPanel");
            UIDataTable.AddPanelData(UIID.RoleGroupPanel, null, "GamePanels/RolePanel/RoleGroupPanel/RoleGroupPanel");
            UIDataTable.AddPanelData(UIID.RoleGetPanel, null, "GamePanels/RolePanel/RoleGetPanel/RoleGetPanel");
            #endregion

            #region 角色成长面板
            UIDataTable.AddPanelData(UIID.RoleSkillUpgradeSuccessPanel, null, "GamePanels/RolePanel/RoleGroupPanel/RoleSkillUpgradeSuccessPanel");
            #endregion
            //UIDataTable.AddPanelData(UIID.TestPanel, null, "GamePanels/TestPanel/TestPanel");

            //Warship Management System
            #region 训练室
            UIDataTable.AddPanelData(UIID.TrainingRoomPanel, null, "GamePanels/WarshipManagement/TrainingRoomPanel/TrainingRoomPanel");
            #endregion
            #region 图书室
            UIDataTable.AddPanelData(UIID.LibraryRoomPanel, null, "GamePanels/WarshipManagement/LibraryRoomPanel/LibraryRoomPanel");
            #endregion
            #region 炼金室
            UIDataTable.AddPanelData(UIID.LaboratoryRoomPanel, null, "GamePanels/WarshipManagement/LaboratoryRoomPanel/LaboratoryRoomPanel");
            #endregion
            #region 战船系统
            UIDataTable.AddPanelData(UIID.BattleShipPanel,null, "GamePanels/WarshipManagement/BattleShipPanel/BattleShipPanel");
            UIDataTable.AddPanelData(UIID.WarshipUpgradePanel, null, "GamePanels/WarshipManagement/WarshipUpgradePanel/WarshipUpgradePanel");
            #endregion

            UIDataTable.AddPanelData(UIID.KitchenPanel, null, "GamePanels/WarshipManagement/KitchenPanel/KitchenPanel");
            UIDataTable.AddPanelData(UIID.FishingPanel, null, "GamePanels/WarshipManagement/FishingPanel/FishingPanel");
            UIDataTable.AddPanelData(UIID.ForgeRoomPanel, null, "GamePanels/WarshipManagement/ForgeRoomPanel/ForgeRoomPanel");
            UIDataTable.AddPanelData(UIID.GardenPanel, null, "GamePanels/WarshipManagement/GardenPanel/GardenPanel");
            UIDataTable.AddPanelData(UIID.ProgressingRoomPanel, null, "GamePanels/WarshipManagement/ProgressRoomPanel/ProgressRoomPanel");

            //Land Management System  
            UIDataTable.AddPanelData(UIID.CongratulationPanel, null, "GamePanels/LandManagement/CongratulationPanel/CongratulationPanel");
            UIDataTable.AddPanelData(UIID.LandUpgradePanel, null, "GamePanels/LandManagement/LandUpgradePanel/LandUpgradePanel");
            #region 随机防御
            UIDataTable.AddPanelData(UIID.RandomDefensePanel, null, "GamePanels/LandManagement/RandomDefensePanel/RandomDefensePanel");
            UIDataTable.AddPanelData(UIID.RandomDefenseChooseRolePanel, null, "GamePanels/LandManagement/RandomDefenseChooseRolePanel/RandomDefenseChooseRolePanel");
            #endregion
            #region 黑市
            UIDataTable.AddPanelData(UIID.BlackMarketPanel, null, "GamePanels/LandManagement/BlackMarketPanel/BlackMarketPanel");
            #endregion
            #region 走私系统
            UIDataTable.AddPanelData(UIID.SmugglePanel, null, "GamePanels/LandManagement/SmugglePanel/SmugglePanel");
            UIDataTable.AddPanelData(UIID.SmuggleChooseRolePanel, null, "GamePanels/LandManagement/SmuggleChooseRolePanel/SmuggleChooseRolePanel");
            #endregion

            #region FloatMessageTMP
            UIDataTable.AddPanelData(UIID.FloatMessageTMPanel, null, "GamePanels/Common/FloatMessageTMPanel", true, 1);
            #endregion
            UIDataTable.AddPanelData(UIID.BuildingLevelUpPanel, null, "GamePanels/BuildingLevelUpPanel/BuildingLevelUpPanel");
            #region 战斗相关
            UIDataTable.AddPanelData(UIID.BattlePreparePanel, null, "GamePanels/BattlePanel/BattlePreparePanel/BattlePreparePanel", true, 1);//战斗排兵布阵界面
            UIDataTable.AddPanelData(UIID.BattleFieldPanel, null, "GamePanels/BattlePanel/BattleFieldPanel/BattleFieldPanel", true, 1);//战斗排兵布阵界面
            UIDataTable.AddPanelData(UIID.BattleWinPanel, null, "GamePanels/BattlePanel/BattleWinPanel/BattleWinPanel");//战斗结束胜利界面
            #endregion

            #region 海战选关相关
            UIDataTable.AddPanelData(UIID.MainSeaLevelPanel, null, "GamePanels/MainSeaBattlePanel/MainSeaLevelPanel/MainSeaLevelPanel");//选关界面
            UIDataTable.AddPanelData(UIID.LevelPeviewPanel, null, "GamePanels/MainSeaBattlePanel/LevelPeviewPanel/LevelPeviewPanel");//选关预览界面
            #endregion

            #region 任务系统
            UIDataTable.AddPanelData(UIID.DailyTaskPanel, null, "GamePanels/TaskPanel/DailyTaskPanel/DailyTaskPanel", true, 1);//日常任务界面
            UIDataTable.AddPanelData(UIID.MainTaskPanel, null, "GamePanels/TaskPanel/MainTaskPanel/MainTaskPanel", true, 1);//主线任务界面
            UIDataTable.AddPanelData(UIID.AchievementTaskPanel, null, "GamePanels/TaskPanel/AchievementTaskPanel/AchievementTaskPanel", true, 1);//成就任务界面
            #endregion
        }
    }
}
