using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System.Linq;
using System;
using System.Collections.Generic;

namespace GameWish.Game
{
    public partial class LibraryRoomPanel : AbstractAnimPanel
    {
        [SerializeField]
        private ScrollRectAutoAdjustPosition m_ScroRecAdjustPos;
        [SerializeField]
        private UGridListView m_ReadPosUGList;
        [SerializeField]
        private USimpleListView m_LibPrepRoleUList;
        #region Data
        private IntReactiveProperty m_SelectedCount = new IntReactiveProperty(0);
        private List<LibraryPreparatorRoleModel> m_LibPrepRoleDatas = new List<LibraryPreparatorRoleModel>();
        private List<ReadPostionModel> m_ReadPosDatas = new List<ReadPostionModel>();

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
        protected override void BeforDestroy()
        {
            base.BeforDestroy();

            ReleasePanelData();
        }

        protected override void OnPanelHideComplete()
        {
            base.OnPanelHideComplete();

            CloseSelfPanel();

            CloseDependPanel(EngineUI.MaskPanel);
        }
        #endregion
        #region OnClickAddListener

        private void OnClickAddListener()
        {
            LeftArrowBtn.OnClickAsObservable().Subscribe(_ =>
            {
                m_ScroRecAdjustPos.Move2Pre();
            }).AddTo(this);

            RightArrowBtn.OnClickAsObservable().Subscribe(_ =>
            {
                m_ScroRecAdjustPos.Move2Next();
            }).AddTo(this);

            TrainBtn.OnClickAsObservable().Subscribe(_ =>
            {
                RefreshSelectedCount();
            }).AddTo(this);

            AutoTrainBtn.OnClickAsObservable().Subscribe(_ =>
            {
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
            EventSystem.S.Register(EventID.OnLibrarySelectRole, HandlerEvent);
        }
        private void UnregisterEvents()
        {
            EventSystem.S.UnRegister(EventID.OnLibrarySelectRole, HandlerEvent);
        }
        private void HandlerEvent(int key, object[] param)
        {
            switch ((EventID)key)
            {
                case EventID.OnLibrarySelectRole:
                    ReadPostionModel spareReadPosModel = null;
                    ReadPostionModel selectedReadPosModel = null;
                    LibraryPreparatorRoleModel libPrepRoleModel = (LibraryPreparatorRoleModel)param[0];
                    foreach (var item in m_ReadPosDatas)
                    {
                        switch (item.librarySlotModel.libraryState.Value)
                        {
                            case LibrarySlotState.Free:
                                if (spareReadPosModel == null)
                                {
                                    spareReadPosModel = item;
                                }
                                break;
                            case LibrarySlotState.Reading:
                            case LibrarySlotState.Locked:
                                break;
                            case LibrarySlotState.HeroSelected:
                                if (item.librarySlotModel.heroId == libPrepRoleModel.roleID)
                                {
                                    selectedReadPosModel = item;
                                }
                                break;
                        }
                    }
                    //Selected remove
                    if (selectedReadPosModel != null)
                    {
                        selectedReadPosModel.librarySlotModel.OnHeroUnselected();
                        m_SelectedRoleID.Remove(libPrepRoleModel.roleID);
                        libPrepRoleModel.libPrepRole.HandleSelectedRole(false);
                        return;
                    }
                    //Unselected add
                    if (spareReadPosModel != null && spareReadPosModel.librarySlotModel.libraryState.Value == LibrarySlotState.Free)
                    {
                        spareReadPosModel.librarySlotModel.OnHeroSelected(libPrepRoleModel.roleID);
                        spareReadPosModel.readPos.OnRefresh(spareReadPosModel);
                        m_SelectedRoleID.Add(libPrepRoleModel.roleID);
                        libPrepRoleModel.libPrepRole.HandleSelectedRole(true, spareReadPosModel.librarySlotModel);
                        return;
                    }
                    if (selectedReadPosModel == null && spareReadPosModel == null)
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
        private void InitData()
        {
            RefreshSelectedCount();

            InitReadPosDatas();
            InitLibPrepDatas();

            //TitleIcon.sprite = SpriteHandler.S.GetSprite(AtlasDefine.TestAtlas, iconName);

            m_ReadPosUGList.SetCellRenderer(OnReadPosCellRenderer);
            m_LibPrepRoleUList.SetCellRenderer(OnLibPrepCellRenderer);

            m_ScroRecAdjustPos.EnableAutoAdjust(m_LibPrepRoleDatas.Count);

            m_ReadPosUGList.SetDataCount(m_ReadPosDatas.Count);
            m_LibPrepRoleUList.SetDataCount(m_LibPrepRoleDatas.Count);
        }
        public void SelectedRoleSort()
        {
            for (var i = 0; i < m_LibPrepRoleDatas.Count - 1; i++)
            {
                for (var j = 0; j < m_LibPrepRoleDatas.Count - 1 - i; j++)
                {
                    if (m_SelectedRoleID.Contains(m_LibPrepRoleDatas[j].roleID))
                    {
                        LibraryPreparatorRoleModel LibPrepRole = m_LibPrepRoleDatas[j];
                        m_LibPrepRoleDatas[j] = m_LibPrepRoleDatas[j + 1];
                        m_LibPrepRoleDatas[j + 1] = LibPrepRole;
                    }
                }
            }
            m_LibPrepRoleUList.SetDataCount(m_LibPrepRoleDatas.Count);
        }
        private void RefreshSelectedCount()
        {
            int number = 0;
            foreach (var item in m_PanelData.libraryModel.slotModelList)
            {
                if (item.libraryState.Value == LibrarySlotState.Reading)
                {
                    number++;
                }
            }
            m_SelectedCount.Value = number;
        }
        private void InitLibPrepDatas()
        {
            foreach (var item in m_SimulationRoleID)
            {
                m_LibPrepRoleDatas.Add(new LibraryPreparatorRoleModel(item));
            }
        }
        private void InitReadPosDatas()
        {
            foreach (var item in m_PanelData.libraryModel.slotModelList)
            {
                m_ReadPosDatas.Add(new ReadPostionModel(item));
            }
        }

        private void OnLibPrepCellRenderer(Transform root, int index)
        {
            LibraryPreparatorRoleModel bottomLibRoleM = m_LibPrepRoleDatas[index];

            LibraryPreparatorRole bottom = root.GetComponent<LibraryPreparatorRole>();

            bottomLibRoleM.SetBottomLibRole(bottom);

            bottom.OnInit(bottomLibRoleM);
        }

        private void OnReadPosCellRenderer(Transform root, int index)
        {
            ReadPostionModel middleLRoleM = m_ReadPosDatas[index];

            ReadPosition readPos = root.GetComponent<ReadPosition>();

            readPos.OnRefresh(middleLRoleM);

            middleLRoleM.SetTraPosData(readPos);
        }
        protected override void OnClose()
        {
            base.OnClose();

            ReleasePanelData();
            UnregisterEvents();
        }
        #endregion
    }
}
