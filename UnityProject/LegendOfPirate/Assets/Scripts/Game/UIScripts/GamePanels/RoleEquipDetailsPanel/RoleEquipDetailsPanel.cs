using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;
using TMPro;

namespace GameWish.Game
{
	public partial class RoleEquipDetailsPanel : AbstractAnimPanel
	{
		[SerializeField] private GameObject m_MaterialItem;
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
			m_PanelData.roleEquipModel = ModelMgr.S.GetModel<RoleGroupModel>().GetRoleModel(m_PanelData.roleID).GetEquipModelByType(m_PanelData.equipmentType);
			m_PanelData.equipConfig = m_PanelData.roleEquipModel.equipConfig;
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
			m_StrengthMaterials.SetDataCount(m_PanelData.equipConfig.equipStrengthenCosts.Length);
			m_StrengthMaterials.SetCellRenderer(OnStrengthMaterialChange);
		}

        private void OnStrengthMaterialChange(Transform root, int index)
        {
            var InvModel = ModelMgr.S.GetModel<InventoryModel>();
			var item = m_PanelData.equipConfig.equipStrengthenCosts[index];
			var materialCount = InvModel.GetItemCountByID(item.materialID);
			root.GetComponentInChildren<TextMeshProUGUI>().text = string.Format(materialCount >= item.materialCostNumber ? "<color=green>{0}</color>/{1}" : "<color=red>{0}</color>/{1}", materialCount, item.materialCostNumber);
			//TODO ÉèÖÃ²ÄÁÏÍ¼Æ¬

		}

		private void InitUIEventListener() 
		{
			m_StrengthBtn.OnClickAsObservable().Subscribe(_ => OnStrengthBtnClick()).AddTo(this);
			m_CloseBtn.OnClickAsObservable().Subscribe(_ => HideSelfWithAnim()).AddTo(this);
		}

        private void OnStrengthBtnClick()
        {
			bool strengthable = false;
			//TODO ²ÄÁÏÊÇ·ñ×ã¹»
			if (strengthable)
			{
				m_PanelData.roleEquipModel.OnLevelUp();
			}
			else 
			{
				FloatMessageTMP.S.ShowMsg("You Don't Have Enough Materials To Strength Now.");
			}
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
