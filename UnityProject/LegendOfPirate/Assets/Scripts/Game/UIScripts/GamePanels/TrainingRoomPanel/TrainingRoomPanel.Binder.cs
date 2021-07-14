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
        public TrainingRoomModel trainingModel;
        public RoleGroupModel roleGroupModel;
        public TrainingRoomPanelData()
        {

        }
    }
    public partial class TrainingRoomPanel
    {
        private TrainingRoomPanelData m_PanelData = null;

        private void AllocatePanelData(params object[] args)
        {
            m_PanelData = UIPanelData.Allocate<TrainingRoomPanelData>();

            try
            {
                m_PanelData.shipModel = ModelMgr.S.GetModel<ShipModel>();

                m_PanelData.trainingModel = m_PanelData.shipModel.GetShipUnitModel(ShipUnitType.TrainingRoom) as TrainingRoomModel;

                m_TrainingSlotModels = m_PanelData.trainingModel.TrainingSlotModels;

                m_PanelData.trainingModel.RefreshCurRoleModels();

                m_RoleModelList = m_PanelData.trainingModel.RoleModelList;
            }
            catch (Exception e)
            {
                Debug.LogError("e = " + e);
            }
        }

        private void ReleasePanelData()
        {
            ObjectPool<TrainingRoomPanelData>.S.Recycle(m_PanelData);
        }

        private void BindModelToUI()
        {
            m_PanelData.trainingModel.level.SubscribeToTextMeshPro(m_TrainingLevelTMP).AddTo(this);

            m_PanelData.trainingModel.IsShowReadBtn.SubscribeToActive(m_SelectRoleState, m_AutoSelectBtn).ForEach(i => i.AddTo(this));

            foreach (var item in m_PanelData.trainingModel.TrainingSlotModels)
            {
                item.refreshCommand.Subscribe(_ => RefreshCommand()).AddTo(this);
            }
        }

        private void BindUIToModel()
        {
            m_TrainingUpgradeBtn.OnClickAsObservable().Subscribe(_ => UpgradeBtn()).AddTo(this);
            m_RraintBtn.OnClickAsObservable().Subscribe(_ => RraintBtn()).AddTo(this);
            m_RoleAutoSelectBtn.OnClickAsObservable().Subscribe(_ => AutoSelectBtn()).AddTo(this);
            m_AutoSelectBtn.OnClickAsObservable().Subscribe(_ => AutoSelectBtn()).AddTo(this);
        }
    }
}
