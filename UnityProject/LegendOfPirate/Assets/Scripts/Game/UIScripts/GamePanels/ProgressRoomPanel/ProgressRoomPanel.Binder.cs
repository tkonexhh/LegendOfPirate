using System.Collections.Generic;
using System;
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

		public int GetProcessingSlotCount() 
		{
			return Math.Max(processingRoomModel.tableConfig.unlockPartSpace,Define.PROCESSING_ROOM_DEFAULT_SLOT_COUNT);
		}

		public int GetPartSlotCount() 
		{
			return processingRoomModel.ProcessingPartModelList.Count;
		}

		public int GetDefaultElementCount() 
		{
			return processingRoomModel.ProcessingPartModelList[0].partConfig.GetMakeResList().Count;
		}
		public List<ResPair> GetDefaultElement() 
		{
			return processingRoomModel.ProcessingPartModelList[0].partConfig.GetMakeResList();
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
			m_PanelData.lockerList.Clear();
		}
		
		private void BindModelToUI()
		{
			m_PanelData.processingRoomModel.level.AsObservable().SubscribeToTextMeshPro(m_BuildingLevel, "Lv.{0}").AddTo(this);
			m_PanelData.processingRoomModel.level.AsObservable().Subscribe(level =>OnLevelChange(level)).AddTo(this);
			m_PanelData.processingRoomModel.processingSlotModelList.ObserveCountChanged().Subscribe(count => m_ProcessingSlotList.SetDataCount(count)).AddTo(this);
		}
		
		private void BindUIToModel()
		{

		}

		private void InitPanelBtn()
		{
			m_AddItemBtn.OnClickAsObservable().Subscribe(_ => OnAddItemBtnClick()).AddTo(this);
			m_LevelUpBtn.OnClickAsObservable().Subscribe(_ => OnLevelUpBtnClick()).AddTo(this);
			m_CloseBtn.OnClickAsObservable().Subscribe(_ => HideSelfWithAnim()).AddTo(this);
			m_ProgressBtn.OnClickAsObservable().Subscribe(_ => OnProgressBtnClick()).AddTo(this);
		}

		private void OnLevelUpBtnClick()
		{
            UIMgr.S.OpenTopPanel(UIID.BuildingLevelUpPanel, null, ShipUnitType.ProcessingRoom);
        }
		private void OnProgressBtnClick() 
		{
			foreach (var item in m_PanelData.processingRoomModel.processingSlotModelList) 
			{
                switch (item.processState.Value)
                {
                    case ProcessSlotState.Free:
                        break;
                    case ProcessSlotState.Processing:
                        break;
                    case ProcessSlotState.Locked:
                        break;
                    case ProcessSlotState.Selected:
						item.StartProcessing(DateTime.Now);
                        break;
                }
            }
        }
        private void OnAddItemBtnClick()
		{
			
		}
		private void OnLevelChange(int level)
        {
         
              
        }
    }
}
