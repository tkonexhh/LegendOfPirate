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
        private UGridListView m_MiddleTRoleUGList;
        [SerializeField]
        private USimpleListView m_BottomTRoleUList;

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

            AllocatePanelData();

            BindModelToUI();
            BindUIToModel();
            OnClickAddListener();
        }

        protected override void OnPanelOpen(params object[] args)
        {
            base.OnPanelOpen(args);
            RegisterEvents();

            OpenDependPanel(EngineUI.MaskPanel, -1, null);

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

        protected override void BeforDestroy()
        {
            base.BeforDestroy();

            ReleasePanelData();
        }

        protected override void OnClose()
        {
            base.OnClose();

            UnregisterEvents();
        }
        #endregion

        #region OnClickAddListener
        private void OnClickAddListener()
        {
            LeftArrowBtn.OnClickAsObservable().Subscribe(_ =>
            {
                m_ScrollRectAdjustPos?.Move2Pre();
            }).AddTo(this);
            RightArrowBtn.OnClickAsObservable().Subscribe(_ =>
            {
                m_ScrollRectAdjustPos?.Move2Next();
            }).AddTo(this);
            TrainBtn.OnClickAsObservable().Subscribe(_ =>
            {
                RefreshSelectedCount();
            }).AddTo(this);
            BgBtn.OnClickAsObservable().Subscribe(_ =>
            {
                HideSelfWithAnim();
            }).AddTo(this);
        }
        #endregion

        #region EventSystem
        private void HandlerEvent(int key, object[] param)
        {
            switch ((EventID)key)
            {
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
                                if (item.trainingSlotModel.heroId == bottomTRoleModel.roleID)
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
                        m_SelectedRoleID.Remove(bottomTRoleModel.roleID);
                        bottomTRoleModel.bottomTrainingRole.HandleSelectedRole();
                        break;
                    }
                    //Unselected add
                    if (spareSelectedModel != null && spareSelectedModel.trainingSlotModel.trainState.Value == TrainingSlotState.Free)
                    {
                        spareSelectedModel.trainingSlotModel.OnHeroSelected(bottomTRoleModel.roleID);
                        spareSelectedModel.middleTrainingRole.OnInit(spareSelectedModel);
                        m_SelectedRoleID.Add(bottomTRoleModel.roleID);
                        bottomTRoleModel.bottomTrainingRole.HandleSelectedRole();
                        break;
                    }
                    if (selectedSlot == null && spareSelectedModel == null)
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
        private void SelectedRoleSort()
        {
            for (var i = 0; i < m_BottomTrainingRoleDatas.Count - 1; i++)
            {
                for (var j = 0; j < m_BottomTrainingRoleDatas.Count - 1 - i; j++)
                {
                    if (m_SelectedRoleID.Contains(m_BottomTrainingRoleDatas[j].roleID))
                    {
                        BottomRoleModel trainingSlotModel = m_BottomTrainingRoleDatas[j];
                        m_BottomTrainingRoleDatas[j] = m_BottomTrainingRoleDatas[j + 1];
                        m_BottomTrainingRoleDatas[j + 1] = trainingSlotModel;
                    }
                }
            }
            m_BottomTRoleUList.SetDataCount(m_BottomTrainingRoleDatas.Count);
        }


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

        private List<TrainingSlotModel> GetCorrespondingStateList(TrainingSlotState trainingSlotState)
        {
            List<TrainingSlotModel> trainingSlotModels = new List<TrainingSlotModel>();

            foreach (var item in m_PanelData.trainingRoomModel.slotModelList)
            {
                if (item.trainState.Value == trainingSlotState)
                {
                    trainingSlotModels.Add(item);
                }
            }
            return trainingSlotModels;
        }

        private void InitData()
        {
            RefreshSelectedCount();

            InitBottomTRoleDatas();
            InitMiddleTRoleDatas();

            m_MiddleTRoleUGList.SetCellRenderer(OnMiddleCellRenderer);
            m_BottomTRoleUList.SetCellRenderer(OnBottomCellRenderer);

            m_ScrollRectAdjustPos.EnableAutoAdjust(m_SimulationRoleID.Count);

            m_MiddleTRoleUGList.SetDataCount(m_MiddleTRoleDatas.Count);
            m_BottomTRoleUList.SetDataCount(m_BottomTrainingRoleDatas.Count);
        }
        private void InitMiddleTRoleDatas()
        {
            foreach (var item in m_PanelData.trainingRoomModel.slotModelList)
            {
                m_MiddleTRoleDatas.Add(new MiddleSlotModel(item));
            }
        }
        private void InitBottomTRoleDatas()
        {
            List<TrainingSlotModel> trainingStates = GetCorrespondingStateList(TrainingSlotState.Training);
            List<TrainingSlotModel> heroSelectedStates = GetCorrespondingStateList(TrainingSlotState.HeroSelected);

            ReactiveCollection<BottomRoleModel> tempSelectedDatas = new ReactiveCollection<BottomRoleModel>();
            for (int i = 0; i < m_SimulationRoleID.Count; i++)
            {
                TrainingSlotModel trainingState = trainingStates.FirstOrDefault(x => x.heroId == m_SimulationRoleID[i]);
                if (trainingState.IsNotNull())
                {
                    BottomRoleModel newBottomTRoleM = new BottomRoleModel(trainingState, this, true);

                    tempSelectedDatas.Add(newBottomTRoleM);
                }
                else
                {
                    TrainingSlotModel heroSelectedState = heroSelectedStates.FirstOrDefault(x => x.heroId == m_SimulationRoleID[i]);

                    if (heroSelectedState.IsNotNull())
                    {
                        BottomRoleModel newBottomTRoleM = new BottomRoleModel(heroSelectedState, this, true);

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
        }

        private void OnBottomCellRenderer(Transform root, int index)
        {
            BottomRoleModel bottomRoleModel = m_BottomTrainingRoleDatas[index];

            BottomTrainingRole bottomTRole = root.GetComponent<BottomTrainingRole>();

            bottomRoleModel.SetTRoleData(bottomTRole);

            bottomTRole.OnInit(bottomRoleModel);
        }

        private void OnMiddleCellRenderer(Transform root, int index)
        {
            MiddleSlotModel middleSlotModel = m_MiddleTRoleDatas[index];

            MiddleTrainingRole middleTrainingRole = root.GetComponent<MiddleTrainingRole>();

            middleSlotModel.SetTRoleData(middleTrainingRole);

            middleTrainingRole.OnInit(middleSlotModel);
        }
        #endregion

    }
}
