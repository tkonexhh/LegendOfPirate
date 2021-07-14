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
        [SerializeField]
        private UGridListView m_TrainingPosUGList;
        [SerializeField]
        private USimpleListView m_TrainingPrepRoleUList;

        #region Data
        private List<TrainingSlotModel> m_TrainingSlotModels;
        private List<TrainingPreparatorRoleModel> m_RoleModelList;
        #endregion

        #region AbstractAnimPanel
        protected override void OnUIInit()
        {
            base.OnUIInit();

            #region TEST
            ModelMgr.S.GetModel<RoleGroupModel>().AddSpiritRoleModel(1037, 100);
            ModelMgr.S.GetModel<RoleGroupModel>().AddSpiritRoleModel(1038, 100);
            ModelMgr.S.GetModel<RoleGroupModel>().AddSpiritRoleModel(1039, 100);
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

            m_PanelData.trainingModel.ClearCacheData();
        }
        #endregion

        #region OnClickAddListener
        private void RefreshCommand()
        {
            m_TrainingPrepRoleUList.SetDataCount(m_RoleModelList.Count);
        }

        private void UpgradeBtn()
        {
            m_PanelData.trainingModel.OnLevelUpgrade();
        }

        private void RraintBtn()
        {
            m_PanelData.trainingModel.StartLearn();

            m_TrainingPrepRoleUList.SetDataCount(m_RoleModelList.Count);

            m_PanelData.trainingModel.ResetSelectedNumber();
        }

        private void AutoSelectBtn()
        {
            m_PanelData.trainingModel.AutoSelectRole();

            m_TrainingPrepRoleUList.SetDataCount(m_RoleModelList.Count);

            m_PanelData.trainingModel.ResetSelectedNumber();
        }

        private void OnClickAddListener()
        {
            m_ExitBtn.OnClickAsObservable().Subscribe(_ => HideSelfWithAnim()).AddTo(this);
        }

        #endregion

        #region Other Method
        private void InitData()
        {
            m_TrainingPosUGList.SetCellRenderer(OnReadPosCellRenderer);
            m_TrainingPrepRoleUList.SetCellRenderer(OnLibPrepCellRenderer);

            m_TrainingPosUGList.SetDataCount(m_TrainingSlotModels.Count);
            m_TrainingPrepRoleUList.SetDataCount(m_RoleModelList.Count);
        }

        private void OnLibPrepCellRenderer(Transform root, int index)
        {
            if (m_RoleModelList != null)
            {
                TrainingPreparatorRoleModel bottomLibRoleModel = m_RoleModelList[index];

                TrainingPreparatorRole bottom = root.GetComponent<TrainingPreparatorRole>();

                bottom.OnRefresh(bottomLibRoleModel);
            }
            else
                Log.e("Error : RoleModelList is null !");
        }

        private void OnReadPosCellRenderer(Transform root, int index)
        {
            if (m_TrainingSlotModels != null)
            {
                TrainingSlotModel trainingSlotModel = m_TrainingSlotModels[index];

                TrainingPosition trainingPos = root.GetComponent<TrainingPosition>();

                trainingPos.OnRefresh(trainingSlotModel);
            }
            else
                Log.e("Error : trainingSlotModels is null !");
        }
        #endregion
    }
}
