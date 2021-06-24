using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System.Linq;

namespace GameWish.Game
{
    #region Other Data Class
    public class BottomLaboratoryPotionModule
    {
        public IntReactiveProperty index;
        public BoolReactiveProperty isSelected;

        public BottomLaboratoryPotionModule(int index, bool selected)
        {
            this.index = new IntReactiveProperty(index);
            this.isSelected = new BoolReactiveProperty(selected);
        }
    }
    #endregion
    public partial class LaboratoryRoomPanel : AbstractAnimPanel
    {
        [SerializeField]
        private USimpleListView m_BottomLaboratoryRoleUList;
        [SerializeField]
        private ScrollRectAutoAdjustPosition m_ScrollRectAutoAdjustPosition;
        #region Data

        private ReactiveCollection<BottomLaboratoryPotionModule> bottomLaboratoryPotionModules = new ReactiveCollection<BottomLaboratoryPotionModule>();

        #endregion
        #region AbstractAnimPanel
        protected override void OnUIInit()
        {
            base.OnUIInit();
        }

        protected override void OnPanelOpen(params object[] args)
        {
            base.OnPanelOpen(args);
            RegisterEvents();

            OpenDependPanel(EngineUI.MaskPanel, -1, null);

            AllocatePanelData(args);

            BindModelToUI();
            BindUIToModel();

            OnClickAddListener();

            InitData();
        }
        protected override void OnPanelHideComplete()
        {
            base.OnPanelHideComplete();

            CloseSelfPanel();

            CloseDependPanel(EngineUI.MaskPanel);
        }

        protected override void OnClose()
        {
            base.OnClose();

            ReleasePanelData();
            UnregisterEvents();
        }
        #endregion
        #region ButtonEvent
        public void RightArrowBtnEvent()
        { m_ScrollRectAutoAdjustPosition?.Move2Next(); }
        public void LeftArrowBtnEvent()
        { m_ScrollRectAutoAdjustPosition?.Move2Pre(); }
        public void MakeBtnEvent()
        { }
        public void LaboratoryUpgradeBtnEvent()
        {
            UIMgr.S.OpenTopPanel(UIID.BuildingLevelUpPanel, null, ShipUnitType.Laboratory);
        }

        public void BgBtnEvent()
        {
            HideSelfWithAnim();
        }
        #endregion
        #region Private
        private void InitData()
        {

            //TitleIcon.sprite = SpriteHandler.S.GetSprite(AtlasDefine.TestAtlas,"");

            m_BottomLaboratoryRoleUList.SetCellRenderer(OnBottomCellRenderer);
            m_BottomLaboratoryRoleUList.SetDataCount(10);
            m_ScrollRectAutoAdjustPosition.EnableAutoAdjust(10);

        }
        private void OnBottomCellRenderer(Transform root, int index)
        {
          
        }

        #endregion
    }
}
