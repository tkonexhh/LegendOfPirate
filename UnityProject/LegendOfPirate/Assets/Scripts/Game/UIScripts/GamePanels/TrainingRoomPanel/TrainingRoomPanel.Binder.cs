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
            m_SelectedCount
                .Select(count => count + Define.SYMBOL_SLASH + m_PanelData.GetSlotLCount())
                .SubscribeToTextMeshPro(RoleSelectNumberTMP);

            m_PanelData.trainingRoomModel
                       .level
                       .Select(level => CommonMethod.GetStringForTableKey(LanguageKeyDefine.FIXED_TITLE_LV) + level.ToString())
                       .SubscribeToTextMeshPro(TrainingLevelTMP).AddTo(this);

            foreach (var item in m_PanelData.trainingRoomModel.slotModelList)
            {
                item.trainState.Subscribe(_ => RefreshSelectedCount()).AddTo(this);
            }
        }

        private void BindUIToModel()
        {
            TrainingUpgradeBtn.OnClickAsObservable().Subscribe(_ =>
            {
                m_PanelData.trainingRoomModel.OnLevelUpgrade(1);

            }).AddTo(this);

            AutoTrainBtn.OnClickAsObservable().Subscribe(_ =>
            {

            }).AddTo(this);

            TrainBtn.OnClickAsObservable().Subscribe(_ =>
            {
                foreach (var item in m_TraPosDatas)
                {
                    if (item.trainingSlotModel.trainState.Value == TrainingSlotState.HeroSelected)
                    {
                        item.trainingSlotModel.StartTraining(DateTime.Now);
                        SelectedRoleSort();
                    }
                }
            }).AddTo(this);
        }
    }
}
