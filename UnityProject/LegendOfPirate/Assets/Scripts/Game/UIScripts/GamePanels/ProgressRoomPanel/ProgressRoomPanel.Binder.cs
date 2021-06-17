using System.Collections.Generic;
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
		public List<GImage> lockerList;
		public ProgressRoomPanelData()
        {
			lockerList = new List<GImage>();

        }
	}
	
	public partial class ProgressRoomPanel
	{
		private ProgressRoomPanelData m_PanelData = null;
		
		private void AllocatePanelData(params object[] args)
		{
			 m_PanelData = UIPanelData.Allocate<ProgressRoomPanelData>();
			m_PanelData.processingRoomModel = ModelMgr.S.GetModel<ShipModel>().GetShipUnitModel(ShipUnitType.ProcessingRoom)as ProcessingRoomModel;
            var plantSlots = Content.GetComponentsInChildren<Toggle>();
            foreach (var item in plantSlots)
            {
                var Locker = item.GetComponentInChildren<GImage>();
                m_PanelData.lockerList.Add(Locker);
            }
        }
		
		private void ReleasePanelData()
		{
			ObjectPool<ProgressRoomPanelData>.S.Recycle(m_PanelData);
			m_PanelData.lockerList.Clear();
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
		private void OnLevelUpBtnClick()
		{
            UIMgr.S.OpenTopPanel(UIID.BuildingLevelUpPanel, null, ShipUnitType.ProcessingRoom);
        }
		private void OnProgressBtnClick() { }
		private void OnAddItemBtnClick() { }
		private void OnLevelChange(int level) 
		{
            var PlantSlots = Content.GetComponentsInChildren<Toggle>();
            for (int i = 0; i < PlantSlots.Length; i++)
            {
                if (level >= TDFacilityProcessingRoomTable.dataList[i].level)
                {
					PlantSlots[i].interactable = true;
					m_PanelData.lockerList[i].gameObject.SetActive(false);
                    PlantSlots[i].GetComponentInChildren<TextMeshProUGUI>().text = TDFacilityProcessingRoomTable.dataList[i].unlockPartID.ToString();
                }
                else
                {
					PlantSlots[i].interactable = false;
                    PlantSlots[i].GetComponentInChildren<TextMeshProUGUI>().text = string.Format("ProgressingRoomLevel {0}", TDFacilityProcessingRoomTable.dataList[i].level);
                }
            }
        }
	}
}
