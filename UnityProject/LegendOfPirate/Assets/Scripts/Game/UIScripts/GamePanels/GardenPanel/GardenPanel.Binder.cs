using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using GFrame;
using TMPro;
namespace GameWish.Game
{
	public class GardenPanelData : UIPanelData
	{
		public GardenModel gardenModel;
		public GardenPanelData()
		{
		}
	}
	
	public partial class GardenPanel
	{
		private GardenPanelData m_PanelData = null;
		
		private void AllocatePanelData(params object[] args)
		{
			m_PanelData = UIPanelData.Allocate<GardenPanelData>();
			m_PanelData.gardenModel = ModelMgr.S.GetModel<ShipModel>().GetShipUnitModel(ShipUnitType.Garden) as GardenModel;
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<GardenPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
			m_PanelData.gardenModel.level.AsObservable().SubscribeToTextMeshPro(BuildingLevel, "Lv.{0}").AddTo(this); 
			m_PanelData.gardenModel.level.AsObservable().Subscribe(level =>OnGardenLevelChange(level)).AddTo(this);
		}
		
		private void BindUIToModel()
		{
			PlantBtn.OnClickAsObservable().Subscribe(_ => OnPlantBtnClick()).AddTo(this);
			CloseBtn.OnClickAsObservable().Subscribe(_ => HideSelfWithAnim()).AddTo(this);
			LevelUpBtn.OnClickAsObservable().Subscribe(_ => OnLevelUpBtnClick()).AddTo(this);
		}
		private void OnPlantBtnClick() { }
		private void OnLevelUpBtnClick() 
		{
          
                UIMgr.S.OpenTopPanel(UIID.BuildingLevelUpPanel, null, ShipUnitType.Garden);
     
        }
		private void OnGardenLevelChange(int level) 
		{
			var PlantSlots = Content.GetComponentsInChildren<Toggle>();
			for (int i = 0; i < PlantSlots.Length; i++) 
			{
				if (level>=TDFacilityGardenTable.dataList[i].level)
				{
					PlantSlots[i].GetComponentInChildren<GImage>().gameObject.SetActive(false);
					PlantSlots[i].GetComponentInChildren<TextMeshProUGUI>().text = TDFacilityGardenTable.dataList[i].seedUnlock;
				}
				else 
				{
					PlantSlots[i].GetComponentInChildren<TextMeshProUGUI>().text =string.Format("GardenLevel {0}",  TDFacilityGardenTable.dataList[i].level);
				}
            }
        }
    }
}
