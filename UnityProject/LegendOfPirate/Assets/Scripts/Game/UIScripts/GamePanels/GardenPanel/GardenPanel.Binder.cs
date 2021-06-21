using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using TMPro;
using System;
using System.Collections.Generic;
namespace GameWish.Game
{
	public class GardenPanelData : UIPanelData
	{
		public GardenModel gardenModel;
		public List<GardenPlantSlot> plantSlotList;
		public GardenPanelData()
		{
			plantSlotList = new List<GardenPlantSlot>();
		}
		public int GetSlotCount() 
		{
			return gardenModel.plantSlotModels.Count;
		}
	}
	
	public partial class GardenPanel
	{
		private GardenPanelData m_PanelData = null;
		
		private void AllocatePanelData(params object[] args)
		{
			
			m_PanelData = UIPanelData.Allocate<GardenPanelData>();
			m_PanelData.gardenModel = ModelMgr.S.GetModel<ShipModel>().GetShipUnitModel(ShipUnitType.Garden) as GardenModel;
			var plantSlots = m_Content.GetComponentsInChildren<Toggle>();
			
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<GardenPanelData>.S.Recycle(m_PanelData);
			m_PanelData.plantSlotList.Clear();
		}
		
		private void BindModelToUI()
		{
		    m_PanelData.gardenModel.level.AsObservable().SubscribeToTextMeshPro(m_BuildingLevel, "Lv.{0}").AddTo(this); 
			m_PanelData.gardenModel.level.AsObservable().Subscribe(level =>OnGardenLevelChange(level)).AddTo(this);
			m_PanelData.gardenModel.gardenPlantModel.gardenState.AsObservable().Subscribe(state =>OnGardenStateChange(state)).AddTo(this);
			m_PanelData.gardenModel.gardenPlantModel.plantRemainTime.Where(time => time > 0).SubscribeToTextMeshPro(m_Timer).AddTo(this);
			m_PanelData.gardenModel.gardenPlantModel.plantRemainTime.Where(time => time <= 0&& m_PanelData.gardenModel.gardenPlantModel.gardenState.Value==GardenState.Plant).Subscribe(_=> m_PanelData.gardenModel.gardenPlantModel.EndPlant()).AddTo(this);
		}
		
		private void BindUIToModel()
		{
			
	       
		}

		private void InitPanelBtn() 
		{
            m_PlantBtn.OnClickAsObservable().Subscribe(_ => OnPlantBtnClick()).AddTo(this);
            m_CloseBtn.OnClickAsObservable().Subscribe(_ => HideSelfWithAnim()).AddTo(this);
            m_LevelUpBtn.OnClickAsObservable().Subscribe(_ => OnLevelUpBtnClick()).AddTo(this);

        } 

		private void OnPlantBtnClick()
		{
			var plantMsg = m_PlantBtn.GetComponentInChildren<TextMeshProUGUI>();
            switch (m_PanelData.gardenModel.gardenPlantModel.gardenState.Value)
            {
                case GardenState.Free:
					FloatMessageTMP.S.ShowMsg("Select Plant");
                    break;
                case GardenState.Select:
					m_PanelData.gardenModel.gardenPlantModel.StartPlant(DateTime.Now);
                    break;
                case GardenState.WaitingHarvest:
					m_PanelData.gardenModel.gardenPlantModel.OnPlantHarvest();
                    break;
            }
          
        }

        private void OnLevelUpBtnClick() 
		{
			//m_PanelData.gardenModel.OnLevelUpgrade(1);
			UIMgr.S.OpenTopPanel(UIID.BuildingLevelUpPanel, null, ShipUnitType.Garden);
		}

        private void OnGardenLevelChange(int level)
        {
            
        }

		private void OnGardenStateChange(GardenState state) 
		{
			string PlantName=string.Empty;
			if (m_PanelData.gardenModel.gardenPlantModel.seedId >= 0)
			{
				 PlantName = TDFacilityGardenTable.dataList[m_PanelData.gardenModel.gardenPlantModel.seedId].seedUnlock;
			}
			switch (state)
            {
                case GardenState.Free:
					m_Timer.gameObject.SetActive(false);
					m_PlantMsg.text = string.Empty; m_PlantBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Plant";
					m_PlantBtn.interactable = true;
					break;
                case GardenState.Plant:
					m_Timer.gameObject.SetActive(true);
					m_PlantMsg.text = "Seed Id: " + m_PanelData.gardenModel.gardenPlantModel.seedId + " " + PlantName;
                    m_PlantBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Growing";
					m_PlantBtn.interactable = false;
					break;
                case GardenState.Select:
                    m_PlantMsg.text = "Seed Id: " + m_PanelData.gardenModel.gardenPlantModel.seedId + " " + PlantName;
                    m_PlantBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Plant";
                    m_PlantBtn.interactable = true;
                    break;
                case GardenState.WaitingHarvest:
					m_Timer.text = "WaitingHarvest";
					m_PlantMsg.text = "Seed Id: " + m_PanelData.gardenModel.gardenPlantModel.seedId + " " + PlantName;
					m_PlantBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Harvest";
                    m_PlantBtn.interactable = true;

                    break;
            }
        }
    }
}
