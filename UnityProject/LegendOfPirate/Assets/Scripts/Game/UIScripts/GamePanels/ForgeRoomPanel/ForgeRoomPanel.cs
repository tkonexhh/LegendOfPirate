using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;
using System;
using System.Collections.Generic;

namespace GameWish.Game
{
    public partial class ForgeRoomPanel : AbstractAnimPanel
	{
		#region AbstractAnimPanel
		[SerializeField] private USimpleListView m_WeaponSlotLstView;
		[SerializeField] private GameObject m_AttrItem;
        #endregion

        #region Data
        private ReactiveCollection<ForgeEquipModel> m_ForgeEquipModels;
		private List<AttrItem> m_AttrItems = new List<AttrItem>();

		private const int ATTRITEM_NUMBER = 3;
		#endregion
		#region AbstractAnimPanel
		protected override void OnUIInit()
		{
			base.OnUIInit();
            AllocatePanelData();

            BindModelToUI();
            BindUIToModel();

			OnClickAsObservable();
		}

        protected override void OnPanelOpen(params object[] args)
		{
			base.OnPanelOpen(args);
			OpenDependPanel(EngineUI.MaskPanel, -1);

			InitData();
		}
		
		protected override void OnPanelHideComplete()
		{
			base.OnPanelHideComplete();
			CloseDependPanel(EngineUI.MaskPanel);
			CloseSelfPanel();
		}
		
		protected override void OnClose()
		{
			base.OnClose();
			
			ReleasePanelData();
		}
		#endregion

		#region OnClickAsObservable
		private void OnClickAsObservable()
		{
			m_BgBtn.OnClickAsObservable().Subscribe(_ => HideSelfWithAnim()).AddTo(this);
			m_ExitBtn.OnClickAsObservable().Subscribe(_ => HideSelfWithAnim()).AddTo(this);
			m_ForgetBtn.OnClickAsObservable().Subscribe(_ => ForgetBtn()).AddTo(this);
			m_RightArrowBtn.OnClickAsObservable().Subscribe(_ => m_WeaponSlotLstView.Move2Next()).AddTo(this);
			m_LeftArrowBtn.OnClickAsObservable().Subscribe(_ => m_WeaponSlotLstView.Move2Pre()).AddTo(this);
		}

        private void ForgetBtn()
        {
			m_PanelData.forgeRoomModel.ForgeEquip();
		}
        #endregion

        private void InitData() 
		{
			for (int i = 0; i < ATTRITEM_NUMBER; i++)
			{
				AttrItem attrItem = Instantiate(m_AttrItem, m_AttrRegion).GetComponent<AttrItem>();
				attrItem.OnInit();
				m_AttrItems.Add(attrItem);
			}

			m_WeaponSlotLstView.SetCellRenderer(OnWeaponSlotCellRenderer);

			m_WeaponSlotLstView.SetDataCount(m_ForgeEquipModels.Count);
		}

		private void OnWeaponSlotCellRenderer(Transform root, int index) 
		{
			if (m_ForgeEquipModels != null)
			{
				ForgeEquipModel forgeEquipModel = m_ForgeEquipModels[index];

				ForgeEquipItem forgeEquipItem = root.GetComponent<ForgeEquipItem>();

				forgeEquipItem.OnRefresh(forgeEquipModel);
			}
			else
				Log.e("Error : m_ForgeEquipModels is null !");
		}
	}
}
