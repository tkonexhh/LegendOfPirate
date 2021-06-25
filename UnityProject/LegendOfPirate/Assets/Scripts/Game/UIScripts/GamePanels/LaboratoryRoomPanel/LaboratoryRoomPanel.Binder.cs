using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;

namespace GameWish.Game
{
	public class LaboratoryRoomPanelData : UIPanelData
	{
		public ShipModel shipModel;
		public LaboratoryModel laboratoryModel;
		public LaboratoryRoomPanelData()
		{

		}
		public int GetLaboratorySlotCount() 
		{
			return Math.Max(laboratoryModel.tableConfig.unlockSpaceCount, Define.LABORATORY_DEFAULT_SLOT_COUNT);
		}
		public int GetPotionSlotCount() 
		{
			return laboratoryModel.potionSlotModelList.Count;
		}
	}
	
	public partial class LaboratoryRoomPanel
	{
		private LaboratoryRoomPanelData m_PanelData = null;
		
		private void AllocatePanelData(params object[] args)
		{
			 m_PanelData = UIPanelData.Allocate<LaboratoryRoomPanelData>();
			try
			{
				m_PanelData.shipModel = ModelMgr.S.GetModel<ShipModel>();

				m_PanelData.laboratoryModel = m_PanelData.shipModel.GetShipUnitModel(ShipUnitType.Laboratory) as LaboratoryModel;
			}
			catch (Exception e)
			{
				Debug.LogError("e = " + e);
			}
		}
		
		private void ReleasePanelData()
		{
			ObjectPool<LaboratoryRoomPanelData>.S.Recycle(m_PanelData);
		}
		
		private void BindModelToUI()
		{
            m_PanelData.laboratoryModel
             .level
             .SubscribeToTextMeshPro(m_BuildingLevel,"Lv.{0}").AddTo(this);
			m_PanelData.laboratoryModel.level.AsObservable().Subscribe(level => OnLaboratoryLevelChange()).AddTo(this);
			m_PanelData.laboratoryModel.labaratorySlotModelList.ObserveCountChanged().Subscribe(count => m_LaboratorySlotList.SetDataCount(count)).AddTo(this);
        }

        private void OnLaboratoryLevelChange()
        {
            
        }

        private void BindUIToModel()
		{
		}
		private void RegisterEvents()
		{

		}

		private void OnClickAddListener()
		{
			
			m_MakeBtn.OnClickAsObservable().Subscribe(_ =>
			{
				MakeBtnEvent();
			}).AddTo(this);
			m_LevelUpBtn.OnClickAsObservable().Subscribe(_ =>
			{
				LaboratoryUpgradeBtnEvent();
			}).AddTo(this);
			m_AddItemBtn.OnClickAsObservable().Subscribe(_ =>
            {
				AddItemBtnEvent();
            }).AddTo(this);
			m_CloseBtn.OnClickAsObservable().Subscribe(_ =>
			{
				HideSelfWithAnim();
			}).AddTo(this);
	
        }
		private void UnregisterEvents()
		{

		}
	}
}
