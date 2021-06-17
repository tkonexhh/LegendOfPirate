using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using GFrame;
using TMPro;
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
		}
		
		private void BindUIToModel()
		{
			m_PlantBtn.OnClickAsObservable().Subscribe(_ => OnPlantBtnClick()).AddTo(this);
			m_CloseBtn.OnClickAsObservable().Subscribe(_ => HideSelfWithAnim()).AddTo(this);
			m_LevelUpBtn.OnClickAsObservable().Subscribe(_ => OnLevelUpBtnClick()).AddTo(this);
		}

		private void OnPlantBtnClick()
		{

		}

		private void OnLevelUpBtnClick() 
		{
            UIMgr.S.OpenTopPanel(UIID.BuildingLevelUpPanel, null, ShipUnitType.Garden);
        }

        private void OnGardenLevelChange(int level)
        {
            
        }

		private void OnGardenStateChange(GardenState state) 
		{
            switch (state)
            {
                case GardenState.Free:
                    break;
                case GardenState.Plant:
                    break;
                case GardenState.Select:

                    break;
                case GardenState.WaitingHarvest:
                    break;
            }
        }
    }
}
