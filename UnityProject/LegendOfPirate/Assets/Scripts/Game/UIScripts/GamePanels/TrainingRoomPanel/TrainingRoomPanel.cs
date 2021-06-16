using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;
using System.Linq;

namespace GameWish.Game
{
 
    public partial class TrainingRoomPanel : AbstractAnimPanel
    {
        [SerializeField]
        private ScrollRectAutoAdjustPosition m_ScrollRectAutoAdjustPosition;
        [SerializeField]
        private UGridListView m_MiddleTrainingRoleUGridList;
        [SerializeField]
        private USimpleListView m_BottomTrainingRoleUList;

        #region Data
        private IntReactiveProperty m_SelectedCount = new IntReactiveProperty(0);
        private ReactiveCollection<BottomTrainingModel> m_BottomTrainingRoleDatas = new ReactiveCollection<BottomTrainingModel>();
        private ReactiveCollection<MiddleTrainingSlotModel> m_MiddleTrainingRoleDatas = new ReactiveCollection<MiddleTrainingSlotModel>();

        private ReactiveCollection<int> m_SelectedRoleID = new ReactiveCollection<int>();
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
        public void TrainingUpgradeBtnEvent()
        {
            m_PanelData.trainingRoomModel.OnUpgrade(1);
        }
        public void TrainBtnEvent()
        {

        }
        public void AutoTrainBtnEvent()
        {
        }
        public void BgBtnEvent()
        {
            HideSelfWithAnim();
        }
        public void LeftArrowBtnEvent()
        {
            m_ScrollRectAutoAdjustPosition?.Move2Pre();
        }
        public void RightArrowBtnEvent()
        {
            m_ScrollRectAutoAdjustPosition?.Move2Next();
        }
        #endregion

        #region EventSystem
        private void HandlerEvent(int key, object[] param)
        {
            switch ((EventID)key)
            {
                case EventID.OnTrainingRoomUpgradeRefresh:
                    foreach (var item in m_MiddleTrainingRoleDatas)
                    {
                        item.middleTrainingRole.OnRefresh();
                    }
                    break;
                case EventID.OnTrainingRoomSelectRole:
                    MiddleTrainingSlotModel spareSelectedModel = null;
                    MiddleTrainingSlotModel selectedSlot = null;
                    BottomTrainingModel bottomTrainingRoleModel = (BottomTrainingModel)param[0];

                    foreach (var item in m_MiddleTrainingRoleDatas)
                    {
                        switch (item.trainingSlotModel.trainState.Value)
                        {
                            case TrainintRoomRoleState.Free:
                                if (spareSelectedModel == null)
                                {
                                    spareSelectedModel = item;
                                }
                                break;
                            case TrainintRoomRoleState.Training:
                            case TrainintRoomRoleState.Locked:

                                break;
                            case TrainintRoomRoleState.SelectedNotStart:
                                if (item.trainingSlotModel.heroId == bottomTrainingRoleModel.roleID.Value)
                                {
                                    selectedSlot = item;
                                }
                                break;
                        }
                    }

                    if (selectedSlot != null)//Selected remove
                    {
                        selectedSlot.trainingSlotModel.SetTraingRRS2Free();
                        selectedSlot.middleTrainingRole.OnRefresh();
                        m_SelectedRoleID.Remove(bottomTrainingRoleModel.roleID.Value);
                        bottomTrainingRoleModel.bottomTrainingRole.HandleSelectedRole();
                        break;
                    }
                    //Unselected add
                    if (spareSelectedModel != null && spareSelectedModel.trainingSlotModel.trainState.Value == TrainintRoomRoleState.Free)
                    {
                        spareSelectedModel.trainingSlotModel.SetTraingRRS2SNS(bottomTrainingRoleModel.roleID.Value);
                        spareSelectedModel.middleTrainingRole.OnInit(spareSelectedModel);
                        m_SelectedRoleID.Add(bottomTrainingRoleModel.roleID.Value);
                        bottomTrainingRoleModel.bottomTrainingRole.HandleSelectedRole();
                        break;
                    }
                    if (selectedSlot==null && spareSelectedModel == null)
                    {
                        Debug.LogError("Full");
                        break;
                    }
                    Debug.LogWarning("Warning!");
                    break;
            }
        }
        #endregion

