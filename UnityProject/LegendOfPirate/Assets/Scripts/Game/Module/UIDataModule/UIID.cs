
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
        RoleSkillUpgradePanel,
        RoleStoryPanel,
        RoleGroupPanel,
        RoleGetPanel,
        RoleGrowthPanel,
        #endregion

        #region 角色成长面板
        RoleSkillUpgradeSuccessPanel,
        #endregion

        //战船经营
        #region 厨房
        KitchenPanel,
        #endregion
        #region 锻造
        ForgeRoomPanel,
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

        //主菜单
        #region 仓库
        WareHousePanel,
        InventoryItemDetailPanel,
        #endregion

        CongratulationPanel,
        LandUpgradePanel,

        #region 登录注册创角
        LoginPanel,
        CreateUserPanel,
        #endregion

        #region 战斗相关
        BattlePreparePanel,//战斗准备界面
        BattleFieldPanel,//战斗排兵布阵
        BattleWinPanel,//战斗胜利界面
        #endregion

        #region 海战选关相关
        MainSeaLevelPanel,//选关界面
        LevelPeviewPanel,//选关界面预览
        #endregion

        #region 任务系统
        DailyTaskPanel,//日常任务界面
        MainTaskPanel,//主线任务界面

        AchievementTaskPanel,//成就任务界面
        #endregion
    }
}
