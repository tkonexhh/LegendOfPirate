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
        private  IntReactiveProperty m_SelectedCount = new IntReactiveProperty(0);
        private IReadOnlyReactiveProperty<bool> RolesZeroState;

        private List<TrainingPreparatorRoleModel> m_PrepRoleDatas = new List<TrainingPreparatorRoleModel>();
        private List<TrainingPositionModel> m_TraPosDatas = new List<TrainingPositionModel>();
        #endregion

        #region AbstractAnimPanel
        protected override void OnUIInit()
        {
            base.OnUIInit();

            OnClickAddListener();
        }

        protected override void OnPanelOpen(params object[] args)
        {
            base.OnPanelOpen(args);

            OpenDependPanel(EngineUI.MaskPanel, -1, null);

            AllocatePanelData();

            #region TEST
            m_PanelData.roleGroupModel.AddSpiritRoleModel(1037, 100);
            m_PanelData.roleGroupModel.AddSpiritRoleModel(1038, 100);
            m_PanelData.roleGroupModel.AddSpiritRoleModel(1039, 100);
            #endregion

            BindModelToUI();
            BindUIToModel();

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
        }
        #endregion

        #region OnClickAddListener
        private void OnClickAddListener()
        {
            m_BgBtn.OnClickAsObservable().Subscribe(_ => HideSelfWithAnim()).AddTo(this);
            m_ExitBtn.OnClickAsObservable().Subscribe(_ => HideSelfWithAnim()).AddTo(this);
        }

        private void AutoSelectBtn()
        {
            List<RoleModel> roleModels = ModelMgr.S.GetModel<RoleGroupModel>().GetRolesByManagementState();
            if (roleModels.Count == 0)
                return;
            m_PanelData.trainingRoomModel.TrainingRoleGroup(roleModels);
            foreach (var item in roleModels)
            {
                TrainingPreparatorRoleModel training = m_PrepRoleDatas.Where(i => i.GetRoleID() == item.id).FirstOrDefault();
           
                training.RefreshTrainingSlotModel();
            }

            SortRefreshList();
        }  

        private void TrainingUpgradeBtn()
        {
            //TODO 比对材料得消耗
            if (true)
            {
                m_PanelData.trainingRoomModel.OnLevelUpgrade();
            }
        }

        private void RraintBtn()
        {
            if (!RolesZeroState.Value)
            {
                m_PanelData.trainingRoomModel.StartTraining();
                foreach (var item in m_PrepRoleDatas)
                {
                    item.CancelSelectedState();
                }
                SortRefreshList();
            }
            else
                FloatMessageTMP.S.ShowMsg(LanguageKeyDefine.TRAININGROOM_CONT_Ⅱ);
        }
        #endregion

        #region Public
        public void AddSelectedCount()
        {
            m_SelectedCount.Value++;
        }

        public void ReduceSelectedCount()
        {
            m_SelectedCount.Value--;
        }
        #endregion

        #region Private

        private void SortRefreshList()
        {
            List<TrainingPreparatorRoleModel> trainRoleModels = new List<TrainingPreparatorRoleModel>();
            foreach (var item in m_PrepRoleDatas)
            {
                if (!item.IsEmpty.Value && item.TrainingSlotModel.IsTraining())
                {
                    trainRoleModels.Add(item);
                }
            }
            foreach (var item in trainRoleModels)
                m_PrepRoleDatas.Remove(item);
            m_PrepRoleDatas.AddRange(trainRoleModels);
            trainRoleModels.Clear();
            m_PrepRoleUList.SetDataCount(m_PrepRoleDatas.Count);
        }

        private void InitData()
        {
            InitTraPosDatas();
            InitPrepRoleDatas();

            m_TraPosUGList.SetCellRenderer(OnTraPosCellRenderer);
            m_PrepRoleUList.SetCellRenderer(OnPropRoleCellRenderer);

            //m_ScrollRectAdjustPos.EnableAutoAdjust(m_SimulationRoleID.Count);

            SortRefreshList();

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
            foreach (var item in m_PanelData.roleGroupModel.RoleUnlockedItemList)
            {
                m_PrepRoleDatas.Add(new TrainingPreparatorRoleModel(item, m_PanelData.trainingRoomModel,this));
            }
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
