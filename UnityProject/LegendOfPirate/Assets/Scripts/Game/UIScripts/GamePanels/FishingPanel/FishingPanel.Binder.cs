using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using TMPro;
using System;

namespace GameWish.Game
{
	public class FishingPanelData : UIPanelData
	{
        public RoleGroupModel roleGroupModel;
		public FishingPlatformModel fishingPlantfromModel;
		public ReactiveProperty<int> chooseRoleCount;
        public FishingPanelData()
        {
		
        }
	}
	
	public partial class FishingPanel
	{
		private FishingPanelData m_PanelData = null;
		private void AllocatePanelData(params object[] args)
		{
			m_PanelData = UIPanelData.Allocate<FishingPanelData>();
			m_PanelData.roleGroupModel = ModelMgr.S.GetModel<RoleGroupModel>();
			m_PanelData.fishingPlantfromModel = ModelMgr.S.GetModel<ShipModel>().GetShipUnitModel(ShipUnitType.FishingPlatform) as FishingPlatformModel;

		}
		
		private void ReleasePanelData()
		{
			ObjectPool<FishingPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
			m_PanelData.roleGroupModel.roleItemList.ObserveCountChanged().Subscribe(count_=> OnRoleListChange()).AddTo(this);
			m_PanelData.chooseRoleCount = new ReactiveProperty<int>();
			m_PanelData.chooseRoleCount.Where(count=>count<=3).AsObservable().Subscribe(count => RoleCount.text = string.Format("{0}/3", count)).AddTo(this);
			m_PanelData.fishingPlantfromModel.level.AsObservable().SubscribeToTextMeshPro(BuildingLevel,"Lv.{0}").AddTo(this); 
		
		}
		
		private void BindUIToModel()
		{
			CloseBtn.OnClickAsObservable().Subscribe(_ => HideSelfWithAnim()).AddTo(this);
			TrainBtn.OnClickAsObservable().Subscribe(_=>OnTrainBtnClick()).AddTo(this);
			AutoBtn.OnClickAsObservable().Subscribe(_ => OnAutoBtnClick()).AddTo(this);
			LevelUpBtn.OnClickAsObservable().Subscribe(_ => OnLevelUpBtnClick()).AddTo(this);
		}

        private void OnLevelUpBtnClick()
        {
			UIMgr.S.OpenTopPanel(UIID.BuildingLevelUpPanel, null, ShipUnitType.FishingPlatform);
        }

        private void OnTrainBtnClick() {}
		private void OnAutoBtnClick() { }
		private void OnRoleListChange()
		{
			SetRoleListUI();
		}
		private void SetRoleListUI() 
		{
			Content.SetWidth(Role_Tamp.GetComponent<RectTransform>().GetWidth() * m_PanelData.roleGroupModel.roleItemList.Count + 15f);
			foreach (var item in m_PanelData.roleGroupModel.roleItemList) 
			{
				var copobj = Instantiate(Role_Tamp);
				copobj.GetComponentInChildren<Image>().sprite = SpriteLoader.S.GetSpriteByName(item.resName);
				copobj.GetComponentInChildren<TextMeshProUGUI>().text = item.name;
				copobj.GetComponent<RectTransform>().parent = Content;
				copobj.onValueChanged.AsObservable().Subscribe(on =>OnRoleChoose(on,copobj) ).AddTo(this);
			}
		}
		private void OnRoleChoose(bool on,Toggle toggle) 
		{
			m_PanelData.chooseRoleCount.Value += on ? 1 : -1;
			if(!toggle.isOn)toggle.interactable = m_PanelData.chooseRoleCount.Value >= 3;
		}
	}
	
}
