using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;

namespace GameWish.Game
{
	public partial class RoleEquipGetPanel : AbstractAnimPanel
	{
		protected override void OnUIInit()
		{
			base.OnUIInit();
			
			AllocatePanelData();
			
			BindModelToUI();
			
			BindUIToModel();
			
		}
		
		protected override void OnPanelOpen(params object[] args)
		{
			base.OnPanelOpen(args);

			InitData(args);
			InitUI();
			InitEventListener();

		}
		
		protected override void OnPanelHideComplete()
		{
			base.OnPanelHideComplete();
			
			CloseSelfPanel();
		}
		private void InitData(object[] args) 
		{
			m_PanelData.equipmentType = (EquipmentType)args[0];
			m_PanelData.roleId = (int)args[1];
			m_PanelData.equipId = ModelMgr.S.GetModel<RoleGroupModel>().GetRoleModel(m_PanelData.roleId).equipLimitDic[m_PanelData.equipmentType];
			m_PanelData.inventoryItemModel = ModelMgr.S.GetModel<InventoryModel>().GetItemModel(InventoryItemType.Equip, m_PanelData.equipId);
		}

		private void InitUI() 
		{
			var equipConfig = TDEquipmentConfigTable.GetEquipmentConfigByID(m_PanelData.equipId);
			m_EquipType.text = equipConfig.equipName;
			string attributeString = string.Empty;
			foreach (var item in equipConfig.equipAttributeValues) 
			{
				attributeString += item.equipAttrType + ":" + item.percentage + "%\n";
			}
			m_EquipAttribute.text = attributeString;
			m_EquipNumber.text = string.Format("Amout:{0}",m_PanelData.inventoryItemModel==null?0:m_PanelData.inventoryItemModel.GetCount());
		}

		private void InitEventListener() 
		{
			m_EquipBtn.OnClickAsObservable().Subscribe(_=> OnEquipBtnClick()).AddTo(this);
			m_GetWay1Btn.OnClickAsObservable().Subscribe(_ => OnForgeBtnClick()).AddTo(this);
			m_GetWay2Btn.OnClickAsObservable().Subscribe(_ => OnUnderGroundMarketBtnClick()).AddTo(this);
			m_CloseBtn.OnClickAsObservable().Subscribe(_ => HideSelfWithAnim()).AddTo(this);
		}

        private void OnUnderGroundMarketBtnClick()
        {
            //TODO Ìø×ªºÚÊÐ

        }

        private void OnForgeBtnClick() 
		{
			UIMgr.S.OpenPanel(UIID.ForgeRoomPanel);
		}

        private void OnEquipBtnClick()
        {
			if (m_PanelData.inventoryItemModel!=null&& m_PanelData.inventoryItemModel.GetCount() >= 1)
			{
				var roleModel = ModelMgr.S.GetModel<RoleGroupModel>().GetRoleModelByIndex(m_PanelData.roleId);
				roleModel.AddEquip(m_PanelData.equipmentType);
				ModelMgr.S.GetModel<InventoryModel>().AddInventoryItemCount(InventoryItemType.Equip, m_PanelData.equipId, -1);
			}
			else
			{
				FloatMessageTMP.S.ShowMsg("You don't have this equipment now.");
			}
        }

        protected override void OnClose()
		{
			base.OnClose();
			
		}
		
		protected override void BeforDestroy()
		{
			base.BeforDestroy();
			
			ReleasePanelData();
		}
		
	}
}
