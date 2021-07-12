using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;

namespace GameWish.Game
{
    public class TrainingRoomPanelData : UIPanelData
    {
        public ShipModel shipModel;
        public TrainingRoomModel trainingRoomModel;
        public RoleGroupModel roleGroupModel;
        public TrainingRoomPanelData()
        {

        }

        #region Public
        /// <summary>
        /// 获取当前等级可训练的槽位数
        /// </summary>
        /// <returns></returns>
        public int GetTrainingRoomCapacity()
        {
            return trainingRoomModel.tableConfig.capacity;
        }

        public int GetSlotLCount()
        {
            return trainingRoomModel.slotModelList.Count;
        }
        #endregion
    }
    public partial class TrainingRoomPanel
    {
        private TrainingRoomPanelData m_PanelData = null;

        private void AllocatePanelData(params object[] args)
        {
            m_PanelData = UIPanelData.Allocate<TrainingRoomPanelData>();

            m_PanelData.shipModel = ModelMgr.S.GetModel<ShipModel>();
            m_PanelData.roleGroupModel = ModelMgr.S.GetModel<RoleGroupModel>();
            m_PanelData.trainingRoomModel = m_PanelData.shipModel.GetShipUnitModel(ShipUnitType.TrainingRoom) as TrainingRoomModel;
            RolesZeroState = m_SelectedCount.Select(val => val <= 0).ToReactiveProperty();
        }

        private void ReleasePanelData()
        {
            ObjectPool<TrainingRoomPanelData>.S.Recycle(m_PanelData);
        }

        private void BindModelToUI()
        {
            m_PanelData.trainingRoomModel.level.SubscribeToColorTextMeshPro(m_TrainingLevelTMP, ColorDefine.LEVEL_COLOR).AddTo(this);
            RolesZeroState.SubscribeToPositiveActive(m_AutoSelectBtn).AddTo(this);
            RolesZeroState.SubscribeToNegativeActive(m_SelectRoleState).AddTo(this);
        }

        private void BindUIToModel()
        {
            m_RoleAutoSelectBtn.OnClickAsObservable().Subscribe(_ => AutoSelectBtn()).AddTo(this);
            m_RraintBtn.OnClickAsObservable().Subscribe(_ => RraintBtn()).AddTo(this);
            m_AutoSelectBtn.OnClickAsObservable().Subscribe(_ => AutoSelectBtn()).AddTo(this);
            m_TrainingUpgradeBtn.OnClickAsObservable().Subscribe(_ => TrainingUpgradeBtn()).AddTo(this);
        }
    }
}
