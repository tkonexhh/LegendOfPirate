using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;

namespace GameWish.Game
{
	public partial class RoleEquipDetailsPanel : AbstractAnimPanel
	{
		protected override void OnUIInit()
		{
			base.OnUIInit();
		}  
		
		protected override void OnPanelOpen(params object[] args)
		{
			base.OnPanelOpen(args);
			
			AllocatePanelData();

			InitializePassValue(args);

			GetInformationForNeed();

			InitUIMsg();

			InitUIEventListener();
			
			BindModelToUI();
			BindUIToModel();
		}

        private void InitializePassValue(object[] args)
        {
            m_PanelData.roleID = (int)args[0];
			m_PanelData.equipmentType = (EquipmentType)args[1];
        }

        private void GetInformationForNeed()
		{
			var roleModel = ModelMgr.S.GetModel<RoleGroupModel>().GetRoleModel(m_PanelData.roleID);
			m_PanelData.roleEquipID= roleModel.equipLimitDic[m_PanelData.equipmentType];
			m_PanelData.IsLocked = roleModel.equipDic.ContainsKey(m_PanelData.equipmentType);
			m_PanelData.roleEquipModel = roleModel.GetEquipModelByType(m_PanelData.equipmentType);
        }

		private void InitUIMsg() 
		{
		    m_EquipName.text=	m_PanelData.roleEquipModel.equipConfig.equipName;
			m_EquipType.text = m_PanelData.roleEquipModel.equipConfig.equipmentType.ToString();
			string equipAttribute = string.Empty;
			foreach (var attributePair in m_PanelData.roleEquipModel.equipAttributeDic) 
			{
				equipAttribute += (attributePair.Key.ToString() + attributePair.Value.ToString("f2")+"\n");
			}
			m_EquipAttribute.text = equipAttribute;


		}

		private void InitUIEventListener() 
		{
			m_StrengthBtn.OnClickAsObservable().Subscribe(_ => OnStrengthBtnClick()).AddTo(this);
			m_CloseBtn.OnClickAsObservable().Subscribe(_ => HideSelfWithAnim()).AddTo(this);
		}

        private void OnStrengthBtnClick()
        {
			m_PanelData.roleEquipModel.OnLevelUp();
		}

        protected override void OnPanelHideComplete()
		{
			base.OnPanelHideComplete();
			
			CloseSelfPanel();
		}
		
		protected override void OnClose()
		{
			base.OnClose();
			
			ReleasePanelData();
		}
		
	}
}
