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
                .SubscribeToTextMeshPro(TrainingLevelTMP);
        }

        private void BindUIToModel()
        {
        }

        private void RegisterEvents()
        {

        }

        private void OnClickAddListener()
        {
            TrainingUpgradeBtn.OnClickAsObservable().Subscribe(_ =>
            {
                Debug.LogError("Upgrade");//TODO
            });
            TrainBtn.OnClickAsObservable().Subscribe(_ =>
            {
                Debug.LogError("Train");//TODO
            });
            AutoTrainBtn.OnClickAsObservable().Subscribe(_ =>
            {
                Debug.LogError("AutoTrain");//TODO
            });
        }
        private void UnregisterEvents()
        {

        }

    }
}
