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

            try
            {
                m_PanelData.shipModel = ModelMgr.S.GetModel<ShipModel>();

                m_PanelData.trainingRoomModel = m_PanelData.shipModel.GetShipUnitModel(ShipUnitType.TrainingRoom) as TrainingRoomModel;

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
            m_PanelData.trainingRoomModel
                .level
                .Select(level => CommonMethod.GetStringForTableKey(LanguageKeyDefine.Fixed_Title_Lv) + level.ToString())
                .SubscribeToTextMeshPro(TrainingLevelTMP).AddTo(this);
        }

        private void BindUIToModel()
        {
        }

        private void RegisterEvents()
        {
            EventSystem.S.Register(EventID.OnTrainingUpgradeRefresh, HandlerEvent);
            EventSystem.S.Register(EventID.OnTrainingSelectRole, HandlerEvent);
        }

        private void OnClickAddListener()
        {
            LeftArrowBtn.OnClickAsObservable().Subscribe(_ =>
            {
                LeftArrowBtnEnt();
            });
            RightArrowBtn.OnClickAsObservable().Subscribe(_ =>
            {
                RightArrowBtnEnt();
            });
            TrainingUpgradeBtn.OnClickAsObservable().Subscribe(_ =>
            {
                TUpgradeBtnEnt();
            });
            TrainBtn.OnClickAsObservable().Subscribe(_ =>
            {
                TrainBtnEnt();
            });
            AutoTrainBtn.OnClickAsObservable().Subscribe(_ =>
            {
                AutoTrainBtnEnt();
            });
            BgBtn.OnClickAsObservable().Subscribe(_ =>
            {
                BgBtnEnt();
            });
        }
        private void UnregisterEvents()
        {
            EventSystem.S.UnRegister(EventID.OnTrainingUpgradeRefresh, HandlerEvent);
            EventSystem.S.UnRegister(EventID.OnTrainingSelectRole, HandlerEvent);
        }
    }
}
