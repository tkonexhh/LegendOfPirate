using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System.Linq;
using System;

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

            InitUI();
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
        {  }
        public void LeftArrowBtnEvent()
        {  }
        public void MakeBtnEvent()
        {
            foreach (var item in m_PanelData.laboratoryModel.labaratorySlotModelList)
            {
                switch (item.laboratorySlotState.Value)
                {
                    case LaboratorySlotState.Free:
                        break;
                    case LaboratorySlotState.Making:
                        break;
                    case LaboratorySlotState.Locked:
                        break;
                    case LaboratorySlotState.Selected:
                        item.StartMaking(DateTime.Now);
                        break;
                }
            }
        }

        public void LaboratoryUpgradeBtnEvent()
        {
            UIMgr.S.OpenTopPanel(UIID.BuildingLevelUpPanel, null, ShipUnitType.Laboratory);
        }

        public void AddItemBtnEvent()
        {
            HideSelfWithAnim();
        }
        #endregion
        #region Private
        private void InitUI()
        {
            m_LaboratorySlotList.SetCellRenderer(OnLaboratorSlotCellRenderer);
            m_LaboratorySlotList.SetDataCount(m_PanelData.GetLaboratorySlotCount());

            m_PotionSlotList.SetCellRenderer(OnPotionSloyCellRenderer);
            m_PotionSlotList.SetDataCount(m_PanelData.GetPotionSlotCount());
            //m_LaboratorySlotList
            //m_PotionSlotList

        }

        private void OnPotionSloyCellRenderer(Transform root, int index)
        {
            throw new NotImplementedException();
        }

        private void OnLaboratorSlotCellRenderer(Transform root, int index)
        {
            throw new NotImplementedException();
        }

        private void OnBottomCellRenderer(Transform root, int index)
        {
          
        }

        #endregion
    }
}
