
namespace GameWish.Game
{
    public enum UIID:byte
    {
        LogPanel = 0,
        LogoPanel = 1,
        TopPanel,
        MainGamePanel,
        UIParticalPanel,
        SettingPanel,
        GuideWordsPanel,
        UIClipPanel,
        SkipGuidePanel,
        ToastPanel,
        WorldUIPanel,
        FloatMessagePanel1,
        MainMenuPanel,
        /// <summary>
        /// 消息提示界面
        /// </summary>
        MyFloatMessagePanel,
        TestPanel,

        #region Role
        RoleDetailsPanel,
        RoleEquipDetailsPanel,
        EvolutionSolePanel,
        RoleSkillPanel,
        RoleStoryPanel,
        #endregion

        FloatMeshMessagePanel,

        #region 训练室
        TrainingRoomPanel,
        #endregion
        #region 图书室
        LibraryRoomPanel,
        #endregion
        #region 炼金室
        AlchemyRoomPanel,
        #endregion
        #region 战船系统
        WarshipUpgradePanel,
        #endregion
        #region 随机防御
        RandomDefensePanel,
        RandomDefenseChooseRolePanel,
        #endregion
        #region 走私系统
        SmugglePanel,
        SmuggleChooseRolePanel,
        #endregion
        CongratulationPanel,
        LandUpgradePanel,
    }
}
