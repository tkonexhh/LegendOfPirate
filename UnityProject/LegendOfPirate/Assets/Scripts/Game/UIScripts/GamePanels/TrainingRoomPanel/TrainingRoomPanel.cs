using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;
using System.Linq;
using System.Collections.Generic;

namespace GameWish.Game
{
    public partial class TrainingRoomPanel : AbstractAnimPanel
    {
        [SerializeField]
        private ScrollRectAutoAdjustPosition m_ScrollRectAdjustPos;
        [SerializeField]
        private UGridListView m_MiddleTRoleUGL;
        [SerializeField]
        private USimpleListView m_BottomTRoleUL;

        #region Data
        private IntReactiveProperty m_SelectedCount = new IntReactiveProperty(0);
        private ReactiveCollection<BottomRoleModel> m_BottomTrainingRoleDatas = new ReactiveCollection<BottomRoleModel>();
        private ReactiveCollection<MiddleSlotModel> m_MiddleTRoleDatas = new ReactiveCollection<MiddleSlotModel>();

        private ReactiveCollection<int> m_SelectedRoleID = new ReactiveCollection<int>();
        private ReactiveCollection<int> m_SimulationRoleID = new ReactiveCollection<int>();
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

            m_SimulationRoleID.Add(10);
            m_SimulationRoleID.Add(9);
            m_SimulationRoleID.Add(8);
            m_SimulationRoleID.Add(7);
            m_SimulationRoleID.Add(5);
            m_SimulationRoleID.Add(32);
            m_SimulationRoleID.Add(132);
            m_SimulationRoleID.Add(654651);
            m_SimulationRoleID.Add(23132);

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
        public void TUpgradeBtnEnt()
        {
            m_PanelData.trainingRoomModel.OnLevelUpgrade(1);
        }
        public void TrainBtnEnt()
        {
            foreach (var item in m_MiddleTRoleDatas)
            {
                if (item.trainingSlotModel.trainState.Value ==  TrainingSlotState.HeroSelected)
                {
                    item.trainingSlotModel.StartTraining(1,DateTime.Now);
                    EventSystem.S.Send(EventID.OnTRoomStartTraining, item.trainingSlotModel.heroId);
                    SelectedRoleSort();
                }
            }
            RefreshSelectedCount();
        }

        private void SelectedRoleSort()
        {
            //List<TrainingSlotModel> trainingSlotModels = m_BottomTrainingRoleDatas;
            for (var i = 0; i < m_BottomTrainingRoleDatas.Count - 1; i++)
            {
                for (var j = 0; j < m_BottomTrainingRoleDatas.Count - 1 - i; j++)
                {
                    if (m_SelectedRoleID.Contains(m_BottomTrainingRoleDatas[j].roleID.Value))
                    {
                        BottomRoleModel trainingSlotModel = m_BottomTrainingRoleDatas[j];
                        m_BottomTrainingRoleDatas[j] = m_BottomTrainingRoleDatas[j + 1];
                        m_BottomTrainingRoleDatas[j + 1] = trainingSlotModel;

                        //Debug.LogError("isSelected = " + m_BottomTrainingRoleDatas[j].isSelected.Value);
                        //BottomRoleModel bottomRoleModel = BottomRoleModel.ExchangeEachOther(m_BottomTrainingRoleDatas[j]);
                        //m_BottomTrainingRoleDatas[j] = BottomRoleModel.ExchangeEachOther(m_BottomTrainingRoleDatas[j + 1]);
                        //m_BottomTrainingRoleDatas[j].bottomTrainingRole.OnRefresh();
                        //m_BottomTrainingRoleDatas[j + 1] = bottomRoleModel;
                        //m_BottomTrainingRoleDatas[j + 1].bottomTrainingRole.OnRefresh();

                    }
                }
            }
            m_BottomTRoleUL.SetDataCount(m_BottomTrainingRoleDatas.Count);
        }

        public void AutoTrainBtnEnt()
        {
        }
        public void BgBtnEnt()
        {
            HideSelfWithAnim();
        }
        public void LeftArrowBtnEnt()
        {
            m_ScrollRectAdjustPos?.Move2Pre();
        }
        public void RightArrowBtnEnt()
        {
            m_ScrollRectAdjustPos?.Move2Next();
        }
        #endregion

