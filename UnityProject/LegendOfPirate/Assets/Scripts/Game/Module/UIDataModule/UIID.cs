
namespace GameWish.Game
{
    public enum UIID : byte
    {
        LogPanel = 0,
        LogoPanel = 1,
        //TopPanel,
        //MainGamePanel,
        UIParticalPanel,
        SettingPanel,
        GuideWordsPanel,
        UIClipPanel,
        SkipGuidePanel,
        //ToastPanel,
        WorldUIPanel,
        //FloatMessagePanel1,
        MainMenuPanel,
        /// <summary>
        /// 消息提示界面
        /// </summary>
        MyFloatMessagePanel,
        FloatMessageTMPanel,
        //TestPanel,

        #region Role
        RoleDetailsPanel,
        RoleEquipDetailsPanel,
        EvolutionSolePanel,
        RoleSkillPanel,
        RoleStoryPanel,
        RoleGroupPanel,
        #endregion


        //战船经营
        #region 厨房
        KitchenPanel,
        #endregion
        #region 锻造
        ForgePanel,
        #endregion
        #region 钓鱼
        FishingPanel,
        #endregion
        #region 菜园
        GardenPanel,
        #endregion
        #region 加工室
        ProgressingRoomPanel,
        #endregion
        #region 训练室
        TrainingRoomPanel,
        #endregion
        #region 图书室
        LibraryRoomPanel,
        #endregion
        #region 实验室
        LaboratoryRoomPanel,
        #endregion
        #region 战船升级
        WarshipUpgradePanel,
        #endregion
        #region 建筑升级
        BuildingLevelUpPanel,
        #endregion

        //海岛经营
        #region 随机防御
        RandomDefensePanel,
        RandomDefenseChooseRolePanel,
        #endregion
        #region 走私系统
        SmugglePanel,
        SmuggleChooseRolePanel,
        #endregion
        #region 黑市
        BlackMarketPanel,
        #endregion

        CongratulationPanel,
        LandUpgradePanel,

        #region 登录注册创角
        LoginPanel,
        CreateUserPanel,
        #endregion
    }
}
