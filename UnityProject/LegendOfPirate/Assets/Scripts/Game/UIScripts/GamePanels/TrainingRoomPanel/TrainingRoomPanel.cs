using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;
using System.Linq;

namespace GameWish.Game
{
    #region Other Data Class
    public enum TrainingRoomRoleState
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
        /// 已选择英雄但是未开始
        /// </summary>
        HeroSelected = 3,
    }

    public class BottomTrainingRoleModule
    {
        public IntReactiveProperty index;
        public BoolReactiveProperty isSelected;
        public IntReactiveProperty roleID;


        public BottomTrainingRoleModule(int index, bool selected)
        {
            this.index = new IntReactiveProperty(index);
            this.isSelected = new BoolReactiveProperty(selected);
            this.roleID = new IntReactiveProperty();
        }
    }

    public class MiddleTrainingRoleModule
    {
        public IntReactiveProperty index;

        public TrainingSlotModel trainingSlotModel;
        public MiddleTrainingRole middleTrainingRole;

        public MiddleTrainingRoleModule(int index, TrainingSlotModel trainingSlotModel, MiddleTrainingRole middleTrainingRole)
        {
            this.index = new IntReactiveProperty(index);
            this.trainingSlotModel = trainingSlotModel;
            this.middleTrainingRole = middleTrainingRole;
        }
    }
    #endregion
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
        private ReactiveCollection<BottomTrainingRoleModule> bottomTrainingRoleDatas = new ReactiveCollection<BottomTrainingRoleModule>();
        private ReactiveCollection<MiddleTrainingRoleModule> middleTrainingRoleDatas = new ReactiveCollection<MiddleTrainingRoleModule>();
        private ReactiveCollection<MiddleTrainingRole> middleTrainingRoles = new ReactiveCollection<MiddleTrainingRole>();
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
            m_PanelData.trainingRoomModel.OnLevelUpgrade(1);
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
                    foreach (var item in middleTrainingRoleDatas)
                    {
                        item.middleTrainingRole.OnRefresh();
                    }
                    break;
                case EventID.OnTrainingRoomSelectRole:
                    MiddleTrainingRoleModule spareMiddleTrainingRoleModule = null;
                    MiddleTrainingRoleModule selectedmiddleTrainingRoleModule = null;
                    foreach (var item in middleTrainingRoleDatas)
                    {
                        switch (item.trainingSlotModel.trainState.Value)
                        {
                            case TrainingRoomRoleState.Free:
                                if (spareMiddleTrainingRoleModule == null)
                                {
                                    spareMiddleTrainingRoleModule = item;
                                }
                                break;
                            case TrainingRoomRoleState.Training:
                            case TrainingRoomRoleState.Locked:

                                break;
                            case TrainingRoomRoleState.HeroSelected:
                                Debug.LogError("id = "+ (int)param[0]);
                                if (item.trainingSlotModel.heroId == (int)param[0])
                                {
                                    selectedmiddleTrainingRoleModule = item;
                                }
                                break;
                        }
                    }

                    if (selectedmiddleTrainingRoleModule!=null)
                    {
                        selectedmiddleTrainingRoleModule.trainingSlotModel.OnHeroUnselected();
                        selectedmiddleTrainingRoleModule.middleTrainingRole.OnRefresh();
                        break;
                    }
                    if (spareMiddleTrainingRoleModule!=null && spareMiddleTrainingRoleModule.trainingSlotModel.trainState.Value == TrainingRoomRoleState.Free )
                    {
                        spareMiddleTrainingRoleModule.trainingSlotModel.OnHeroSelected((int)param[0]);
                        spareMiddleTrainingRoleModule.middleTrainingRole.OnInit(spareMiddleTrainingRoleModule);
                        break;
                    }
                    Debug.LogError("出现未知错误");
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
            BottomTrainingRoleModule bottomTrainingRoleData = bottomTrainingRoleDatas.FirstOrDefault(item => item.index.Value == index);
            if (bottomTrainingRoleData != null)
            {
                root.GetComponent<BottomTrainingRole>().OnInit(bottomTrainingRoleData, m_SelectedCount);
            }
            else
            {
                BottomTrainingRoleModule newBottomTrainingRoleModule = new BottomTrainingRoleModule(index, false);
                bottomTrainingRoleDatas.Add(newBottomTrainingRoleModule);
                root.GetComponent<BottomTrainingRole>().OnInit(newBottomTrainingRoleModule, m_SelectedCount);
            }
        }

        private void OnMiddleCellRenderer(Transform root, int index)
        {
            MiddleTrainingRoleModule middleTrainingRoleModule = middleTrainingRoleDatas.FirstOrDefault(item => item.index.Value == index);
            if (middleTrainingRoleModule != null)
            {
                root.GetComponent<MiddleTrainingRole>().OnInit(middleTrainingRoleModule);
            }
            else
            {
                MiddleTrainingRoleModule newMiddleTrainingRoleModule;

                MiddleTrainingRole middleTrainingRole = root.GetComponent<MiddleTrainingRole>();

                newMiddleTrainingRoleModule = new MiddleTrainingRoleModule(index, (m_PanelData.trainingRoomModel.slotModelList)[index], middleTrainingRole);
                middleTrainingRoleDatas.Add(newMiddleTrainingRoleModule);

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
}