        #region EventSystem
        private void HandlerEvent(int key, object[] param)
        {
            switch ((EventID)key)
            {
                case EventID.OnTrainingUpgradeRefresh:
                    foreach (var item in m_MiddleTRoleDatas)
                    {
                        item.middleTrainingRole.OnRefresh();
                    }
                    break;
                case EventID.OnTrainingSelectRole:
                    MiddleSlotModel spareSelectedModel = null;
                    MiddleSlotModel selectedSlot = null;
                    BottomRoleModel bottomTRoleModel = (BottomRoleModel)param[0];

                    foreach (var item in m_MiddleTRoleDatas)
                    {
                        switch (item.trainingSlotModel.trainState.Value)
                        {
                            case TrainingSlotState.Free:
                                if (spareSelectedModel == null)
                                {
                                    spareSelectedModel = item;
                                }
                                break;
                            case TrainingSlotState.Training:
                            case TrainingSlotState.Locked:

                                break;
                            case TrainingSlotState.HeroSelected:
                                if (item.trainingSlotModel.heroId == bottomTRoleModel.roleID.Value)
                                {
                                    selectedSlot = item;
                                }
                                break;
                        }
                    }

                    if (selectedSlot != null)//Selected remove
                    {
                        selectedSlot.trainingSlotModel.OnHeroUnselected();
                        selectedSlot.middleTrainingRole.OnRefresh();
                        m_SelectedRoleID.Remove(bottomTRoleModel.roleID.Value);
                        bottomTRoleModel.bottomTrainingRole.HandleSelectedRole();
                        break;
                    }
                    //Unselected add
                    if (spareSelectedModel != null && spareSelectedModel.trainingSlotModel.trainState.Value == TrainingSlotState.Free)
                    {
                        spareSelectedModel.trainingSlotModel.OnHeroSelected(bottomTRoleModel.roleID.Value);
                        spareSelectedModel.middleTrainingRole.OnInit(spareSelectedModel);
                        m_SelectedRoleID.Add(bottomTRoleModel.roleID.Value);
                        bottomTRoleModel.bottomTrainingRole.HandleSelectedRole();
                        break;
                    }
                    if (selectedSlot==null && spareSelectedModel == null)
                    {
                        FloatMessageTMP.S.ShowMsg("Full");
                        break;
                    }
                    Debug.LogWarning("Warning!");
                    break;
            }
        }
        #endregion

        #region Other Method

        public void RefreshSelectedCount()
        {
            int number = 0;
            foreach (var item in m_PanelData.trainingRoomModel.slotModelList)
            {
                if (item.trainState.Value == TrainingSlotState.Training)
                {
                    number++;
                }
            }
            m_SelectedCount.Value = number;
        }

        private ReactiveCollection<int> GetCorrespondingStateList(TrainingSlotState trainingSlotState)
        {
            ReactiveCollection<int> heroSelectedRoleID = new ReactiveCollection<int>();

            foreach (var item in m_PanelData.trainingRoomModel.slotModelList)
            {
                if (item.trainState.Value == trainingSlotState)
                {
                    heroSelectedRoleID.Add(item.heroId);
                }
            }
            return heroSelectedRoleID;
        }

        private void InitData()
        {
            BindUniRxUI();
            RefreshSelectedCount();

            ReactiveCollection<int> trainingStates = GetCorrespondingStateList(TrainingSlotState.Training);
            ReactiveCollection<int> heroSelectedStates = GetCorrespondingStateList(TrainingSlotState.HeroSelected);

            ReactiveCollection<BottomRoleModel> tempSelectedDatas = new ReactiveCollection<BottomRoleModel>();
            for (int i = 0; i < m_SimulationRoleID.Count; i++)
            {
                if (trainingStates.Contains(m_SimulationRoleID[i]))
                {
                    BottomRoleModel newBottomTRoleM = new BottomRoleModel(m_SimulationRoleID[i], this,true);

                    tempSelectedDatas.Add(newBottomTRoleM);
                }
                else
                {
                    if (heroSelectedStates.Contains(m_SimulationRoleID[i]))
                    {
                        BottomRoleModel newBottomTRoleM = new BottomRoleModel(m_SimulationRoleID[i], this, true);

                        m_SelectedRoleID.Add(m_SimulationRoleID[i]);

                        m_BottomTrainingRoleDatas.Add(newBottomTRoleM);
                    }
                    else
                    {
                        BottomRoleModel newBottomTRoleM = new BottomRoleModel(m_SimulationRoleID[i], this, false);

                        m_BottomTrainingRoleDatas.Add(newBottomTRoleM);
                    }
                }
            }

            foreach (var item in tempSelectedDatas)
            {
                m_BottomTrainingRoleDatas.Add(item);
            }
            tempSelectedDatas.Clear();
            trainingStates.Clear();
            heroSelectedStates.Clear();
            //TitleIcon.sprite = SpriteHandler.S.GetSprite(AtlasDefine.TestAtlas, iconName);

            m_MiddleTRoleUGL.SetCellRenderer(OnMiddleCellRenderer);
            m_BottomTRoleUL.SetCellRenderer(OnBottomCellRenderer);

            m_ScrollRectAdjustPos.EnableAutoAdjust(m_SimulationRoleID.Count);

            m_MiddleTRoleUGL.SetDataCount(m_PanelData.GetSlotLCount());
            m_BottomTRoleUL.SetDataCount(m_BottomTrainingRoleDatas.Count);
        }
        private void OnBottomCellRenderer(Transform root, int index)
        {
            BottomRoleModel bottomRoleModel = m_BottomTrainingRoleDatas[index];

            BottomTrainingRole bottomTR = root.GetComponent<BottomTrainingRole>();

            bottomRoleModel.SetTRoleData(index, bottomTR);

            bottomTR.OnInit(bottomRoleModel,m_SelectedCount);


            //BottomRoleModel bottomTrainingRoleData = m_BottomTrainingRoleDatas.FirstOrDefault(item => item.index.Value == index);
            //if (bottomTrainingRoleData != null)
            //{
            //    root.GetComponent<BottomTrainingRole>().OnInit(bottomTrainingRoleData, m_SelectedCount);
            //}
            //else
            //{
            //    BottomTrainingRole bottomTrainingRole = root.GetComponent<BottomTrainingRole>();
            //    BottomRoleModel newBottomTRoleM = new BottomRoleModel(index, false, test[index], bottomTrainingRole,this);
            //    m_BottomTrainingRoleDatas.Add(newBottomTRoleM);
            //    bottomTrainingRole.OnInit(newBottomTRoleM, m_SelectedCount);
            //}
        }

