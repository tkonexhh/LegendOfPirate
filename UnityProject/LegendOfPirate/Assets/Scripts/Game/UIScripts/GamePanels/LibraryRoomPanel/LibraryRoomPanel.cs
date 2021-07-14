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
        private UGridListView m_ReadPosUGList;
        [SerializeField]
        private USimpleListView m_LibPrepRoleUList;

        #region Data
        private List<LibrarySlotModel> m_LibrarySlotModels ;
        private List<LibraryPreparatorRoleModel> m_RoleModelList;
        #endregion

        #region AbstractAnimPanel
        protected override void OnUIInit()
        {
            base.OnUIInit();

            #region TEST
            ModelMgr.S.GetModel<RoleGroupModel>().AddSpiritRoleModel(1037, 100);
            ModelMgr.S.GetModel<RoleGroupModel>().AddSpiritRoleModel(1038, 100);
            ModelMgr.S.GetModel<RoleGroupModel>().AddSpiritRoleModel(1039, 100);
            ModelMgr.S.GetModel<RoleGroupModel>().AddSpiritRoleModel(1040, 100);
            ModelMgr.S.GetModel<RoleGroupModel>().AddSpiritRoleModel(1041, 100);
            ModelMgr.S.GetModel<RoleGroupModel>().AddSpiritRoleModel(1042, 100);
            ModelMgr.S.GetModel<RoleGroupModel>().AddSpiritRoleModel(1043, 100);
            ModelMgr.S.GetModel<RoleGroupModel>().AddSpiritRoleModel(1044, 100);
            ModelMgr.S.GetModel<RoleGroupModel>().AddSpiritRoleModel(1045, 100);
            ModelMgr.S.GetModel<RoleGroupModel>().AddSpiritRoleModel(1046, 100);
            #endregion

            AllocatePanelData();

            BindModelToUI();
            BindUIToModel();

            OnClickAddListener();
        }

        protected override void OnPanelOpen(params object[] args)
        {
            base.OnPanelOpen(args);

            OpenDependPanel(EngineUI.MaskPanel, -1, null);

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
        protected override void OnClose()
        {
            base.OnClose();

            ReleasePanelData();

            m_PanelData.libraryModel.ClearCacheData();
        }
        #endregion

        #region OnClickAddListener
        private void RefreshCommand()
        {
            m_LibPrepRoleUList.SetDataCount(m_RoleModelList.Count);
        }

        private void UpgradeBtn()
        {
            m_PanelData.libraryModel.OnLevelUpgrade();
        }  

        private void RraintBtn()
        {
            m_PanelData.libraryModel.StartLearn();

            m_LibPrepRoleUList.SetDataCount(m_RoleModelList.Count);

            m_PanelData.libraryModel.ResetSelectedNumber();
        }

        private void AutoSelectBtn()
        {
            m_PanelData.libraryModel.AutoSelectRole();

            m_LibPrepRoleUList.SetDataCount(m_RoleModelList.Count);

            m_PanelData.libraryModel.ResetSelectedNumber();
        }

        private void OnClickAddListener()
        {
            m_ExitBtn.OnClickAsObservable().Subscribe(_ => HideSelfWithAnim()).AddTo(this);
        }
   
        #endregion
   
        #region Other Method
        private void InitData()
        {
            m_ReadPosUGList.SetCellRenderer(OnReadPosCellRenderer);
            m_LibPrepRoleUList.SetCellRenderer(OnLibPrepCellRenderer);

            m_ReadPosUGList.SetDataCount(m_LibrarySlotModels.Count);
            m_LibPrepRoleUList.SetDataCount(m_RoleModelList.Count);
        }
  
        private void OnLibPrepCellRenderer(Transform root, int index)
        {
            if (m_RoleModelList!=null)
            {
                LibraryPreparatorRoleModel bottomLibRoleM = m_RoleModelList[index];

                LibraryPreparatorRole bottom = root.GetComponent<LibraryPreparatorRole>();

                bottom.OnRefresh(bottomLibRoleM);
            }
            else
                Log.e("Error : RoleModelList is null !");
        }

        private void OnReadPosCellRenderer(Transform root, int index)
        {
            if (m_LibrarySlotModels != null)
            {
                LibrarySlotModel librarySlotModel = m_LibrarySlotModels[index];

                ReadPosition readPos = root.GetComponent<ReadPosition>();

                readPos.OnRefresh(librarySlotModel);
            }
            else
                Log.e("Error : LibrarySlotModels is null !");
        }
        #endregion
    }
}
