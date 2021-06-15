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
             .Select(level => CommonMethod.GetStringForTableKey(LanguageKeyDefine.Fixed_Title_Lv) + level.ToString())
             .SubscribeToTextMeshPro(LaboratoryTMP).AddTo(this);
        }
		
		private void BindUIToModel()
		{
		}
		private void RegisterEvents()
		{

		}

		private void OnClickAddListener()
		{
			RightArrowBtn.OnClickAsObservable().Subscribe(_ =>
			{
				RightArrowBtnEvent();
			});
			LeftArrowBtn.OnClickAsObservable().Subscribe(_ =>
			{
				LeftArrowBtnEvent();
			});
			MakeBtn.OnClickAsObservable().Subscribe(_ =>
			{
				MakeBtnEvent();
			});
			LaboratoryUpgradeBtn.OnClickAsObservable().Subscribe(_ =>
			{
				LaboratoryUpgradeBtnEvent();
			});
            BgBtn.OnClickAsObservable().Subscribe(_ =>
            {
				BgBtnEvent();
            });
        }
		private void UnregisterEvents()
		{

		}
	}
}