        private void OnMiddleCellRenderer(Transform root, int index)
        {
            MiddleSlotModel middleTrainingRoleModule = m_MiddleTRoleDatas.FirstOrDefault(item => item.index.Value == index);
            if (middleTrainingRoleModule != null)
            {
                root.GetComponent<MiddleTrainingRole>().OnInit(middleTrainingRoleModule);
            }
            else
            {
                MiddleSlotModel newMiddleTRoleM;

                MiddleTrainingRole middleTrainingRole = root.GetComponent<MiddleTrainingRole>();

                newMiddleTRoleM = new MiddleSlotModel(index, (m_PanelData.trainingRoomModel.slotModelList)[index], middleTrainingRole);
                m_MiddleTRoleDatas.Add(newMiddleTRoleM);

                //middleTrainingRoles.Add(middleTrainingRole);
                middleTrainingRole.OnInit(newMiddleTRoleM);
            }
        }

        private void BindUniRxUI()
        {
            m_SelectedCount.Select(count => count + Define.SYMBOL_SLASH + m_PanelData.GetSlotLCount()).SubscribeToTextMeshPro(RoleSelectNumberTMP);
        }

        public void CreateMiddleTRole()
        {
            //MiddleTrainingRole middleTraining = Instantiate(MiddleTrainingRole, MiddleTrainingRoleTra.transform).GetComponent<MiddleTrainingRole>();

        }
        #endregion
       
    }
    #region Other Data Class
    public enum TrainingSlotState
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
        HeroSelected = 3,
    }
    public class BottomRoleModel
    {
        public IntReactiveProperty index;
        public BoolReactiveProperty isSelected;
        public IntReactiveProperty roleID;
        public BottomTrainingRole bottomTrainingRole;
        public TrainingRoomPanel trainingRoomPanel;

        public BottomRoleModel(int index, bool selected, int roleID, BottomTrainingRole bottomTrainingRole, TrainingRoomPanel trainingRoomPanel)
        {
            this.index = new IntReactiveProperty(index);
            this.isSelected = new BoolReactiveProperty(selected);
            this.roleID = new IntReactiveProperty(roleID);
            this.bottomTrainingRole = bottomTrainingRole;
            this.trainingRoomPanel = trainingRoomPanel;
        }

        public BottomRoleModel(int roleID, TrainingRoomPanel trainingRoomPanel,bool selected)
        {
            this.roleID = new IntReactiveProperty(roleID);
            this.trainingRoomPanel = trainingRoomPanel;
            this.isSelected = new BoolReactiveProperty(selected);
        }

        public void SetTRoleData(int index, BottomTrainingRole bottomTrainingRole)
        {
            this.index = new IntReactiveProperty(index);
            this.bottomTrainingRole = bottomTrainingRole;
        }
    }

    public class MiddleSlotModel
    {
        public IntReactiveProperty index;

        public TrainingSlotModel trainingSlotModel;
        public MiddleTrainingRole middleTrainingRole;

        public MiddleSlotModel(int index, TrainingSlotModel trainingSlotModel, MiddleTrainingRole middleTrainingRole)
        {
            this.index = new IntReactiveProperty(index);
            this.trainingSlotModel = trainingSlotModel;
            this.middleTrainingRole = middleTrainingRole;
        }
    }
    #endregion
}
