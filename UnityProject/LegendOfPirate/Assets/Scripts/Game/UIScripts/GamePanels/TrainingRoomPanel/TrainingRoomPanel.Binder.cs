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
		public TrainingRoomPanelData()
		{

		}
	}
	
	public partial class TrainingRoomPanel
	{

		private TrainingRoomPanelData m_PanelData = null;


		private string iconName;
		private StringReactiveProperty reactiveProperty1 = new StringReactiveProperty("6");

		private void AllocatePanelData(params object[] args)
		{
			 m_PanelData = UIPanelData.Allocate<TrainingRoomPanelData>();

		}

        private void ReleasePanelData()
		{
			ObjectPool<TrainingRoomPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
            reactiveProperty1
				.Select(str => CommonMethod.GetStringForTableKey(LanguageKeyDefine.Fixed_Title_Lv) + str)
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
			TrainingUpgradeBtn.OnClickAsObservable().Subscribe(_ => {
				Debug.LogError("Upgrade");//TODO
			});
			TrainBtn.OnClickAsObservable().Subscribe(_ => {
				Debug.LogError("Train");//TODO
			});
			AutoTrainBtn.OnClickAsObservable().Subscribe(_ => {
				Debug.LogError("AutoTrain");//TODO
			});
		}
		private void UnregisterEvents()
		{

		}

	}
}
