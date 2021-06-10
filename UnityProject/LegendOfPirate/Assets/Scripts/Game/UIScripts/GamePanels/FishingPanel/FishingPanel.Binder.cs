using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
	public class FishingPanelData : UIPanelData
	{
		RoleGroupModel m_RoleGroupModel;
		public FishingPanelData()
        {
			m_RoleGroupModel = ModelMgr.S.GetModel<RoleGroupModel>();
        }
	}
	
	public partial class FishingPanel
	{
		private FishingPanelData m_PanelData = null;
		
		private void AllocatePanelData(params object[] args)
		{
			 m_PanelData = UIPanelData.Allocate<FishingPanelData>();
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<FishingPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
		}
		
		private void BindUIToModel()
		{
			CloseBtn.OnClickAsObservable().Subscribe(_ => HideSelfPanel()).AddTo(this);
			TrainBtn.OnClickAsObservable().Subscribe(_=>OnTrainBtnClick()).AddTo(this);
			AutoBtn.OnClickAsObservable().Subscribe(_ => OnAutoBtnClick()).AddTo(this);
		}
		private void OnTrainBtnClick() {}
		private void OnAutoBtnClick() { }
	}
}
