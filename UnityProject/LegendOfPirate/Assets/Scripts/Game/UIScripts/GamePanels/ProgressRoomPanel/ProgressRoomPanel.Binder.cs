using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using GFrame;
using TMPro;
namespace GameWish.Game
{
	public class ProgressRoomPanelData : UIPanelData
	{
		public ProcessingRoomModel processingRoomModel;

		public ProgressRoomPanelData()
		{
		}
	}
	
	public partial class ProgressRoomPanel
	{
		private ProgressRoomPanelData m_PanelData = null;
		
		private void AllocatePanelData(params object[] args)
		{
			 m_PanelData = UIPanelData.Allocate<ProgressRoomPanelData>();
			m_PanelData.processingRoomModel = ModelMgr.S.GetModel<ShipModel>().GetShipUnitModel(ShipUnitType.ProcessingRoom)as ProcessingRoomModel;
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<ProgressRoomPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
			m_PanelData.processingRoomModel.level.AsObservable().SubscribeToTextMeshPro(BuildingLevel, "Lv.{0}").AddTo(this);
			m_PanelData.processingRoomModel.level.AsObservable().Subscribe(level =>OnLevelChange(level)).AddTo(this);
		}
		
		private void BindUIToModel()
		{
			CloseBtn.OnClickAsObservable().Subscribe(_ => HideSelfWithAnim()).AddTo(this);
			LevelUpBtn.OnClickAsObservable().Subscribe(_ => OnLevelUpBtnClick()).AddTo(this);
			ProgressBtn.OnClickAsObservable().Subscribe(_ => OnProgressBtnClick()).AddTo(this);
			AddItemBtn.OnClickAsObservable().Subscribe(_ => OnAddItemBtnClick()).AddTo(this);
		}
		private void OnLevelUpBtnClick() { }
		private void OnProgressBtnClick() { }
		private void OnAddItemBtnClick() { }
		private void OnLevelChange(int level) 
		{
            var PlantSlots = Content.GetComponentsInChildren<Toggle>();
            for (int i = 0; i < PlantSlots.Length; i++)
            {
                if (level >= TDFacilityProcessingRoomTable.dataList[i].level)
                {
                    PlantSlots[i].GetComponentInChildren<GImage>().gameObject.SetActive(false);
                    PlantSlots[i].GetComponentInChildren<TextMeshProUGUI>().text = TDFacilityProcessingRoomTable.dataList[i].unlockPartID.ToString();
                }
                else
                {
                    PlantSlots[i].GetComponentInChildren<TextMeshProUGUI>().text = string.Format("ProgressingRoomLevel {0}", TDFacilityProcessingRoomTable.dataList[i].level);
                }
            }
        }
	}
}
