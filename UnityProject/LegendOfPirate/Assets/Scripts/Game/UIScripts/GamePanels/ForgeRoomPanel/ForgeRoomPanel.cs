using UnityEngine;
using UnityEngine.UI;
using Qarth.Extension;
using Qarth;
using UniRx;

namespace GameWish.Game
{
    public enum ForgeStage
    {
        /// <summary>
        /// ������
        /// </summary>
        Free = 0,
        /// <summary>
        /// ʹ����
        /// </summary>
        Forging = 1,
        ///<summary>
        /// ѡ��
        ///</summary>
        Select = 2,
        ///<summary>
        /// �������
        ///</summary>
        ForgeComplate = 3
    }
    public partial class ForgeRoomPanel : AbstractAnimPanel
	{
		[SerializeField] private USimpleListView m_WeaponSlotLstView;
		protected override void OnUIInit()
		{

			base.OnUIInit();
            AllocatePanelData();

            BindModelToUI();
            BindUIToModel();

            InitPanelBtn();
        }
		
		protected override void OnPanelOpen(params object[] args)
		{
			base.OnPanelOpen(args);
			OpenDependPanel(EngineUI.MaskPanel, -1);

			InitUI();
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

		private void InitUI() 
		{
            m_WeaponSlotLstView.SetCellRenderer(OnWeaponSlotCellRenderer);
			m_WeaponSlotLstView.SetDataCount(m_PanelData.GetSlotCount());
        }

		private void OnWeaponSlotCellRenderer(Transform root, int index) 
		{
            var slotItem = root.GetComponent<ForgeRoomWeaponSlot>();
            slotItem.SetInit(m_Content, index);
        }


	}
}
