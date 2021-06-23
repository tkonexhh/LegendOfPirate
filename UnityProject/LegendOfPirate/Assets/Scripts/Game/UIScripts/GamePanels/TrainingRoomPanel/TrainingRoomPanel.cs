using Qarth;
using Qarth.Extension;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace GameWish.Game
{
    public partial class TrainingRoomPanel : AbstractAnimPanel
    {
        #region SerializeField
        [SerializeField]
        private ScrollRectAutoAdjustPosition m_ScrollRectAdjustPos;
        [SerializeField]
        private UGridListView m_TraPosUGList;
        [SerializeField]
        private USimpleListView m_PrepRoleUList;
        #endregion

        #region Data
        private IntReactiveProperty m_SelectedCount = new IntReactiveProperty(0);
        private List<TrainingPreparatorRoleModel> m_PrepRoleDatas = new List<TrainingPreparatorRoleModel>();
        private List<TrainingPositionModel> m_TraPosDatas = new List<TrainingPositionModel>();

        private List<int> m_SelectedRoleID = new List<int>();
        private List<int> m_SimulationRoleID = new List<int>();
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
        private void RegisterEvents()
        {
            EventSystem.S.Register(EventID.OnTrainingSelectRole, HandlerEvent);
        }
        private void UnregisterEvents()
        {
            EventSystem.S.UnRegister(EventID.OnTrainingSelectRole, HandlerEvent);
        }
        private void HandlerEvent(int key, object[] param)
        {
            switch ((EventID)key)
            {
                case EventID.OnTrainingSelectRole:
                    TrainingPositionModel spareTraPosModel = null;
                    TrainingPositionModel selectedTraPosModel = null;
                    TrainingPreparatorRoleModel prepRoleModel = (TrainingPreparatorRoleModel)param[0];

                    foreach (var item in m_TraPosDatas)
                    {
                        switch (item.trainingSlotModel.trainState.Value)
                        {
                            case TrainingSlotState.Free:
                                if (spareTraPosModel == null)
                                {
                                    spareTraPosModel = item;
                                }
                                break;
                            case TrainingSlotState.Training:
                            case TrainingSlotState.Locked:
                                break;
                            case TrainingSlotState.HeroSelected:
                                if (item.trainingSlotModel.heroId == prepRoleModel.roleID)
                                {
                                    selectedTraPosModel = item;
                                }
                                break;
                        }
                    }

                    if (selectedTraPosModel != null)//Selected remove
                    {
                        selectedTraPosModel.trainingSlotModel.OnHeroUnselected();
                        m_SelectedRoleID.Remove(prepRoleModel.roleID);
                        prepRoleModel.traPrepRole.HandleSelectedRole(false);
                        break;
                    }
                    //Unselected add
                    if (spareTraPosModel != null && spareTraPosModel.trainingSlotModel.trainState.Value == TrainingSlotState.Free)
                    {
                        spareTraPosModel.trainingSlotModel.OnHeroSelected(prepRoleModel.roleID);
                        spareTraPosModel.traPos.OnRefresh(spareTraPosModel);
                        m_SelectedRoleID.Add(prepRoleModel.roleID);
                        prepRoleModel.traPrepRole.HandleSelectedRole(true, spareTraPosModel.trainingSlotModel);
                        break;
                    }
                    if (selectedTraPosModel == null && spareTraPosModel == null)
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
        public void SelectedRoleSort()
        {
            for (var i = 0; i < m_PrepRoleDatas.Count - 1; i++)
            {
                for (var j = 0; j < m_PrepRoleDatas.Count - 1 - i; j++)
                {
                    if (m_SelectedRoleID.Contains(m_PrepRoleDatas[j].roleID))
                    {
                        TrainingPreparatorRoleModel trainingSlotModel = m_PrepRoleDatas[j];
                        m_PrepRoleDatas[j] = m_PrepRoleDatas[j + 1];
                        m_PrepRoleDatas[j + 1] = trainingSlotModel;
                    }
                }
            }
            m_PrepRoleUList.SetDataCount(m_PrepRoleDatas.Count);
        }

        private void RefreshSelectedCount()
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

            InitPrepRoleDatas();
            InitTraPosDatas();

            m_TraPosUGList.SetCellRenderer(OnTraPosCellRenderer);
            m_PrepRoleUList.SetCellRenderer(OnPropRoleCellRenderer);

            m_ScrollRectAdjustPos.EnableAutoAdjust(m_SimulationRoleID.Count);

            m_TraPosUGList.SetDataCount(m_TraPosDatas.Count);
            m_PrepRoleUList.SetDataCount(m_PrepRoleDatas.Count);
        }
        private void InitTraPosDatas()
        {
            foreach (var item in m_PanelData.trainingRoomModel.slotModelList)
            {
                m_TraPosDatas.Add(new TrainingPositionModel(item));
            }
        }
        private void InitPrepRoleDatas()
        {
            List<TrainingSlotModel> trainingStates = GetCorrespondingStateList(TrainingSlotState.Training);
            List<TrainingSlotModel> heroSelectedStates = GetCorrespondingStateList(TrainingSlotState.HeroSelected);

            ReactiveCollection<TrainingPreparatorRoleModel> tempSelectedDatas = new ReactiveCollection<TrainingPreparatorRoleModel>();
            for (int i = 0; i < m_SimulationRoleID.Count; i++)
            {
                TrainingSlotModel trainingState = trainingStates.FirstOrDefault(x => x.heroId == m_SimulationRoleID[i]);
                if (trainingState.IsNotNull())
                {
                    TrainingPreparatorRoleModel newPrepRoleModel = new TrainingPreparatorRoleModel(trainingState, this, true);

                    tempSelectedDatas.Add(newPrepRoleModel);
                }
                else
                {
                    TrainingSlotModel heroSelectedState = heroSelectedStates.FirstOrDefault(x => x.heroId == m_SimulationRoleID[i]);

                    if (heroSelectedState.IsNotNull())
                    {
                        TrainingPreparatorRoleModel newPrepRoleModel = new TrainingPreparatorRoleModel(heroSelectedState, this, true);

                        m_SelectedRoleID.Add(m_SimulationRoleID[i]);

                        m_PrepRoleDatas.Add(newPrepRoleModel);
                    }
                    else
                    {
                        TrainingPreparatorRoleModel newPrepRoleModel = new TrainingPreparatorRoleModel(m_SimulationRoleID[i], this, false);

                        m_PrepRoleDatas.Add(newPrepRoleModel);
                    }
                }
            }

            foreach (var item in tempSelectedDatas)
            {
                m_PrepRoleDatas.Add(item);
            }
            tempSelectedDatas.Clear();
            trainingStates.Clear();
            heroSelectedStates.Clear();
        }

        private void OnPropRoleCellRenderer(Transform root, int index)
        {
            TrainingPreparatorRoleModel prepRoleModel = m_PrepRoleDatas[index];

            TrainingPreparatorRole prepRole = root.GetComponent<TrainingPreparatorRole>();

            prepRoleModel.SetPrepRoleData(prepRole);

            prepRole.OnRefresh(prepRoleModel);
        }

        private void OnTraPosCellRenderer(Transform root, int index)
        {
            TrainingPositionModel traPosModel = m_TraPosDatas[index];

            TrainingPosition traPos = root.GetComponent<TrainingPosition>();

            traPosModel.SetTraPosData(traPos);

            traPos.OnRefresh(traPosModel);
        }
        #endregion
    }
}