        #region Other Method
        private void InitData()
        {
            BindUniRxUI();

            //TitleIcon.sprite = SpriteHandler.S.GetSprite(AtlasDefine.TestAtlas, iconName);

            m_MiddleTrainingRoleUGridList.SetCellRenderer(OnMiddleCellRenderer);
            m_BottomTrainingRoleUList.SetCellRenderer(OnBottomCellRenderer);

            m_ScrollRectAutoAdjustPosition.EnableAutoAdjust(m_PanelData.GetSlotModelListCount());

            m_MiddleTrainingRoleUGridList.SetDataCount(m_PanelData.GetSlotModelListCount());
            m_BottomTrainingRoleUList.SetDataCount(m_PanelData.GetSlotModelListCount());
        }
        private void OnBottomCellRenderer(Transform root, int index)
        {
            BottomTrainingModel bottomTrainingRoleData = m_BottomTrainingRoleDatas.FirstOrDefault(item => item.index.Value == index);
            if (bottomTrainingRoleData != null)
            {
                root.GetComponent<BottomTrainingRole>().OnInit(bottomTrainingRoleData, m_SelectedCount);
            }
            else
            {
                BottomTrainingRole bottomTrainingRole = root.GetComponent<BottomTrainingRole>();
                BottomTrainingModel newBottomTrainingRoleModule = new BottomTrainingModel(index, false, index, bottomTrainingRole);
                m_BottomTrainingRoleDatas.Add(newBottomTrainingRoleModule);
                bottomTrainingRole.OnInit(newBottomTrainingRoleModule, m_SelectedCount);
            }
        }

        private void OnMiddleCellRenderer(Transform root, int index)
        {
            MiddleTrainingSlotModel middleTrainingRoleModule = m_MiddleTrainingRoleDatas.FirstOrDefault(item => item.index.Value == index);
            if (middleTrainingRoleModule != null)
            {
                root.GetComponent<MiddleTrainingRole>().OnInit(middleTrainingRoleModule);
            }
            else
            {
                MiddleTrainingSlotModel newMiddleTrainingRoleModule;

                MiddleTrainingRole middleTrainingRole = root.GetComponent<MiddleTrainingRole>();

                newMiddleTrainingRoleModule = new MiddleTrainingSlotModel(index, (m_PanelData.trainingRoomModel.slotModelList)[index], middleTrainingRole);
                m_MiddleTrainingRoleDatas.Add(newMiddleTrainingRoleModule);

                //middleTrainingRoles.Add(middleTrainingRole);
                middleTrainingRole.OnInit(newMiddleTrainingRoleModule);
            }
        }

        private void BindUniRxUI()
        {
            m_SelectedCount.Select(count => count + "/" + 10).SubscribeToTextMeshPro(RoleSelectNumberTMP);
        }

        public void CreateMiddleTrainingRole()
        {
            //MiddleTrainingRole middleTraining = Instantiate(MiddleTrainingRole, MiddleTrainingRoleTra.transform).GetComponent<MiddleTrainingRole>();

        }
        #endregion
       
    }
    #region Other Data Class
    public enum TrainintRoomRoleState
    {
        /// <summary>
        /// 空闲中
        /// </summary>
        Free = 0,
        /// <summary>
        /// 训练中
        /// </summary>
        Training = 1,
        /// <summary>
        /// 未解锁
        /// </summary>
        Locked = 2,
        /// <summary>
        /// 选择但是未开始
        /// </summary>
        SelectedNotStart = 3,
    }
    public class BottomTrainingModel
    {
        public IntReactiveProperty index;
        public BoolReactiveProperty isSelected;
        public IntReactiveProperty roleID;
        public BottomTrainingRole bottomTrainingRole;

        public BottomTrainingModel(int index, bool selected, int roleID, BottomTrainingRole bottomTrainingRole)
        {
            this.index = new IntReactiveProperty(index);
            this.isSelected = new BoolReactiveProperty(selected);
            this.roleID = new IntReactiveProperty(roleID);
            this.bottomTrainingRole = bottomTrainingRole;
        }
    }

    public class MiddleTrainingSlotModel
    {
        public IntReactiveProperty index;

        public TrainingSlotModel trainingSlotModel;
        public MiddleTrainingRole middleTrainingRole;

        public MiddleTrainingSlotModel(int index, TrainingSlotModel trainingSlotModel, MiddleTrainingRole middleTrainingRole)
        {
            this.index = new IntReactiveProperty(index);
            this.trainingSlotModel = trainingSlotModel;
            this.middleTrainingRole = middleTrainingRole;
        }
    }
    #endregion
}
